using Pizza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Services
{
    public class Login : ILogin
    {
        private static BrugerModel _login;

        static Login()
        {
            _login = null;
        }

        public BrugerModel GetLogin()
        {
            return _login;
        }

        public void SetLogin(BrugerModel bruger)
        {
            _login = bruger;
        }
    }
}
