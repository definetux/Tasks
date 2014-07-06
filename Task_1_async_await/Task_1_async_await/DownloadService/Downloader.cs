namespace Task_1_async_await.DownloadService
{
    public class Downloader
    {
        private IDownloadMethod _downloadMethod;

        public void SetDownloadMethod(IDownloadMethod downloadMethod)
        {
            _downloadMethod = downloadMethod;
        }

        public void TryDownload(string url, string destinationPath )
        {
            _downloadMethod.Download(url, destinationPath);
        }
    }
}