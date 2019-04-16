using AutofacContrib.NSubstitute;
using NumberStringGenerator.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator.UnitTests
{
    class NumberComponentGeneratorTests
    {

        private INumberComponentGenerator _numberComponentGenerator;

        [SetUp]
        public void Setup()
        {
            var autoSubstitute = new AutoSubstitute();
            _numberComponentGenerator = autoSubstitute.Resolve<NumberComponentGenerator>();
        }

        [Test]
        [TestCase(12345, 0)]
        [TestCase(100000, 1)]
        [TestCase(200000, 2)]
        [TestCase(636489, 6)]
        [TestCase(912547, 9)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsHundredThousands(decimal input, int hundredThousandComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(hundredThousandComponent, split.HundredThousands);
        }

        [Test]
        [TestCase(100, 0)]
        [TestCase(2000, 2)]
        [TestCase(1000, 1)]
        [TestCase(12345, 12)]
        [TestCase(13487, 13)]
        [TestCase(29876, 29)]
        [TestCase(50876, 50)]
        [TestCase(999547, 99)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsThousands(decimal input, int thousandComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(thousandComponent, split.Thousands);
        }

        [Test]
        [TestCase(10, 0)]
        [TestCase(100, 1)]
        [TestCase(200, 2)]
        [TestCase(12345, 3)]
        [TestCase(100100, 1)]
        [TestCase(200900, 9)]
        [TestCase(636489, 4)]
        [TestCase(912047, 0)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsHundreds(decimal input, int hundredsComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(hundredsComponent, split.Hundreds);
        }

        [Test]
        [TestCase(5, 0)]
        [TestCase(12345, 4)]
        [TestCase(100000, 0)]
        [TestCase(200090, 9)]
        [TestCase(636489, 8)]
        [TestCase(912567, 6)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsTens(decimal input, int tensComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(tensComponent, split.Tens);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(12345, 5)]
        [TestCase(100000, 0)]
        [TestCase(200009, 9)]
        [TestCase(636487, 7)]
        [TestCase(912546, 6)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsOnes(decimal input, int onesComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(onesComponent, split.Ones);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0.15, 1)]
        [TestCase(0.6, 6)]
        [TestCase(636487.36, 3)]
        [TestCase(200009.398, 4)]
        [TestCase(636487.99, 9)]
        [TestCase(912546.365, 3)]
        [TestCase(912546.364, 3)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsCentTens(decimal input, int centTensComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(centTensComponent, split.CentTens);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(0.15, 5)]
        [TestCase(636487.36, 6)]
        [TestCase(200009.398, 0)]
        [TestCase(636487.99, 9)]
        [TestCase(912546.365, 6)]
        [TestCase(912546.375, 8)]
        [TestCase(912546.364, 6)]
        [TestCase(636487.1, 0)]
        public void SpitNumberIntoComponentPartsCorrectlySplitsCentOnes(decimal input, int centOnesComponent)
        {
            var split = _numberComponentGenerator.SpitNumberIntoComponentParts(input);
            Assert.AreEqual(centOnesComponent, split.CentOnes);
        }
    }
}
