using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cliente

{
    private string _nome;
    private int _idade;
    private string _cpf;
    private string _password;

    public string Nome
    {
        get { return _nome; }
        set
        {
            if (!String.IsNullOrEmpty(value))
            {
                _nome = value;
            }
            else
            {
                throw new ArgumentException("E preciso informar o nome");
            }
        }
    }
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    public int Idade
    {
        get { return _idade; }

        set
        {
            if (value > 0)
            {
                _idade = value;
            }
            else
            {
                throw new ArgumentException("A idade não pode ser zero ou nula");
            }
        }
    }
    public string CPF
    {
        get { return _cpf; }

        set
        {
            _cpf = value;
            /*if (validaCPF(value))
            {
                _cpf = value;
            }
            else
            {
                throw new ArgumentException("CPF invalido, tente novamente.");
            }*/
        }
    }


    /*public bool validaCPF(string pessoaCPF)
    {
        if (string.IsNullOrEmpty(_cpf) || _cpf.Length != 11)
        {
            return false;
        }

        int soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (_cpf[i] - '0') * (10 - i);
        }

        int digito1 = 11 - (soma % 11);
        if (digito1 >= 10)
        {
            digito1 = 0;
        }

        if ((_cpf[9] - '0') != digito1)
        {
            return false;
        }

        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (_cpf[i] - '0') * (11 - i);
        }

        int digito2 = 11 - (soma % 11);
        if (digito2 >= 10)
        {
            digito2 = 0;
        }

        if ((_cpf[10] - '0') != digito2)
        {
            return false;
        }

        return true;
    }*/



}
