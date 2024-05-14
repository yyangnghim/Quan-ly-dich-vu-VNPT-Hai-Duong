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
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanHang
{
    public partial class frmHoaDonDichVu : Form
    {
        DataTable tblCTHDB;
        public frmHoaDonDichVu()
        {
            InitializeComponent();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.MaDichVu, b.TenDichVu, a.SoLuong, b.DonGia, b.DVT,a.ThanhTien FROM tblChiTietHDDichVu AS a, tblDichVu AS b WHERE a.MaHDDichVu = N'" + txtMaHDDichVu.Text + "' AND a.MaDichVu=b.MaDichVu";
            tblCTHDB = Functions.GetDataToTable(sql);
            dgvHoaDonDichVu.DataSource = tblCTHDB;
            dgvHoaDonDichVu.Columns[0].HeaderText = "Mã dịch vụ";
            dgvHoaDonDichVu.Columns[1].HeaderText = "Tên dịch vụ";
            dgvHoaDonDichVu.Columns[2].HeaderText = "Số lượng";
            dgvHoaDonDichVu.Columns[3].HeaderText = "Đơn giá";
            dgvHoaDonDichVu.Columns[4].HeaderText = "Đơn vị tính";
            dgvHoaDonDichVu.Columns[5].HeaderText = "Thành tiền";
            dgvHoaDonDichVu.Columns[0].Width = 80;
            dgvHoaDonDichVu.Columns[1].Width = 130;
            dgvHoaDonDichVu.Columns[2].Width = 80;
            dgvHoaDonDichVu.Columns[3].Width = 90;
            dgvHoaDonDichVu.Columns[4].Width = 90;
            dgvHoaDonDichVu.Columns[5].Width = 90;
            dgvHoaDonDichVu.AllowUserToAddRows = false;
            dgvHoaDonDichVu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NgayLapDat FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
            dtpNgayLapDat.Value = DateTime.Parse(Functions.GetFieldValues(str));
            str = "SELECT MaNV FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
            cboMaNhanVien.SelectedValue = Functions.GetFieldValues(str);
            str = "SELECT MaKH FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
            cboMaKhach.SelectedValue = Functions.GetFieldValues(str);
            str = "SELECT TongTien FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(Double.Parse(txtTongTien.Text));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHDDichVu.Text = Functions.CreateKey("HDB");
            LoadDataGridView();
        }
        private void ResetValues()
        {
            txtMaHDDichVu.Text = "";
            dtpNgayLapDat.Value = DateTime.Now;
            cboMaNhanVien.Text = "";
            cboMaKhach.Text = "";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: ";
            cboMaDichVu.Text = "";
            txtSoLuong.Text = "";
            txtDonViTinh.Text = "";
            txtThanhTien.Text = "0";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLconn , tong, Tongmoi;
            sql = "SELECT MaHDDichVu FROM tblHDDichVu WHERE MaHDDichVu=N'" + txtMaHDDichVu.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDDichVu được sinh tự động do đó không có trường hợp trùng khóa
               /* if (txtNgayLapDat.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNgayLapDat.Focus();
                    return;
                }*/
                if (cboMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }
                if (cboMaKhach.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhach.Focus();
                    return;
                }
                sql = "INSERT INTO tblHDDichVu(MaHDDichVu, NgayLapDat, MaNV, MaKH, TongTien) VALUES (N'" + txtMaHDDichVu.Text.Trim() + "','" +
                        dtpNgayLapDat.Value + "',N'" + cboMaNhanVien.SelectedValue + "',N'" +
                        cboMaKhach.SelectedValue + "'," + txtTongTien.Text + ")";
                Functions.RunSqlDel(sql);
            }
            // Lưu thông tin của các mặt dịch vụ
            if (cboMaDichVu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaDichVu.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            
            sql = "SELECT MaDichVu FROM tblChiTietHDDichVu WHERE MaDichVu=N'" + cboMaDichVu.SelectedValue + "' AND MaHDDichVu = N'" + txtMaHDDichVu.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã dịch vụ này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesDichVu();
                cboMaDichVu.Focus();
                return;
            }
            // Kiểm tra xem số lượng dịch vụ trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblDichVu WHERE MaDichVu = N'" + cboMaDichVu.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt dịch vụ này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO tblChiTietHDDichVu(MaHDDichVu,MaDichVu,SoLuong,DonGia,ThanhTien) VALUES(N'" + txtMaHDDichVu.Text.Trim() + "',N'" + cboMaDichVu.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + ","  + txtThanhTien.Text + ")";
            Functions.RunSqlDel(sql);
            LoadDataGridView();
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tblHDDichVu SET TongTien =" + Tongmoi + " WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
            Functions.RunSqlDel(sql);
            txtTongTien.Text = Tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(Double.Parse(Tongmoi.ToString()));
            ResetValuesDichVu();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnInHoaDon.Enabled = true;
        }
        private void ResetValuesDichVu()
        {
            cboMaDichVu.Text = "";
            txtSoLuong.Text = "";
            txtDonViTinh.Text = "";
            txtThanhTien.Text = "0";
        }
        private void cboMaKhach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaKhach.Text == "")
            {
                txtTenKhach.Text = "";
                txtDiaChi.Text = "";
                txtPhuong.Text = "";
                txtThanhPho.Text = "";
            }
            //Khi chọn Mã khách dịch vụ thì các thông tin của khách dịch vụ sẽ hiện ra
            str = "Select TenKH from tblKhach where MaKH = N'" + cboMaKhach.SelectedValue + "'";
            txtTenKhach.Text = Functions.GetFieldValues(str);
            str = "Select DiaChi from tblKhach where MaKH = N'" + cboMaKhach.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select Phuong from tblKhach where MaKH= N'" + cboMaKhach.SelectedValue + "'";
            txtPhuong.Text = Functions.GetFieldValues(str);
            txtThanhPho.Text = Functions.GetFieldValues(str);
            str = "Select ThanhPho from tblKhach where MaKH= N'" + cboMaKhach.SelectedValue + "'";
            txtThanhPho.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
    
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();
        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhanVien.Text == "")
            {
                txtTenNhanVien.Text = "";
            }
            str = "SELECT TenNV FROM tblNhanvien WHERE MaNV =N'" + cboMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);
        }

        private void cboMaHDBan_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHDDichVu FROM tblHDDichVu", cboMaHDDichVu, "MaHDDichVu", "MaHDDichVu");
            cboMaHDDichVu.SelectedIndex = -1;
        }

        private void btnTimKiem(object sender, EventArgs e)
        {
            if (cboMaHDDichVu.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHDDichVu.Focus();
                return;
            }
            txtMaHDDichVu.Text = cboMaHDDichVu.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = true;
            cboMaHDDichVu.SelectedIndex = -1;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int DichVu = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinDichVu;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "VNPT TP HẢI DƯƠNG";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "TP HẢI DƯƠNG";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "HOTLINE 24/7: 0949 75 2468";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN DỊCH VỤ";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDDichVu, a.NgayLapDat, a.TongTien, b.TenKH, b.Diachi, b.Phuong,b.ThanhPho, c.TenNV FROM tblHDDichVu AS a, tblKhach AS b, tblNhanvien AS c WHERE a.MaHDDichVu = N'" + txtMaHDDichVu.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Phường:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt dịch vụ
            sql = "SELECT b.TenDichVu, a.SoLuong, b.DonGia,b.DVT, a.ThanhTien " +
                  "FROM tblChiTietHDDichVu AS a , tblDichVu AS b WHERE a.MaHDDichVu = N'" +
                  txtMaHDDichVu.Text + "' AND a.MaDichVu = b.MaDichVu";
            tblThongtinDichVu = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên dịch vụ";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "DVT";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (DichVu = 0; DichVu < tblThongtinDichVu.Rows.Count; DichVu++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][DichVu + 12] = DichVu + 1;
                for (cot = 0; cot < tblThongtinDichVu.Columns.Count; cot++)
                //Điền thông tin dịch vụ từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][DichVu + 12] = tblThongtinDichVu.Rows[DichVu][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][DichVu + 12] = tblThongtinDichVu.Rows[DichVu][cot].ToString() + ".";
                }
            }
            exRange = exSheet.Cells[cot][DichVu + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][DichVu + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][DichVu + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(Double.Parse(tblThongtinHD.Rows[0][2].ToString()));
            exRange = exSheet.Cells[4][DichVu + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][7];
            exSheet.Name = "Hóa đơn dịch vụ";
            exApp.Visible = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slconn, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaDichVu,SoLuong FROM tblChiTietHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
                DataTable tblDichVu = Functions.GetDataToTable(sql);
                for (int DichVu = 0; DichVu <= tblDichVu.Rows.Count - 1; DichVu++)
                {
                    // Cập nhật lại số lượng cho các mặt dịch vụ
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblDichVu WHERE MaDichVu = N'" + tblDichVu.Rows[DichVu][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblDichVu.Rows[DichVu][1].ToString());
                    slconn = sl + slxoa;
                    sql = "UPDATE tblDichVu SET SoLuong =" + slconn + " WHERE MaDichVu= N'" + tblDichVu.Rows[DichVu][0].ToString() + "'";
                    Functions.RunSqlDel(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE tblChiTietHDDichVu WHERE MaHDDichVu=N'" + txtMaHDDichVu.Text + "'";
                Functions.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE tblHDDichVu WHERE MaHDDichVu=N'" + txtMaHDDichVu.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnInHoaDon.Enabled = false;
            }
        }

        private void cboMaDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaDichVu.Text == "")
            {
                txtTenDichVu.Text = "";
                txtDonGia.Text = "";
                txtDonViTinh.Text = "";
            }
            // Khi chọn mã dịch vụ thì các thông tin về dịch vụ hiện ra
            str = "SELECT TenDichVu FROM tblDichVu WHERE MaDichVu =N'" + cboMaDichVu.SelectedValue + "'";
            txtTenDichVu.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGia FROM tblDichVu WHERE MaDichVu =N'" + cboMaDichVu.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
            str = "SELECT DVT FROM tblDichVu WHERE MaDichVu =N'" + cboMaDichVu.SelectedValue + "'";
            txtDonViTinh.Text = Functions.GetFieldValues(str);
        }

        private void dgvHoaDonDichVu_DoubleClick(object sender, EventArgs e)
        {
            string MaDichVuxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slconn, tong, tongmoi;
            if (tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa dịch vụ và cập nhật lại số lượng dịch vụ 
                MaDichVuxoa = dgvHoaDonDichVu.CurrentRow.Cells["MaDichVu"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvHoaDonDichVu.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvHoaDonDichVu.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE tblChiTietHDDichVu WHERE MaHDDichVu=N'" + txtMaHDDichVu.Text + "' AND MaDichVu = N'" + MaDichVuxoa + "'";
                Functions.RunSqlDel(sql);
                // Cập nhật lại số lượng cho các mặt dịch vụ
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblDichVu WHERE MaDichVu = N'" + MaDichVuxoa + "'"));
                slconn = sl + SoLuongxoa;
                sql = "UPDATE tblDichVu SET SoLuong =" + slconn + " WHERE MaDichVu= N'" + MaDichVuxoa + "'";
                Functions.RunSqlDel(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblHDDichVu WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE tblHDDichVu SET TongTien =" + tongmoi + " WHERE MaHDDichVu = N'" + txtMaHDDichVu.Text + "'";
                Functions.RunSqlDel(sql);
                txtTongTien.Text = tongmoi.ToString();
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(tongmoi);
                LoadDataGridView();
            }
        }

        private void frmHoaDonDichVu_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnInHoaDon.Enabled = false;
            txtMaHDDichVu.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtTenKhach.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtPhuong.ReadOnly = true;
            txtThanhPho.ReadOnly = true;
            txtTenDichVu.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtDonViTinh.ReadOnly = true;
            txtTongTien.Text = "0";
            Functions.FillCombo("SELECT MaKH, TenKH FROM tblKhach", cboMaKhach, "MaKH", "MaKH");
            cboMaKhach.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaNV, TenNV FROM tblNhanVien", cboMaNhanVien, "MaNV", "MaNV");
            cboMaNhanVien.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaDichVu, TenDichVu FROM tblDichVu", cboMaDichVu, "MaDichVu", "MaDichVu");
            cboMaDichVu.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHDDichVu.Text != "")
            {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;
                btnInHoaDon.Enabled = true;
            }
            LoadDataGridView();
        }
    }
}
