using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Business
{
    public enum CompoundingFrequency : short
    {
        Daily = 0,
        Monthly,
        Quarterly,
        Yearly
    }

    public class Calculations
    {

        //formule    = P ( 1+ r/n) raise to (n * t)
        public static double CalculateFinalAmount(decimal deposit, float interestRate, int months,
            CompoundingFrequency compudingFreq = CompoundingFrequency.Quarterly)
        {
            int compFreq = GetCompoundingFrequency(compudingFreq);

            var r_by_n = (interestRate / 100) / compFreq;

            var powTerm = compFreq * (months / 12);

            var finalValue = (double)deposit * Math.Pow(1 + r_by_n, powTerm);

            return Math.Round(finalValue, 2);
        }


        private static int GetCompoundingFrequency(CompoundingFrequency freq)
        {
            switch (freq)
            {
                case CompoundingFrequency.Daily:
                    return 365;
                case CompoundingFrequency.Monthly:
                    return 12;
                case CompoundingFrequency.Quarterly:
                    return 4;
                case CompoundingFrequency.Yearly:
                    return 1;
                default:
                    return 4;
            }
        }
    }
}
