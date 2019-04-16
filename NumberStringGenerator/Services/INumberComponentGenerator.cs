using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator.Services
{
    public interface INumberComponentGenerator
    {
        SplitCurrencyNumberMetadata SpitNumberIntoComponentParts(decimal input);
    }

    public class NumberComponentGenerator : INumberComponentGenerator
    {
        public SplitCurrencyNumberMetadata SpitNumberIntoComponentParts(decimal input)
        {
            var splitNumber = new SplitCurrencyNumberMetadata();

            splitNumber.HundredThousands = (int)input / 100000;
            input -= ((int)input / 100000) * 100000;

            splitNumber.Thousands = (int)input / 1000;
            input -= ((int)input / 1000) * 1000;

            splitNumber.Hundreds = (int)input / 100;
            input -= ((int)input / 100) * 100;

            splitNumber.Tens = (int)input / 10;
            input -= ((int)input / 10) * 10;

            splitNumber.Ones = (int)input;
            input -= ((int)input);

            if ((int)input != 0) throw new Exception("failed to zero out dollar component");

            //(int)input should be zero at this stage now round decimal
            input = Math.Round(input, 2, MidpointRounding.ToEven);

            input *= 100;

            splitNumber.CentTens = (int)input / 10;
            input -= ((int)input / 10) * 10;

            splitNumber.CentOnes = (int)input;
            input -= ((int)input);

            //input if not something has gone wrong
            if ((int)input != 0) throw new Exception("value could not be split down to zero");

            return splitNumber;
        }
    }
}
