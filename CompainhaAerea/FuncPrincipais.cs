using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompainhaAerea
{
    internal interface IFuncPrincipais
    {
        public void GuadarDados<T>(T TypeOfData, string fileName);
    }
}
