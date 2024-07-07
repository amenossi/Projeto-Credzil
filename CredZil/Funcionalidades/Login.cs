using CredZil.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredZil.Funcionalidades
{
    public class Login
    {
        List<Usuario> logado = new List<Usuario>();
        List<Conta> logadoUser = new List<Conta>();
        public bool UserLoginAdmin(List<Usuario> listaUser)
        {
            int l = 0;
            while (l < 3)
            {
                Console.Clear();
                Console.WriteLine("Faca o Login");
                Console.WriteLine("Informe o seu username");
                string username = Console.ReadLine();
                Console.WriteLine("Informe a sua senha");
                string senha = Console.ReadLine();

                foreach (Usuario usuario in listaUser)
                {
                    if (senha == usuario.Password && username == usuario.Username)
                    {
                        logado.Add(usuario);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Senha incorreta, tente novamente");
                        Console.ReadKey();
                        l++;
                    }
                }
                
            }
            return false;
        }

        public bool UserLogin(List<Conta> listaUser, out Conta contaLogada)
        {
            Console.Clear();
            Console.WriteLine("Informe o numero da sua conta");
            string numeroConta = Console.ReadLine();
            Console.WriteLine("Informe a sua senha");
            string senha = Console.ReadLine();

            foreach (Conta conta in listaUser)
            {
                if (senha.Equals(conta.titular.Password) && numeroConta.Equals(conta.numeroConta.ToString()))
                {
                    contaLogada = conta;
                    return true;
                }
            }
            contaLogada = null;
            return false;
        }
    }  
}

