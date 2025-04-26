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
    public partial class FormKhachHang: Form
    {
        Database db = new Database();
        private int selectedMaKhachHang = 0;
        public FormKhachHang()
        {
            InitializeComponent();
        }
        private void LoadKhachHang()
        {
            dgvKhachHang.DataSource = null;
            DataTable dt = db.SelectData("sp_LayDanhSachKhachHang");
            if (dt != null)
            {
                dgvKhachHang.DataSource = dt;
                dgvKhachHang.Columns["MaKhachHang"].HeaderText = "Mã Khách Hàng";
                dgvKhachHang.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dgvKhachHang.Columns["Email"].HeaderText = "Email";
                dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            }
        }
        private void BtnThem_Click(object sender, EventArgs e)
        {
            List<CustomParameter> lstPara = new List<CustomParameter>
    {
        new CustomParameter { key = "@HoTen", value = txtHoten.Text },
        new CustomParameter { key = "@SoDienThoai", value = txtSdt.Text },
        new CustomParameter { key = "@Email", value = txtEmail.Text },
        new CustomParameter { key = "@DiaChi", value = txtDiaChi.Text }
    };

            int kq = db.ExeCute("sp_ThemKhachHang", lstPara);
            if (kq != 0)
            {
                MessageBox.Show("Thêm khách hàng thành công!");
                LoadKhachHang();
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!");
                LoadKhachHang();
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaKhachHang == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa.");
                return;
            }

            List<CustomParameter> lstPara = new List<CustomParameter>
    {
        new CustomParameter { key = "@MaKhachHang", value = selectedMaKhachHang.ToString() },
        new CustomParameter { key = "@HoTen", value = txtHoten.Text },
        new CustomParameter { key = "@SoDienThoai", value = txtSdt.Text },
        new CustomParameter { key = "@Email", value = txtEmail.Text },
        new CustomParameter { key = "@DiaChi", value = txtDiaChi.Text }
    };

            int kq = db.ExeCute("sp_SuaKhachHang", lstPara);
            if (kq > 0)
            {
                MessageBox.Show("Cập nhật khách hàng thành công!");
                LoadKhachHang();
            }
            else
            {
                MessageBox.Show("Cập nhật khách hàng thất bại!");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaKhachHang == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var parameters = new List<CustomParameter>
        {
            new CustomParameter { key = "@MaKhachHang", value = selectedMaKhachHang.ToString() }
        };

                int result = db.ExeCute("sp_XoaKhachHang", parameters);
                if (result != 0)
                {
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadKhachHang();
                }
                else
                {
                    MessageBox.Show("Không thể xóa!");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            var parameters = new List<CustomParameter>
    {
        new CustomParameter { key = "@TuKhoa", value = keyword }
    };

            DataTable dt = db.SelectData("sp_TimKiemKhachHang", parameters);
            dgvKhachHang.DataSource = dt;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không click vào header
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                // Cập nhật mã khách hàng được chọn
                selectedMaKhachHang = Convert.ToInt32(row.Cells["MaKhachHang"].Value);
                // Gán dữ liệu từ DataGridView vào các TextBox
                txtHoten.Text = row.Cells["HoTen"].Value.ToString();
                txtSdt.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            }
        }
    }
}
