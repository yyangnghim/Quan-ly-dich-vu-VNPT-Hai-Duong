using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmDoiMatKhau : Form
    {
        private string tentaikhoan;
        public frmDoiMatKhau (string tentaikhoan)
        {
            InitializeComponent();
            this.tentaikhoan = tentaikhoan;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            //Kiểm tra nếu ô textbox không trống dữ liệu
            if (tb_matkhau_thanhdat.Text.Trim().Length > 0)
            {
                string sql = "UPDATE nguoi_dung SET mat_khau = '" + tb_matkhau_thanhdat.Text + "' WHERE ten_dang_nhap = '" + this.tentaikhoan + "'";
                Class.Functions.CheckKey(sql);
                MessageBox.Show("Thay đổi mật khẩu thành công");
                this.Close();
            }
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
