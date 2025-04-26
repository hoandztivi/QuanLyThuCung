using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythucungv2._0.formcon
{
    public partial class FormLichHen: Form
    {
        Database db = new Database();
        public FormLichHen()
        {
            InitializeComponent();
        }

        private void FormLichHen_Load(object sender, EventArgs e)
        {
            LoadLichHen();
        }
        private void LoadLichHen(string tukhoa = "")
        {
            List<CustomParameter> lstPara = new List<CustomParameter>();
            lstPara.Add(new CustomParameter()
            {
                key = "@tukhoa",
                value = tukhoa
            });

            var dt = db.SelectData("sp_TimLichHen", lstPara);
            dgvLichHen.DataSource = dt;

            dgvLichHen.Columns["MaLichHen"].HeaderText = "Mã Lịch Hẹn";
            dgvLichHen.Columns["MaKhachHang"].HeaderText = "Mã KH";
            dgvLichHen.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvLichHen.Columns["SoDienThoai"].HeaderText = "Số ĐT";
            dgvLichHen.Columns["MaDichVu"].HeaderText = "Mã DV";
            dgvLichHen.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvLichHen.Columns["NgayHen"].HeaderText = "Ngày Hẹn";
            dgvLichHen.Columns["GhiChu"].HeaderText = "Ghi Chú";
            dgvLichHen.Columns["Gia"].HeaderText = "Giá";

            dgvLichHen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tukhoa = txtTuKhoa.Text.Trim();

            var lstPara = new List<CustomParameter>()
    {
        new CustomParameter() { key = "@tukhoa", value = tukhoa }
    };

            dgvLichHen.DataSource = db.SelectData("sp_TimLichHen", lstPara);
        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (dgvLichHen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lịch hẹn để hoàn thành!");
                return;
            }

            // Lấy dòng được chọn
            var row = dgvLichHen.SelectedRows[0];
            string maLichHen = row.Cells["MaLichHen"].Value.ToString();
            string maKhachHang = row.Cells["MaKhachHang"].Value.ToString();
            string maDichVu = row.Cells["MaDichVu"].Value.ToString();
            string ngayHen = Convert.ToDateTime(row.Cells["NgayHen"].Value).ToString("yyyy-MM-dd");
            string ghiChu = row.Cells["GhiChu"].Value.ToString();
            decimal gia = Convert.ToDecimal(row.Cells["Gia"].Value);

            // 1. Thêm vào bảng ThongKe
            var parametersInsert = new List<CustomParameter>
    {
        new CustomParameter { key = "@Loai", value = "Hoàn thành lịch hẹn" },
        new CustomParameter { key = "@MaKhachHang", value = maKhachHang },
        new CustomParameter { key = "@MaDichVu", value = maDichVu },
        new CustomParameter { key = "@Ngay", value = ngayHen },
        new CustomParameter { key = "@GhiChu", value = ghiChu },
        new CustomParameter { key = "@Gia", value = gia.ToString(System.Globalization.CultureInfo.InvariantCulture) }
    };

            int rsInsert = db.ExeCute("sp_ThemThongKe", parametersInsert);
            if (rsInsert == 0)
            {
                MessageBox.Show("Lỗi khi lưu vào bảng thống kê!");
                return;
            }

            // 2. Xóa khỏi bảng LichHen
            var parametersDelete = new List<CustomParameter>
    {
        new CustomParameter { key = "@MaLichHen", value = maLichHen }
    };

            int rsDelete = db.ExeCute("sp_XoaLichHen", parametersDelete);
            if (rsDelete > 0)
            {
                MessageBox.Show("Hoàn thành lịch hẹn thành công!");
                LoadLichHen(); // hàm reload dữ liệu lịch hẹn
            }
            else
            {
                MessageBox.Show("Xóa lịch hẹn thất bại!");
            }
        }
    }
}
