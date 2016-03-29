namespace TransferUnit3Pos
{
    partial class ConfigPage
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
            this.configUserPage1 = new TransferUnit3Pos.ConfigUserPage();
            this.SuspendLayout();
            // 
            // configUserPage1
            // 
            this.configUserPage1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.configUserPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configUserPage1.Location = new System.Drawing.Point(3, 2);
            this.configUserPage1.Name = "configUserPage1";
            this.configUserPage1.Size = new System.Drawing.Size(303, 358);
            this.configUserPage1.TabIndex = 0;
            this.configUserPage1.Load += new System.EventHandler(this.configUserPage1_Load);
            // 
            // ConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(309, 363);
            this.Controls.Add(this.configUserPage1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigPage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigPage_FormClosing);
            this.Load += new System.EventHandler(this.ConfigPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ConfigUserPage configUserPage1;
    }
}