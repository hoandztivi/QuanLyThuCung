using quanlythucungv2._0;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace quanlythucungv2._0.formcon
{
    public partial class dangki: Form
    {
        private Database db;
        public dangki()
        {
            InitializeComponent();
            rdQuanLy.Enabled = false;
        }

        private void dangki_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            db = new Database();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            String email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string nhapLaiMatKhau = txtNhapLaiMK.Text.Trim();
            string quyenTruyCap = "";

            if (rdQuanLy.Checked)
            {
                quyenTruyCap = "quanly";
            }
            else if (rdNhanVien.Checked)
            {
                quyenTruyCap = "nhanvien";
            }

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(nhapLaiMatKhau) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (matKhau != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(quyenTruyCap))
            {
                MessageBox.Show("Vui lòng chọn quyền truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo danh sách tham số
            List<CustomParameter> lstPara = new List<CustomParameter>
                {
                    new CustomParameter { key = "@TaiKhoan", value = taiKhoan },
                    new CustomParameter { key = "@Email", value = taiKhoan },
                    new CustomParameter { key = "@MatKhau", value = matKhau },
                    new CustomParameter { key = "@QuyenTruyCap", value = quyenTruyCap }
                };

            try
            {
                var kq = db.ExeCute("TaoTaiKhoan", lstPara); // Gọi Stored Procedure

                if (kq > 0) // Nếu thêm thành công
                {
                    MessageBox.Show("Tài khoản đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose(); // Đóng form đăng ký
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký tài khoản: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
