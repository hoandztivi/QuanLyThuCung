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

    public partial class FormBieuDo: Form
    {
        Database db = new Database();
        public FormBieuDo()
        {
            InitializeComponent();
        }

        private void FormBieuDo_Load(object sender, EventArgs e)
        {
            LoadBieuDoDoanhThu();
        }
        private void LoadBieuDoDoanhThu()
        {
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas.Clear();

            chartDoanhThu.ChartAreas.Add("AreaDoanhThu");
            var series = chartDoanhThu.Series.Add("Doanh Thu");

            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

            // Gọi stored procedure
            var dt = db.SelectData("sp_DoanhThuTheoNgay", null);

            foreach (DataRow row in dt.Rows)
            {
                string ngay = row["Ngay"].ToString();
                decimal tong = Convert.ToDecimal(row["TongDoanhThu"]);
                series.Points.AddXY(ngay, tong);
            }

            chartDoanhThu.ChartAreas[0].AxisX.Title = "Ngày";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Tổng doanh thu (VND)";
        }
    }
}
