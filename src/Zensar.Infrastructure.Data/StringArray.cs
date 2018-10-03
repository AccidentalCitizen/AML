using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace AI.Infrastructure.Data
{
    [DelimitedRecord(",")]
    public class StringArray
    {
        public string[] values;
    }
}
