using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Formatting.Interfaces;
using AI.Infrastructure.Connections.Interfaces;
using AI.Infrastructure.Data;

namespace AI.Formatting
{
    public class PersonFileFormatter : IFormat<IList<PersonDto>>
    {
        IDataConnection<IList<PersonDto>> con;
        public PersonFileFormatter(IDataConnection<IList<PersonDto>> con)
        {
            this.con = con;
        }

        public IList<PersonDto> Format()
        {
            var connectionResults = con.LoadData();
            foreach (var rawDto in connectionResults)
            {
            }
                throw new NotImplementedException();
        }
    }
}
