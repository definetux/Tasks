using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Task_1_async_await.DownloadService
{
    /// <summary>
    /// Алгоритм асинхронной загрузки файлов с использованием класса Thread
    /// Данные читаются небольшими порциями
    /// </summary>
    public class DownloadThread:IDownloadMethod
    {
        private HttpWebRequest _webRequest;

        private Stream _strResponse;

        private Stream _strLocal;

        private string _url;

        private string _destination;

        public void Download(string url, string dst)
        {
            _url = url;
            _destination = dst;

            // В новом потоке запускаем метод загрузки файла
            new Thread(Download).Start();
        }

        private void Download()
        {
            using (var wcDownload = new WebClient())
            {
                // При загрузке и записи могут возникнуть исключительные ситуации
                try
                {
                    // Запрос к серверу
                    _webRequest = WebRequest.Create(_url) as HttpWebRequest;

                    // Настройка аутентификации
                    if (_webRequest != null) 
                        _webRequest.Credentials = CredentialCache.DefaultCredentials;

                    // Открываем поток для чтения данных с интернет-ресурса
                    _strResponse = wcDownload.OpenRead(_url);

                    // Открываем поток для записи данных на диск
                    _strLocal = new FileStream(_destination, FileMode.Create, FileAccess.Write, FileShare.None);

                    int bytesSize = 0;
                    var downBuffer = new byte[2048];

                    // Читаем данные из интернета в буфер по 2 КБ,
                    // чтобы не грузить оперативную память, затем буфер записываем в файл
                    while (_strResponse != null && (bytesSize = _strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        _strLocal.Write(downBuffer, 0, bytesSize);
                    }
                    MessageBox.Show("Download Complete.");
                }
                catch( Exception ex)
                {
                    MessageBox.Show("Download failed. Error:" + ex.Message);
                }
                finally
                {
                    // Закрываем открытые потоки, если они существуют
                    if (_strResponse != null) 
                        _strResponse.Close();

                    if (_strLocal != null) 
                        _strLocal.Close();
                }
            }
        }

    }
}