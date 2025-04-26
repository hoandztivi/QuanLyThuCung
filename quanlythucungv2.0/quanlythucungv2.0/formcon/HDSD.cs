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
    public partial class HDSD: Form
    {
        public HDSD()
        {
            InitializeComponent();
        }

        private void HDSD_Load(object sender, EventArgs e)
        {
            lblHuongDan.Text =
"🐾 Giới thiệu phần mềm\n\n" +
"Phần mềm quản lý thú cưng giúp các cửa hàng thú cưng dễ dàng quản lý khách hàng, dịch vụ chăm sóc, lịch hẹn, đơn hàng và thống kê doanh thu một cách nhanh chóng, chính xác và hiệu quả.\n\n" +
"🧑‍💼 Quản lý khách hàng\n" +
"- Thêm, sửa, xóa và tìm kiếm thông tin khách hàng.\n" +
"- Lưu trữ đầy đủ thông tin: họ tên, số điện thoại, địa chỉ,...\n\n" +
"🐶 Danh mục thú cưng\n" +
"- Hiển thị danh sách các thú cưng đang được bán.\n" +
"- Hỗ trợ thêm mới, chỉnh sửa hoặc xóa thú cưng khỏi danh sách.\n\n" +
"💼 Dịch vụ chăm sóc\n" +
"- Quản lý danh sách dịch vụ: tắm, cắt tỉa, khám sức khỏe,...\n" +
"- Cho phép thuê dịch vụ theo lịch hẹn.\n\n" +
"🕒 Lịch hẹn dịch vụ\n" +
"- Đặt lịch hẹn chăm sóc thú cưng theo ngày giờ.\n" +
"- Hoàn thành lịch hẹn và lưu vào thống kê doanh thu.\n\n" +
"🧾 Quản lý đơn hàng\n" +
"- Ghi nhận các đơn mua thú cưng.\n" +
"- Hỗ trợ in hóa đơn và quản lý lịch sử giao dịch.\n\n" +
"📊 Thống kê & Doanh thu\n" +
"- Hiển thị biểu đồ doanh thu theo ngày.\n" +
"- Tra cứu thông tin theo khách hàng, dịch vụ hoặc thú cưng.\n\n" +
"🔐 Phân quyền người dùng\n" +
"- Tài khoản quản lý và nhân viên có quyền truy cập khác nhau.\n" +
"- Bảo mật dữ liệu và kiểm soát truy cập hiệu quả.";

        }
    }
}
