using quanlythucungv2._0;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace quanlythucungv2._0.formcon
{
    public partial class FormQuanLyThuCung : Form
    {
        private string imgPath = "";
        private int selectedMaThuCung = 0;
        Database db = new Database();

        public FormQuanLyThuCung()
        {
            InitializeComponent();
        }

        private void LoadThuCung()
        {
            dgvThuCung.DataSource = null;
            DataTable dt = db.SelectData("sp_LayDanhSachThuCung");

            if (dt != null)
            {
                dgvThuCung.DataSource = dt;

                // Đặt lại DataPropertyName cho đúng cột SQL
                dgvThuCung.Columns["MaThuCung"].HeaderText = "Mã Thú Cưng";
                dgvThuCung.Columns["TenThuCung"].HeaderText = "Tên Thú Cưng";
                dgvThuCung.Columns["Loai"].HeaderText = "Loài";
                dgvThuCung.Columns["Giong"].HeaderText = "Giống";
                dgvThuCung.Columns["Tuoi"].HeaderText = "Tuổi";
                dgvThuCung.Columns["AnhDaiDien"].HeaderText = "Ảnh Đại Diện";
                dgvThuCung.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvThuCung.Columns["GiaTien"].HeaderText = "Giá Tiền";
                dgvThuCung.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
            }
        }
        private void dtgvDSTC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng đang chọn
                DataGridViewRow row = dgvThuCung.Rows[e.RowIndex];

                // Lấy dữ liệu từ DataGridView
                selectedMaThuCung = int.Parse(dgvThuCung.Rows[e.RowIndex].Cells["MaThuCung"].Value.ToString());
                txtTenThuCung.Text = row.Cells["TenThuCung"].Value.ToString();
                txtLoai.Text = row.Cells["Loai"].Value.ToString();
                txtGiong.Text = row.Cells["Giong"].Value.ToString();
                txtTuoi.Text = row.Cells["Tuoi"].Value.ToString();
                txtGiaTien.Text = Convert.ToInt32(row.Cells["GiaTien"].Value).ToString();

                // Xử lý giới tính
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                rdDuc.Checked = gioiTinh == "Đực";
                rdCai.Checked = gioiTinh == "Cái";

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
                        string folderPath = "D:\\quanlythucung2.0\\anhthucung\\";
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
        private void FormQuanLyThuCung_Load(object sender, EventArgs e)
        {
            LoadThuCung();
        }
        //sửa
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (selectedMaThuCung == 0)
            {
                MessageBox.Show("Vui lòng chọn thú cưng để sửa.");
                return;
            }

            var parameters = new List<CustomParameter>
    {
        new CustomParameter { key = "@MaThuCung", value = selectedMaThuCung.ToString() },
        new CustomParameter { key = "@TenThuCung", value = txtTenThuCung.Text },
        new CustomParameter { key = "@Loai", value = txtLoai.Text },
        new CustomParameter { key = "@Giong", value = txtGiong.Text },
        new CustomParameter { key = "@Tuoi", value = txtTuoi.Text },
        new CustomParameter { key = "@GioiTinh", value = rdDuc.Checked ? "Đực" : "Cái" },
        new CustomParameter { key = "@AnhDaiDien", value = imgPath },
        new CustomParameter { key = "@GiaTien", value = txtGiaTien.Text }
    };

            int result = db.ExeCute("sp_SuaThuCung", parameters);

            if (result > 0)
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadThuCung();
                selectedMaThuCung = 0; // Reset biến
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }
        //xóa
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (selectedMaThuCung == 0)
            {
                MessageBox.Show("Vui lòng chọn thú cưng để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var parameters = new List<CustomParameter>
        {
            new CustomParameter { key = "@MaThuCung", value = selectedMaThuCung.ToString() }
        };

                int result = db.ExeCute("sp_XoaThuCung", parameters);

                if (result > 0) // Kiểm tra kết quả từ stored procedure
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadThuCung();
                    selectedMaThuCung = 0;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thú cưng để xóa!");
                }
            }
        }
        //Thêm
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenThuCung.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thú cưng.");
                return;
            }

            var parameters = new List<CustomParameter>
            {
                new CustomParameter { key = "@TenThuCung", value = txtTenThuCung.Text },
                new CustomParameter { key = "@Loai", value = txtLoai.Text },
                new CustomParameter { key = "@Giong", value = txtGiong.Text },
                new CustomParameter { key = "@Tuoi", value = txtTuoi.Text },
                new CustomParameter { key = "@GioiTinh", value = rdDuc.Checked ? "Đực" : "Cái" },
                new CustomParameter { key = "@AnhDaiDien", value = imgPath },
                new CustomParameter { key = "@GiaTien", value = txtGiaTien.Text }
            };

            int result = db.ExeCute("sp_ThemThuCung", parameters);
            MessageBox.Show(result > 0 ? "Thêm thành công!" : "Thêm thất bại!");
            LoadThuCung();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Nếu ô tìm kiếm rỗng, load lại toàn bộ danh sách
            if (string.IsNullOrEmpty(keyword))
            {
                LoadThuCung();
                return;
            }

            // Truyền tham số tìm kiếm vào danh sách
            var parameters = new List<CustomParameter>
    {
        new CustomParameter { key = "@TuKhoa", value = keyword }
    };

            // Gọi stored procedure tìm kiếm
            DataTable dt = db.SelectData("sp_TimThuCung", parameters);

            if (dt != null)
            {
                dgvThuCung.DataSource = dt;
            }
        }
    }
}