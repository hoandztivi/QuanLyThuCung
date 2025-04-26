using quanlythucungv2;
using quanlythucungv2._0.formcon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythucungv2._0
{
    public partial class dangnhap: Form
    {
        private Database db;
        private FormMain mainForm; // Biến lưu tham chiếu tới FormMain
        public dangnhap()
        {
            InitializeComponent();
            linkLabel2.Enabled = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new dangki().ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new quenmatkhau().ShowDialog();
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void ckbHien_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHien.Checked)
            {
                // Hiển thị mật khẩu
                txtMatKhau.PasswordChar = '\0';  // Không ẩn mật khẩu
            }
            else
            {
                // Ẩn mật khẩu
                txtMatKhau.PasswordChar = '*';  // Ẩn mật khẩu với dấu sao
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            db = new Database();
            // Lấy thông tin từ giao diện
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string quyenTruyCap = "";
            if (rdQuanLy.Checked)
            {
                quyenTruyCap = "quanly";
            }
            else if (rdNhanVien.Checked)
            {
                quyenTruyCap = "nhanvien";
            }

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                new CustomParameter { key = "@MatKhau", value = matKhau },
                new CustomParameter { key = "@QuyenTruyCap", value = quyenTruyCap }
            };

            // Gọi Stored Procedure để kiểm tra tài khoản
            var kq = db.SelectData("[CheckLogin]", lstPara);

            if (kq != null && kq.Rows.Count > 0)
            {
                FormMain mainForm = new FormMain(quyenTruyCap); // Truyền quyền vào FormMain
                mainForm.Show();
                this.Hide(); // Ẩn form đăng nhập
            }
            else
            {
                // Thông báo lỗi đăng nhập
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
        }
    }
}
