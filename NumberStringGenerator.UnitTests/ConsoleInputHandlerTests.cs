using AutofacContrib.NSubstitute;
using NSubstitute;
using NumberStringGenerator.Services;
using NUnit.Framework;
using System;

namespace NumberStringGenerator.UnitTests
{
    public class ConsoleInputHandlerTests
    {
        private IConsoleWrapper _consoleWrapper;
        private IInputHandler _inputHandler;

        [SetUp]
        public void Setup()
        {
            _consoleWrapper = Substitute.For<IConsoleWrapper>();
            var autoSubstitute = new AutoSubstitute();
            autoSubstitute.Provide(_consoleWrapper);

            _inputHandler = autoSubstitute.Resolve<ConsoleInputHandler>();
        }

        [Test]
        [TestCase("0")]
        [TestCase("123")]
        [TestCase("456.78")]
        public void ValidateReturnsTrueWithNumeriString(string inputValue)
        {
            Assert.IsTrue(_inputHandler.Validate(inputValue));  
        }

        [Test]
        [TestCase("123a")]
        [TestCase("")]
        [TestCase("asdfv")]
        [TestCase("a23")]
        [TestCase("1a3")]
        public void ValidateReturnsFalseWithNonNumeriString(string inputValue)
        {
            Assert.IsFalse(_inputHandler.Validate(inputValue));
        }

        [Test]
        [TestCase("-1")]
        public void ValidateReturnsFalseWith0String(string inputValue)
        {
            Assert.IsFalse(_inputHandler.Validate(inputValue));
        }

        [Test]
        [TestCase("1000000")]
        [TestCase("2000000")]
        public void ValidateReturnsFalseWithnumberGreaterOrEqualThen1000000(string inputValue)
        {
            Assert.IsFalse(_inputHandler.Validate(inputValue));
        }

        [Test]
        [TestCase("123")]
        [TestCase("456.78")]
        public void GetNumericStringDoesNotThrowWithNumericString(string input)
        {
            _consoleWrapper.ReadLine().Returns(input);
            Assert.DoesNotThrow(() => { _inputHandler.GetDecimalFromUser(); });
        }


        [Test]
        [TestCase("123a")]
        [TestCase("asdfv")]
        [TestCase("a23")]
        [TestCase("1a3")]
        [TestCase("2000000")]
        public void GetNumericStringThrowsWithNonValidInput(string input)
        {
            _consoleWrapper.ReadLine().Returns(input);
            Assert.Throws<Exception>(code:() => { _inputHandler.GetDecimalFromUser(); }, message:"Not A a Valid Input, please only use numeric characters and '.'");
        }
    }
}