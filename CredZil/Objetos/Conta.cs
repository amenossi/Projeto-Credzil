using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Conta
{
    private int _numeroConta;
    private double _saldo;
    private Cliente _titular;
    

    public Cliente titular
    {
        get { return _titular; }
        set { _titular = value; }
    }

    public int numeroConta
    {
        get { return _numeroConta; }
        set
        {
            if(_numeroConta < 0)
            {
                throw new ArgumentException("O numero da conta não pode ser zero ou menor que zero");
            }
            else
            {
                _numeroConta = value;
            }
        }
    }
    public double saldo
    {
        get
        {
            return _saldo;
        }

        set
        {
            if (!(_saldo<0.0))
            {
                _saldo = value;
            }
        }
    }

    public void Transferir(double valor, Conta destino)
    {
        if (valor <= 0)
        {
            throw new ArgumentException("O valor da transferência deve ser maior que zero");
        }
        if (valor > _saldo)
        {
            throw new ArgumentException("O valor da transferência não pode ser maior que o saldo da conta");
        }
        else
        {
            _saldo -= valor;
            destino.saldo += valor;
        }

    }

    public void Depositar(double valor, Conta destino)
    {
            if (valor <= 0.0)
            {
                throw new ArgumentException("O valor de sua transferencia não pode ser menor ou igual zero");
            }
            else { _saldo += valor; }
        
    }
}