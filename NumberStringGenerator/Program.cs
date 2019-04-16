using NumberStringGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator
{
    public class Program
    {
        private readonly IInputHandler _inputHandler;
        private readonly ICurrencyStringGenerator _currencyStringGenerator;
        private readonly IConsoleWrapper _consoleWrapper;
        public Program(IInputHandler inputHandler, ICurrencyStringGenerator currencyStringGenerator, IConsoleWrapper consoleWrapper)
        {
            _inputHandler = inputHandler;
            _currencyStringGenerator = currencyStringGenerator;
            _consoleWrapper = consoleWrapper;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    _consoleWrapper.WriteLine("Please enter a decimal number greater then or equal to zero");
                    var input = _inputHandler.GetDecimalFromUser();
                    _consoleWrapper.WriteLine(_currencyStringGenerator.GenerateNumberString(input));
                }
                catch(Exception e)
                {
                    _consoleWrapper.WriteLine(e.Message);
                }
            }
        }
    }
}
