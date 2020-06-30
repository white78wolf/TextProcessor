namespace TextProcessor
{
    partial class FormPatternToReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatternToReplace));
            this.textPattern = new System.Windows.Forms.TextBox();
            this.textReplace = new System.Windows.Forms.TextBox();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.labelPattern = new System.Windows.Forms.Label();
            this.labelReplace = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textPattern
            // 
            this.textPattern.Location = new System.Drawing.Point(24, 27);
            this.textPattern.Name = "textPattern";
            this.textPattern.Size = new System.Drawing.Size(182, 20);
            this.textPattern.TabIndex = 0;            
            this.textPattern.Leave += new System.EventHandler(this.TextPattern_Leave);
            // 
            // textReplace
            // 
            this.textReplace.Location = new System.Drawing.Point(23, 79);
            this.textReplace.Name = "textReplace";
            this.textReplace.Size = new System.Drawing.Size(183, 20);
            this.textReplace.TabIndex = 1;
            // 
            // buttonReplace
            // 
            this.buttonReplace.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReplace.Location = new System.Drawing.Point(227, 39);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(75, 37);
            this.buttonReplace.TabIndex = 2;
            this.buttonReplace.Text = "Заменить";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.ButtonReplace_Click);
            // 
            // labelPattern
            // 
            this.labelPattern.AutoSize = true;
            this.labelPattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPattern.Location = new System.Drawing.Point(24, 11);
            this.labelPattern.Name = "labelPattern";
            this.labelPattern.Size = new System.Drawing.Size(43, 16);
            this.labelPattern.TabIndex = 3;
            this.labelPattern.Text = "Найти";
            // 
            // labelReplace
            // 
            this.labelReplace.AutoSize = true;
            this.labelReplace.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelReplace.Location = new System.Drawing.Point(24, 60);
            this.labelReplace.Name = "labelReplace";
            this.labelReplace.Size = new System.Drawing.Size(82, 16);
            this.labelReplace.TabIndex = 4;
            this.labelReplace.Text = "Заменить на";
            // 
            // FormPatternToReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 121);
            this.Controls.Add(this.labelReplace);
            this.Controls.Add(this.labelPattern);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.textReplace);
            this.Controls.Add(this.textPattern);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPatternToReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Найти и заменить";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPattern;
        private System.Windows.Forms.TextBox textReplace;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Label labelPattern;
        private System.Windows.Forms.Label labelReplace;
    }
}