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
    public class PersonDto
    {
        [FieldQuoted]
        [FieldNullValue(0)]
        public int person_id;
        [FieldQuoted]
        [FieldNullValue(0)]
        public int date_year;
        [FieldQuoted]
        [FieldNullValue("")]
        public string gender;
        [FieldQuoted]
        [FieldNullValue("")]
        public string person_namex;
        [FieldQuoted]
        [FieldNullValue("")]
        public string SANCTIONS_REFERENCES_DESC;
        [FieldQuoted]
        [FieldNullValue("")]
        public string SANCTIONS_REFERENCES;
        [FieldQuoted]
        [FieldNullValue("")]
        public string country_code;
    }
}
//person_id~date_year~gender~person_namex~SANCTIONS_REFERENCES_DESC~SANCTIONS_REFERENCES~country_code