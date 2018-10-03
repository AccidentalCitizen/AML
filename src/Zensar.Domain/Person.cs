using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class Person
    {
        public Person(int person_id,
                      int date_year ,             
                      string gender,              
                      string person_namex,        
                      string sanctions_descr,     
                      string sanctions_references,
                      string country_code)
        {
            this.person_id = person_id;
            this.date_year = date_year;
            this.gender = gender;
            this.person_namex = person_namex;
            this.sanctions_descr = sanctions_descr;
            this.sanctions_references = sanctions_references;
            this.country_code = country_code;   
        }
        public int      person_id                {get; private set;}
        public int      date_year                {get; private set;}
        public string gender                {get; private set;}
        public string person_namex          {get; private set;}
        public string sanctions_descr       {get; private set;}
        public string sanctions_references  {get; private set;}
        public string country_code          {get; private set;}
    }
}
