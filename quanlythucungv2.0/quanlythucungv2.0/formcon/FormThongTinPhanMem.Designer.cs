namespace quanlythucungv2._0.formcon
{
    partial class FormThongTinPhanMem
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
            this.lblLienHe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLienHe
            // 
            this.lblLienHe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLienHe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLienHe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLienHe.Location = new System.Drawing.Point(0, 0);
            this.lblLienHe.Name = "lblLienHe";
            this.lblLienHe.Size = new System.Drawing.Size(1006, 603);
            this.lblLienHe.TabIndex = 2;
            this.lblLienHe.Text = "label1";
            // 
            // FormThongTinPhanMem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 603);
            this.Controls.Add(this.lblLienHe);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormThongTinPhanMem";
            this.Text = "FormThongTinPhanMem";
            this.Load += new System.EventHandler(this.FormThongTinPhanMem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLienHe;
    }
}