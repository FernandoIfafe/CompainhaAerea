using System.Text.Json.Serialization;

namespace CompainhaAerea
{
    internal class Fly
    {
        /********************************************** 
         * Atributos privados para validação de dados *
         **********************************************/
        [JsonInclude]// 
        private string? _nomedovoo {  get; set; }//
        [JsonInclude]//
        private string? _pontodepartida { get; set; }//
        [JsonInclude]//
        private string? _pontodechegada { get; set; }//
        [JsonInclude]//
        private TimeOnly _hourtoleave { get; set; }//
        [JsonInclude]//
        private TimeOnly _hourtoget { get; set; }//
        [JsonInclude]//
        private TimeOnly _hourtoback { get; set; }
        [JsonInclude]
        private int _numerofly { get; set; }//
        [JsonInclude]//
        private string? _estadodovoo { get; set; } = "Disponivel";//
        [JsonInclude]
        private string? _tipodevoo { get; set; }
        [JsonInclude]
        private DadosCliente[] _dadosCliente { get; set; } = new DadosCliente[10];
        [JsonInclude]//
        private string? _copilotname { get; set; }//
        [JsonInclude]
        private int _numeroResistro { get; set; }
        [JsonInclude]
        private DateOnly _dateNumeroResistro { get; set; }
        [JsonInclude]
        private decimal _precovoo { get; set; }
        public Fly(string? copilotNome, int numeroResistro, DateOnly dateNumeroResistro, string? tipoDeVoo, string? pontoDePartida, int numerofly) 
        {
            this._tipodevoo = tipoDeVoo;
            this._copilotname = copilotNome;
            this._numeroResistro = numeroResistro;
            this._dateNumeroResistro = dateNumeroResistro;
            this._pontodepartida = pontoDePartida; 
            this._numerofly = numerofly;
            for (int i = 0; i < _dadosCliente.Length; i++)
            {
                this._dadosCliente[i] = new DadosCliente();
            }
        }
        public Fly() { }

        /***********************************************************
         * Geters: Para obtensão do valores dos atributos privados *
         ***********************************************************/
        public string? getNomeDoVoo() {  return this._nomedovoo; }//
        public string? getTipoDeVoo() { return this._tipodevoo; }
        public string? getPontoDePartida() { return this._pontodepartida; }//
        public string? getPontoDeChegada() { return this._pontodechegada; }//
        public TimeOnly getHourToLeave() { return this._hourtoleave; }//
        public TimeOnly getHourToGet() { return this._hourtoget; }//
        public int getNumeroFly() { return this._numerofly; }//
        public string? getEstadoDoVoo() 
        {
            int i;
            for (i = 0; i < this._dadosCliente.Length; i++)
            {
                if (this._dadosCliente[1].getClienteNome() is "" || this._dadosCliente[1].getClienteNome() is null)
                {
                    break;
                }
            }
            if(i == 10)
            {
                this._estadodovoo = "Indisponivel";
            }
            return this._estadodovoo;
        }//
        public TimeOnly getHourToBack() { return this._hourtoback; }//
        public decimal getPrecoVoo() { return this._precovoo; }

        /*******************************************************************************
         * Seters: Para seleção de valores e depois atribuí-los nos atributos privados *
         *******************************************************************************/
        public void setNomeDoVoo(string? nomeDoVoo) { this._nomedovoo = nomeDoVoo; }// 
        public void setPontoDeChegada(string? pontoDeChegada) { this._pontodechegada = pontoDeChegada; }//
        public void setPontoDePartida(string? pontoDePartida) { this._pontodepartida = pontoDePartida; }//
        public void setHourToLeave(TimeOnly dateToLeave) { this._hourtoleave = dateToLeave; }//
        public void setHourToGet(TimeOnly dateToLeave) { this._hourtoget = dateToLeave; }// 
        public void setNumeroFly(int numeroFly) { this._numerofly = numeroFly; }//
        public void setDadosCliente(int indice, string? nomeCliente, string? bi, string? phone, string? endereco, string? numeroCadeira, int quantBagagens, int clieteID)
        {
            if(indice >= 0 && indice < _dadosCliente.Length)
            {
                this._dadosCliente[indice] = new DadosCliente(nomeCliente, bi, phone, endereco, numeroCadeira, quantBagagens, clieteID);
            }
        }// X
        public void setHourToBack(TimeOnly hourToBack) { this._hourtoback = hourToBack; }// 
        public void setPrecoVoo(decimal preco) { this._precovoo = preco; }
        public string? getCopilotName() { return this._copilotname; }//
        public int getCopilotNumeroResistro() { return this._numeroResistro; }
        public DateOnly getCopilotDateNumeroResistro() { return this._dateNumeroResistro; }

        public void ExibirDadosVoos()
        {
            Console.WriteLine($" Nome do voo --> {getNomeDoVoo()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Realizador do voo --> {getCopilotName()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Numero do resistro do piloto --> {getCopilotNumeroResistro()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Data de validade do resistro do piloto --> {getCopilotDateNumeroResistro()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Tipo de voo --> {getTipoDeVoo()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Numero do voo --> {getNumeroFly()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Ponto de partida do voo --> {getPontoDePartida()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Ponto de chegada do voo --> {getPontoDeChegada()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Hora de paratir do voo --> {getHourToLeave()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($" Hora de chegada do voo --> {getHourToGet()}");
            Console.WriteLine("|====================================================|");
            if(getTipoDeVoo().Equals("Turitico", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($" Hora de regresso do voo --> {getHourToBack()}");
                Console.WriteLine("|====================================================|");
            }
            Console.WriteLine($" Estado do voo --> {getEstadoDoVoo()}");
            Console.WriteLine("|====================================================|");
        }
        public void ExibirDadosCliente()
        {
            for (int i = 0; i < 10; i++)
            {
                if (!string.IsNullOrEmpty(this._dadosCliente[i].getClienteNome()))
                {
                    Console.WriteLine($" Nome do Cliente --> {this._dadosCliente[i].getClienteNome()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" BI do Cliente --> {this._dadosCliente[i].getClienteBI()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" Id do Cliente --> {this._dadosCliente[i].getClienteID()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" Telemovel do Cliente --> {this._dadosCliente[i].getClientePhone()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" Endereco do Cliente --> {this._dadosCliente[i].getClienteEndereco()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" Numero de cadeira do Cliente --> {this._dadosCliente[i].getClienteNumeroCadeira()}");
                    Console.WriteLine("|====================================================|");
                    Console.WriteLine($" Qunatidade de Bagagem(ns) do Cliente --> {this._dadosCliente[i].getClienteQuantBagagens()}");
                    Console.WriteLine("|====================================================|");
                }
                else
                {
                    break;
                }
            }
        }
        public string GerarCadeira(string fileName)
        {
            if (File.Exists(fileName))
            {

            }
            else
            {
                File.Create(fileName).Close();
            }
            return "";
        }
        private class DadosCliente
        {
            [JsonInclude]
            string? _clienteNome { get; set; }
            [JsonInclude]
            string? _clienteBI { get; set; }
            [JsonInclude]
            string? _clientePhone { get; set; }
            [JsonInclude]
            string? _clienteEndereco { get; set; }
            [JsonInclude]
            string? _clienteNumeroCadeira { get; set; }
            [JsonInclude]
            int _clienteQuantBagagens { get; set; }
            [JsonInclude]
            int _clienteID { get; set; }
            public DadosCliente() { }
            public DadosCliente(string? ClienteNome, string? ClienteBI, string? ClientePhone, string? ClienteEndereco, string? ClienteNumeroCadeira, int ClienteQuantBagagens, int ClienteID)
            {
                this._clienteNome = ClienteNome;
                this._clienteBI = ClienteBI;
                this._clientePhone = ClientePhone;
                this._clienteEndereco = ClienteEndereco;
                this._clienteNumeroCadeira = ClienteNumeroCadeira;
                this._clienteQuantBagagens = ClienteQuantBagagens;
                this._clienteID = ClienteID;
            }

            public string? getClienteNome() { return this._clienteNome; }
            public string? getClienteBI() { return this._clienteBI; }
            public string? getClientePhone() { return this._clientePhone; }
            public string? getClienteEndereco() { return this._clienteEndereco; }
            public string? getClienteNumeroCadeira() { return this._clienteNumeroCadeira; }
            public int getClienteQuantBagagens() { return this._clienteQuantBagagens; }
            public int getClienteID() { return this._clienteID; }    
        }
    }
}
