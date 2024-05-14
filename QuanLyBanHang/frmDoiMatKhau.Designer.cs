namespace QuanLyBanHang
{
    partial class frmDoiMatKhau
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
            this.btn_luu_thanhdat = new System.Windows.Forms.Button();
            this.tb_matkhau_thanhdat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_luu_thanhdat
            // 
            this.btn_luu_thanhdat.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu_thanhdat.Location = new System.Drawing.Point(194, 134);
            this.btn_luu_thanhdat.Name = "btn_luu_thanhdat";
            this.btn_luu_thanhdat.Size = new System.Drawing.Size(104, 44);
            this.btn_luu_thanhdat.TabIndex = 8;
            this.btn_luu_thanhdat.Text = "Lưu";
            this.btn_luu_thanhdat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_luu_thanhdat.UseVisualStyleBackColor = true;
            this.btn_luu_thanhdat.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // tb_matkhau_thanhdat
            // 
            this.tb_matkhau_thanhdat.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_matkhau_thanhdat.Location = new System.Drawing.Point(209, 57);
            this.tb_matkhau_thanhdat.Margin = new System.Windows.Forms.Padding(2);
            this.tb_matkhau_thanhdat.Name = "tb_matkhau_thanhdat";
            this.tb_matkhau_thanhdat.Size = new System.Drawing.Size(216, 38);
            this.tb_matkhau_thanhdat.TabIndex = 7;
            this.tb_matkhau_thanhdat.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mật khẩu mới:";
            // 
            // frmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 190);
            this.Controls.Add(this.btn_luu_thanhdat);
            this.Controls.Add(this.tb_matkhau_thanhdat);
            this.Controls.Add(this.label1);
            this.Name = "frmDoiMatKhau";
            this.Text = "frmDoiMatKhau";
            this.Load += new System.EventHandler(this.frmDoiMatKhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_luu_thanhdat;
        private System.Windows.Forms.TextBox tb_matkhau_thanhdat;
        private System.Windows.Forms.Label label1;
    }
}