namespace WinGameOfLife
{
    partial class Introduction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Introduction));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBegin = new System.Windows.Forms.Button();
            this.checkBoxNoIntro = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 276);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // buttonBegin
            // 
            this.buttonBegin.Location = new System.Drawing.Point(270, 390);
            this.buttonBegin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonBegin.Name = "buttonBegin";
            this.buttonBegin.Size = new System.Drawing.Size(116, 44);
            this.buttonBegin.TabIndex = 1;
            this.buttonBegin.Text = "Begin";
            this.buttonBegin.UseVisualStyleBackColor = true;
            this.buttonBegin.Click += new System.EventHandler(this.ButtonBegin_Click);
            // 
            // checkBoxNoIntro
            // 
            this.checkBoxNoIntro.AutoSize = true;
            this.checkBoxNoIntro.Location = new System.Drawing.Point(24, 458);
            this.checkBoxNoIntro.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxNoIntro.Name = "checkBoxNoIntro";
            this.checkBoxNoIntro.Size = new System.Drawing.Size(330, 29);
            this.checkBoxNoIntro.TabIndex = 2;
            this.checkBoxNoIntro.Text = "Stop showing introduction at startup";
            this.checkBoxNoIntro.UseVisualStyleBackColor = true;
            this.checkBoxNoIntro.CheckedChanged += new System.EventHandler(this.CheckBoxNoIntro_CheckedChanged);
            // 
            // Introduction
            // 
            this.AcceptButton = this.buttonBegin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 502);
            this.Controls.Add(this.checkBoxNoIntro);
            this.Controls.Add(this.buttonBegin);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Introduction";
            this.Text = "Conway\'s Game of Life";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBegin;
        private System.Windows.Forms.CheckBox checkBoxNoIntro;
    }
}