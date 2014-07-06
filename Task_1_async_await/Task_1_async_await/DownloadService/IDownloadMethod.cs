namespace Task_1_async_await.DownloadService
{
    /// <summary>
    /// Интерфейс для алгоритмов асинхронной загрузки
    /// </summary>
    public interface IDownloadMethod
    {
        void Download(string url, string dst);
    }
}