using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class DjListOutputFieldsEntity
    {
        public string EntityID { get; set; }//id attribute
        public string EntityDate { get; set; }//Date Attribute
        public string EntityName { get; set; }
        public string EntityNameU { get; set; }
        public string EntityNameSpacesRemoved { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCity { get; set; }
        public string CountryValue { get; set; }//Code attribute
        public string SourceDescription { get; set; }//
        public string SanctionsReferences { get; set; }
        public string ProfileNotes { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class DjListOutputFieldsPerson
    {
        public string PersonId { get; set; }//id attribute
        public string Initial1 { get; set; }
        public string Initial2 { get; set; }
        public string NameSpacesRemoved { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }//Code attribute
        public string MonthOfBirth { get; set; }
        public string DayOfBirth { get; set; }
        public string CountryValue { get; set; }//Code attribute
        public string AddressLine { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public string OccupationTitle { get; set; }
        public string SanctionsDescription { get; set; }
        public string SourceDescription { get; set; }//
        public string SanctionsReferences { get; set; }
        public string Date { get; set; }//Date Attribute
        public string ProfileNotes { get; set; }
        public string ActiveStatus { get; set; }
        public string RoleType { get; set; }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public string Name { get; set; }
        public string NameU { get; set; }
    }
}
