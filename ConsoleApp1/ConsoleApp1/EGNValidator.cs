using ConsoleApp13;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class EGNValidator : IEGNValidator
    {
        private Dictionary<string, (int start, int end)> regionalCodes = new Dictionary<string, (int, int)>()
        {
        { "Blagoevgrad", (0, 43) },
        { "Burgas", (44, 93) },
        { "Varna", (94, 139) },
        { "Veliko Tarnovo", (140, 169) },
        { "Vidin", (170, 183) },
        { "Vratsa", (184, 217) },
        { "Gabrovo", (218, 233) },
        { "Kardzhali", (234, 281) },
        { "Kyustendil", (282, 301) },
        { "Lovech", (302, 319) },
        { "Montana", (320, 341) },
        { "Pazardzhik", (342, 377) },
        { "Pernik", (378, 395) },
        { "Pleven", (396, 435) },
        { "Plovdiv", (436, 501) },
        { "Razgrad", (502, 527) },
        { "Ruse", (528, 555) },
        { "Silistra", (556, 575) },
        { "Sliven", (576, 601) },
        { "Smolyan", (602, 623) },
        { "Sofia - grad", (624, 721) },
        { "Sofia - oblast", (722, 751) },
        { "Stara Zagora", (752, 789) },
        { "Dobrich", (790, 821) },
        { "Targovishte", (822, 843) },
        { "Haskovo", (844, 871) },
        { "Shumen", (872, 903) },
        { "Yambol", (904, 925) },
        { "Друг", (926, 999) }
        };

        private int[] coefficients = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };


        public string[] Generate(DateTime birthDate, string city, bool isMale)
        {
            int year = birthDate.Year % 100;
            int month = birthDate.Month;
            int day = birthDate.Day;

            if (birthDate.Year >= 1800 && birthDate.Year <= 1899)
            {
                month += 20;
            }
            else if (birthDate.Year >= 2000 && birthDate.Year <= 2099)
            {
                month += 40;
            }

            var (start, end) = regionalCodes[city];

            if (!regionalCodes.ContainsKey(city))
            {
                throw new ArgumentException("Nevaliden grad.");
            }


            List<int> validNumbers = new List<int>();

            for (int i = start; i <= end; i++)
            {
                if ((isMale && i % 2 == 0) || (!isMale && i % 2 != 0))
                {
                    validNumbers.Add(i);
                }
            }

            if (validNumbers.Count == 0)
            {
                throw new Exception("Nqma nomera za tozi grad/pol.");
            }

            int regionalNumber = validNumbers[new Random().Next(validNumbers.Count)];


            int randomDigits = new Random().Next(100, 1000);

            string egnBase = $"{year:D2}{month:D2}{day:D2}{regionalNumber:D3}";

            int sum = 0;

               for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(egnBase[i].ToString()) * coefficients[i];
            }

            int controlDigit = sum % 11;
            if (controlDigit == 10)
                controlDigit = 0;

            string finalEgn = egnBase + controlDigit.ToString();

            return new string[] { finalEgn };
        }
            
        public bool Validate(string egn)
        {
            if (egn.Length != 10 || !egn.All(char.IsDigit))
            {
                return false;
            }

            int year = int.Parse(egn.Substring(0, 2));
            int month = int.Parse(egn.Substring(2, 2));
            int day = int.Parse(egn.Substring(4, 2));

            if (month >= 1 && month <= 12)
            {
                year += 1900;
            }
            else if (month >= 21 && month <= 32)
            {
                year += 1800;
                month -= 20;
            }
            else if (month >= 41 && month <= 52)
            {
                year += 2000;
                month -= 40;
            }
            else
            {
                return false;
            }

            try
            {
                DateTime time = new DateTime(year, month, day);
            }
            catch
            {
                return false;
            }

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(egn[i].ToString()) * coefficients[i];
            }

            int controlDigit = sum % 11;
            if (controlDigit == 10)
                controlDigit = 0;

            int egnControlDigit = int.Parse(egn[9].ToString());

            return controlDigit == egnControlDigit;

            return true;
        }
    }
}
