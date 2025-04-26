using Sunny.UI;
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
    public partial class FormDSThuCung: Form
    {
        Database db = new Database();
        public FormDSThuCung()
        {
            InitializeComponent();
        }
        private List<ucThuCung> gioHang = new List<ucThuCung>(); // Danh sách thú cưng trong giỏ
        private int tongGiaTien = 0;

        private void PetCard_OnPetClick(object sender, EventArgs e)
        {
            ucThuCung pet = sender as ucThuCung;
            if (pet != null)
            {
                // Kiểm tra xem thú cưng đã có trong giỏ hàng chưa
                foreach (ucThuCung item in flpGioHang.Controls)
                {
                    if (item.TenThuCung == pet.TenThuCung) // So sánh theo tên thú cưng
                    {
                        MessageBox.Show("Thú cưng này đã có trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Thoát, không thêm nữa
                    }
                }
                    // Thêm vào danh sách giỏ hàng
                    gioHang.Add(pet);

                // Cộng tổng giá tiền
                tongGiaTien += pet.GiaTien;

                // Hiển thị trong FlowLayoutPanel giỏ hàng
                ucThuCung petItem = new ucThuCung();
                petItem.SetData(pet.TenThuCung, pet.Loai, pet.Tuoi, pet.GiaTien, pet.AnhDaiDien);
                flpGioHang.Controls.Add(petItem);

                // Cập nhật tổng giá tiền
                lblTongGia.Text = $"Tổng tiền: {tongGiaTien:N0} VND";
            }
        }
        private void LoadThuCung()
        {
            // Kiểm tra flpThuCung có bị null không
            if (flpThuCung == null)
            {
                MessageBox.Show("flpThuCung chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = db.SelectData("sp_LayDanhSachThuCung"); // Lấy dữ liệu từ DB
            flpThuCung.Controls.Clear(); // Xóa dữ liệu cũ

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    // Kiểm tra giá trị null trước khi sử dụng
                    string ten = row["TenThuCung"]?.ToString() ?? "Không rõ";
                    string loai = row["Loai"]?.ToString() ?? "Không rõ";
                    string tuoi = row["Tuoi"]?.ToString() ?? "Không rõ";
                    int giaTien = row["GiaTien"] != DBNull.Value ? Convert.ToInt32(row["GiaTien"]) : 0;
                    string anhDaiDien = row["AnhDaiDien"]?.ToString() ?? "";

                    // Tạo UserControl mới
                    ucThuCung petCard = new ucThuCung();
                    petCard.SetData(ten, loai,tuoi, giaTien, anhDaiDien);

                    petCard.OnPetClick += PetCard_OnPetClick;
                    // Thêm vào FlowLayoutPanel
                    flpThuCung.Controls.Add(petCard);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thú cưng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void FormDSThuCung_Load(object sender, EventArgs e)
        {
            LoadThuCung();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower(); // Lấy từ khóa và chuẩn hóa chữ thường

            foreach (ucThuCung pet in flpThuCung.Controls)
            {
                // Kiểm tra nếu từ khóa xuất hiện trong tên hoặc loại thú cưng
                bool isMatch = pet.TenThuCung.ToLower().Contains(tuKhoa) || pet.Loai.ToLower().Contains(tuKhoa);

                // Hiển thị nếu khớp, ẩn nếu không khớp
                pet.Visible = isMatch;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Xóa tất cả thú cưng khỏi giỏ hàng
            flpGioHang.Controls.Clear();

            // Reset tổng giá tiền
            tongGiaTien = 0;
            lblTongGia.Text = "Tổng tiền: 0 VND";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Danh sách các thú cưng được chọn để xóa
            List<Control> selectedPets = new List<Control>();

            foreach (ucThuCung pet in flpGioHang.Controls)
            {
                if (pet.IsSelected)
                {
                    selectedPets.Add(pet);
                    tongGiaTien -= pet.GiaTien;
                }
            }

            // Xóa từng thú cưng được chọn
            foreach (Control pet in selectedPets)
            {
                flpGioHang.Controls.Remove(pet);
                pet.Dispose(); // Giải phóng bộ nhớ
            }

            // Cập nhật tổng tiền
            lblTongGia.Text = $"Tổng tiền: {tongGiaTien:N0} VND";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Tạo form thanh toán và truyền danh sách thú cưng từ flpGioHang
            List<ucThuCung> danhSachThuCung = new List<ucThuCung>();
            foreach (ucThuCung item in flpGioHang.Controls)
            {
                danhSachThuCung.Add(item);
            }

            FormThanhToan thanhtoan = new FormThanhToan(tongGiaTien, danhSachThuCung);
            thanhtoan.ShowDialog();
        }
    }
}
