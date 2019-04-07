using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Models;
using Pizza.Models.View;
using Pizza.Services;

namespace Pizza.Controllers
{
    public class BestillingController : Controller
    {
        readonly IDataBasen _dataBasen;
        readonly ILogin _login;
        readonly IEmailService _emailService;

        public BestillingController(IDataBasen dataBasen, ILogin login, IEmailService emailService)
        {
            _dataBasen = dataBasen;
            _login = login;
            _emailService = emailService;
        }

        public ActionResult Bestilt()
        {
            return View();
        }
        
        // GET: Bestilling/Create
        public ActionResult Create()
        {
            BrugerModel login = _login.GetLogin();
            if (login == null)
                return RedirectToAction("Login", "Bruger");
            else
            {
                string brugernavn = Request.HttpContext.Session.GetString("login");
                BestillingViewModel bestillingView = new BestillingViewModel { Brugernavn = login.Brugernavn, Meddelelse = "Velkommen " + login.Navn };
                return View(bestillingView);
            }
        }

        // POST: Bestilling/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BestillingViewModel bestillingView)
        {
            bestillingView.Meddelelse = "";

            BrugerModel login = _login.GetLogin();

            BestillingModel bestilling = new BestillingModel
            {
                Brugernavn = bestillingView.Brugernavn,
                AntalMargherita = bestillingView.AntalMargherita,
                AntalCapricciosa = bestillingView.AntalCapricciosa,
                AntalQuattroStagioni = bestillingView.AntalQuattroStagioni
            };

            try
            {
                _dataBasen.OpretBestilling(bestilling);
            }
            catch (DataBasenException dbe)
            {
                switch (dbe.Message)
                {
                    case "Brugernavn ukendt":
                        bestillingView.Meddelelse = "Brugernavnet findes ikke. Opret brugeren før du bestiller.";
                        break;
                    case "Brugernavn mangler":
                        bestillingView.Meddelelse = "Husk at udfylde Brugernavn.";
                        break;
                    default:
                        bestillingView.Meddelelse = dbe.Message;
                        break;
                }
                return View(bestillingView);
            }
            catch (Exception e)
            {
                bestillingView.Meddelelse = e.Message;
                return View(bestillingView);
            }

            StringBuilder tekst = new StringBuilder();
            tekst.AppendLine("Ny bestilling fra kl " + DateTime.Now.ToShortTimeString());
            tekst.AppendFormat("Antal Margherita: {0}", bestilling.AntalMargherita).AppendLine();
            tekst.AppendFormat("Antal Capricciosa: {0}", bestilling.AntalCapricciosa).AppendLine();
            tekst.AppendFormat("Antal Quattro Stagioni: {0}", bestilling.AntalQuattroStagioni).AppendLine();
            tekst.AppendLine();
            tekst.AppendLine("Bestillingen skal sendes til");
            tekst.AppendLine(login.Navn);
            tekst.AppendLine(login.Gade);
            tekst.AppendLine(login.Postnummer.ToString() + " " + login.Bynavn );

            try
            {
                _emailService.SendEmail("kokken@pizza.dk", "Ny bestilling", tekst.ToString()).Wait();
      
            }
            catch (Exception)
            {
                bestillingView.Meddelelse = "kokken har ikke modtaget din bestiling.";
            }

            return RedirectToAction(nameof(Bestilt));

        }

    }
}