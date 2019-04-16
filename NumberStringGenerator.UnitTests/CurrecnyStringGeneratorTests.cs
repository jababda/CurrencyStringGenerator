using AutofacContrib.NSubstitute;
using NSubstitute;
using NumberStringGenerator.Services;
using NUnit.Framework;
using System;

namespace NumberStringGenerator.UnitTests
{
    public class EnglishCurrecnyStringGeneratorTests
    {
        private EnglishNumericValuesToStringMapping _characterMapping = new EnglishNumericValuesToStringMapping();
        private INumberComponentGenerator _numberComponentGenerator;
        private EnglishCurrencyStringGenerator _currencyStringGenerator;

        [SetUp]
        public void Setup()
        {

            _numberComponentGenerator = Substitute.For<INumberComponentGenerator>();
            var autoSubstitute = new AutoSubstitute();
            autoSubstitute.Provide(_characterMapping);
            autoSubstitute.Provide(_numberComponentGenerator);

            _currencyStringGenerator = autoSubstitute.Resolve<EnglishCurrencyStringGenerator>();
        }

        [Test]
        [TestCase(0, "")]
        [TestCase(1, "one hundred thousand")]
        [TestCase(2, "two hundred thousand")]
        [TestCase(3, "three hundred thousand")]
        [TestCase(10, "ten hundred thousand")]
        [TestCase(34, "thirty four hundred thousand")]
        public void GetHundredThousandsStringReturnsCorrectString(int input, string res)
        {
            Assert.AreEqual(_currencyStringGenerator.GetHundredThousandsString(input), res);
        }

        [Test]
        [TestCase(0, "")]
        [TestCase(4, "four thousand")]
        [TestCase(5, "five thousand")]
        [TestCase(6, "six thousand")]
        [TestCase(11, "eleven thousand")]
        [TestCase(34, "thirty four thousand")]
        public void GetThousandsStringReturnsCorrectString(int input, string res)
        {
            Assert.AreEqual(_currencyStringGenerator.GetThousandsString(input), res);
        }

        [Test]
        [TestCase(0, "")]
        [TestCase(7, "seven hundred")]
        [TestCase(8, "eight hundred")]
        [TestCase(9, "nine hundred")]
        [TestCase(12, "twelve hundred")]
        public void GetHundredStringReturnsCorrectString(int input, string res)
        {
            Assert.AreEqual(_currencyStringGenerator.GetHundredsString(input), res);
        }

        [Test]
        [TestCase(0, 0, "")]
        [TestCase(1, 3, "thirteen")]
        [TestCase(1, 4, "fourteen")]
        [TestCase(1, 5, "fifteen")]
        [TestCase(1, 6, "sixteen")]
        [TestCase(1, 7, "seventeen")]
        [TestCase(1, 8, "eighteen")]
        [TestCase(1, 9, "nineteen")]
        [TestCase(2, 0, "twenty")]
        [TestCase(3, 0, "thirty")]
        [TestCase(4, 0, "forty")]
        [TestCase(5, 0, "fifty")]
        [TestCase(6, 0, "sixty")]
        [TestCase(7, 0, "seventy")]
        [TestCase(8, 0, "eighty")]
        [TestCase(9, 0, "ninety")]
        [TestCase(2, 1, "twenty one")]
        [TestCase(3, 2, "thirty two")]
        [TestCase(4, 3, "forty three")]
        [TestCase(5, 4, "fifty four")]
        [TestCase(6, 5, "sixty five")]
        [TestCase(7, 6, "seventy six")]
        [TestCase(8, 7, "eighty seven")]
        [TestCase(9, 8, "ninety eight")]
        [TestCase(9, 9, "ninety nine")]
        public void GetTensAndOnesReturnsCorrectString(int tens, int ones, string res)
        {
            Assert.AreEqual(_currencyStringGenerator.GetTensAndOnesString(tens, ones), res);
        }

        [Test]
        public void GetDollarStringReturnsEmptyForZeroValue()
        {
            var expectedString = "";
            var res = _currencyStringGenerator.GetDollarString(new SplitCurrencyNumberMetadata()).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetDollarStringReturnsCorrectStringForSplitNumberWithTensAndOnes()
        {
            var expectedString = "ONE HUNDRED AND TWENTY THREE DOLLARS";
            var res = _currencyStringGenerator.GetDollarString(new SplitCurrencyNumberMetadata
            {
                Hundreds = 1,
                Tens = 2,
                Ones = 3,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetDollarStringReturnsCorrectStringForSplitNumberWithOnes()
        {
            var expectedString = "ONE HUNDRED AND THREE DOLLARS";
            var res = _currencyStringGenerator.GetDollarString(new SplitCurrencyNumberMetadata
            {
                Hundreds = 1,
                Tens = 0,
                Ones = 3,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetDollarStringReturnsCorrectStringForSplitNumberWithTens()
        {
            var expectedString = "ONE HUNDRED AND TWENTY DOLLARS";
            var res = _currencyStringGenerator.GetDollarString(new SplitCurrencyNumberMetadata
            {
                Hundreds = 1,
                Tens = 2,
                Ones = 0,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetCentsStringReturnsCorrectStringForSplitNumberWithTensAndOnes()
        {
            var expectedString = "FORTY SIX CENTS";
            var res = _currencyStringGenerator.GetCentsString(new SplitCurrencyNumberMetadata
            {
                CentTens = 4,
                CentOnes = 6,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetCentsStringReturnsCorrectStringForSplitNumberWithOnes()
        {
            var expectedString = "FORTY CENTS";
            var res = _currencyStringGenerator.GetCentsString(new SplitCurrencyNumberMetadata
            {
                CentTens = 4,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetCentsStringReturnsCorrectStringForSplitNumberWithTens()
        {
            var expectedString = "SIX CENTS";
            var res = _currencyStringGenerator.GetCentsString(new SplitCurrencyNumberMetadata
            {
                CentOnes = 6,
            }).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GetCentsStringReturnsEmptyForZeroCents()
        {
            var expectedString = "";
            var res = _currencyStringGenerator.GetCentsString(new SplitCurrencyNumberMetadata()).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GenerateNumberStringReturnZeroOnZero()
        {
            Assert.AreEqual("zero dollars", _currencyStringGenerator.GenerateNumberString(0));
        }

        [Test]
        public void GenerateNumberStringErrorsWhenLessThenZero()
        {
            Assert.Throws<ArgumentException>(() =>_currencyStringGenerator.GenerateNumberString(-1));
        }

        [Test]
        public void GenerateNumberStringReturnsCorrectValue()
        {
            var expectedString = "ONE HUNDRED AND TWENTY THREE DOLLARS AND FORTY SIX CENTS";

            var value = 123.56m;

            _numberComponentGenerator.SpitNumberIntoComponentParts(value).Returns(new SplitCurrencyNumberMetadata
            {
                Hundreds = 1,
                Tens = 2,
                Ones = 3,
                CentTens = 4,
                CentOnes = 6
            });

            var res = _currencyStringGenerator.GenerateNumberString(value).ToUpper();
            Assert.AreEqual(expectedString, res);
        }

        [Test]
        public void GenerateNumberStringReturnsCorrectValueWithoutHundred()
        {
            var expectedString = "TWENTY THREE DOLLARS AND FORTY SIX CENTS";

            var value = 123.56m;

            _numberComponentGenerator.SpitNumberIntoComponentParts(value).Returns(new SplitCurrencyNumberMetadata
            {
                Tens = 2,
                Ones = 3,
                CentTens = 4,
                CentOnes = 6
            });

            var res = _currencyStringGenerator.GenerateNumberString(value).ToUpper();
            Assert.AreEqual(expectedString, res);
        }
    }
}