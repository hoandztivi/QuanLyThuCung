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
    public partial class FormGiaiTri: Form
    {
        public FormGiaiTri()
        {
            InitializeComponent();
        }

        private async void FormChaoMung_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.Source = new Uri("https://www.youtube.com");
        }
    }
}
