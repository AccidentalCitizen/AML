using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Domain;
using AI.Infrastructure.Connections.Interfaces;
using AI.Infrastructure.Data;

namespace AI.Infrastructure.Readers
{
    public class BPOPersonReader : Interfaces.IDataReader<IList<Person>>
    {
        private readonly IDataConnection<IList<PersonDto>> con;
        public BPOPersonReader(IDataConnection<IList<PersonDto>> con)
        {
            this.con = con;
        }

        public IList<Person> GetData()
        {
            var connectionResults = con.LoadData();
            var entityList = new List<Person>();
            Console.WriteLine("Reading Ended");
            var cntr = 0;
            foreach (var rawDto in connectionResults)
            {
                var domainObject =
                    new Person(rawDto.person_id, rawDto.date_year,
                    rawDto.gender, rawDto.person_namex,
                    rawDto.SANCTIONS_REFERENCES_DESC,rawDto.SANCTIONS_REFERENCES,rawDto.country_code);
                Console.WriteLine("Converting Row " + cntr);
                entityList.Add(domainObject);
                cntr++;
            }

            return entityList;
        }
    }
}
