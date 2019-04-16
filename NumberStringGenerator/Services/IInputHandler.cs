using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator.Services
{
    public interface IInputHandler
    {
        decimal GetDecimalFromUser();

        bool Validate(string inputString);
    }

    public class ConsoleInputHandler : IInputHandler
    {
        private readonly IConsoleWrapper _consoleWrapper;

        public ConsoleInputHandler(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public decimal GetDecimalFromUser()
        {
            var inputString = _consoleWrapper.ReadLine();

            if(!Validate(inputString)) throw new Exception("Not A a Valid Input, please only use numeric characters and '.', and non negative and less then 1000000");

            return decimal.Parse(inputString);
        }

        public bool Validate(string inputString)
        {
            return decimal.TryParse(inputString, out var number) ? number >= 0 && number < 1000000 : false;
        }
    }
}
