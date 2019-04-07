using Pizza.Models;
using System.Text;

namespace Pizza.Services
{
    public interface ILogin
    {

        BrugerModel GetLogin();
        void SetLogin(BrugerModel bruger);
    }
}