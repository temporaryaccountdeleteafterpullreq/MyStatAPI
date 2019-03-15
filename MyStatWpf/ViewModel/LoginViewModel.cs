using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace MyStatWpf
{
    class LoginViewModel:PropertyChangedBase
    {
        private Api _myStat;
        public string Username { get; set; }
        public string Password { get; set; }
        public async void Login()
        {
            _myStat = new Api(Username, Password);
            _myStat.TryLogin();
            MessageBox.Show($"Hello, {_myStat.CurrentUser?.FullName}");
        }
    }
}
