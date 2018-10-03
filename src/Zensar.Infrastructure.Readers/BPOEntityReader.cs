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
    public class BPOEntityReader : Interfaces.IDataReader<IList<Entity>>
    {
        private readonly IDataConnection<IList<EntityDto>> con;
        public BPOEntityReader(IDataConnection<IList<EntityDto>> con)
        {
            this.con = con;
        }
        public IList<Entity> GetData()
        {
            //var connectionResults = con.LoadData();
            //var entityList = new List<Entity>();
            //foreach (var rawDto in connectionResults)
            //{
            //    var domainObject =
            //        new Entity(rawDto.entity_id, rawDto.entity_name, 
            //        rawDto.sanctions_descr, rawDto.sanctions_references, 
            //        rawDto.company_country);

            //    entityList.Add(domainObject);
            //}

            return null;
        }
    }
}
