using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AI.Domain;
using AI.Infrastructure.Readers.Interfaces;

namespace AI.Infrastructure.Readers
{
    public class XMLEntityReader : IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsEntity>, IList<string>>
    {
        public XMLEntityReader()
        {

        }
        public IList<DjListOutputFieldsEntity> GetData(string path, IList<string> values)
        {
            return ParseXML(path, values);
        }

        private static List<DjListOutputFieldsEntity> ParseXML(string path, IList<string> nodeName)
        {
            var fieldsObjectList = new List<DjListOutputFieldsEntity>();
            string xmlFile = File.ReadAllText(path);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlFile);

            var xmlNodeList = xmldoc.SelectNodes("/PFA/Entity");

            foreach(XmlNode nodes in xmlNodeList)
            {
                var textForm = nodes.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>"+textForm+"</Encap>");
                var djFields = new DjListOutputFieldsEntity();
                djFields.EntityID = nodes.Attributes["id"]==null? "": nodes.Attributes["id"].Value.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.EntityDate =nodes.Attributes["date"]==null?"": nodes.Attributes["date"].Value.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.ProfileNotes =nodes["ProfileNotes"]==null?"": nodes["ProfileNotes"].InnerText.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                if(djFields.ProfileNotes.Contains(","))
                {
                    var tempCheck = nodes["ProfileNotes"].InnerText.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                }
                djFields.SourceDescription = nodes["SourceDescription"]==null?"": nodes["SourceDescription"].InnerText.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");

                djFields.EntityName = GetPrimaryName(xmlTempdoc.GetElementsByTagName("NameDetails")).Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.EntityNameU =djFields.EntityName.ToUpper().Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.EntityNameSpacesRemoved =djFields.EntityName.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.CountryValue =xmlTempdoc.GetElementsByTagName("CountryValue") == null ? "" 
                    : xmlTempdoc.GetElementsByTagName("CountryValue")[0].Attributes["Code"].Value.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.ActiveStatus =nodes["ActiveStatus"] ==null?"": nodes["ActiveStatus"].InnerText.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                djFields.SanctionsReferences =nodes["SanctionsReferences"] ==null?"": nodes["SanctionsReferences"].InnerText.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ");
                fieldsObjectList.Add(djFields);
            }

            return fieldsObjectList;
            //string id = xmldoc.SelectSingleNode("Data/Short_Fall").InnerText;

        }

        public IList<string> GetInStringFormat(char delimiter, IList<DjListOutputFieldsEntity> list)
        {
            var res = new List<string>();
            foreach (var djFields in list)
            {
                var row = djFields.EntityID + delimiter.ToString() +
                    djFields.EntityNameSpacesRemoved + delimiter.ToString() +
                     djFields.EntityName + delimiter.ToString() +
                djFields.EntityNameU + delimiter.ToString() +
                djFields.EntityNameU + delimiter.ToString() +
                djFields.CompanyAddress + delimiter.ToString() +
                djFields.CompanyCity + delimiter.ToString() +
                djFields.CountryValue + delimiter.ToString() +
                djFields.SourceDescription + delimiter.ToString() +
                djFields.ProfileNotes.Replace(System.Environment.NewLine, " ").Replace("\'", string.Empty).Replace("\"", string.Empty).Replace(",", " ") + delimiter.ToString() +
                djFields.SanctionsReferences + delimiter.ToString() +
                djFields.ActiveStatus;
                res.Add(row);
            }
            return res;
        }

        private static string GetPrimaryName(XmlNodeList list)
        {
            var primaryName = "";
            foreach(XmlNode nd in list)
            {
                var textForm = nd.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>" + textForm + "</Encap>");
                var nameTypeAttribute = xmlTempdoc.GetElementsByTagName("Name") == null ? "" 
                        : xmlTempdoc.GetElementsByTagName("Name")[0].Attributes["NameType"].Value;
                var innerXml = xmlTempdoc.GetElementsByTagName("Name") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Name")[0].InnerXml;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml("<TempInner>"+ innerXml + "</TempInner>");
                if (nameTypeAttribute.Trim().ToUpper() == "Primary Name".Trim().ToUpper())
                {
                    primaryName = xdoc.GetElementsByTagName("EntityName") == null ? "" 
                        : xdoc.GetElementsByTagName("EntityName")[0].InnerText;
                }
            }
            return primaryName;
        }

    }

    public class XMLPersonReader : IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsPerson>, IList<string>>
    {
        public XMLPersonReader()
        {

        }
        public IList<DjListOutputFieldsPerson> GetData(string path, IList<string> values)
        {
            return ParseXML(path, values);
        }

        private static List<DjListOutputFieldsPerson> ParseXML(string path, IList<string> nodeName)
        {
            var fieldsObjectList = new List<DjListOutputFieldsPerson>();
            string xmlFile = File.ReadAllText(path);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlFile);

            var xmlNodeList = xmldoc.SelectNodes("/PFA/Person");

            foreach (XmlNode nodes in xmlNodeList)
            {
                var textForm = nodes.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>" + textForm + "</Encap>");
                var djFields = new DjListOutputFieldsPerson();
                djFields.PersonId = nodes.Attributes["id"] == null ? "" : nodes.Attributes["id"].Value;
                djFields.Date = nodes.Attributes["date"] == null ? "" : nodes.Attributes["date"].Value;
                djFields.ProfileNotes = nodes["ProfileNotes"] == null ? "" : nodes["ProfileNotes"].InnerText.Replace(System.Environment.NewLine, " ");
                djFields.SanctionsDescription = nodes["Descriptions"] == null ? "" : nodes["Descriptions"].InnerText.Replace(System.Environment.NewLine, " ");
                djFields.SourceDescription = nodes["SourceDescription"] == null ? "" : nodes["SourceDescription"].InnerText.Replace(System.Environment.NewLine, " ");
                djFields.DateOfBirth = GetPrimaryYearOfBirth(xmlTempdoc.GetElementsByTagName("DateDetails"))==("")?"||"
                    : GetPrimaryYearOfBirth(xmlTempdoc.GetElementsByTagName("DateDetails"));
                djFields.Gender = nodes["Gender"] == null ? "" : nodes["Gender"].InnerText.Replace(System.Environment.NewLine, " "); ;
                djFields.Name = GetPrimaryName(xmlTempdoc.GetElementsByTagName("NameDetails"));
                var tempName = djFields.Name;
                djFields.NameU = tempName.ToUpper();
                djFields.Surname = tempName.Split('|')[2];
                djFields.MiddleName = tempName.Split('|')[1];
                djFields.NameSpacesRemoved = tempName.Replace("|",string.Empty).Replace(" ", string.Empty).Trim().ToUpper();
                djFields.CountryValue = xmlTempdoc.GetElementsByTagName("CountryValue") == null ? ""
                    : xmlTempdoc.GetElementsByTagName("CountryValue")[0].Attributes["Code"].Value.Replace(System.Environment.NewLine, " ");
                djFields.ActiveStatus = nodes["ActiveStatus"] == null ? "" : nodes["ActiveStatus"].InnerText.Replace(System.Environment.NewLine, " ");
                djFields.SanctionsReferences = nodes["SanctionsReferences"] == null ? "" : nodes["SanctionsReferences"].InnerText.Replace(System.Environment.NewLine, " ");
                djFields.OccupationTitle = GetPrimaryOccupation(xmlTempdoc.GetElementsByTagName("RoleDetail"));
                djFields.RoleType = "Primary Occupation";
                fieldsObjectList.Add(djFields);
            }

            return fieldsObjectList;
            //string id = xmldoc.SelectSingleNode("Data/Short_Fall").InnerText;

        }

        public IList<string> GetInStringFormat(char delimiter, IList<DjListOutputFieldsPerson> list)
        {
            var res = new List<string>();
            foreach (var djFields in list)
            {
                var row = djFields.PersonId + delimiter.ToString() +
                djFields.Initial1 + delimiter.ToString() +
                djFields.Initial2 + delimiter.ToString() +
                djFields.NameSpacesRemoved + delimiter.ToString() +
                djFields.MiddleName + delimiter.ToString() +
                djFields.Surname + delimiter.ToString() +
                djFields.Gender + delimiter.ToString() +
                djFields.DateOfBirth + delimiter.ToString() +
                djFields.CountryValue + delimiter.ToString() +
                djFields.AddressLine + delimiter.ToString() +
                djFields.AddressCity + delimiter.ToString() +
                djFields.AddressCountry + delimiter.ToString() +
                djFields.OccupationTitle + delimiter.ToString() +
                djFields.SanctionsDescription.Replace("'","\'").Replace("|",string.Empty).Replace("\n",string.Empty) + delimiter.ToString() +
                djFields.SanctionsReferences.Replace("'", "\'").Replace("|", string.Empty).Replace("\n", string.Empty) + delimiter.ToString() +
                djFields.SourceDescription.Replace("'", "\'").Replace("|", string.Empty).Replace("\n", string.Empty) + delimiter.ToString() +
                djFields.ProfileNotes.Replace("'", "\'").Replace("|", string.Empty).Replace("\n", string.Empty) + delimiter.ToString() +
                djFields.ActiveStatus + delimiter.ToString() +
                djFields.RoleType;

                res.Add(row);
            }
            return res;
        }

        private static string GetPrimaryName(XmlNodeList list)
        {
            var primaryName = "";
            foreach (XmlNode nd in list)
            {
                var textForm = nd.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>" + textForm + "</Encap>");
                var nameTypeAttribute = xmlTempdoc.GetElementsByTagName("Name") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Name")[0].Attributes["NameType"].Value;
                var innerXml = xmlTempdoc.GetElementsByTagName("Name") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Name")[0].InnerXml;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml("<TempInner>" + innerXml + "</TempInner>");
                if (nameTypeAttribute.Trim().ToUpper() == "Primary Name".Trim().ToUpper())
                {
                    var first = xdoc.GetElementsByTagName("FirstName") == null ? ""
                         : (xdoc.GetElementsByTagName("FirstName")[0] == null ? "" :
                        xdoc.GetElementsByTagName("FirstName")[0].InnerText);

                    var last = xdoc.GetElementsByTagName("Surname") == null ? ""
                        : (xdoc.GetElementsByTagName("Surname")[0] == null ? "" :
                        xdoc.GetElementsByTagName("Surname")[0].InnerText);

                    var middle = xdoc.GetElementsByTagName("MiddleName")==null ? "" 
                        : (xdoc.GetElementsByTagName("MiddleName")[0] == null? "": 
                        xdoc.GetElementsByTagName("MiddleName")[0].InnerText);
                    primaryName = first + "|" + middle + "|" + last;
                }
            }
            return primaryName;
        }

        private static string GetPrimaryYearOfBirth(XmlNodeList list)
        {
            var dateValue = "";
            foreach (XmlNode nd in list)
            {
                var textForm = nd.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>" + textForm + "</Encap>");
                var nameTypeAttribute = xmlTempdoc.GetElementsByTagName("Date") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Date")[0].Attributes["DateType"].Value;
                var innerXml = xmlTempdoc.GetElementsByTagName("Date") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Date")[0].InnerXml;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml("<TempInner>" + innerXml + "</TempInner>");
                if (nameTypeAttribute.Trim().ToUpper() == "Date of Birth".Trim().ToUpper())
                {
                    var year = xdoc.GetElementsByTagName("DateValue") == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0] == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0].Attributes["Year"] == null?""
                        : xdoc.GetElementsByTagName("DateValue")[0].Attributes["Year"].Value));

                    var month = xdoc.GetElementsByTagName("DateValue") == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0] == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0].Attributes["Month"] == null ? ""
                        : xdoc.GetElementsByTagName("DateValue")[0].Attributes["Month"].Value));

                    var day = xdoc.GetElementsByTagName("DateValue") == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0] == null ? ""
                        : (xdoc.GetElementsByTagName("DateValue")[0].Attributes["Day"] == null ? ""
                        : xdoc.GetElementsByTagName("DateValue")[0].Attributes["Day"].Value));
                    dateValue = year + "|" + month + "|" + day;
                }
            }
            return dateValue;
        }


        private static string GetPrimaryOccupation(XmlNodeList list)
        {
            var roleValue = "";
            foreach (XmlNode nd in list)
            {
                var textForm = nd.InnerXml.ToString();
                XmlDocument xmlTempdoc = new XmlDocument();
                xmlTempdoc.LoadXml("<Encap>" + textForm + "</Encap>");
                var nameTypeAttribute = xmlTempdoc.GetElementsByTagName("Roles") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Roles")[0].Attributes["RoleType"].Value;
                var innerXml = xmlTempdoc.GetElementsByTagName("Roles") == null ? ""
                        : xmlTempdoc.GetElementsByTagName("Roles")[0].InnerXml;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml("<TempInner>" + innerXml + "</TempInner>");
                if (nameTypeAttribute.Trim().ToUpper() == "Primary Occupation".Trim().ToUpper())
                {
                    var role = xdoc.GetElementsByTagName("OccTitle") == null ? ""
                        : (xdoc.GetElementsByTagName("OccTitle")[0] == null ? ""
                        : (xdoc.GetElementsByTagName("OccTitle")[0].InnerText));

                    roleValue =role;
                }
            }
            return roleValue;
        }

    }
}
