using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompainhaAerea
{
    internal class ValidarDados
    {
        public void VerifyNumber(ref string? Phone)
        {
            goto verificationCharacterInNumber;
            verificationCharacterInNumber:
            {
                if (decimal.TryParse(Phone, out decimal Test))
                {
                    goto verificationLengthOfNumber;
                    verificationLengthOfNumber:
                    {
                        if (9 == Phone.Length)
                        {
                            char[] phoneVerificate = Phone.ToCharArray();
                            goto verificationBigenOfNumber;
                            verificationBigenOfNumber:
                            {
                                if (!(phoneVerificate[0] is '9'))
                                {
                                    Console.Write("O numero de telemovel deve começar obrigatoriamente com 9: ");
                                    Phone = Console.ReadLine();
                                    phoneVerificate = Phone.ToCharArray();
                                    goto verificationBigenOfNumber;
                                }
                            }
                        }
                        else
                        {
                            Console.Write("O numero de telemovel digitado está incompleto por favor digite o numero completo: ");
                            Phone = Console.ReadLine();
                            goto verificationLengthOfNumber;
                        }
                    }
                }
                else
                {
                    Console.Write("O numero de telemovel digitado contém uma letra ou caracter especial digite o numero novamente: ");
                    Phone = Console.ReadLine();
                    goto verificationCharacterInNumber;
                }
            }
            return;
        }
        public void VerificarNome(ref string? Nome)
        {
            int space;
            goto verificarNome;
            verificarNome:
            {
                if (!(string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome)))
                {
                    if (!VerificarNumeroNoNome(Nome.ToCharArray()))
                    {
                        space = Nome.IndexOf(' ');
                        if (space != -1)
                        {
                            string strExtract = Nome.Substring(space + 1);
                            space = strExtract.IndexOf(' ');
                            if(space != -1)
                            {
                                Console.Write("Digitou 3 nomes precisamos apenas do primeiro e ultimo nome: ");
                                Nome = Console.ReadLine();
                                goto verificarNome;
                            }
                        }
                        else
                        {
                            Console.Write("Digitou apenas um nome precisamos do primeiro e ultimo nome: ");
                            Nome = Console.ReadLine();
                            goto verificarNome;
                        }
                    }
                    else
                    {
                        Console.Write("Nome invalido por favor digite um nome valido: ");
                        Nome = Console.ReadLine();
                        goto verificarNome;
                    }
                }
                else
                {
                    Console.Write("Nome invalido por favor digite um nome valido: ");
                    Nome = Console.ReadLine();
                    goto verificarNome;
                }
            }
        }
        private bool VerificarNumeroNoNome(char[] charset)
        {
            foreach (var charOne in charset)
            {
                if (char.IsNumber(charOne))
                {
                    return true;
                }
            }
            return false;
        }
        public void VerificarBI(ref string? BI)
        {
            int IPLiteral = BI.ToLower().IndexOf('a'), IPNumerica = 0;
            string? PLiteral, PNumerica;
            goto verificarBI;
            verificarBI:
            {
                if(BI.Length == 9)
                {
                    if (IPLiteral == -1)
                    {
                        Console.WriteLine("O BI deve conter 9 digitos 6 numeros e AOL digite um novo valor por favor: ");
                        BI = Console.ReadLine();
                        IPLiteral = BI.ToLower().IndexOf('a');
                        goto verificarBI;
                    }
                    else
                    {
                        PLiteral = BI.Substring(IPLiteral, 3);
                        PNumerica = BI.Substring(IPNumerica, 6);
                        if (PLiteral.ToLower() != "aol" && !decimal.TryParse(PNumerica, out decimal Test))
                        {
                            Console.WriteLine("O BI deve conter 9 digitos 6 numeros e AOL digite um novo valor por favor: ");
                            BI = Console.ReadLine();
                            IPLiteral = BI.ToLower().IndexOf('a');
                            goto verificarBI;
                        }
                        BI = BI.ToUpper();
                    }
                }
                else
                {
                    Console.WriteLine("O BI deve conter 9 digitos 6 numeros e AOL digite um novo valor por favor: ");
                    BI = Console.ReadLine();
                    IPLiteral = BI.ToLower().IndexOf('a');
                    goto verificarBI;
                }
            }
        }
        public void VerificarDataNascimento(ref string? data)
        {
            int ano, mes, dia, controler;
            string[] data2;

            goto verificarDataNascimento;
            verificarDataNascimento:
            {
                if (data.Length >= 8 && data.Length <= 10)
                {
                    controler = data.IndexOf('/');
                    if (controler != -1)
                    {
                        string subString = data.Substring(controler + 1);
                        controler = subString.IndexOf('/');
                        if(controler != -1)
                        {
                            data2 = data.Split('/');
                            if ((int.TryParse(data2[0], out ano) && int.TryParse(data2[1], out mes)) && int.TryParse(data2[2], out dia))
                            {
                                if(ano >= 1960 && ano <= 2008)
                                {
                                    if(mes >= 1 && mes <= 12)
                                    {
                                        if(!(dia >= 1 && dia <= 31))
                                        {
                                            Console.WriteLine("O dia nao pode ser menor que 1 e maior 31.");
                                            Console.WriteLine("Digite uma data de nascimento valida: ");
                                            data = Console.ReadLine();
                                            goto verificarDataNascimento;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("O mes nao pode ser menor que 1 e maior 12.");
                                        Console.WriteLine("Digite uma data de nascimento valida: ");
                                        data = Console.ReadLine();
                                        goto verificarDataNascimento;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("O ano nao pode ser menor que 1960 e maior 2008.");
                                    Console.WriteLine("Digite uma data de nascimento valida: ");
                                    data = Console.ReadLine();
                                    goto verificarDataNascimento;
                                }
                            }
                            else
                            {
                                Console.WriteLine("A data digitada contem letras.");
                                Console.WriteLine("Digite uma data de nascimento valida: ");
                                data = Console.ReadLine();
                                goto verificarDataNascimento;
                            }
                        }
                        else
                        {
                            Console.WriteLine("O digito \"/\" e obrigatorio");
                            Console.WriteLine("Digite uma nova data de nascimento: ");
                            data = Console.ReadLine();
                            goto verificarDataNascimento;
                        }
                    }
                    else
                    {
                        Console.WriteLine("O digito \"/\" e obrigatorio");
                        Console.WriteLine("Digite uma nova data de nascimento: ");
                        data = Console.ReadLine();
                        goto verificarDataNascimento;
                    }
                }
                else
                {
                    Console.WriteLine("A data digitada nao corresponde com a nossa formatação(ano/mes/dia) as barras sao origatoria deve ter no maximo 10 digitos no minimo 8 digitos.");
                    Console.WriteLine("Digite uma nova data de nascimento: ");
                    data = Console.ReadLine();
                    goto verificarDataNascimento;
                }
            }
        }
    }
}
