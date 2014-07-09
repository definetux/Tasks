using System;
using DependencyInjection.Interfaces;

namespace DependencyInjection.Concrete
{
    public class Logger : ILogger
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine( message );
        }
    }
}