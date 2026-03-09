using System.Text.Json.Serialization;

namespace CompainhaAerea
{
    internal class Copilot : Person
    {
        /**********************************************
         * Atributos privados para validação de dados * 
         **********************************************/
        [JsonInclude]
        private string? _copilotcode { get; set; }//
        [JsonInclude]
        private string? _typeoffly { get; set; }//
        [JsonInclude]
        private int _numeroresistro { get; set; }//
        [JsonInclude] 
        private DateOnly _datevalidateresistro { get; set; }//
        public Copilot(string? nome, string? bi, string? phone, string? address, DateOnly dateBorn, string? copilotcode, string typeOfFly, DateOnly datevalidateResistro, int numeroResistro, int id) : base(nome, bi, phone, address, dateBorn, id)
        {
            this._copilotcode = copilotcode;
            this._typeoffly = typeOfFly;
            this._datevalidateresistro = datevalidateResistro; 
            this._numeroresistro = numeroResistro;
        }
        public Copilot() : base()
        {
            
        }

        /***********************************************************
         * Geters: Para obtensão do valores dos atributos privados *
         ***********************************************************/
        public string? getCopilotCode() { return this._copilotcode; }//
        public string? getTypeOfFly() { return this._typeoffly; }//
        public int getNumeroResistro() { return this._numeroresistro; }//
        public DateOnly getDateValidateResistro() { return this._datevalidateresistro; }//

        /*******************************************************************************
         * Seters: Para seleção de valores e depois atribuí-los nos atributos privados *
         *******************************************************************************/
        public void setCopilotCode(string? copilotcode) { this._copilotcode = copilotcode; }//
        public void setTypeOfFly(string typeoffly) { this._typeoffly = typeoffly; }//
        public void setNumeroResisto(int numeroresisto) { this._numeroresistro =  numeroresisto; }//
        public void setDateValidateResistro(DateOnly datevalidateresisto) { this._datevalidateresistro = datevalidateresisto; }//

        public void ExibirDadosCopilots()
        {
            Console.WriteLine($"  Nome completo --> {getNome()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  ID --> {getID()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Bilhete de identidade --> {getBi()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Numero de telefone --> {getPhone()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Data de nascimento --> {getDateBorn()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Moradia --> {getAddress()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Codigo do piloto --> {getCopilotCode()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Tipo de voo a realizar --> {getTypeOfFly()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Data de validade do resistro de voo --> {getDateValidateResistro()}");
            Console.WriteLine("|====================================================|");
            Console.WriteLine($"  Numero de resistro de voo --> {getNumeroResistro()}");
            Console.WriteLine("|====================================================|");
        }
        public void ConsultarCopilots()
        {

        }
    }
}
