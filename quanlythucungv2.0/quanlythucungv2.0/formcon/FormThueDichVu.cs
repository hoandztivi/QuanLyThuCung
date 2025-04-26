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
    public partial class FormThueDichVu: Form
    {
        private int maDichVu;
        private decimal giaTien;
        private Database db;
        public FormThueDichVu(int maDichVu, decimal giaTien)
        {
            InitializeComponent();
            this.maDichVu = maDichVu;
            this.giaTien = giaTien;
            db = new Database();
        }

        private void FormThueDichVu_Load(object sender, EventArgs e)
        {
            var dtKhach = db.SelectData("sp_LayDanhSachKhachHang");
            cbKhachHang.DataSource = dtKhach;
            cbKhachHang.DisplayMember = "HoTen";
            cbKhachHang.ValueMember = "MaKhachHang";

            // Gán giá tiền từ biến truyền vào
            lblGia.Text = "Giá: " + giaTien.ToString("N0") + " VND";
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnThue_Click(object sender, EventArgs e)
        {
            if (cbKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            DateTime ngayHen;
            if (!DateTime.TryParse(mtbNgayHen.Text, out ngayHen))
            {
                MessageBox.Show("Ngày hẹn không hợp lệ!");
                return;
            }

            var maKhachHang = cbKhachHang.SelectedValue.ToString();
            var ghiChu = txtGhiChu.Text;

            var lstPara = new List<CustomParameter>()
    {
        new CustomParameter() { key = "@MaKhachHang", value = maKhachHang },
        new CustomParameter() { key = "@MaDichVu", value = maDichVu.ToString() },
        new CustomParameter() { key = "@NgayHen", value = ngayHen.ToString("yyyy-MM-dd") },
        new CustomParameter() { key = "@GhiChu", value = ghiChu },
        new CustomParameter() { key = "@Gia", value =  giaTien.ToString(System.Globalization.CultureInfo.InvariantCulture) }
    };

            var kq = db.ExeCute("sp_ThemLichHen", lstPara);
            if (kq > 0)
            {
                MessageBox.Show("Thuê dịch vụ thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thuê thất bại. Vui lòng thử lại.");
            }
        }
    }
}
