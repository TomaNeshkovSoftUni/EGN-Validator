using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    public interface IEGNValidator
    {
        /// <summary>
        /// Returns true for valid EGN,false otherwise.
        /// </summary>
        /// <param name="egn">EGN To Check</param>
        /// <returns></returns>
        bool Validate(string egn);


        /// <summary>
        /// Returns array of all valid EGNs for a given criteria
        /// </summary>
        /// <param name="birthDate">Date of birth</param>
        /// <param name="city">City </param>
        /// <param name="isMale">True for male,false otherwise</param>
        /// <returns></returns>
        string[] Generate(DateTime birthDate, string city, bool isMale);
    }
}