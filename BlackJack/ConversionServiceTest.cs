using Blackjack.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UnitTests
{
    public class ConversionServiceTest
    {
        [Fact]
        public void Tests_Are_Working()
        {
            Assert.True(true);
        }

        [Fact]
        public void ConvertStringToInt_InvalidNumberString_ConvertsToIntMinimumValue()
        {
            Assert.Equal(int.MinValue, ConversionService.ConvertStringToInt("xyzzy"));
        }

        [Fact]
        public void ConvertStringToInt_NegativeNumberString_ConvertsToNegativeNumber()
        {
            Assert.Equal(-1, ConversionService.ConvertStringToInt("-1"));
        }

        [Fact]
        public void ConvertStringToInt_NullString_ConvertsToIntMinimumValue()
        {
            Assert.Equal(int.MinValue, ConversionService.ConvertStringToInt(null));
        }
    }
}
