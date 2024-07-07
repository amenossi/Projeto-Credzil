using CredZil.Objetos;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

partial class Program
{
    public static bool validaNumeroConta(int value, List<Conta> lista)
    {

        foreach (Conta conta in lista)
        {
            if (value == conta.numeroConta)
            {
                Console.WriteLine("Já existe uma conta com este número");
                return false;
            }
        }
        return true;
    }

    public static void Transferencia(double transf, int destino, int origem, List<Conta> lista)
    {
        for (int i = lista.Count - 1; i >= 0; i--)
        {
            if (lista[i].numeroConta == destino)
            {
                lista[i].saldo += transf;
                for (int j = lista.Count - 1; j >= 0; j--)
                {
                    if (lista[j].numeroConta == origem)
                    {
                        lista[j].saldo -= transf;
                    }
                }
            }
        }
        Console.WriteLine("Transferencia realizada com sucesso");
    }
    public static void leitor(List<Conta> lista)
    {
        var caminhoArquivo = "historico.txt";
        using (StreamReader leitor = new StreamReader(caminhoArquivo))
        {
            string[] linhas;
            while (!leitor.EndOfStream)
            {
                Conta conta = new Conta();
                Cliente cliente = new Cliente();

                linhas = leitor.ReadLine().Split(',');
                cliente.Nome = linhas[0];
                cliente.CPF = linhas[1];
                cliente.Idade = int.Parse(linhas[2]);
                conta.numeroConta = int.Parse(linhas[3]);
                conta.saldo = double.Parse(linhas[4]);
                cliente.Password = linhas[5];
                conta.titular = cliente;
                lista.Add(conta);
            }
        }
    }
    public static void UserLeitor(List<Usuario> lista)
    {
        using(StreamReader leitor = new StreamReader("bdU.txt"))
        {
            string[] linhas;
            while (!leitor.EndOfStream)
            {
                Usuario usuario = new Usuario();

                linhas = leitor.ReadLine().Split(',');
                usuario.Nome = linhas[0];
                usuario.Email = linhas[1];
                usuario.Username = linhas[2];
                usuario.Password = linhas[3];
                lista.Add(usuario);
            }
        }
    }
    static void TransfFluxUser(Conta contaLogada, List<Conta> lista, int NumeroConta, double ValorTransf)
    {
        for(int i = lista.Count - 1; i >= 0; i--)
        {
            if (NumeroConta.Equals(lista[i].numeroConta) && ValorTransf > 0 && ValorTransf <= contaLogada.saldo)
            {
                lista[i].saldo = lista[i].saldo + ValorTransf;
                contaLogada.saldo = contaLogada.saldo - ValorTransf;
                for(int j = lista.Count -1; j >= 0; j--)
                {
                    if (lista[j].numeroConta.ToString().Equals(contaLogada.numeroConta.ToString()))
                    {
                        lista[j].saldo = contaLogada.saldo;
                    }
                }

            }

        }
        Console.WriteLine("Transferencia realizada com sucesso");
        AtualizaDoc(lista);
        Console.ReadKey();
    }
    static void AtualizaDoc(List<Conta> lista)
    {
        using (FileStream atualizador = new FileStream("historico.txt", FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(atualizador, Encoding.UTF8))
        {
            foreach (Conta conta in lista)
            {
                string linha = $"{conta.titular.Nome},{conta.titular.CPF},{conta.titular.Idade},{conta.numeroConta},{conta.saldo},{conta.titular.Password}";
                writer.WriteLine(linha);
            }
        }
    }
}