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
    public partial class dangnhapnew: Form
    {

        Database db = new Database();
        public dangnhapnew()
        {
            InitializeComponent();
        }

        private void dangnhapnew_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.pexels_pixabay_57416;
            label1.Parent = pictureBox1;// Đặt PictureBox làm Control cha của Label
            label1.BackColor = Color.Transparent; 
            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;
            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;
            cbkHien.Parent = pictureBox1;
            cbkHien.BackColor = Color.Transparent;
            rdQuanLy.Parent = pictureBox1;
            rdQuanLy.BackColor = Color.Transparent;
            rdNhanVien.Parent = pictureBox1;
            rdNhanVien.BackColor = Color.Transparent;
            linkLabel1.Parent = pictureBox1;
            linkLabel1.BackColor = Color.Transparent;
            txtTaiKhoan.Parent = pictureBox1;
            txtTaiKhoan.BackColor = Color.Transparent;
            txtMatKhau.Parent = pictureBox1;
            txtMatKhau.BackColor = Color.Transparent;
            guna2Button1.Parent = pictureBox1;
            guna2Button1.BackColor = Color.Transparent;
            guna2Button2.Parent = pictureBox1;
            guna2Button2.BackColor = Color.Transparent;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new quenmatkhau().ShowDialog();
        }

        private void cbkHien_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkHien.Checked)
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Xác nhận thoát",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
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
    }
}
