using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Pizza.Models;
using Pizza.Models.View;
using Pizza.Services;

namespace Pizza.Controllers
{
    
    public class BrugerController : Controller
    {
        readonly IDataBasen _dataBasen;
        readonly ILogin _login;

        public BrugerController(IDataBasen dataBasen, ILogin login)
        {
            _dataBasen = dataBasen;
            _login = login;
        }

        //// GET: Bruger
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Bruger/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        public ActionResult Login()
        {
            BrugerViewModel brugerView = new BrugerViewModel { visLink = false };
            return View(brugerView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(BrugerViewModel brugerView)
        {

            try
            {
                BrugerModel bruger = _dataBasen.HentBruger(brugerView.Brugernavn);
                _login.SetLogin(bruger);
                Request.HttpContext.Session.SetString("login", bruger.Brugernavn);
                return RedirectToAction("Create", "Bestilling");
            }
            //catch (DataBasenException e)
            //{
     
            //    brugerView.Meddelelse = "Brugernavnet er ikke oprettet";
            //    brugerView.visLink = true;
            //    return View(brugerView);
            //}
            catch (DataBasenException dbe)
            {
                switch (dbe.Message)
                {
                    case "Brugernavn ukendt":
                        brugerView.Meddelelse = "Brugernavnet findes ikke.";
                        brugerView.visLink = true;
                        return View(brugerView);
                    default:
                        brugerView.Meddelelse = dbe.Message;
                        break;
                }
                return View(brugerView);
            }
            catch (Exception e)
            {
                brugerView.Meddelelse = e.Message;
                return View(brugerView);
            }
        }


        // GET: Bruger/Create
        public ActionResult Create()
        {
            BrugerViewModel brugerView = new BrugerViewModel();
            return View(brugerView);
        }

        // POST: Bruger/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrugerViewModel brugerView)
        {
            BrugerModel bruger = new BrugerModel
            {
                Brugernavn = brugerView.Brugernavn,
                Navn = brugerView.Navn,
                Gade = brugerView.Gade,
                Postnummer = brugerView.Postnummer,
                Bynavn = brugerView.Bynavn
            };
       
            try
            {
                _dataBasen.OpretBruger(bruger);
                _login.SetLogin(bruger);
                Request.HttpContext.Session.SetString("login", bruger.Brugernavn);
                return RedirectToAction("Create", "Bestilling");
            }
            catch (ArgumentException)
            {
                brugerView.Meddelelse = "Brugernavn findes allerede";
                return View(brugerView);
            }
            catch (Exception e)
            {
                brugerView.Meddelelse = e.Message;
                return View(brugerView);
            }
        }

        //// GET: Bruger/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Bruger/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Bruger/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Bruger/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}