using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zensar.Domain;
using Zensar.Infrastructure.Data;
using Zensar.Infrastructure.Data.EF;
using Zensar.Infrastructure.Writers.Infrastructure;

namespace Zensar.Infrastructure.Writers
{
    public class PersonWriter : IDataWriter<IList<Person>>
    {
        private BPOEntities context;
        public PersonWriter(BPOEntities context)
        {
            this.context = context;
        }
        public void WriteData(IList<Person> data)
        {
            var cntr = 0;
            foreach (var person in data)
            {

                var dto = new BPO_Person();
                dto.country_code = person.country_code;
                dto.date_year = person.date_year.ToString();
                dto.gender = person.gender;
                dto.person_id = person.person_id.ToString(); 
                dto.person_namex = person.person_namex;
                dto.sanctions_descr = person.sanctions_descr;
                dto.sanctions_references = person.sanctions_references;
                Console.WriteLine("Adding line to batch write " + cntr);
                cntr++;
                context.BPO_Person.Add(dto);
            }
            Console.WriteLine("About to batch write..");
            context.SaveChanges();
            Console.WriteLine("Batch write complete!!..");
        }
    }
}
