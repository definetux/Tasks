using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1_async_await.DownloadService
{
    /// <summary>
    /// Алгоритм асинхронной загрузки файлов с использованием ключевых слов async/await 
    /// </summary>
    public class DownloadAsyncAwait:IDownloadMethod
    {
        public async void Download(string url, string dst)
        {
            await Task.Run(() =>
            {
                using (var wcDownload = new WebClient())
                {
                    // При загрузке и сохранении на диск могут возникнуть исключительные ситуации
                    try
                    {
                        // Загрузка массива данных с интернет-ресурса
                        var data = wcDownload.DownloadData(new Uri(url));
                        // Сохранение данных на диск
                        File.WriteAllBytes(dst, data);

                        MessageBox.Show("Download Completed!");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Download failed:" + exception.Message);
                    }
                }
            });
        }
    }
}