using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlythucungv2._0.formcon;

namespace quanlythucungv2._0
{
    public partial class FormMain: Form
    {
        private string quyenTruyCap;
        public FormMain(string quyenTruyCap)
        {
            InitializeComponent();
            this.quyenTruyCap = quyenTruyCap;
        }

        //hàm add form lên gruopbox grbHTTT
        public void AddForm(Form form)
        {
            //xóa các control có trên gruopbox
            this.grbHTTT.Controls.Clear();
            form.TopLevel = false;
            form.AutoScroll = true;
            //bỏ viền form
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            //this.Text = form.Text;
            this.grbHTTT.Controls.Add(form);
            form.Show();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            if (quyenTruyCap == "quanly")
            {
                menuStrip1.Enabled = true;
            }
            else if (quyenTruyCap == "nhanvien")
            {
                menuNV.Enabled = false;
                quảnLýThúCưngToolStripMenuItem.Enabled = false;
            }
            var form = new FormManHinhChinh();
            AddForm(form);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dangnhapnew formDangNhap = new dangnhapnew();
            formDangNhap.Show();
            this.Dispose();
        }

        private void quảnLýThúCưngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormQuanLyThuCung();
            AddForm(form);
        }

        private void danhSáchThúCưngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDSThuCung();
            AddForm(form);
        }

        private void menuNV_Click(object sender, EventArgs e)
        {
            var form = new FormQuanLyNhanVien();
            AddForm(form);
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormKhachHang();
            AddForm(form);
        }

        private void quayLạiTrangChủToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormManHinhChinh();
            AddForm(form);
        }

        private void giảiTríToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormGiaiTri();
            AddForm(form);
        }

        private void quảnLýĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormQuanLyDonHang(quyenTruyCap);
            AddForm(form);
        }

        private void dịchVụChămSócToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDichVu();
            AddForm(form);
        }

        private void quảnLýLịchHẹnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormLichHen();
            AddForm(form);
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Không thể thực hiện vui lòng liên hệ quản lý!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cấuHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void báoCáoDoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormThongKe();
            AddForm(form);
        }

        private void sốLượngBánĐượcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormBieuDo();
            AddForm(form);
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new HDSD();
            AddForm(form);
        }

        private void liênHệHỗTrợToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormLienHe();
            AddForm(form);
        }

        private void thôngTinPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormThongTinPhanMem();
            AddForm(form);
        }
    }
}
