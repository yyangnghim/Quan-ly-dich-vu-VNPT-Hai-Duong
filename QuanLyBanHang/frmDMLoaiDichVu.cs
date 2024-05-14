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
    public partial class frmDMLoaiDichVu : Form
    {
        DataTable tblLDV;
        public frmDMLoaiDichVu()
        {
            InitializeComponent();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaLoaiDichVu, TenLoaiDichVu FROM tblLoaiDichVu";
            tblLDV = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvLoaiDichVu.DataSource = tblLDV; //Nguồn dữ liệu            
            dgvLoaiDichVu.Columns[0].HeaderText = "Mã loại dịch vụ";
            dgvLoaiDichVu.Columns[1].HeaderText = "Tên loại dịch vụ";
            dgvLoaiDichVu.Columns[0].Width = 100;
            dgvLoaiDichVu.Columns[1].Width = 300;
            dgvLoaiDichVu.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvLoaiDichVu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaLoaiDichVu.Enabled = true; //cho phép nhập mới
            txtMaLoaiDichVu.Focus();
        }

        private void ResetValue()
        {
            txtMaLoaiDichVu.Text = "";
            txtMaLoaiDichVu.Text = "";
        }

        private void frmDMLoaiHang_Load_1(object sender, EventArgs e)
        {
            txtMaLoaiDichVu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblLDV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiDichVu.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblLoaiDichVu WHERE MaLoaiDichVu=N'" + txtMaLoaiDichVu.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaLoaiDichVu.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiDichVu.Focus();
                return;
            }
            if (txtTenLoaiDichVu.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên loại dịch vụ ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoaiDichVu.Focus();
                return;
            }
            sql = "Select MaLoaiDichVu From tblLoaiDichVu where MaLoaiDichVu=N'" + txtMaLoaiDichVu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại dịch vụ này đã có, bạn phải nhập loại khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiDichVu.Focus();
                return;
            }

            sql = "INSERT INTO tblLoaiDichVu VALUES(N'" +
            txtMaLoaiDichVu.Text + "',N'" + txtTenLoaiDichVu.Text + "')";
            Functions.RunSqlDel(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaLoaiDichVu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
                string sql; //Lưu câu lệnh sql
                if (tblLDV.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtMaLoaiDichVu.Text == "") //nếu chưa chọn bản ghi nào
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTenLoaiDichVu.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
                {
                    MessageBox.Show("Bạn chưa nhập tên loại dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                sql = "UPDATE tblLoaiDichVu SET TenLoaiDichVu=N'" +
                    txtTenLoaiDichVu.Text.ToString() +
                    "' WHERE MaLoaiDichVu=N'" + txtMaLoaiDichVu.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();

                btnBoQua.Enabled = false;
            
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaLoaiDichVu.Enabled = false;
        }

        private void dgvLoaiDichVu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiDichVu.Focus();
                return;
            }
            if (tblLDV.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaLoaiDichVu.Text = dgvLoaiDichVu.CurrentRow.Cells["MaLoaiDichVu"].Value.ToString();
            txtTenLoaiDichVu.Text = dgvLoaiDichVu.CurrentRow.Cells["TenLoaiDichVu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
        private void frmDMLoaiDichVu_Load(object sender, EventArgs e)
        {
            txtMaLoaiDichVu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
    } 
}

