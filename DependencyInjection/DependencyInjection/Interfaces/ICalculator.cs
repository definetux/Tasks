namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Калькулятор должен выполнять основные операции (установить начальное значение,
    /// добавить число, вычесть число, умножить на число, разделить на число
    /// </summary>
    public interface ICalculator
    {
        void SetInitialValue(double value);

        double AddValue(double value);

        double SubValue(double value);

        double MultuplyValue(double value);

        double DivideValue(double value);
    }
}