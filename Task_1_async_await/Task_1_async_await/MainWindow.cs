using System;
using System.Windows.Forms;
using Task_1_async_await.DownloadService;

namespace Task_1_async_await
{
    public partial class FrmDownloader : Form
    {
        // Объявляем загрузчик файлов
        private readonly Downloader _downloader;

        public FrmDownloader()
        {
            InitializeComponent();
            _downloader = new Downloader();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // В зависимости от выбранной радиокнопки, определяется тип метода загрузки (Стратегия)
            DownloadMethodFactory.DownloadMethodType type; 

            if (rbAsyncAwait.Checked)
            {
                type = DownloadMethodFactory.DownloadMethodType.UseAsyncAwait;
            }
            else if (rbThread.Checked)
            {
                type = DownloadMethodFactory.DownloadMethodType.UseThread;
            }
            else
            {
                type = DownloadMethodFactory.DownloadMethodType.UseDownloadDataAsync;
            }

            // Простая фабрика создает нужный экземпляр метода загрузки
            _downloader.SetDownloadMethod(DownloadMethodFactory.CreateMethod(type));

            // Пользователю предлогается выбрать файл для сохранения данных
            var sfDialog = new SaveFileDialog
                            {
                                RestoreDirectory = true, 
                                Filter = @"All Files (*.*)|*.*"
                            };

            if (sfDialog.ShowDialog() == DialogResult.OK)
            {
                var url = comboBox1.Text;
                _downloader.TryDownload(url, sfDialog.FileName);
            }
        }
    }
}
