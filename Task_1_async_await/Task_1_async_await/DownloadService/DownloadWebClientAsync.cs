using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Task_1_async_await.DownloadService
{
    /// <summary>
    /// Алгоритм асинхронной загрузки файлов с использованием метода DownloadDataAsync
    /// </summary>
    public class DownloadWebClientAsync : IDownloadMethod
    {
        public void Download(string url, string dst)
        {
            using (var wcDownload = new WebClient())
            {
                // При загрузке и сохранении на диск могут возникнуть исключительные ситуации
                try
                {
                    // Подписываемся на событие окончания загрузки,
                    // записываем полученный массив байт в файл
                    wcDownload.DownloadDataCompleted += (s, eArgs) =>
                    {
                        File.WriteAllBytes(dst, eArgs.Result);
                        MessageBox.Show("Download completed!");
                    };

                    // Выполняем асинхронную загрузку данных
                    wcDownload.DownloadDataAsync(new Uri(url));
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Download failed:" + exception.Message);
                }
            }
        }
    }
}