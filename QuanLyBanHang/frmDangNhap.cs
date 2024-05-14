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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            tb_username.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tb_username.Focus();
        }

        private void cb_matkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_matkhau.Checked)
            {
                tb_password.UseSystemPasswordChar = false;
            }
            else
            {
                tb_password.UseSystemPasswordChar = true;
            }
        }

        private bool kiemTraDuLieu()
        {
            if (tb_username.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (tb_password.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

       

        private void btn_dangnhap_Click_1(object sender, EventArgs e)
        {
            if (kiemTraDuLieu())
            {
                string ten_dang_nhap = tb_username.Text;
                string mat_khau = tb_password.Text;
                if (Class.Functions.kiemTraDangNhap(ten_dang_nhap, mat_khau))
                {
                      string quyenUser =Class.Functions.getRecord("SELECT loai_nguoi_dung FROM nguoi_dung WHERE ten_dang_nhap = '" + ten_dang_nhap + "'").ToString();
                    frmMain f = new frmMain(ten_dang_nhap,quyenUser);
                    this.Hide();
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tên đăng nhập hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void cb_matkhau_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cb_matkhau.Checked)
            {
                tb_password.UseSystemPasswordChar = false;
            }
            else
            {
                tb_password.UseSystemPasswordChar = true;
            }
        }
    }
}
