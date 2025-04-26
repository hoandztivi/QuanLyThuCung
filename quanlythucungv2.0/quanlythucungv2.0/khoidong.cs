using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace quanlythucungv2._0
{
    public partial class khoidong: Form
    {
        private int progress = 0;
        public khoidong()
        {
            InitializeComponent();
            timer.Start();
        }
        int start = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            start += 1;
            uiProcessBar1.Value = start;
            lblPhanTram.Text = start + "%";
            // Di chuyển PictureBox theo ProgressBar
            int newX = uiProcessBar1.Left + (uiProcessBar1.Width - pictureBoxPet.Width) * start / 100;
            pictureBoxPet.Location = new Point(newX, pictureBoxPet.Top);
            if (uiProcessBar1.Value == 100)
            {
                uiProcessBar1.Value = 0;
                dangnhapnew dn = new dangnhapnew();
                dn.Show();
                this.Hide();
                timer.Stop();
            }
        }
    }
}
