using CredZil.Funcionalidades;
using CredZil.Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;





partial class Program
{
    static void Main(string[] args)
    {
        List<Conta> ListaDeContas = new List<Conta>();
        List<Conta> LogadoUser = new List<Conta>();
        List<Usuario> ListaUsuario = new List<Usuario>();
        List<Usuario> LogadoAdmin = new List<Usuario>();
        Login login = new Login();
        leitor(ListaDeContas);
        UserLeitor(ListaUsuario);
        //login.UserAdminLogado(LogadoAdmin);

        Console.WriteLine("Olá, se voce quer acessar como Administrador digite 1, caso nao, digite 2");
        string opc = Console.ReadLine();

        switch (opc)
        {
            case "1":
                if (login.UserLoginAdmin(ListaUsuario) == true)
                {
                    MenuAdmin();
                }
                break;
            case "2":
                if(login.UserLogin(ListaDeContas, out Conta contaLogada) == true)
                {
                    MenuUser(contaLogada);
                }
                break;

        }
        
        
        
    }
    private static void MenuUser(Conta contaLogada)
    {
        List<Conta> ListaDeContas = new List<Conta>();
        Login login = new Login();
        leitor(ListaDeContas);

        string i;
        do
        {
            Console.Clear();
            Console.WriteLine($"Nome: {contaLogada.titular.Nome}   Conta: {contaLogada.numeroConta}   Saldo: {contaLogada.saldo}");
            Console.WriteLine("");
            Console.WriteLine("1 - Para fazer transferencia");
            Console.WriteLine("2 - Para alterar os dados");
            Console.WriteLine("3 - teste");
            Console.WriteLine("0 - Para sair");
            i = Console.ReadLine();

            switch (i)
            {
                case "1":
                    Console.WriteLine("Fazer transferência");
                    transfFluxoUser(contaLogada, ListaDeContas);
                    break;
                case "2":
                    Console.WriteLine("Alterar dados");
                    alteraDadosUser(contaLogada, ListaDeContas);
                    break;
                case "3":
                    listarContas(ListaDeContas);
                    break;
                case "0":
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (i != "0");
    }
    private static void alteraDadosUser(Conta contaLogada, List<Conta> listaDeContas)
    {
        string i = "0";
        do
        {
            Console.Clear();
            Console.WriteLine("Qual informação sobre a sua conta, você deseja alterar ?");
            Console.WriteLine("1- Alterar nome");
            Console.WriteLine("2- Alterar idade");
            Console.WriteLine("3- Numero da conta");
            Console.WriteLine("0- Sair e voltar ao menu");
            i = Console.ReadLine();

            switch (i)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine("Informe o nome correto");
                        string nome = Console.ReadLine();

                        for(int j = listaDeContas.Count - 1; j >= 0; j--)
                        {
                            if (contaLogada.numeroConta.Equals(listaDeContas[j].numeroConta))
                            {
                                contaLogada.titular.Nome = nome;
                                listaDeContas[j].titular.Nome = contaLogada.titular.Nome;
                                AtualizaDoc(listaDeContas);
                            }
                        }
                    }
                break;

            }
        }while (i != "0");
    }
    private static void transfFluxoUser(Conta contalogada, List<Conta> listaDeContas)
    {
        Console.Clear();
        Console.WriteLine($"Nome: {contalogada.titular.Nome}   Conta: {contalogada.numeroConta}   Saldo: {contalogada.saldo}");
        Console.WriteLine("Informe a conta que deseja realizar a transferencia");
        int cta = int.Parse(Console.ReadLine());
        Console.WriteLine("Qual o valor que deseja transferir ?");
        double valor = double.Parse(Console.ReadLine());

        if(valor > 0 && valor <= contalogada.saldo)
        {
            TransfFluxUser(contalogada, listaDeContas, cta, valor);
        }
        else
        {
            Console.WriteLine("Numero de conta invalido ou valor invalido");
            Console.ReadKey();
        }
    }
    private static void MenuAdmin()
    {
        List<Usuario> ListaUsuario = new List<Usuario>();
        List<Conta> ListaDeContas = new List<Conta>();
        leitor(ListaDeContas);
        UserLeitor(ListaUsuario);

        string i;


        do
        {
            Console.Clear();
            Console.WriteLine("1-Cadastrar Conta");
            Console.WriteLine("2-Alterar contas");
            Console.WriteLine("3-excluir conta");
            Console.WriteLine("4-listar conta");
            Console.WriteLine("5-Fazer transferencia");
            Console.WriteLine("6-Consultar uma conta");
            Console.WriteLine("7-cadastrar usuario");
            Console.WriteLine("8-Listar usuario");
            Console.WriteLine("0-Sair do sistema");

            i = Console.ReadLine();

            switch (i)
            {
                case "1":
                    cadastrarConta(ListaDeContas);
                    break;
                case "2":
                    alterarConta(ListaDeContas);
                    break;
                case "3":
                    excluirConta(ListaDeContas);
                    break;
                case "4":
                    listarContas(ListaDeContas);
                    break;
                case "5":
                    transferir(ListaDeContas);
                    break;
                case "6":
                    contaUnica(ListaDeContas);
                    break;
                case "7":
                    cadastarUsuario(ListaUsuario);
                    break;
                case "8":
                    ListarUsuarios(ListaUsuario);
                    break;
                case "0":
                    Console.WriteLine("Saindo do programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        } while (i != "0");
    }
    private static void ListarUsuarios(List<Usuario> listaUsuario)
    {
        Console.Clear();
        foreach(Usuario usuario in listaUsuario)
        {
            Console.WriteLine($"Nome: {usuario.Nome} e-mail: {usuario.Email}");
        }
        Console.ReadKey();
    }
    private static void cadastarUsuario(List<Usuario> listaUsuario)
    {
        Console.Clear();
        Usuario usuario = new Usuario();

        Console.WriteLine("Informe o nome do usuário");
        usuario.Nome = Console.ReadLine();
        Console.WriteLine("informe o e-mail do usuário");
        usuario.Email = Console.ReadLine();
        Console.WriteLine("Informe o username do usuário");
        usuario.Username = Console.ReadLine();
        Console.WriteLine("Informe a senha do usuário");
        usuario.Password = Console.ReadLine();
        listaUsuario.Add(usuario);

        using(StreamWriter userStream = new StreamWriter("bdU.txt", true))
        {
            userStream.WriteLine($"{usuario.Nome},{usuario.Email},{usuario.Password}");
        }
    }
    private static void transferir(List<Conta> listaDeContas)
    {
        Console.Clear();
        Console.WriteLine("Informe o numero da conta que irá receber a transferencia");
        int numConta = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe o numero da conta que esta realizando transferencia");
        int numConta2 = int.Parse(Console.ReadLine());

        Console.WriteLine("Informe o valor que será transferido");
        double valor = double.Parse(Console.ReadLine());

        Transferencia(valor, numConta, numConta2, listaDeContas);
        Console.ReadKey();

    }
    private static void cadastrarConta(List<Conta> listaDeContas)
    {

        Console.WriteLine("Informe os seguintes dados:");

        Conta conta = new Conta();
        Cliente cliente = new Cliente();

        Console.Write("Nome do titular: ");
        cliente.Nome = Console.ReadLine();

        Console.Write("Informe sua idade: ");
        cliente.Idade = int.Parse(Console.ReadLine());

        Console.Write("Informe seu CPF: ");
        cliente.CPF = Console.ReadLine();

        Console.WriteLine("Informe a senha do usuario");
        cliente.Password = Console.ReadLine();

        conta.titular = cliente;

        Console.Write("Informe o numero da conta: ");
        conta.numeroConta = int.Parse(Console.ReadLine());

        Console.Write("Informe o Saldo: ");
        conta.saldo = double.Parse(Console.ReadLine());

        listaDeContas.Add(conta);

        using (StreamWriter escritor = new StreamWriter("historico.txt", true))
        {
            escritor.WriteLine($"{conta.titular.Nome},{conta.titular.CPF},{conta.titular.Idade},{conta.numeroConta},{conta.saldo}");
            escritor.Flush();
        }

        Console.WriteLine("Conta cadastrada com sucesso!");
        Console.ReadKey();

        
    }
    private static void listarContas(List<Conta> listaDeContas)
    {
        Console.Clear();
        Console.WriteLine("Lista de Contas:");
        foreach (Conta conta in listaDeContas)
        {
            Console.WriteLine($"Titular: {conta.titular.Nome},{conta.titular.CPF},{conta.titular.Idade}, Número da Conta: {conta.numeroConta}, Saldo: {conta.saldo}");
        }
        Console.ReadKey();
    }
    private static void excluirConta(List<Conta> listaDeContas)
    {
        Console.Clear();
        Console.WriteLine("informe o numero da conta que deseja excluir os dados: ");
        var numeroConta = Console.ReadLine();

        for (int i = listaDeContas.Count - 1; i >= 0; i--)
        {
            if (int.Parse(numeroConta) == listaDeContas[i].numeroConta)
            {
                listaDeContas.RemoveAt(i);
            }
        }
        Console.ReadKey();
    }
    private static void alterarConta(List<Conta> listaDeContas)
    {
        Console.Clear();
        Conta conta = new Conta();
        Cliente cliente = new Cliente();
        Console.WriteLine("Informe qual conta você deseja alterar: ");
        int num = int.Parse(Console.ReadLine());
        Console.WriteLine("Qual dado deseja alterar ?");
        Console.WriteLine("1-Nome, 2-Conta");
        var dec = Console.ReadLine();

        switch (dec)
        {
            case "1":
                for (int i = listaDeContas.Count - 1; i >= 0; i--)
                {
                    if (listaDeContas[i].numeroConta.Equals(num))
                    {
                        Console.WriteLine("Informe o nome correto");
                        string nome = Console.ReadLine();
                        listaDeContas[i].titular.Nome = nome;
                    }
                }
                break;
            case "2":
                Console.WriteLine("Informe o novo numero da conta: ");
                int cta = int.Parse(Console.ReadLine());

                if (validaNumeroConta(cta, listaDeContas) == true)
                {
                    for (int i = listaDeContas.Count - 1; i >= 0; i--)
                    {
                        if (listaDeContas[i].numeroConta.Equals(num))
                        {
                            listaDeContas[i].numeroConta = cta;
                            Console.WriteLine("Conta Alterada com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Já existe uma conta com este numero");
                        }
                    }
                }
                break;
            case "0":
                Console.WriteLine("Saindo do programa...");
                break;

        }
        Console.ReadKey();


    }
    private static void contaUnica(List<Conta> listaDeContas)
    {
        Console.Clear();
        Console.WriteLine("Informe numero da conta");
        var cta = int.Parse(Console.ReadLine());
        for (int i = listaDeContas.Count - 1; i >= 0; i--)
        {
            if(cta == listaDeContas[i].numeroConta)
            {
                Console.WriteLine($"Titular: {listaDeContas[i].titular.Nome} CPF: {listaDeContas[i].titular.CPF}" +
                    $", Idade: {listaDeContas[i].titular.Idade}, Número da Conta: {listaDeContas[i].numeroConta}, Saldo: {listaDeContas[i].saldo}");
            }
            
        }
        Console.ReadKey();
    }
}
