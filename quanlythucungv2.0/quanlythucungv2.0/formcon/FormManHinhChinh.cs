using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace quanlythucungv2._0.formcon
{
    public partial class FormManHinhChinh : Form
    {
        Database db = new Database();
        Timer timer = new Timer();
        double hue = 0;

        public FormManHinhChinh()
        {
            InitializeComponent();
        }

        private void FormManHinhChinh_Load(object sender, EventArgs e)
        {
            timer.Interval = 50;
            timer.Tick += (s, ev) =>
            {
                hue += 3;
                if (hue >= 360) hue = 0;
                lblThongBao.ForeColor = ColorFromHSV(hue, 1, 1);
            };
            timer.Start();

            LoadThuCung();
        }

        private Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0: return Color.FromArgb(v, t, p);
                case 1: return Color.FromArgb(q, v, p);
                case 2: return Color.FromArgb(p, v, t);
                case 3: return Color.FromArgb(p, q, v);
                case 4: return Color.FromArgb(t, p, v);
                default: return Color.FromArgb(v, p, q);
            }
        }

        private void LoadThuCung()
        {
            if (flpThuCung == null)
            {
                MessageBox.Show("flpThuCung chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = db.SelectData("sp_LayDanhSachThuCung");
            flpThuCung.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string ten = row["TenThuCung"]?.ToString() ?? "Không rõ";
                    string loai = row["Loai"]?.ToString() ?? "Không rõ";
                    string tuoi = row["Tuoi"]?.ToString() ?? "Không rõ";
                    int giaTien = row["GiaTien"] != DBNull.Value ? Convert.ToInt32(row["GiaTien"]) : 0;
                    string anhDaiDien = row["AnhDaiDien"]?.ToString() ?? "";

                    ucThuCung petCard = new ucThuCung();
                    petCard.SetData(ten, loai, tuoi, giaTien, anhDaiDien);

                    // Không disable label nữa -> giữ màu
                    petCard.Cursor = Cursors.Default;
                    foreach (Control ctrl in petCard.Controls)
                    {
                        ctrl.TabStop = false;
                        ctrl.Cursor = Cursors.Default;
                    }

                    petCard.Margin = new Padding(10);
                    flpThuCung.Controls.Add(petCard);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thú cưng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
