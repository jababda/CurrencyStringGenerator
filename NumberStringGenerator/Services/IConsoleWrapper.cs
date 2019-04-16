using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringGenerator.Services
{
    public interface IConsoleWrapper
    {
        string ReadLine();
        void WriteLine(string line);
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }


}
