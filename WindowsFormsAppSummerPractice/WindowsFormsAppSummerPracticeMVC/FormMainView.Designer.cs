namespace WindowsFormsAppSummerPracticeMVC
{
    partial class FormMainView
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelURL = new System.Windows.Forms.Label();
            this.buttonGetStatistics = new System.Windows.Forms.Button();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelURL
            // 
            this.labelURL.AutoSize = true;
            this.labelURL.Location = new System.Drawing.Point(12, 37);
            this.labelURL.Name = "labelURL";
            this.labelURL.Size = new System.Drawing.Size(32, 13);
            this.labelURL.TabIndex = 0;
            this.labelURL.Text = "URL:";
            // 
            // buttonGetStatistics
            // 
            this.buttonGetStatistics.Location = new System.Drawing.Point(150, 100);
            this.buttonGetStatistics.Name = "buttonGetStatistics";
            this.buttonGetStatistics.Size = new System.Drawing.Size(125, 25);
            this.buttonGetStatistics.TabIndex = 1;
            this.buttonGetStatistics.Text = "Get Statistics";
            this.buttonGetStatistics.UseVisualStyleBackColor = true;
            this.buttonGetStatistics.Click += new System.EventHandler(this.ButtonGetStatistics_ClickAsync);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(60, 34);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(350, 20);
            this.textBoxURL.TabIndex = 2;
            // 
            // FormMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 161);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonGetStatistics);
            this.Controls.Add(this.labelURL);
            this.Name = "FormMainView";
            this.Text = "Main form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.Button buttonGetStatistics;
        private System.Windows.Forms.TextBox textBoxURL;
    }
}