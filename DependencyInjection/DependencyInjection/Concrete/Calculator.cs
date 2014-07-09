using System;
using DependencyInjection.Interfaces;

namespace DependencyInjection.Concrete
{
    public class Calculator : ICalculator
    {
        private readonly ILogger _logger;

        private double _currentNumber;

        // Constructor Injection
        public Calculator(ILogger logger)
        {
            _logger = logger;
            _currentNumber = 0;
        }


        public void SetInitialValue(double value)
        {
            _currentNumber = value;
            
            _logger.ShowMessage(string.Format("Current value is {0}", _currentNumber));
        }

        public double AddValue(double value)
        {
            var result = _currentNumber + value;
            _logger.ShowMessage(string.Format("{0} + {1} = {2}", _currentNumber, value, result));
            _currentNumber = result;
            return result;
        }

        public double SubValue(double value)
        {
            var result = _currentNumber - value;
            _logger.ShowMessage(string.Format("{0} - {1} = {2}", _currentNumber, value, result));
            _currentNumber = result;
            return result;
        }

        public double MultuplyValue(double value)
        {
            var result = _currentNumber * value;
            _logger.ShowMessage(string.Format("{0} * {1} = {2}", _currentNumber, value, result));
            _currentNumber = result;
            return result;
        }

        public double DivideValue(double value)
        {
            // При делении на ноль должно возникать исключение,
            // но для double .NET возвращает результат infinity
            try
            {
                var result = _currentNumber / value;
                _logger.ShowMessage(string.Format("{0} / {1} = {2}", _currentNumber, value, result));
                _currentNumber = result;
                return result;
            }
            catch (DivideByZeroException ex)
            {
                _logger.ShowMessage("Error: divide by zero");
                return double.Epsilon;
            }
        }
    }
}