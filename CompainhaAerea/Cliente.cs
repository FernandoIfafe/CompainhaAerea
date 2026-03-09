using System.Text.Json.Serialization;

namespace CompainhaAerea
{
    internal class Cliente : Person
    {
        /**********************************************
         * Atributos privados para validação de dados *
         **********************************************/ 
        [JsonInclude]
        private bool _passport { get; set; }//
        [JsonInclude]
        private int _quantbagagem { get; set; }//
        [JsonInclude]
        private string? _numerocadeira { get; set; }//
        [JsonInclude]
        private string? _tipodevoo{ get; set; }//
        public Cliente(string? nome, string? bi, string? phone, bool passport, string? address, DateOnly dateborn, int quantbagagem, string? numerocadeira, string? tipodevoo, int id) : base(nome, bi, phone, address, dateborn, id)
        {
            this._passport = passport;
            this._quantbagagem = quantbagagem;
            this._numerocadeira = numerocadeira;
            this._tipodevoo = tipodevoo;
        }
        public Cliente() : base() { }

        /***********************************************************
         * Geters: Para obtensão do valores dos atributos privados *
         ***********************************************************/
        public bool getPassport() { return this._passport; }//Retorna
        public int getQuantBagagem() { return this._quantbagagem; }//Retorna
        public string? getNumeroCadeira() { return this._numerocadeira; }//
        public string? getTipoDeVoo() { return this._tipodevoo; }//

        /*******************************************************************************
         * Seters: Para seleção de valores e depois atribuí-los nos atributos privados *
         *******************************************************************************/
        public void setPassport(bool passport) { this._passport = passport; }//Seleciona um novo valor para
        public void setQuantBagagem(int quantBagagem) { this._quantbagagem = quantBagagem; }//Seleciona um novo valor para
        public void setNumeroCadeira(string numerocadeira) { this._numerocadeira = numerocadeira; }//
        public void setTipoDeVoo(string tipoDeVoo) { this._tipodevoo = tipoDeVoo; }//

        public void ExibirDadosClientes()
        {
            Console.WriteLine($"  Nome completo: {getNome()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  ID: {getID()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Bilhete de identidade: {getBi()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Numero de telefone: {getPhone()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Data de nascimento: {getDateBorn()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Moradia: {getAddress()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Passaporte: {((getPassport() is true) ? "Possui passaporte" : "N/Possui passaporte")}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Tipo de voo a participar: {getTipoDeVoo()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Numero de cadeira: {getNumeroCadeira()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Quantidade d bagagem: {getQuantBagagem()}");
            Console.WriteLine("|====================================================|");
        }
    }
}
