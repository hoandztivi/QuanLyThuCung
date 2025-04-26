using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Sunny.UI.Win32;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace quanlythucungv2._0.formcon
{
    public partial class FormQuanLyDonHang : Form
    {
        private Database db;
        private string QuyenTruyCap;
        public FormQuanLyDonHang(string quyen)
        {
            InitializeComponent();
            db = new Database();
            QuyenTruyCap = quyen;
        }

        private void FormQuanLyDonHang_Load(object sender, EventArgs e)
        {
            if (QuyenTruyCap != "quanly")
            {
                btnXoa.Visible = false;
            }
            LoadData();
        }

        private void LoadData(string tuKhoa = "")
        {
            try
            {
                var lstPara = new List<CustomParameter>
                {
                    new CustomParameter { key = "@tukhoa", value = tuKhoa }
                };

                var dt = db.SelectData("sp_TimKiemDonHang", lstPara);
                dgvDonHang.DataSource = dt;

                // Gán tên hiển thị cột (HeaderText)
                dgvDonHang.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
                dgvDonHang.Columns["MaKhachHang"].HeaderText = "Mã Khách Hàng";
                dgvDonHang.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                dgvDonHang.Columns["TenThuCungMua"].HeaderText = "Thú Cưng Đã Mua";
                dgvDonHang.Columns["NgayDatHang"].HeaderText = "Ngày Mua";
                dgvDonHang.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (dgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xóa.");
                return;
            }

            var maDon = dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value.ToString();

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa đơn hàng này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                var lstPara = new List<CustomParameter>
                {
                    new CustomParameter { key = "@MaDonHang", value = maDon }
                };

                var kq = db.ExeCute("sp_XoaDonHang", lstPara);
                if (kq == 1)
                {
                    MessageBox.Show("Xóa đơn hàng thành công.");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.");
                }
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để in hóa đơn.");
                return;
            }

            // Lấy thông tin từ dòng được chọn
            string maDon = dgvDonHang.SelectedRows[0].Cells["MaDonHang"].Value?.ToString();
            string tenKhach = dgvDonHang.SelectedRows[0].Cells["TenKhachHang"].Value?.ToString();
            string TenThuCungMua = dgvDonHang.SelectedRows[0].Cells["TenThuCungMua"].Value?.ToString();
            string NgayDatHang = dgvDonHang.SelectedRows[0].Cells["NgayDatHang"].Value?.ToString();
            string tongTien = dgvDonHang.SelectedRows[0].Cells["TongTien"].Value?.ToString();

            try
            {
                Excel.Application excelApp = new Excel.Application();
                if (excelApp == null)
                {
                    MessageBox.Show("Không thể khởi tạo Excel.");
                    return;
                }

                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "HoaDon";

                // Tiêu đề
                Excel.Range title = worksheet.get_Range("A1", "E1");
                title.Merge();
                title.Value2 = "HÓA ĐƠN MUA THÚ CƯNG";
                title.Font.Bold = true;
                title.Font.Size = 16;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Nội dung hóa đơn
                worksheet.Cells[3, 1] = "Mã đơn hàng:";
                worksheet.Cells[3, 2] = maDon;

                worksheet.Cells[4, 1] = "Tên khách hàng:";
                worksheet.Cells[4, 2] = tenKhach;

                worksheet.Cells[5, 1] = "Thú cưng đã mua:";
                worksheet.Cells[5, 2] = TenThuCungMua;

                worksheet.Cells[6, 1] = "Ngày mua:";
                worksheet.Cells[6, 2] = NgayDatHang;

                worksheet.Cells[7, 1] = "Tổng tiền:";
                worksheet.Cells[7, 2] = tongTien + " VND";

                // Tự động dãn cột cho vừa nội dung
                worksheet.Columns.AutoFit();

                // Hiển thị hộp thoại lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Mày lưu cái chó gì";
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.FileName = $"HoaDon_{maDon}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("In hóa đơn thành công!");
                }

                // Đóng và giải phóng tài nguyên
                workbook.Close(false);
                excelApp.Quit();

                // Giải phóng COM
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất hóa đơn: " + ex.Message);
            }
        }
    }
}
