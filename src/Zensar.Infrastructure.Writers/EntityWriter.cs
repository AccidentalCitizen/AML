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
    public class EntityWriter : IDataWriter<IList<Entity>>
    {
        private BPOEntities context;
        public EntityWriter(BPOEntities context)
        {
            this.context = context;
        }
        public void WriteData(IList<Entity> data)
        {
            var cntr = 0;
            foreach (var person in data)
            {
                var dto = new BPO_Entity();
                dto.company_country = person.company_country;
                dto.entity_id = person.entity_id.ToString();
                dto.entity_name = person.entity_name;
                dto.sanctions_descr = person.sanctions_descr;
                dto.sanctions_references = person.sanctions_references;
                context.BPO_Entity.Add(dto);
                Console.WriteLine("Adding line to batch write " + cntr);
                cntr++;
            }
            context.SaveChanges();
        }
    }
}
