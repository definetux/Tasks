using DependencyInjection.Interfaces;

namespace DependencyInjection.Concrete
{
    public class CalculationEngine : ICalculationEngine
    {
        private readonly ICalculator _calculator;

        // Constructor Injection
        public CalculationEngine(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public void Set(double value)
        {
            _calculator.SetInitialValue(value);
        }

        public double Add(double value)
        {
            return _calculator.AddValue(value);
        }

        public double Sub(double value)
        {
            return _calculator.SubValue(value);
        }

        public double Multiply(double value)
        {
            return _calculator.MultuplyValue(value);
        }

        public double Divide(double value)
        {
            return _calculator.DivideValue(value);
        }
    }
}