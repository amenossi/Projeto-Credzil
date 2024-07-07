using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredZil.Objetos
{
    public class Usuario
    {
        private string _nome;
        private string _email;
        private string _username;
        private string _password;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}
