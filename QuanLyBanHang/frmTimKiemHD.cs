using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyBanHang.Class;

namespace QuanLyBanHang
{
    public partial class frmTimKiemHD : Form
    {
        DataTable tblHDDV;
        public frmTimKiemHD()
        {
            InitializeComponent();
        }

        private void frmTimKiemHD_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvDanhSachHD.DataSource = null;
        }
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHoaDon.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHoaDon.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNhanVien.Text == "") && (txtMaKhachHang.Text == "") &&
               (txtTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblHDDichVu WHERE 1=1";
            if (txtMaHoaDon.Text != "")
                sql = sql + " AND MaHDDichVu Like N'%" + txtMaHoaDon.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(NgayLapDat) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(NgayLapDat) =" + txtNam.Text;
            if (txtMaNhanVien.Text != "")
                sql = sql + " AND MaNV Like N'%" + txtMaNhanVien.Text + "%'";
            if (txtMaKhachHang.Text != "")
                sql = sql + " AND MaKH Like N'%" + txtMaHoaDon.Text + "%'";
            if (txtTongTien.Text != "")
                sql = sql + " AND TongTien <=" + txtTongTien.Text;
            tblHDDV = Functions.GetDataToTable(sql);
            if (tblHDDV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblHDDV.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDanhSachHD.DataSource = tblHDDV;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvDanhSachHD.Columns[0].HeaderText = "Mã HĐB";
            dgvDanhSachHD.Columns[1].HeaderText = "Mã nhân viên";
            dgvDanhSachHD.Columns[2].HeaderText = "Ngày bán";
            dgvDanhSachHD.Columns[3].HeaderText = "Mã khách";
            dgvDanhSachHD.Columns[4].HeaderText = "Tổng tiền";
            dgvDanhSachHD.Columns[0].Width = 150;
            dgvDanhSachHD.Columns[1].Width = 100;
            dgvDanhSachHD.Columns[2].Width = 80;
            dgvDanhSachHD.Columns[3].Width = 80;
            dgvDanhSachHD.Columns[4].Width = 80;
            dgvDanhSachHD.AllowUserToAddRows = false;
            dgvDanhSachHD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvDanhSachHD.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dgvDanhSachHD_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dgvDanhSachHD.CurrentRow.Cells["MaHDDichVu"].Value.ToString();
                frmHoaDonDichVu frm = new frmHoaDonDichVu();
                frm.txtMaHDDichVu.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }
    }
}
