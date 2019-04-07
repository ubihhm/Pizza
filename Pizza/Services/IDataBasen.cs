using System.Threading.Tasks;
using Pizza.Models;

namespace Pizza.Services
{
    public interface IDataBasen
    {
        BrugerModel HentBruger(string brugerId);
        void OpretBestilling(BestillingModel bestilling);
        void OpretBruger(BrugerModel bruger);
    }
}