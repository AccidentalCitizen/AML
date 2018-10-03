using AI.Infrastructure.Data;
using FileHelpers;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AI.Calculators
{
    public static class AMLCalculator
    {
        public static void AscertainAndWritePEPorSanc()
        {
            var streamWriter = new StreamWriter(ConfigurationManager.AppSettings["WriteLocationCS"]);
            var engine = new FileHelperEngine<FileHelpersDto>();
            Console.WriteLine("Began Reading file : " + ConfigurationManager.AppSettings["ReadLocationCS"]);
            var result = engine.ReadFile(ConfigurationManager.AppSettings["ReadLocationCS"])
                .ToList();
            var con = new SqlConnection(ConfigurationManager.AppSettings["con"]);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;

            Stopwatch stopWatch = new Stopwatch();
            var cnt = 0;
            var writeResult = "";
            foreach (var row in result)
            {
                stopWatch.Start();

                var YOB = row.Rows[7];
                var name = row.Rows[1].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim() + "%" +// there are no commas or spaces in the DataTable AML DB
                row.Rows[2].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim() + "%" +
                row.Rows[3].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();

                var cmdText = "SELECT CONCAT(CONCAT(CONCAT(CONCAT(Sanctions_references,'|'),Sanctions_descr),'|'),Person_id) FROM [AML].[dbo].[AML_Person] WHERE (Person_namex = '" + name + "' AND Date_year = '" + YOB + "');";
                cmd.CommandText = cmdText;
                var reader = cmd.ExecuteScalar() == null ? "||".Split('|') : cmd.ExecuteScalar().ToString().Split('|');
                var Sanctions_references = reader[0];
                var sanctionDesc = reader[1];
                var personID = reader[2];
                var amlResult = "";
                amlResult = (String.IsNullOrEmpty(Sanctions_references) ? "Y|N" :
                            (sanctionDesc.StartsWith("OFAC") || sanctionDesc.StartsWith("EC") ||
                            sanctionDesc.StartsWith("UN") || sanctionDesc.StartsWith("HM")) ?
                            "N|Y" : "Y|N");
                stopWatch.Stop();
                Console.WriteLine("Time ellapsed DB Call " + cnt + ":  " + stopWatch.Elapsed);
                writeResult += personID + "|" + row.Rows[0] + "|" + row.Rows[1] + "|" + row.Rows[2] + "|" + row.Rows[3] + "|" + row.Rows[4] + "|" + row.Rows[5] + "|" + row.Rows[6] + "|" + row.Rows[7] + "|" + amlResult + "\n";
                cnt++;
            }

            streamWriter.Write(writeResult);
            streamWriter.Close();
        }

        public static void AscertainAndWritePEPorSancFuzzy()
        {
            var streamWriter = new StreamWriter(ConfigurationManager.AppSettings["WriteLocationCS"]);
            var engine = new FileHelperEngine<FileHelpersDto>();
            Console.WriteLine("Began Reading file : " + ConfigurationManager.AppSettings["ReadLocationCS"]);
            var result = engine.ReadFile(ConfigurationManager.AppSettings["ReadLocationCS"])
                .ToList();
            var con = new SqlConnection(ConfigurationManager.AppSettings["con"]);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;

            Stopwatch stopWatch = new Stopwatch();
            var cnt = 0;
            var writeResult = "";
            foreach (var row in result)
            {
                stopWatch.Start();

                var YOB = row.Rows[7];
                var name = row.Rows[1].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();// +  // there are no commas or spaces in the DataTable AML DB
                //row.Rows[2].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim() +
                //row.Rows[3].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();

                var cmdText = "SELECT CONCAT(CONCAT(CONCAT(CONCAT(Sanctions_references,'|'),Sanctions_descr),'|'),Person_id) FROM [AML].[dbo].[AML_Person] WHERE (Person_namex LIKE '" + name + "%' AND Date_year = '" + YOB + "');";
                cmd.CommandText = cmdText;
                var reader = cmd.ExecuteScalar() == null ? "||".Split('|') : cmd.ExecuteScalar().ToString().Split('|');
                var Sanctions_references = reader[0];
                var sanctionDesc = reader[1];
                var personID = reader[2];
                var amlResult = "";
                amlResult = (String.IsNullOrEmpty(Sanctions_references) ? "Y|N" :
                            (sanctionDesc.StartsWith("OFAC") || sanctionDesc.StartsWith("EC") ||
                            sanctionDesc.StartsWith("UN") || sanctionDesc.StartsWith("HM")) ?
                            "N|Y" : "Y|N");
                stopWatch.Stop();
                Console.WriteLine("Time ellapsed DB Call " + cnt + ":  " + stopWatch.Elapsed);
                writeResult += personID + "|" + row.Rows[0] + "|" + row.Rows[1] + "|" + row.Rows[2] + "|" + row.Rows[3] + "|" + row.Rows[4] + "|" + row.Rows[5] + "|" + row.Rows[6] + "|" + row.Rows[7] + "|" + amlResult + "\n";
                cnt++;
            }

            streamWriter.Write(writeResult);
            streamWriter.Close();
        }

        public static void WriteAtLeastPEP()
        {
            var streamWriter = new StreamWriter(ConfigurationManager.AppSettings["WriteLocation"]);
            var engine = new FileHelperEngine<FileHelpersDto>();
            Console.WriteLine("Began Reading file : " + ConfigurationManager.AppSettings["ReadLocation"]);
            var result = engine.ReadFile(ConfigurationManager.AppSettings["ReadLocation"])
                .ToList();
            var con = new SqlConnection(ConfigurationManager.AppSettings["con"]);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 36000;

            Stopwatch stopWatch = new Stopwatch();
            var cnt = 0;
            var writeResult = "";
            foreach (var row in result)
            {
                stopWatch.Start();
                var gender = row.Rows[4].Trim().Substring(0, 1).ToUpper() == "F" ? "FEMALE" : "MALE";
                var YOB = row.Rows[3].Trim().Substring(0, 4);
                var name = row.Rows[1].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim() + "%" + // there are no commas or spaces in the DataTable AML DB
                row.Rows[2].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();// +
                //row.Rows[3].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();

                var cmdText = "SELECT CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(CONCAT(person_id,'|'),initial1,'|'),initial2,'|'),person_namex,'|'),middle_name,'|'),surname,'|'),gender,'|'),date_year,'|'),date_month,'|'),date_day,'|'),country_code,'|'),address_line,'|'),address_city,'|'),address_country,'|'),occupation_title,'|'),Sanctions_descr,'|'),sanctions_references,'|'),source_description,'|'),profile_notes,'|'),active_status,'|'),role_type) FROM [AML].[data].[BPO_PersonExtended] WHERE (Person_namex LIKE '" + name + "%' AND date_year = '" + YOB + "' AND UPPER(Gender)='" + gender + "');";
                cmd.CommandText = cmdText;
                var reader = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
                var amlResult = "";
                amlResult = String.IsNullOrEmpty(reader)
                            == true ? "N|N" : "Y|Y";

                stopWatch.Stop();
                var readerResult = String.IsNullOrEmpty(reader) ? string.Empty : reader.ToString();
                Console.WriteLine("Time ellapsed DB Call " + cnt + ":  " + stopWatch.Elapsed);
                writeResult += readerResult + "|" + row.Rows[0] + "|" + row.Rows[1] + "|" + row.Rows[2] + "|" + row.Rows[3] + "|" + row.Rows[4] + "|" + amlResult + "\n";
                reader = null;
                cnt++;
            }
            streamWriter.Write(writeResult);
            streamWriter.Close();
        }

        public static void WriteAtLeastPEPFuzzy()
        {
            var streamWriter = new StreamWriter(ConfigurationManager.AppSettings["WriteLocation"]);
            var engine = new FileHelperEngine<FileHelpersDto>();
            Console.WriteLine("Began Reading file : " + ConfigurationManager.AppSettings["ReadLocation"]);
            var result = engine.ReadFile(ConfigurationManager.AppSettings["ReadLocation"])
                .ToList();
            var con = new SqlConnection(ConfigurationManager.AppSettings["con"]);
            con.Open();
            Console.WriteLine("Connection Successfully Openend!!");
            var cmd = new SqlCommand();
            cmd.Connection = con;

            Stopwatch stopWatch = new Stopwatch();
            var cnt = 0;
            var writeResult = "";
            foreach (var row in result)
            {
                stopWatch.Start();
                //var gender = row.Rows[3].Trim().Substring(0, 1).ToUpper() == "F" ? "FEMALE" : "MALE";
                var YOB = row.Rows[0].Trim().Substring(0, 4);
                var name = row.Rows[1].Replace("\'", string.Empty).Replace(" ", "%").ToUpper().Trim() + "%" +  // there are no commas or spaces in the DataTable AML DB   SUBSTRING(date_full,3,8) Date_year
                row.Rows[2].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();// + "%" +
                //row.Rows[3].Replace("\'", string.Empty).Replace(" ", string.Empty).ToUpper().Trim();

                var cmdText = "SELECT CONCAT(Person_namex,date_year) FROM [AML].[data].[AML_Person] WHERE (person_namex LIKE '" + name + "%' AND date_year = '" + YOB + "');";// AND UPPER(gender)='" + gender + "');";
                cmd.CommandText = cmdText;
                var reader = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
                var amlResult = "";
                amlResult = String.IsNullOrEmpty(reader)
                            == true ? "N|N" : "Y|Y";

                stopWatch.Stop();
                Console.WriteLine("Time ellapsed DB Call " + cnt + ":  " + stopWatch.Elapsed);
                var readerResult = String.IsNullOrEmpty(reader) ? string.Empty : reader.ToString();
                writeResult += readerResult + "|" + row.Rows[0] + "|" + row.Rows[1] + "|" + row.Rows[2] + "|" + row.Rows[3] + "|" + row.Rows[4] + "|" + amlResult + "\n";
                cnt++;
            }
            streamWriter.Write(writeResult);
            streamWriter.Close();
        }

        public static void WriteAtLeastPEPCorporateFuzzy()
        {
            var streamWriter = new StreamWriter(ConfigurationManager.AppSettings["WriteLocationEntity"]);
            var engine = new FileHelperEngine<FileHelpersDto>();
            Console.WriteLine("Began Reading file : " + ConfigurationManager.AppSettings["ReadLocationEntity"]);
            var result = engine.ReadFile(ConfigurationManager.AppSettings["ReadLocationEntity"])
                .ToList();
            var con = new SqlConnection(ConfigurationManager.AppSettings["con"]);
            con.Open();
            var cmd = new SqlCommand();
            cmd.Connection = con;

            Stopwatch stopWatch = new Stopwatch();
            var cnt = 0;
            var writeResult = "";
            foreach (var row in result)
            {
                stopWatch.Start();
                var name = row.Rows[1].Replace("\'", string.Empty).Replace("\"", string.Empty).Trim();
                var cmdText = "SELECT Entity_namex FROM [AML].[dbo].[AML_Entity] WHERE (Entity_namex LIKE '" + name + "%');";
                cmd.CommandText = cmdText;
                var reader = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
                var amlResult = "";
                amlResult = String.IsNullOrEmpty(reader)
                            == true ? "N|N" : "Y|Y";

                stopWatch.Stop();
                Console.WriteLine("Time ellapsed DB Call " + cnt + ":  " + stopWatch.Elapsed);
                writeResult += row.Rows[0] + "|" + row.Rows[1] + "|" + row.Rows[2] + "|" + row.Rows[3] + "|" + reader + "|" + amlResult + "\n";
                cnt++;
            }
            streamWriter.Write(writeResult);
            streamWriter.Close();
        }
    }
}
