using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmMain : Form
    {
        private string quyen, tentaikhoan;
        public frmMain(string tentaikhoan, string quyenUser)
        {
            InitializeComponent();
            this.tentaikhoan = tentaikhoan;
            this.quyen = quyenUser;
        }

        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect(); //Mở kết nối
            switch (this.quyen)
            {
                case "nhan_vien":
                    mnuLoaiDichVu.Visible = true;
                    mnuKhachHang.Visible = true;
                    mnuHoaDonDichVu.Visible = true;
                    tìmKiếmHóaĐơnToolStripMenuItem.Visible = true;
                    mnuNhanVien.Visible = false;
                    mnuDichVu.Visible = false;
                    break;
                case "admin":
                    mnuNhanVien.Visible = true;
                    mnuDichVu.Visible = true;
                    mnuLoaiDichVu.Visible = true;
                    mnuKhachHang.Visible = true;
                    mnuHoaDonDichVu.Visible = true;
                    tìmKiếmHóaĐơnToolStripMenuItem.Visible = true;
                    break;
            }
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            Application.Exit(); //Thoát
        }

        private void mnuFile_Click(object sender, EventArgs e)
        {

        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {

            frmDMKhachHang frmKhachHang = new frmDMKhachHang(); //Khởi tạo đối tượng
            frmKhachHang.ShowDialog(); //Hiển thị
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmDMNhanVien frmNhanVien = new frmDMNhanVien(); //Khởi tạo đối tượng
            frmNhanVien.ShowDialog(); //Hiển thị
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất tài khoản không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                frmDangNhap f = new frmDangNhap();
                this.Hide();
                f.Show();
            }
        }

        private void mnuDoiMatKhau_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau f = new frmDoiMatKhau(this.tentaikhoan);
            f.ShowDialog();
        }

        private void mnuTroGiup_Click(object sender, EventArgs e)
        {
            frmTroGiup frmTroGiup = new frmTroGiup(); //Khởi tạo đối tượng
            frmTroGiup.ShowDialog(); //Hiển thị
        }

        private void mnuLoaiDichVu_Click(object sender, EventArgs e)
        {
            frmDMLoaiDichVu frmLoaiDichVu = new frmDMLoaiDichVu(); //Khởi tạo đối tượng
            frmLoaiDichVu.ShowDialog(); //Hiển thị
        }

        private void mnuDichVu_Click(object sender, EventArgs e)
        {
            frmDMDichVu frmDichVu = new frmDMDichVu(); //Khởi tạo đối tượng
            frmDichVu.ShowDialog(); //Hiển thị
        }

        private void mnuHoaDonDichVu_Click(object sender, EventArgs e)
        {
            frmHoaDonDichVu frmHoaDonDichVu = new frmHoaDonDichVu(); //Khởi tạo đối tượng
            frmHoaDonDichVu.ShowDialog(); //Hiển thị
        }

        private void tìmKiếmHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemHD frmTimKiemHD = new frmTimKiemHD(); //Khởi tạo đối tượng
            frmTimKiemHD.ShowDialog(); //Hiển thị
        }
    }
}
