using System;
using AI.DependencyResolution;

namespace DataMigrationEXE
{
    class Program
    {
        static void Main(string[] args)
        {
            var dr = new DependencyResolution();

            //var calculator = dr.GetInsertCalculator("[StatAI].[dbo].[CustByTypes]", "[StatAI].[dbo].[CustByTypesTest]",
            //    6, 
            //    @"data source=JYP1510;initial catalog=StatAI;integrated security=True;MultipleActiveResultSets=true;", 
            //    "0|1|2|3|4|5",true);

            //Starts at 0
            Console.WriteLine("Type {file} for file.");
            var result = Console.ReadLine();
            if (result == "file")
            {
                    var calculator = dr.GetFileInsertCalculator(true, @"D:\Lifestyle_Data\contract_Claim_2.txt",
                        "[StatAI].[data].[ClaimsDataLifeStyle]",
                    7,
                    @"Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                    (HOST =cmpidevlz1)(PORT = 1529)))(CONNECT_DATA =(SERVICE_NAME =LPBDEV)));User ID=interface;Password=milktart;",
                    "", false);

                    var insertStatements = calculator.Calculate();
                    var writer = dr.GetGeneralDataWriter(@"data source=JYP1510;initial catalog=StatAI;integrated security=True;MultipleActiveResultSets=true;");
                    writer.WriteData(insertStatements);
            }
            else
            {
                    var calculator = dr.GetORACLEInsertCalculator("COMPASS.CASE_DATA", 
                        "[StatAI].[stg].[MAY_HAVE_CLAIMED_BY_PRODUCT_TYPE]",
                    5,
                    @"Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                    (HOST =cmpidevlz1)(PORT = 1529)))(CONNECT_DATA =(SERVICE_NAME =LPBDEV)));User ID=interface;Password=milktart;",
                    "0|3|4", false);

                    var insertStatements = calculator.Calculate();
                    var writer = dr.GetGeneralDataWriter(@"data source=JYP1510;initial catalog=StatAI;integrated security=True;MultipleActiveResultSets=true;");
                    writer.WriteData(insertStatements);
            }
        }
    }
}
