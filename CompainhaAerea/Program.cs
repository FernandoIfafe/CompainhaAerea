using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CompainhaAerea
{
    internal class Program
    {
        const int SizeOfArray = 30;
        const string PontoDePartida = "Sede da AirPlane Compain";

        enum TipoDevoo
        {
            ServicoDeViagensComum = 1,
            ServicoDeTurismo = 2
        }
        enum FicheiroType
        {
            FicheiroVoos = 1,
            FicheiroCopilots = 2,
            FicheiroClientes = 3
        }

        public static Fly[]? fly;
        public static int CountPosicaoFly = 0;
        public static Copilot[]? copilots;
        public static int CountPosicaoCopilot = 0;
        public static Cliente[]? clientes;
        public static int CountPosicaoCliente = 0;

        public static int TimeToAddVooPortogal = 0;
        public static int TimeToAddVooBrasil = 0;
        public static int TimeToAddVooParis = 0;
        public static int TimeToAddVooCalandulas = 0;

        public static int CountPortugal = 0;
        public static int CountBrasil = 0;
        public static int CountParis = 0;
        public static int CountCalandulas = 0;

        public static string? fileNameVoo     = "BaseDataVoo.json";
        public static string? fileNameCliente = "BaseDataCliente.json";
        public static string? fileNameCopilot = "BaseDataCopilot.json";
        public static string? DataString;
        public static string? Endereco;
        public static string? Nome;
        public static string? BI;
        public static string? TipoDeVoo;
        public static string? Phone;
        public static DateOnly DataBorn;

        public static ValidarDados  valiadarDado = new ValidarDados();

        public static async Task<int> Main(string[] args)
        {
            fly = new Fly[SizeOfArray];
            copilots = new Copilot[SizeOfArray];
            clientes = new Cliente[SizeOfArray];
            InitObject();
            while (true)
            {
                MenuPrincipal();
                try
                {
                    int opcao = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (opcao)
                    {
                        case 1:
                            MenuVoo();
                            break;
                        case 2:
                            MenuBilhete();
                            break;
                        case 3:
                            MenuCopilot();
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("Programa encerrado!.");
                            return 0;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para continuar...");
                    Console.ReadKey();
                }
            }
        }
        public static void MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t|*|================================================================|*|");
            Console.WriteLine("\t\t\t|*|                                                                |*|");
            Console.WriteLine("\t\t\t|*|======== <$> Bem vindo a AirPlane Compain de Angola <$> ========|*|");
            Console.WriteLine("\t\t\t|*|                                                                |*|");
            Console.WriteLine("\t\t\t|*|================================================================|*|\n");

            Console.WriteLine("\t[1] Gerir voos.");
            Console.WriteLine("\t[2] Gerir bilhetes.");
            Console.WriteLine("\t[3] Gerir pilotos.");
            Console.WriteLine("\t[0] Encerrar o programa.");
            Console.Write("\n\tO que deseja gerir --> ");
        }
        public static void MenuVoo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t|*|=============================================================================|*|");
                Console.WriteLine("\t\t\t|*|                                                                             |*|");
                Console.WriteLine("\t\t\t|*|======== <$> Gerenciamento de Voos da AirPlane Compain de Angola <$> ========|*|");
                Console.WriteLine("\t\t\t|*|                                                                             |*|");
                Console.WriteLine("\t\t\t|*|=============================================================================|*|\n");

                Console.WriteLine("\t[1] Cadastrar novo Voos.");
                Console.WriteLine("\t[2] Consultar Voos.");
                Console.WriteLine("\t[0] Votar ao menu principal.");
                Console.Write("\n\tO que deseja fazer --> ");

                try
                {
                    int opcao = Convert.ToInt32(Console.ReadLine());
                    switch (opcao)
                    {
                        case 1:
                            CadastrarVoos();
                            break;
                        case 2:
                            ConsultarVoos();
                            break;
                        case 0:
                            SaindoMenu();
                            return;
                        default:
                            Console.WriteLine("Opcao invalida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para continuar...");
                    Console.ReadKey();
                }
            }
        }
        public static void MenuCopilot()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t|*|================================================================================|*|");
                Console.WriteLine("\t\t\t|*|                                                                                |*|");
                Console.WriteLine("\t\t\t|*|======== <$> Gerenciamento de Pilotos da AirPlane Compain de Angola <$> ========|*|");
                Console.WriteLine("\t\t\t|*|                                                                                |*|");
                Console.WriteLine("\t\t\t|*|================================================================================|*|\n");

                Console.WriteLine("\t[1] Cadastrar novo Piloto.");
                Console.WriteLine("\t[2] Consultar Pilotos.");
                Console.WriteLine("\t[3] Alterar dado do piloto.");
                Console.WriteLine("\t[0] Votar ao menu principal.");
                Console.Write("\n\tO que deseja fazer --> ");

                try
                {
                    int opcao = Convert.ToInt32(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            CadastrarCopilots();
                            break;
                        case 2:
                            ConsultarCopilots();
                            break;
                        case 3:
                            AlterarDadosDoCopilot();
                            break;
                        case 0:
                            SaindoMenu();
                            return;
                        default:
                            Console.WriteLine("Opcao invalida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para continuar...");
                    Console.ReadKey();
                }
            }
        }
        public static void MenuBilhete()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t|*|=================================================================================|*|");
                Console.WriteLine("\t\t\t|*|                                                                                 |*|");
                Console.WriteLine("\t\t\t|*|======== <$> Gerenciamento de Bilhetes da AirPlane Compain de Angola <$> ========|*|");
                Console.WriteLine("\t\t\t|*|                                                                                 |*|");
                Console.WriteLine("\t\t\t|*|=================================================================================|*|\n");

                Console.WriteLine("\t[1] Ver voos em estoque.");
                Console.WriteLine("\t[2] Comporar bilhetes.");
                Console.WriteLine("\t[3] Ver dados do cadastro.");
                Console.WriteLine("\t[0] Votar ao menu principal.");
                Console.Write("\n\tO que deseja fazer --> ");

                try
                {
                    int opcao = Convert.ToInt32(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            VerVoosEmEstoque();
                            break;
                        case 3:
                            ConsultarClientes();
                            break;
                        case 2:
                            Comprarbilhetes();
                            break;
                        case 0:
                            SaindoMenu();
                            return;
                        default:
                            Console.WriteLine("Opcao invalida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para continuar...");
                    Console.ReadKey();
                }

            }
        }
        public static void CadastrarVoos()
        {
            Console.Write("Quantos voos deseja cadastrar --> ");
            int counter = 0;
            if (int.TryParse(Console.ReadLine(), out int QuantidadeDeVoosACadastrar))
            {
                while (counter < QuantidadeDeVoosACadastrar)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t|*|========================================|*|");
                    Console.WriteLine("\t\t\t|*|                                        |*|");
                    Console.WriteLine("\t\t\t|*|======= <$> Cadastro de Voos <$> =======|*|");
                    Console.WriteLine("\t\t\t|*|                                        |*|");
                    Console.WriteLine("\t\t\t|*|========================================|*|\n");

                    int copilotID = -1;
                    goto EncontrarCopilot;
                    EncontrarCopilot:
                    {
                        if (!(copilots[0].getNome() is "") || !(copilots[0].getNome() is null))
                        {
                            Console.Write("Digite o ID ou o nome ou codigo do piloto que ira realizar o voo -->");
                            string? Dados = Console.ReadLine();
                            if (int.TryParse(Dados, out int id))
                            {
                                for (int indice = 0; indice < copilots.Length; indice++)
                                {
                                    if (copilots[indice].getID() == id)
                                    {
                                        copilotID = indice;
                                        Console.WriteLine($"O piloto {copilots[indice]} sera o responsavel deste voo.");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int indice = 0; indice < copilots.Length; indice++)
                                {
                                    if (copilots[indice].getNome() == Dados || copilots[indice].getCopilotCode() == Dados)
                                    {
                                        copilotID = indice;
                                        Console.WriteLine($"O piloto {copilots[indice]} sera o responsavel deste voo.");
                                        break;
                                    }
                                }
                            }
                        }
                        if (copilotID is -1)
                        {
                            Console.WriteLine("O nome, o id ou o codico do piloto digitado está incorreto.");
                            goto EncontrarCopilot;
                        }
                        fly[CountPosicaoFly] = new Fly(copilots[copilotID].getNome(), copilots[copilotID].getNumeroResistro(), copilots[copilotID].getDateValidateResistro(), copilots[copilotID].getTypeOfFly(), PontoDePartida, CountPosicaoFly + 1);
                        fly[CountPosicaoFly].setPontoDeChegada(DifinirPontoDePartidaDoVoo(fly[CountPosicaoFly].getTipoDeVoo()));
                        CountPosicaoFly++;
                        counter++;
                    } 
                }
            }
        }
        public static void CadastrarCopilots()
        {
            Console.Write("Quantos clientes deseja cadastrar --> ");
            int counter = 0;
            if (int.TryParse(Console.ReadLine(), out int QuantidadeDeCopilotsACadastrar))
            {
                while (counter < QuantidadeDeCopilotsACadastrar)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t|*|============================================|*|");
                    Console.WriteLine("\t\t\t|*|                                            |*|");
                    Console.WriteLine("\t\t\t|*|======= <$> Cadastro de Copilots <$> =======|*|");
                    Console.WriteLine("\t\t\t|*|                                            |*|");
                    Console.WriteLine("\t\t\t|*|============================================|*|\n");

                    GetDadosComum();
                    Console.WriteLine("----------------------------------------------------");
                    Console.Write("Codigo do piloto(#A1,#A5, #Z1, #Z5) --> ");
                    string? code = Console.ReadLine();
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine($"Numero de resistro --> {CountPosicaoCopilot + 1}");
                    Console.WriteLine($"Resistro valido de {DateTime.Now:dd/MM/yyyy} ate {DateOnly.FromDateTime(DateTime.Now).AddYears(3)}");
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine($"Id --> {CountPosicaoCopilot}");
                    Console.WriteLine("----------------------------------------------------");
                    TipoDeVoo = GerirTipoDeVoo();
                    Console.WriteLine("Guadando os dados inseridos.");
                    Thread.Sleep(3000);
                    copilots[CountPosicaoCopilot] = new Copilot(Nome, BI, Phone, Endereco, DataBorn, code, TipoDeVoo, DateOnly.FromDateTime(DateTime.Now).AddYears(3), CountPosicaoCopilot + 1, CountPosicaoCopilot);
                    ++CountPosicaoCopilot;
                    counter++;
                }
            }
            else
            {
                Console.WriteLine("Erro valor invalido!");
            }
        }
        public static bool CadastrarClientes(int Num)
        {
            string? NumeroCadeira;
            Console.Clear();
            Console.WriteLine("Antes da compra do ilhete de deve disponiilisar alguns dados.");
            GetDadosComum();
            Console.Write("Passa porte(T[Tem] e NT[N/Tem]) --> ");
            bool passPort = (Console.ReadLine() == "T") ? true : false;
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Quantidade de Bagagem(por mala) --> ");
            int QuantBagagem = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------------------------------------------");
            goto numeroCadeira;
            numeroCadeira:
            {
                Console.Write("Numero da cadeira onde deseja sentar(A1-A5/B1-B5) --> ");
                NumeroCadeira = Console.ReadLine();
            }
            foreach (var cadeira in clientes)
            {
                if(cadeira.getNumeroCadeira() == NumeroCadeira)
                {
                    Console.WriteLine("cadeira ocupada");
                    goto numeroCadeira;

                }
            }
            Console.WriteLine("----------------------------------------------------");
            if (!(passPort == false && (fly[Num - 1].getPontoDeChegada().Equals("Portugal", StringComparison.OrdinalIgnoreCase) || fly[Num - 1].getPontoDeChegada().Equals("Paris", StringComparison.OrdinalIgnoreCase)) || fly[Num - 1].getPontoDeChegada().Equals("Brasil", StringComparison.OrdinalIgnoreCase)))
            {
                clientes[CountPosicaoCliente] = new Cliente(Nome, BI, Phone, passPort, Endereco, DataBorn, QuantBagagem, NumeroCadeira, TipoDeVoo, CountPosicaoCliente);
                fly[Num - 1].setDadosCliente(CountPosicaoCliente, clientes[CountPosicaoCliente].getNome(), clientes[CountPosicaoCliente].getBi(), clientes[CountPosicaoCliente].getPhone(), clientes[CountPosicaoCliente].getAddress(), clientes[CountPosicaoCliente].getNumeroCadeira(), clientes[CountPosicaoCliente].getQuantBagagem(), clientes[CountPosicaoCliente].getID());
                Console.WriteLine("Guadando os dados inseridos.");
                Thread.Sleep(3000);
                CountPosicaoCliente++;
                return true;
            }
            else
            {
                Console.WriteLine("Nao pode viajar para: Portugal, Paris e Brasil sem passport.");
                return false;
            }
            return false;
        }
        public static void ConsultarVoos()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t|*|=============================================================================|*|");
            Console.WriteLine("\t\t\t|*|                                                                             |*|");
            Console.WriteLine("\t\t\t|*|======== <$> Cosultório de Voos da AirPlane Compain de Angola <$> ===========|*|");
            Console.WriteLine("\t\t\t|*|                                                                             |*|");
            Console.WriteLine("\t\t\t|*|=============================================================================|*|\n");

            try
            {
                while (true)
                {
                    Console.WriteLine("Digite o numero ou o nome do voo que deseja consultar: ");
                    string? dado = Console.ReadLine();
                    int i;
                    if (!(fly[0].getNomeDoVoo() is "") || !(fly[0].getNomeDoVoo() is null))
                    {
                        if (int.TryParse(dado, out int id))
                        {
                            for (i = 0; i < SizeOfArray; i++)
                            {
                                if (fly[i].getNumeroFly() == id)
                                {
                                    fly[i].ExibirDadosVoos();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for(i = 0; i < SizeOfArray; i++)
                            {
                                if(fly[i].getNomeDoVoo().Equals(dado, StringComparison.OrdinalIgnoreCase))
                                {
                                    fly[i].ExibirDadosVoos();
                                    break;
                                }
                            }
                        }
                        if (i == 30)
                        {
                            Console.WriteLine("Voo não encontrado o nome ou numero do voo estão incoretos.");                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existe voo(os) cadastrados o sistema.\nPor favor cadastre antes de consultar.");
                        break;
                    }
                    Console.WriteLine("Deseja consultar mais voos[n/s]: ");
                    string? resp = Console.ReadLine();
                    if(!(resp.Equals("s", StringComparison.OrdinalIgnoreCase)))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("Dados invalido!");
                Console.WriteLine($"Aviso: {ex.Message}");
                Console.Write("Pressione enter para continuar: ");
                Console.ReadKey();
            }
        }
        public static void ConsultarCopilots()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t|*|================================================================================|*|");
            Console.WriteLine("\t\t\t|*|                                                                                |*|");
            Console.WriteLine("\t\t\t|*|======== <$> Cosultório de Pilotos da AirPlane Compain de Angola <$> ===========|*|");
            Console.WriteLine("\t\t\t|*|                                                                                |*|");
            Console.WriteLine("\t\t\t|*|================================================================================|*|\n");

            try
            {
                while (true)
                {
                    int i;

                    if (!(copilots[0].getNome() is "") || !(copilots[0].getNome() is null))
                    {
                        Console.WriteLine("Digite o id ou o nome ou o codigo do piloto que deseja consultar: ");
                        string? dado = Console.ReadLine();

                        if (int.TryParse(dado, out int id))
                        {
                            for (i = 0; i < SizeOfArray; i++)
                            {
                                if (copilots[i].getID() == id)
                                {
                                    copilots[i].ExibirDadosCopilots();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (i = 0; i < SizeOfArray; i++)
                            {
                                if (copilots[0].getNome().Equals(dado, StringComparison.OrdinalIgnoreCase) || copilots[i].getCopilotCode().Equals(dado, StringComparison.OrdinalIgnoreCase))
                                {
                                    copilots[i].ExibirDadosCopilots();
                                    break;
                                }
                            }
                        }
                        if (i == 30)
                        {
                            Console.WriteLine("Piloto não encontrado o id ou o nome ou o codigo do piloto estão incoretos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existe piloto(os) cadastrados o sistema.\nPor favor cadastre antes de consultar.");
                        break;
                    }
                    Console.WriteLine("Deseja consultar mais voos[n/s]: ");
                    string? resp = Console.ReadLine();
                    if (!(resp.Equals("s", StringComparison.OrdinalIgnoreCase)))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("Dados invalido!");
                Console.WriteLine($"Aviso: {ex.Message}");
                Console.Write("Pressione enter para continuar...");
                Console.ReadKey();
            }
        }
        public static void ConsultarClientes()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t|*|=================================================================================|*|");
            Console.WriteLine("\t\t\t|*|                                                                                 |*|");
            Console.WriteLine("\t\t\t|*|======== <$> Cosultório de Clientes da AirPlane Compain de Angola <$> ===========|*|");
            Console.WriteLine("\t\t\t|*|                                                                                 |*|");
            Console.WriteLine("\t\t\t|*|=================================================================================|*|\n");

            try
            {
                while (true)
                {
                    Console.WriteLine("Digite o id ou o nome do cliente que deseja consultar: ");
                    string? dado = Console.ReadLine();
                    int i;

                    if (!(clientes[0].getNome() is "") || !(clientes[0].getNome() is null))
                    {
                        if (int.TryParse(dado, out int id))
                        {
                            for (i = 0; i < SizeOfArray; i++)
                            {
                                if (clientes[i].getID() == id)
                                {
                                    clientes[i].ExibirDadosClientes();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (i = 0; i < SizeOfArray; i++)
                            {
                                if (clientes[0].getNome().Equals(dado, StringComparison.OrdinalIgnoreCase))
                                {
                                    clientes[i].ExibirDadosClientes();
                                    break;
                                }
                            }
                        }
                        if (i == 30)
                        {
                            Console.WriteLine("Cliente não encontrado o nome ou numero do cliente estão incoretos.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existe cliente(es) cadastrados o sistema.\nPor favor cadastre antes de consultar.");
                        break;
                    }
                    Console.WriteLine("Deseja consultar mais voos[n/s]: ");
                    string? resp = Console.ReadLine();
                    if (!(resp.Equals("s", StringComparison.OrdinalIgnoreCase)))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("Dados invalido!");
                Console.WriteLine($"Aviso: {ex.Message}");
                Console.Write("Pressione enter para continuar...");
                Console.ReadKey();
            }
        }
        public static void AlterarDadosDoCliente()
        {

        }
        public static void AlterarDadosDoCopilot()
        {

        }
        public static void SaindoMenu()
        {
            Console.Clear();
            Console.WriteLine("Saindo...");
            Thread.Sleep(2000);
        }
        public static void InitObject()
        {
            for (int i = 0; i < SizeOfArray; i++)
            {
                #pragma warning disable
                fly[i] = new Fly();
                copilots[i] = new Copilot();
                clientes[i] = new Cliente();
            }
            if (File.Exists(fileNameCliente) && File.Exists(fileNameCopilot) && File.Exists(fileNameVoo))
            {

            }
        }
        public static string? GerirTipoDeVoo()
        {
            string? tipoDeVoo = string.Empty;
            goto difinirTipoDeVoo;
            difinirTipoDeVoo:
            {
                Console.WriteLine("\t[1] Voo viagem comum.");
                Console.WriteLine("\t[2] Voo turisticos.");
                Console.Write("\n\tO que tipo de voo deseja participar: ");
                try
                {
                    int opcao = Convert.ToInt32(Console.ReadLine());

                    switch (opcao)
                    {
                        case (int)TipoDevoo.ServicoDeViagensComum:
                            tipoDeVoo = "Viagm comum";
                            break;
                        case (int)TipoDevoo.ServicoDeTurismo:
                            tipoDeVoo = "Turistico";
                            break;
                        default:
                            Console.WriteLine("Opcao invalida.");
                            Console.Write("Pressione enter para tentar novamete: ");
                            Console.ReadKey();
                            goto difinirTipoDeVoo;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para tentar novamete: ");
                    Console.ReadKey();
                    goto difinirTipoDeVoo;
                }
            }

            return tipoDeVoo;
        }
        public static void GetDadosComum()
        {
            Console.Write("Nome(primeiro e ultimo) --> ");
            Nome = Console.ReadLine();
            valiadarDado.VerificarNome(ref Nome);
            Console.WriteLine("----------------------------------------------------");
            Console.Write("BI(bilhete de identidade) 000000AOL--> ");
            BI = Console.ReadLine();
            valiadarDado.VerificarBI(ref BI);
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Data de nascimento ordem(ano/mes/dia) --> ");
            DataString = Console.ReadLine();
            valiadarDado.VerificarDataNascimento(ref DataString);
            DataBorn = DateOnly.Parse(DataString);
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Numero de telemovel --> ");
            Phone = Console.ReadLine();
            valiadarDado.VerifyNumber(ref Phone);
            Console.WriteLine("----------------------------------------------------");
            Console.Write("Localização(Provincia e bairo) --> ");
            Endereco = Console.ReadLine();
            Console.WriteLine("----------------------------------------------------");
        }
        public static string? DifinirPontoDePartidaDoVoo(string? tipoDeVoo)
        {
            string? PotoDeChegada;
            int opcao;
            if (tipoDeVoo.Equals("Voo comum", StringComparison.OrdinalIgnoreCase))
            {
                goto difinirPontoDePartida1;
                difinirPontoDePartida1:
                {
                    Console.WriteLine("[1] Portugal.");
                    Console.WriteLine("[2] Brasil.");
                    Console.Write("\n\tOnde deseja fazer o viajar: ");
                    try
                    {
                        opcao = Convert.ToInt32(Console.ReadLine());

                        switch (opcao)
                        {
                            case 1:
                                PotoDeChegada = "Portugal";
                                fly[CountPosicaoFly].setHourToLeave(TimeOnly.Parse(DifinirHoraDePartida(fly[CountPosicaoFly].getTipoDeVoo(), PotoDeChegada)).AddHours((double)TimeToAddVooPortogal));
                                fly[CountPosicaoFly].setHourToGet(fly[CountPosicaoFly].getHourToGet().AddHours((double)8));
                                fly[CountPosicaoFly].setNomeDoVoo($"Viagm Portugal-{CountPortugal}");
                                fly[CountPosicaoFly].setPrecoVoo(2000);
                                CountPortugal++;
                                TimeToAddVooPortogal += 3;
                                break;
                            case 2:
                                PotoDeChegada = "Brasil";
                                fly[CountPosicaoFly].setHourToLeave(TimeOnly.Parse(DifinirHoraDePartida(fly[CountPosicaoFly].getTipoDeVoo(), PotoDeChegada)).AddHours((double)TimeToAddVooBrasil));
                                fly[CountPosicaoFly].setHourToGet(fly[CountPosicaoFly].getHourToGet().AddHours((double)7));
                                fly[CountPosicaoFly].setNomeDoVoo($"Viagm Brasil-{CountBrasil}");
                                fly[CountPosicaoFly].setPrecoVoo(2500);
                                TimeToAddVooBrasil += 3;
                                CountBrasil++;
                                break;
                            default:
                                Console.WriteLine("Opcao invalida.");
                                Console.Write("Pressione enter para tentar novamete: ");
                                Console.ReadKey();
                                goto difinirPontoDePartida1;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ex = new Exception("Dados invalido!");
                        Console.WriteLine($"Aviso: {ex.Message}");
                        Console.Write("Pressione enter para tentar novamete: ");
                        Console.ReadKey();
                        goto difinirPontoDePartida1;
                    }
                }
            }
            else
            {
                goto difinirPontoDePartida2;
                difinirPontoDePartida2:
                {
                    Console.WriteLine("[1] Paris(Torre henfel)");
                    Console.WriteLine("[2] Quedas de caladula");
                    Console.Write("\n\tOnde deseja fazer o turismo: ");
                    try
                    {
                        opcao = Convert.ToInt32(Console.ReadLine());

                        switch (opcao)
                        {
                            case 1:
                                PotoDeChegada = "Paris";
                                fly[CountPosicaoFly].setHourToLeave(TimeOnly.Parse(DifinirHoraDePartida(fly[CountPosicaoFly].getTipoDeVoo(), PotoDeChegada)).AddHours((double)TimeToAddVooParis));
                                fly[CountPosicaoFly].setHourToGet(fly[CountPosicaoFly].getHourToGet().AddHours((double)10));
                                fly[CountPosicaoFly].setHourToBack(fly[CountPosicaoFly].getHourToGet().AddHours((double)3));
                                fly[CountPosicaoFly].setNomeDoVoo($"Turismo Paris-{CountParis}");
                                fly[CountPosicaoFly].setPrecoVoo(3000);
                                CountParis++;
                                TimeToAddVooParis += 3;
                                break;
                            case 2:
                                PotoDeChegada = "Quedas de calandula";
                                fly[CountPosicaoFly].setHourToLeave(TimeOnly.Parse(DifinirHoraDePartida(fly[CountPosicaoFly].getTipoDeVoo(), PotoDeChegada)).AddHours((double)TimeToAddVooCalandulas));
                                fly[CountPosicaoFly].setHourToGet(fly[CountPosicaoFly].getHourToGet().AddHours((double)2));
                                fly[CountPosicaoFly].setHourToBack(fly[CountPosicaoFly].getHourToGet().AddHours((double)3));
                                fly[CountPosicaoFly].setNomeDoVoo($"Turismo Calandulas-{CountCalandulas}");
                                fly[CountPosicaoFly].setPrecoVoo(3500);
                                CountCalandulas++;
                                TimeToAddVooCalandulas += 3;
                                break;
                            default:
                                Console.WriteLine("Opcao invalida.");
                                Console.Write("Pressione enter para tentar novamete: ");
                                Console.ReadKey();
                                goto difinirPontoDePartida2;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ex = new Exception("Dados invalido!");
                        Console.WriteLine($"Aviso: {ex.Message}");
                        Console.Write("Pressione enter para tentar novamete: ");
                        Console.ReadKey();
                        goto difinirPontoDePartida2;
                    }
                }
            }
            return PotoDeChegada;
        }
        public static string? DifinirHoraDePartida(string? tipoDeVoo, string? pontoDeChegada)
        {
            if (tipoDeVoo.Equals("Voo comum", StringComparison.OrdinalIgnoreCase))
            {
                if (pontoDeChegada.Equals("Portugal", StringComparison.OrdinalIgnoreCase))
                    return "07:30:00";
                else
                    return "08:30:00";
            }
            else
            {
                if (pontoDeChegada.Equals("Paris", StringComparison.OrdinalIgnoreCase))
                    return "09:00:00";
                else
                    return "10:00:00";
            }
        }
        public static void VerVoosEmEstoque()
        {
            int id = 0;
            if (!(fly[0].getNomeDoVoo() is "" || fly[0].getNomeDoVoo() is null))
            {
                foreach (var flys in fly)
                {
                    if (flys.getNomeDoVoo() is "" || flys.getNomeDoVoo() is null)
                    {
                        break;
                    }
                    Console.WriteLine($"\tNome do voo: {flys.getNomeDoVoo()} | Estado do voo: {flys.getEstadoDoVoo()} | Numero do voo: {flys.getNumeroFly()} | Preco do voo {flys.getPrecoVoo()} KWS.");
                    id++;
                }
                Console.Write("\nPressione enter para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Nao existe voo cadastrados.");
                Console.Write("\nPressione enter para continuar...");
                Console.ReadKey();
            }
        }
        public static void Comprarbilhetes()
        {
            int Num;
            comprarVOO:
            {
                try
                {
                    Console.Write("Digite o umero do voo que deseja comprar o bilhete: ");
                    Num = int.Parse(Console.ReadLine());

                    if (!(fly[0].getNomeDoVoo() is "" || fly[0].getNomeDoVoo() is null))
                    {
                        foreach (var flys in fly)
                        {
                            if (flys.getNumeroFly() == Num)
                            {
                                if (CadastrarClientes(Num))
                                {
                                    Console.WriteLine($"\tNome do voo do bilhete comprado: {flys.getNomeDoVoo()}.");
                                }
                                else
                                {
                                    Console.WriteLine($"Compra anulada");
                                }
                                break;
                            }
                        }
                        Console.Write("\nPressione enter para continuar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Nao existe voo cadastrados.");
                        Console.Write("Pressione enter para tentar novamente...");
                        Console.ReadKey();
                        goto comprarVOO;
                    }
                }
                catch (Exception ex)
                {
                    ex = new Exception("Dados invalido!");
                    Console.WriteLine($"Aviso: {ex.Message}");
                    Console.Write("Pressione enter para tentar novamente...");
                    Console.ReadKey();
                    goto comprarVOO;
                }
            }
        }
    }
}
