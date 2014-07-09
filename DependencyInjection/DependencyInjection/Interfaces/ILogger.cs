namespace DependencyInjection.Interfaces
{
    /// <summary>
    /// Механизм вывода должен поддерживать единый интерфейс вывода строки
    /// </summary>
    public interface ILogger
    {
        void ShowMessage(string message);
    }
}