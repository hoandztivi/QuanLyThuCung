using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythucungv2._0.formcon
{
    public partial class FormQuanLyNhanVien: Form
    {
        string quyenTruyCap = "";
        private string imgPath = "";
        private int selectedMaNhanVien = 0;
        Database db = new Database();
        public FormQuanLyNhanVien()
        {
            InitializeComponent();
        }
        private void LoadNhanVien()
        {
            dgvNhanVien.DataSource = null;
            DataTable dt = db.SelectData("sp_LayDanhSachNhanVien");

            if (dt != null)
            {
                dgvNhanVien.DataSource = dt;

                // Định dạng lại tên cột hiển thị
                dgvNhanVien.Columns["MaNhanVien"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
                dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dgvNhanVien.Columns["Email"].HeaderText = "Email";
                dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài Khoản";
                dgvNhanVien.Columns["MatKhau"].HeaderText = "Mật Khẩu";
                dgvNhanVien.Columns["QuyenTruyCap"].HeaderText = "Quyền Truy Cập";
                dgvNhanVien.Columns["AnhDaiDien"].HeaderText = "Ảnh Đại Diện";
            }
        }

        private void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoten.Text) || string.IsNullOrEmpty(txtTaiKhoan.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdNhanVien.Checked)
            {
                quyenTruyCap = "nhanvien";
            }
            List<CustomParameter> lstPara = new List<CustomParameter>
    {
        new CustomParameter { key = "@HoTen", value = txtHoten.Text },
        new CustomParameter { key = "@SoDienThoai", value = txtSdt.Text },
        new CustomParameter { key = "@Email", value = txtEmail.Text },
        new CustomParameter { key = "@TaiKhoan", value = txtTaiKhoan.Text },
        new CustomParameter { key = "@MatKhau", value = txtMatKhau.Text },
        new CustomParameter { key = "@AnhDaiDien", value = imgPath },
        new CustomParameter { key = "@QuyenTruyCap",value = quyenTruyCap }
    };

            int kq = db.ExeCute("sp_ThemNhanVien", lstPara);
            if (kq != 0)
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            var parameters = new List<CustomParameter>
    {
        new CustomParameter { key = "@TuKhoa", value = keyword }
    };

            DataTable dt = db.SelectData("sp_TimKiemNhanVien", parameters);
            dgvNhanVien.DataSource = dt;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (selectedMaNhanVien == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.");
                return;
            }
            if (rdNhanVien.Checked)
            {
                quyenTruyCap = "nhanvien";
            }
            var parameters = new List<CustomParameter>
    {
        new CustomParameter { key = "@MaNhanVien", value = selectedMaNhanVien.ToString() },
        new CustomParameter { key = "@HoTen", value = txtHoten.Text },
        new CustomParameter { key = "@SoDienThoai", value = txtSdt.Text },
        new CustomParameter { key = "@Email", value = txtEmail.Text },
        new CustomParameter { key = "@TaiKhoan", value = txtTaiKhoan.Text },
        new CustomParameter { key = "@MatKhau", value = txtMatKhau.Text },
        new CustomParameter { key = "@QuyenTruyCap", value = quyenTruyCap },
        new CustomParameter { key = "@AnhDaiDien", value = imgPath }
    };

            int result = db.ExeCute("sp_SuaNhanVien", parameters);

            if (result > 0)
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (selectedMaNhanVien == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var parameters = new List<CustomParameter>
        {
            new CustomParameter { key = "@MaNhanVien", value = selectedMaNhanVien.ToString() }
        };

                int result = db.ExeCute("sp_XoaNhanVien", parameters);

                if (result != 0)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadNhanVien();
                }
                else
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadNhanVien();
                }
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng đang chọn
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                // Lấy dữ liệu từ DataGridView
                selectedMaNhanVien = int.Parse(dgvNhanVien.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString());
                txtHoten.Text = row.Cells["HoTen"].Value.ToString();
                txtSdt.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                // Xử lý giới tính
                string quyenTruyCap = row.Cells["QuyenTruyCap"].Value.ToString();
                if (rdNhanVien.Checked)
                {
                    quyenTruyCap = "nhanvien";
                }

                // Lấy đường dẫn ảnh
                imgPath = row.Cells["AnhDaiDien"].Value?.ToString().Trim() ?? "";

                // Xóa ảnh cũ trước khi tải ảnh mới
                if (ptbAnhDaiDien.Image != null)
                {
                    ptbAnhDaiDien.Image.Dispose();
                    ptbAnhDaiDien.Image = null;
                }

                // Kiểm tra đường dẫn ảnh và hiển thị
                if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                {
                    ptbAnhDaiDien.Image = Image.FromFile(imgPath);
                }
                else
                {
                    ptbAnhDaiDien.Image = null; // Nếu không có ảnh thì để trống
                }
            }
        }

        private void btnThemAnh_Click(object sender, EventArgs e)
        {
            try 
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Hình ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string folderPath = "D:\\quanlythucung2.0\\anhnhanvien\\";
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        string fileName = Path.GetFileName(ofd.FileName);
                        string savePath = Path.Combine(folderPath, fileName);
                        File.Copy(ofd.FileName, savePath, true);
                        ptbAnhDaiDien.Image = Image.FromFile(savePath);
                        imgPath = savePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
