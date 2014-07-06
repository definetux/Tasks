namespace Task_1_async_await
{
    partial class FrmDownloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.gbKinds = new System.Windows.Forms.GroupBox();
            this.rbDownloadAsync = new System.Windows.Forms.RadioButton();
            this.rbThread = new System.Windows.Forms.RadioButton();
            this.rbAsyncAwait = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gbKinds.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "http://www.ucl.ac.uk/news/news-articles/1213/muscle-fibres-heart.jpg",
            "http://mcgovern.mit.edu/news/wp-content/uploads/2013/08/image7LR.jpg",
            "http://www.ac-grenoble.fr/ien.vienne1-2/spip/IMG/bmp_Image001.bmp"});
            this.comboBox1.Location = new System.Drawing.Point(12, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(307, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 68);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(307, 47);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // gbKinds
            // 
            this.gbKinds.Controls.Add(this.rbDownloadAsync);
            this.gbKinds.Controls.Add(this.rbThread);
            this.gbKinds.Controls.Add(this.rbAsyncAwait);
            this.gbKinds.Location = new System.Drawing.Point(337, 41);
            this.gbKinds.Name = "gbKinds";
            this.gbKinds.Size = new System.Drawing.Size(200, 100);
            this.gbKinds.TabIndex = 2;
            this.gbKinds.TabStop = false;
            this.gbKinds.Text = "Kinds of download";
            // 
            // rbDownloadAsync
            // 
            this.rbDownloadAsync.AutoSize = true;
            this.rbDownloadAsync.Location = new System.Drawing.Point(7, 68);
            this.rbDownloadAsync.Name = "rbDownloadAsync";
            this.rbDownloadAsync.Size = new System.Drawing.Size(176, 17);
            this.rbDownloadAsync.TabIndex = 2;
            this.rbDownloadAsync.TabStop = true;
            this.rbDownloadAsync.Text = "Use WebClient.DownloadAsync";
            this.rbDownloadAsync.UseVisualStyleBackColor = true;
            // 
            // rbThread
            // 
            this.rbThread.AutoSize = true;
            this.rbThread.Location = new System.Drawing.Point(7, 44);
            this.rbThread.Name = "rbThread";
            this.rbThread.Size = new System.Drawing.Size(169, 17);
            this.rbThread.TabIndex = 1;
            this.rbThread.TabStop = true;
            this.rbThread.Text = "Use System.Threading.Thread";
            this.rbThread.UseVisualStyleBackColor = true;
            // 
            // rbAsyncAwait
            // 
            this.rbAsyncAwait.AutoSize = true;
            this.rbAsyncAwait.Checked = true;
            this.rbAsyncAwait.Location = new System.Drawing.Point(7, 20);
            this.rbAsyncAwait.Name = "rbAsyncAwait";
            this.rbAsyncAwait.Size = new System.Drawing.Size(105, 17);
            this.rbAsyncAwait.TabIndex = 0;
            this.rbAsyncAwait.TabStop = true;
            this.rbAsyncAwait.Text = "Use async/await";
            this.rbAsyncAwait.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter the URL:";
            // 
            // FrmDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 164);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbKinds);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmDownloader";
            this.Text = "Downloader";
            this.gbKinds.ResumeLayout(false);
            this.gbKinds.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.GroupBox gbKinds;
        private System.Windows.Forms.RadioButton rbDownloadAsync;
        private System.Windows.Forms.RadioButton rbThread;
        private System.Windows.Forms.RadioButton rbAsyncAwait;
        private System.Windows.Forms.Label label1;
    }
}

