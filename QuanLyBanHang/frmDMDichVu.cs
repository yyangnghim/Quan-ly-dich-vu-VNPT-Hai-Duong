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
    public partial class frmDMDichVu : Form
    {
        DataTable tblDV;
        public frmDMDichVu()
        {
            InitializeComponent();
        }
        private void LoadDataGridView()
        {
            dgvDichVu.DataSource = Functions.GetDataToTable("SELECT * FROM tblDichVu");
        }
        private void ResetValues()
        {
            txtMaDichVu.Text = "";
            txtTenDichVu.Text = "";
            cbxLoaiDichVu.Text = "";
            txtDVT.Text = "";
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGia.Enabled = false;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaDichVu.Enabled = true;
            txtMaDichVu.Focus();
            txtDonGia.Enabled = true;
            txtSoLuong.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDichVu.Focus();
                return;
            }
            if (txtTenDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDichVu.Focus();
                return;
            }
            if (cbxLoaiDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxLoaiDichVu.Focus();
                return;
            }
            if (txtDVT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn vị tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDVT.Focus();
                return;
            }

            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return;
            }
            sql = "SELECT MaDichVu FROM tblDichVu WHERE MaDichVu=N'" + txtMaDichVu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã dịch vụ này đã tồn tại, bạn phải chọn mã dịch vụ khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDichVu.Focus();
                return;
            }
            string query = "INSERT INTO tblDichVu VALUES ('" + txtMaDichVu.Text + "',N'" + cbxLoaiDichVu.SelectedItem.ToString() + "',N'" + txtTenDichVu.Text + "',N'" + txtDVT.Text + "'," + txtSoLuong.Text + "," + txtDonGia.Text + ",N'" + txtAnh.Text + "',N'" + txtGhiChu.Text + "')";
            Functions.RunSqlDel(query);
            LoadDataGridView();
            //ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaDichVu.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaDichVu,LoaiDichVu,TenDichVu,DVT,SoLuong,DonGia,Anh,GhiChu FROM tblDichVu";
            tblDV = Functions.GetDataToTable(sql);
            dgvDichVu.DataSource = tblDV;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaDichVu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDichVu.Focus();
                return;
            }
            if (txtTenDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDichVu.Focus();
                return;
            }
            if (cbxLoaiDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbxLoaiDichVu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải ảnh minh hoạ cho dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnh.Focus();
                return;
            }
            sql = "UPDATE tblDichVu SET TenDichVu=N'" + txtTenDichVu.Text.Trim().ToString() +
                "',LoaiDichVu=N'" + cbxLoaiDichVu.SelectedItem.ToString() +
                "',DVT = N'"+txtDVT.Text+ "', SoLuong= " + txtSoLuong.Text + ", DonGia = " + txtDonGia.Text+", Anh='" + txtAnh.Text + "',GhiChu=N'" + txtGhiChu.Text + "' WHERE MaDichVu=N'" + txtMaDichVu.Text + "'";
            Functions.RunSqlDel(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaDichVu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblDichVu WHERE MaDichVu=N'" + txtMaDichVu.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaDichVu.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaDichVu.Text == "") && (txtTenDichVu.Text == "") && (cbxLoaiDichVu.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tblDichVu WHERE 1=1";
            if (txtMaDichVu.Text != "")
                sql += " AND MaDichVu LIKE N'%" + txtMaDichVu.Text + "%'";
            if (txtTenDichVu.Text != "")
                sql += " AND TenDichVu LIKE N'%" + txtTenDichVu.Text + "%'";
            if (cbxLoaiDichVu.Text != "")
                sql += " AND MaDichVu LIKE N'%" + cbxLoaiDichVu.SelectedValue + "%'";
            tblDV = Functions.GetDataToTable(sql);
            if (tblDV.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblDV.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvDichVu.DataSource = tblDV;
            ResetValues();
        }

        private void frmDMDichVu_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblLoaiDichVu";
            DataTable dt = Functions.GetDataToTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                cbxLoaiDichVu.Items.Add(row["TenLoaiDichVu"].ToString());
            }
            LoadDataGridView();
        }

        private void dgvDichVu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow viewRow = dgvDichVu.Rows[rowIndex];
            txtMaDichVu.Text = viewRow.Cells[0].Value.ToString();
            cbxLoaiDichVu.Text = viewRow.Cells[1].Value.ToString();
            txtTenDichVu.Text = viewRow.Cells[2].Value.ToString();
            txtDVT.Text = viewRow.Cells[3].Value.ToString();
            txtSoLuong.Text = viewRow.Cells[4].Value.ToString();
            txtDonGia.Text = viewRow.Cells[5].Value.ToString();
            txtAnh.Text = viewRow.Cells[6].Value.ToString();
            txtGhiChu.Text = viewRow.Cells[7].Value.ToString();
        }
    }
}
