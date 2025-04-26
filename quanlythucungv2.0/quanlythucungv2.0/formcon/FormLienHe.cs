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
    public partial class FormLienHe: Form
    {
        public FormLienHe()
        {
            InitializeComponent();
        }

        private void FormLienHe_Load(object sender, EventArgs e)
        {
            lblLienHe.Text = "📇 Thông tin liên hệ\r\n\r\n" +
                "👤 Họ và tên: Lê Quí Hoàn \r\n" +
                "📞 Số điện thoại: 0325 899 425 \r\n" +
                "📧 Email: hoandztv@gmail.com \r\n" +
                "🏠 Địa chỉ: Số nhà 19 ,Ngõ 149 Trâu Quỳ ,Gia Lâm, Hà Nội  \r\n" +
                "🕒 Thời gian hỗ trợ: 8:00 – 17:00 (Thứ 2 – Thứ 7)  \r\n" +
                "🌐 Website: www.hoanle.com \r\n" +
                "📌 Fanpage: fb.com/hoanle.profile";
        }
    }
}
