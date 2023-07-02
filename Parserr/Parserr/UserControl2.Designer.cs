namespace Parserr
{
    partial class userControl2
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
            treeView1 = new TreeView();
            checkBox1 = new CheckBox();
            label2 = new Label();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.CheckBoxes = true;
            treeView1.Location = new Point(467, 45);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(398, 312);
            treeView1.TabIndex = 0;
            treeView1.AfterCheck += treeView1_AfterCheck;
            treeView1.AfterSelect += treeView1_AfterCheck;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(32, 81);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(49, 23);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Все";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 45);
            label2.Name = "label2";
            label2.Size = new Size(421, 15);
            label2.TabIndex = 15;
            label2.Text = "Виберіть, що конкретно ви хочете завантажити нижче, або в меню справа.";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox2.Location = new Point(32, 110);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(73, 23);
            checkBox2.TabIndex = 16;
            checkBox2.Text = "Оцінки";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox3.Location = new Point(32, 139);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(164, 23);
            checkBox3.TabIndex = 17;
            checkBox3.Text = "Навчальний матеріал";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // userControl2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(label2);
            Controls.Add(checkBox1);
            Controls.Add(treeView1);
            Name = "userControl2";
            Size = new Size(900, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private CheckBox checkBox1;
        private Label label2;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}
