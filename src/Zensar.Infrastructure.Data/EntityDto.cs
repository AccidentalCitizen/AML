using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace AI.Infrastructure.Data
{
    [DelimitedRecord("~")]
    [IgnoreFirst(1)]
    public class EntityDto
    {
        [FieldQuoted]
        public int entity_id;
        [FieldQuoted]
        public string entity_name;
        [FieldQuoted]
        public string entity_nameu;
        [FieldQuoted]
        public string entity_name_excl;
        [FieldQuoted]
        public string SANCTIONS_DESCR;
        [FieldQuoted]
        public string date_year;
        [FieldQuoted]
        public string SANCTIONS_REFERENCES;
        [FieldQuoted]
        public string country_code;
    }
}
//entity_id~entity_name~entity_nameu~entity_name_excl~SANCTIONS_DESCR~date_year~SANCTIONS_REFERENCES~country_code