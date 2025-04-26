using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel.Design;

namespace quanlythucungv2._0.formcon
{
    public partial class ucThuCung: UserControl
    {
        public bool IsSelected { get; private set; } = false;
        public string TenThuCung { get; private set; }
        public string Loai { get; private set; }
        public string Tuoi { get; private set; }
        public int GiaTien { get; private set; }
        public string AnhDaiDien { get; private set; }

        public ucThuCung()
        {
            InitializeComponent();
            // Gán sự kiện Click cho tất cả thành phần bên trong
            this.Click += ucThuCung_Click;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += ucThuCung_Click;
            }
        }
        public void SetData(string ten, string loai, string tuoi, int gia, string anhPath)
        {
            // Gán dữ liệu vào các thuộc tính
            TenThuCung = ten;
            Loai = loai;
            Tuoi = tuoi;
            GiaTien = gia;
            AnhDaiDien = anhPath;

            lblTenThuCung.Text = ten;
            lblLoai.Text = "Loài: " + loai;
            lblTuoi.Text = "Tuổi: " + tuoi;
            lblGiaTien.Text = gia.ToString("N0") + " VND";
            lblTenThuCung.ForeColor = Color.DodgerBlue;
            lblLoai.ForeColor = Color.SteelBlue;
            lblTuoi.ForeColor = Color.SlateGray;
            lblGiaTien.ForeColor = Color.Orange;

            if (!string.IsNullOrEmpty(anhPath) && File.Exists(anhPath))
            {
                ptbAnh.Image = Image.FromFile(anhPath);
                ptbAnh.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                ptbAnh.Image = Properties.Resources.meow2; // Ảnh mặc định
            }
        }

        [Category("Custom Events")] // Hiển thị nhóm sự kiện trong Properties
        [Description("Sự kiện khi Click vào Thú Cưng")] // Mô tả hiển thị trong Properties
        public event EventHandler OnPetClick;
        private void ucThuCung_Click(object sender, EventArgs e)
        {
            OnPetClick?.Invoke(this, e); // Kích hoạt sự kiện
            IsSelected = !IsSelected;
            this.BackColor = IsSelected ? Color.LightGray : Color.White; // Đổi màu khi chọn
        }
    }
}
