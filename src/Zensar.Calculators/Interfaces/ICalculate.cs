using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Calculators.Interfaces
{
    public interface ICalculate<T>
    {
        T Calculate();
        T Calculate(int noOfRowsYouIntendToRetrieveForInsert,int startIndex);
    }

    public interface IAMLFormat
    {
        void FormatPerson();
        void FormatEntity();
    }
}
