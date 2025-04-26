using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace quanlythucungv2._0.formcon
{
    public partial class FormThanhToan : Form
    {
        private Database db;
        private decimal tongTien;
        private List<ucThuCung> gioHang;
        public FormThanhToan(decimal tongTien, List<ucThuCung> gioHang)
        {
            InitializeComponent();
            db = new Database();
            this.tongTien = tongTien;
            this.gioHang = gioHang;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            lblNgayMua.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lblTongTien.Text = $"{tongTien:N0} VND";

            var dt = db.SelectData("sp_LoadKhachHang");
            cbKhachHang.DataSource = dt;
            cbKhachHang.DisplayMember = "ThongTin";   // "Họ tên - SĐT"
            cbKhachHang.ValueMember = "MaKhachHang";
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            if (cbKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            string maKhach = cbKhachHang.SelectedValue.ToString();
            string tenKhach = ((DataRowView)cbKhachHang.SelectedItem)["ThongTin"].ToString();

            // Ghép tên thú cưng mua lại thành chuỗi
            string tenThuCungMua = string.Join(" - ", gioHang.Select(x => x.TenThuCung));

            // Ngày đặt hàng
            string ngayDat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Thêm vào bảng Đơn Hàng
            var lstPara = new List<CustomParameter>
    {
        new CustomParameter { key = "@MaKhachHang", value = maKhach },
        new CustomParameter { key = "@TenKhachHang", value = tenKhach },
        new CustomParameter { key = "@TenThuCungMua", value = tenThuCungMua },
        new CustomParameter { key = "@NgayDatHang", value = ngayDat },
        new CustomParameter { key = "@TongTien", value = tongTien.ToString(System.Globalization.CultureInfo.InvariantCulture) }
    };

            var kq = db.ExeCute("sp_ThemDonHang", lstPara);

            if (kq == 1)
            {
                // Thêm vào thống kê
                var thongKeParams = new List<CustomParameter>
    {
        new CustomParameter { key = "@Loai", value = "Mua thú cưng" },
        new CustomParameter { key = "@MaKhachHang", value = maKhach },
        new CustomParameter { key = "@TenThuCung", value = tenThuCungMua },
        new CustomParameter { key = "@Ngay", value = DateTime.Now.ToString("yyyy-MM-dd") },
        new CustomParameter { key = "@Gia", value = tongTien.ToString(System.Globalization.CultureInfo.InvariantCulture) }
    };

                db.ExeCute("sp_ThemThongKe", thongKeParams);

                MessageBox.Show("Thanh toán thành công!");
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
