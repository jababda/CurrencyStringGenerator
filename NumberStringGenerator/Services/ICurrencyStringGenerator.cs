using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator.Services
{
    public interface ICurrencyStringGenerator
    {
        string GenerateNumberString(decimal input);
    }

    public class EnglishCurrencyStringGenerator : ICurrencyStringGenerator
    {
        private readonly EnglishNumericValuesToStringMapping _characterMapping;
        private readonly INumberComponentGenerator _numberComponentGenerator;
        public EnglishCurrencyStringGenerator(EnglishNumericValuesToStringMapping characterMapping, INumberComponentGenerator numberComponentGenerator)
        {
            _characterMapping = characterMapping;
            _numberComponentGenerator = numberComponentGenerator;
        }

        public string GenerateNumberString(decimal input)
        {

            if (input == 0) return "zero dollars";
            if (input < 0) throw new ArgumentException("must enter a value greater or equal to 0", "input");

            var splitNum = _numberComponentGenerator.SpitNumberIntoComponentParts(input);

            var currencyString = GetDollarString(splitNum);
            currencyString += string.IsNullOrWhiteSpace(GetCentsString(splitNum)) ? "" : $" and {GetCentsString(splitNum)}";

            return currencyString;
        }

        public string GetHundredThousandsString(int hundredThousands)
        {
            return hundredThousands != 0 ? $"{GetTensAndOnesString(hundredThousands / 10, hundredThousands % 10)} {_characterMapping.StringForHundredThousand}" : "";
        }

        public string GetThousandsString(int thousands)
        {
            return thousands != 0 ? $"{GetTensAndOnesString(thousands / 10, thousands % 10)} {_characterMapping.StringForThousand}" : "";
        }

        public string GetHundredsString(int hundreds)
        {
            return hundreds != 0 ? $"{_characterMapping.UnquieNumberMapping[hundreds]} {_characterMapping.StringForHundred}" : "";
        }

        public string GetTensAndOnesString(int tens, int ones)
        {
            //if there is a number between 11 and 19
            if (tens == 1 && ones != 0)
            {
                return _characterMapping.UnquieNumberMapping[tens * 10 + ones];
            }

            //has tens 
            else if (tens >= 1)
            {
                return _characterMapping.UnquieNumberMapping[tens * 10] + (ones != 0 ? $" {_characterMapping.UnquieNumberMapping[ones]}" : "");
            }

            return ones != 0 ? $"{_characterMapping.UnquieNumberMapping[ones]}" : "";

        }

        public string GetDollarString(SplitCurrencyNumberMetadata splitNum)
        {
            var dollarString = "";

            dollarString += string.IsNullOrWhiteSpace(GetHundredThousandsString(splitNum.HundredThousands)) ? "" : GetThousandsString(splitNum.Thousands) + " ";
            dollarString += string.IsNullOrWhiteSpace(GetThousandsString(splitNum.Thousands)) ? "" : GetThousandsString(splitNum.Thousands) + " ";
            dollarString += string.IsNullOrWhiteSpace(GetHundredsString(splitNum.Hundreds)) ? "" : GetHundredsString(splitNum.Hundreds) + " ";

            if(!string.IsNullOrWhiteSpace(GetTensAndOnesString(splitNum.Tens, splitNum.Ones)))
            {
                var tensAndOnesString = GetTensAndOnesString(splitNum.Tens, splitNum.Ones);
                dollarString += string.IsNullOrWhiteSpace(dollarString) ? $"{tensAndOnesString} " : $"and {tensAndOnesString} ";
            }

            return string.IsNullOrWhiteSpace(dollarString) ? "": dollarString + "dollars";
        }

        public string GetCentsString(SplitCurrencyNumberMetadata splitNum)
        {
            return string.IsNullOrWhiteSpace(GetTensAndOnesString(splitNum.CentTens, splitNum.CentOnes)) ? "" : $"{GetTensAndOnesString(splitNum.CentTens, splitNum.CentOnes)} cents";
        }
    }

}
