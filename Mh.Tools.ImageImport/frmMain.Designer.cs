namespace Mh.Tools.ImageImport
{
    partial class frmMain
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
            this.btnInspect = new System.Windows.Forms.Button();
            this.txtImageDirectory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnInspect
            // 
            this.btnInspect.Location = new System.Drawing.Point(713, 12);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(75, 23);
            this.btnInspect.TabIndex = 0;
            this.btnInspect.Text = "Inspect";
            this.btnInspect.UseVisualStyleBackColor = true;
            this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
            // 
            // txtImageDirectory
            // 
            this.txtImageDirectory.Location = new System.Drawing.Point(12, 12);
            this.txtImageDirectory.Name = "txtImageDirectory";
            this.txtImageDirectory.Size = new System.Drawing.Size(695, 20);
            this.txtImageDirectory.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtImageDirectory);
            this.Controls.Add(this.btnInspect);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.TextBox txtImageDirectory;
    }
}