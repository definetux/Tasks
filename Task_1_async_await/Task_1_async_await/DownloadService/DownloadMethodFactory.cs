namespace Task_1_async_await.DownloadService
{
    public class DownloadMethodFactory
    {
        /// <summary>
        /// Типы алгоритмов асинхронной загрузки файлов
        /// </summary>
        public enum DownloadMethodType
        {
            UseAsyncAwait,
            UseThread,
            UseDownloadDataAsync
        }

        public static IDownloadMethod CreateMethod(DownloadMethodType type)
        {
            switch (type)
            {
                case DownloadMethodType.UseAsyncAwait:
                    return new DownloadAsyncAwait();
                case DownloadMethodType.UseThread:
                    return new DownloadThread();
                case DownloadMethodType.UseDownloadDataAsync:
                    return new DownloadWebClientAsync();
                default:
                    return null;
            }
        }
    }
}