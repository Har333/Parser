namespace Parserr
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            checkedListBox1 = new CheckedListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 45);
            label1.Name = "label1";
            label1.Size = new Size(366, 75);
            label1.TabIndex = 14;
            label1.Text = "Спочатку завантажте список курсів (це займе близько 30 секунд)\r\n\r\nДалі виберіть які з них ви хочете завантажити чи проаналізувати \r\n\r\nПісля цього нажміть \"Продовжити далі\"";
            // 
            // checkedListBox1
            // 
            checkedListBox1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(467, 45);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(398, 312);
            checkedListBox1.TabIndex = 13;
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(checkedListBox1);
            Name = "UserControl1";
            Size = new Size(900, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private CheckedListBox checkedListBox1;
    }
}
