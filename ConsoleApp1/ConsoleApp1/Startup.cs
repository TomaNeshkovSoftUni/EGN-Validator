using System;
using ConsoleApp13;

namespace ConsoleApp1
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            EGNValidator validator = new EGNValidator();

            Console.WriteLine("Izberi opciq:");
            Console.WriteLine("1. Validirane na EGN");
            Console.WriteLine("2. Generirane na EGN");
            Console.Write("Izbor: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Vuvedi EGN: ");
                string inputEgn = Console.ReadLine();

                bool isValid = validator.Validate(inputEgn);
                if (isValid)
                    Console.WriteLine("EGN-to e validno!");
                else
                    Console.WriteLine("EGM-to NE e validno!");
            }
            else if (option == "2")
            {
                Console.Write("Vyvedi datata na rajdane (YYYY-MM-DD): ");
                DateTime birthDate;
                while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    Console.Write("Nevalidna data. Opitai otnovo: ");
                }

                Console.Write("Vuvedi grad: ");
                string city = Console.ReadLine();

                Console.Write("Vuvedi pol (m = male; f = female): ");
                bool isMale = Console.ReadLine().ToLower() == "m";

                try
                {
                    string[] egns = validator.Generate(birthDate, city, isMale);
                    Console.WriteLine("Sgenerirano EGN:");
                    foreach (var egn in egns)
                    {
                        Console.WriteLine(egn);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GRESHKA: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Nevaliden izbor.");
            }
        }
    }
}
