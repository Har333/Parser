namespace Parserr
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userControl1 = new UserControl1();
            userControl2 = new userControl2();
            panel1 = new Panel();
            btnDownload = new Button();
            btnDownloadFiles = new Button();
            btnNextBack = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            saveFileDialog1 = new SaveFileDialog();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // userControl1
            // 
            userControl1.Location = new Point(0, 0);
            userControl1.Name = "userControl1";
            userControl1.Size = new Size(886, 370);
            userControl1.TabIndex = 0;
            // 
            // userControl2
            // 
            userControl2.Location = new Point(0, 0);
            userControl2.Name = "userControl2";
            userControl2.Size = new Size(886, 370);
            userControl2.TabIndex = 1;
            userControl2.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnDownload);
            panel1.Controls.Add(btnDownloadFiles);
            panel1.Controls.Add(btnNextBack);
            panel1.Location = new Point(0, 376);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 73);
            panel1.TabIndex = 2;
            // 
            // btnDownload
            // 
            btnDownload.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnDownload.Location = new Point(533, 14);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(274, 46);
            btnDownload.TabIndex = 16;
            btnDownload.Text = "Завантажити список курсів";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click_1;
            // 
            // btnDownloadFiles
            // 
            btnDownloadFiles.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnDownloadFiles.Location = new Point(533, 14);
            btnDownloadFiles.Name = "btnDownloadFiles";
            btnDownloadFiles.Size = new Size(274, 46);
            btnDownloadFiles.TabIndex = 18;
            btnDownloadFiles.Text = "Завантажити курси";
            btnDownloadFiles.UseVisualStyleBackColor = true;
            btnDownloadFiles.Visible = false;
            btnDownloadFiles.Click += btnDownloadFiles_Click;
            // 
            // btnNextBack
            // 
            btnNextBack.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btnNextBack.Location = new Point(79, 14);
            btnNextBack.Name = "btnNextBack";
            btnNextBack.Size = new Size(274, 46);
            btnNextBack.TabIndex = 17;
            btnNextBack.Text = "Продовжити далі";
            btnNextBack.UseVisualStyleBackColor = true;
            btnNextBack.Click += btnNextBack_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 461);
            Controls.Add(panel1);
            Controls.Add(userControl2);
            Controls.Add(userControl1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private UserControl1 userControl1;
        private userControl2 userControl2;
        private Panel panel1;
        private Button btnNextBack;
        private Button btnDownload;
        private Button btnDownloadFiles;
        private FolderBrowserDialog folderBrowserDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}