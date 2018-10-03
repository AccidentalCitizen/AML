using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class Entity
    {
        public Entity(int entity_id,
                      string entity_name,
                      string sanctions_descr,
                      string sanctions_references,
                      string company_country)
        {
            this.entity_id = entity_id;
            this.entity_name = entity_name;
            this.sanctions_descr = sanctions_descr;
            this.sanctions_references = sanctions_references;
            this.company_country = company_country;
        }   


        public int entity_id                 { get; private set; }
        public string entity_name           { get; private set; }
        public string sanctions_descr       { get; private set; }
        public string sanctions_references { get; private set; }
        public string company_country       { get; private set; }
    }
}
