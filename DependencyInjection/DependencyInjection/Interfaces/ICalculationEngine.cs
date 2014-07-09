namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Нехватило воображения для методов движка,
    /// поэтому он просто выступает в роли адаптера ( и повторяет методы интерфейса ICalculator)
    /// </summary>
    public interface ICalculationEngine
    {
        void Set(double value);

        double Add(double value);

        double Sub(double value);

        double Multiply(double value);

        double Divide(double value);
    }
}