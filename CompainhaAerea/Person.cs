using System.Text.Json.Serialization;

namespace CompainhaAerea
{
    internal class Person
    {
        /**********************************************
         * Atributos privados para validação de dados *
         **********************************************/
        [JsonInclude]
        private string? _bi { get; set; }//
        [JsonInclude]
        private string? _name { get; set; }//
        [JsonInclude]
        private string? _phone { get; set; }//
        [JsonInclude]
        private string? _address { get; set; }//
        [JsonInclude]
        private DateOnly _dateborn { get; set; }//
        [JsonInclude]
        private int _id { get; set; }//
        public Person(string? nome, string? bi, string? phone, string? address, DateOnly dateBorn, int id) 
        {
            this._bi = bi;
            this._name = nome;
            this._phone = phone;
            this._address = address;
            this._dateborn = dateBorn;
            this._id = id;
        }
        public Person()
        {
            
        }

        /***********************************************************
         * Geters: Para obtensão do valores dos atributos privados *
         ***********************************************************/
        public int getID() { return this._id; }//
        public string? getBi() { return this._bi; }//
        public string? getNome() { return this._name; }//
        public string? getPhone() { return this._phone; }//
        public string? getAddress() { return this._address; }//
        public DateOnly getDateBorn() { return this._dateborn; }//

        /*******************************************************************************
         * Seters: Para seleção de valores e depois atribuí-los nos atributos privados *
         *******************************************************************************/
        public void setBi(string? bi) { this._bi = bi; }//
        public void setNome(string? nome) { this._name = nome; }//
        public void setPhone(string? phone) { this._phone = phone; }//
        public void setAddress(string? address) {  this._address = address; }//
        public void setDateBorn(DateOnly dateborn) { this._dateborn = dateborn; }//
    }
}
