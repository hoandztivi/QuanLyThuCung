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

namespace quanlythucungv2._0.formcon
{
    public partial class quenmatkhau: Form
    {
        private Database db;
        public quenmatkhau()
        {
            InitializeComponent();
            lblmk.Text = "";
        }

        private void quenmatkhau_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            db = new Database();
            string taiKhoan = txtemail.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Vui lòng nhập email của tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtemail.Select();
                return;
            }

            // Tạo danh sách tham số
            List<CustomParameter> lstPara = new List<CustomParameter>
            {
                new CustomParameter { key = "@Email", value = taiKhoan }
            };

            // Gọi Stored Procedure để lấy mật khẩu
            var kq = db.SelectData("[QuenMatKhau]", lstPara);

            if (kq != null && kq.Rows.Count > 0)
            {
                string matKhau = kq.Rows[0]["MatKhau"].ToString();
                lblmk.ForeColor = Color.Blue;
                lblmk.Text = "Mật khẩu là:" + matKhau;
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtemail.Select();
            }
        }
    }
}
