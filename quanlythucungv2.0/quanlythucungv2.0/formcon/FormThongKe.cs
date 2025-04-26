using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace quanlythucungv2._0.formcon
{
    public partial class FormThongKe: Form
    {
        Database db = new Database();
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            LoadThongKe();
        }
        private void LoadThongKe(string tuKhoa = "")
        {
            dgvThongKe.DataSource = null;

            var lstPara = new List<CustomParameter>()
    {
        new CustomParameter { key = "@TuKhoa", value = tuKhoa }
    };

            var dt = db.SelectData("sp_TimThongKe", lstPara);
            if (dt != null)
            {
                dgvThongKe.DataSource = dt;

                dgvThongKe.Columns["MaThongKe"].HeaderText = "Mã Thống Kê";
                dgvThongKe.Columns["Loai"].HeaderText = "Loại";
                dgvThongKe.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                dgvThongKe.Columns["TenDichVu"].HeaderText = "Dịch Vụ";
                dgvThongKe.Columns["TenThuCung"].HeaderText = "Thú Cưng";
                dgvThongKe.Columns["NgayThucHien"].HeaderText = "Ngày Thực Hiện";
                dgvThongKe.Columns["Gia"].HeaderText = "Giá Tiền";
                dgvThongKe.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var tuKhoa = txtTimKiem.Text.Trim();
            LoadThongKe(tuKhoa);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var kq = db.ExeCute("sp_XoaThongKeQua30Ngay");
            if (kq > 0)
            {
                MessageBox.Show($"Đã xóa {kq} thống kê quá 30 ngày.");
                LoadThongKe();
            }
            else if (kq == 0)
            {
                MessageBox.Show("Không có thống kê nào quá 30 ngày để xóa.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa.");
            }
        }

        private void btnxuatfile_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Không thể mở Excel. Vui lòng kiểm tra Office!");
                    return;
                }

                Excel.Workbook wb = xlApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                ws.Name = "ThongKe";

                // Header
                for (int i = 0; i < dgvThongKe.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1] = dgvThongKe.Columns[i].HeaderText;
                }

                Excel.Range headerRange = ws.Range[ws.Cells[1, 1], ws.Cells[1, dgvThongKe.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Dữ liệu
                for (int i = 0; i < dgvThongKe.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvThongKe.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] = dgvThongKe.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Tự động căn giữa dữ liệu
                Excel.Range allData = ws.Range[ws.Cells[2, 1], ws.Cells[dgvThongKe.Rows.Count + 1, dgvThongKe.Columns.Count]];
                allData.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Viền toàn bảng
                Excel.Range usedRange = ws.UsedRange;
                usedRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                usedRange.Columns.AutoFit();

                // Lưu file
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Mày lưu cái chó gì";
                save.Filter = "Excel File|*.xlsx";
                save.FileName = "ThongKe.xlsx";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    wb.SaveAs(save.FileName);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                wb.Close(false);
                xlApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message);
            }
        }
    }
}
