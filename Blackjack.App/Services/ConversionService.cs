using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.App.Services
{
    public class ConversionService
    {
        public static int ConvertStringToInt(string? str)
        {
            int returnValue = int.MinValue;

            if (int.TryParse(str, out returnValue))
            {
                return returnValue;
            }
            else return int.MinValue;
        }
    }
}
