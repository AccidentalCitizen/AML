namespace DJSourceReadAndConfigureEXE
{
    class Program
    {

        static void Main(string[] args)
        {
            var dr = new AI.DependencyResolution.DependencyResolution();
            dr.ConstructContainer();
            var calculator = dr.GetAMLFormatCalculator();
            if (args[0] == "1")
            {
                calculator.FormatPerson();
            }

            if (args[0] == "2")
            {
                calculator.FormatEntity();
            }

        }

    }
}
