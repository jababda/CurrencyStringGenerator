using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator
{
    public class SplitCurrencyNumberMetadata
    {
        public int HundredThousands { get; set; }
        public int Thousands { get; set; }
        public int Hundreds { get; set; }
        public int Tens { get; set; }
        public int Ones { get; set; }
        public int CentTens { get; set; }
        public int CentOnes { get; set; }
    }
}
