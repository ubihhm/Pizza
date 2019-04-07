using Pizza.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Services
{
    public class DataBasen : IDataBasen
    {

        private static Dictionary<string, BrugerModel> _brugere;
        private static Dictionary<Guid, BestillingModel> _bestillinger;

        static DataBasen()
        {
            _brugere = new Dictionary<string, BrugerModel>();
            _bestillinger = new Dictionary<Guid, BestillingModel>();

            // Opret en bruger til testformål
            _brugere.Add("q", new BrugerModel { Brugernavn = "q", Navn = "Testbruger", Gade = "Testgade", Postnummer = 8888, Bynavn = "Testby" });
        }

        public BrugerModel HentBruger(string brugerId)
        {
            BrugerModel bruger;
            if (_brugere.TryGetValue(brugerId, out bruger))
                return bruger;
            else
                throw new DataBasenException("Brugernavn ukendt");

        }
        public void OpretBruger(BrugerModel bruger)
        {
            _brugere.Add(bruger.Brugernavn, bruger);
            return;
        }

        public void OpretBestilling(BestillingModel bestilling)
        {
            if (bestilling.Brugernavn == null | bestilling.Brugernavn == "")
                throw new DataBasenException("Brugernavn mangler");

            if (_brugere.ContainsKey(bestilling.Brugernavn)) {
                _bestillinger.Add(Guid.NewGuid(), bestilling);
                return;
            }
            else
            {
                throw new DataBasenException("Brugernavn ukendt");
            }
        }
    }
}
