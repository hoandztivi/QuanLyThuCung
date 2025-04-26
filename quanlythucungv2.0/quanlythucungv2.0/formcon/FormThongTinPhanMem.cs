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
    public partial class FormThongTinPhanMem: Form
    {
        public FormThongTinPhanMem()
        {
            InitializeComponent();
        }

        private void FormThongTinPhanMem_Load(object sender, EventArgs e)
        {
            lblLienHe.Text = "🐾 Thông tin phần mềm\r\n" +
                "📛 Tên phần mềm: PetCare - Quản lý thú cưng\r\n" +
                "🛠️ Phiên bản: 2.0\r\n" +
                "📅 Ngày phát hành: 11/04/2025\r\n" +
                "👨‍💻 Phát triển bởi: Nhóm Học Xây Dựng Phát Triển Phần Mềm\r\n" +
                "💼 Mục đích: Quản lý khách hàng, thú cưng, dịch vụ, đơn hàng và doanh thu cho các cửa hàng thú cưng.\r\n";
        }
    }
}
