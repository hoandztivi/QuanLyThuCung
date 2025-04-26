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
    public partial class FormDichVu : Form
    {
        Database db = new Database();
        public FormDichVu()
        {
            InitializeComponent();
        }
        private void LoadDichVu()
        {
            dgvDichVu.DataSource = null;
            DataTable dt = db.SelectData("sp_LayDanhSachDichVu");

            if (dt != null)
            {
                dgvDichVu.DataSource = dt;

                dgvDichVu.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
                dgvDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
                dgvDichVu.Columns["MoTa"].HeaderText = "Mô Tả";
                dgvDichVu.Columns["Gia"].HeaderText = "Giá";
                dgvDichVu.Columns["Gia"].DefaultCellStyle.Format = "N0";

                dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void FormDichVu_Load(object sender, EventArgs e)
        {
            LoadDichVu();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var lst = new List<CustomParameter>
            {
                new CustomParameter{ key = "@TenDichVu", value = txtTimKiem.Text }
            };

            var kq = db.SelectData("sp_TimKiemDichVu", lst);
            dgvDichVu.DataSource = kq;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var lst = new List<CustomParameter>
            {
                new CustomParameter{ key = "@TenDichVu", value = txtTenDichVu.Text },
                new CustomParameter{ key = "@MoTa", value = txtMoTa.Text },
                new CustomParameter{ key = "@Gia", value = txtGia.Text }
            };

            var kq = db.ExeCute("sp_ThemDichVu", lst);
            if (kq > 0)
            {
                MessageBox.Show("Thêm dịch vụ thành công");
                LoadDichVu();
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow != null)
            {
                string maDV = dgvDichVu.CurrentRow.Cells["MaDichVu"].Value.ToString();

                var lstPara = new List<CustomParameter>()
        {
            new CustomParameter() { key = "@MaDichVu", value = maDV },
            new CustomParameter() { key = "@TenDichVu", value = txtTenDichVu.Text },
            new CustomParameter() { key = "@MoTa", value = txtMoTa.Text },
            new CustomParameter() { key = "@Gia", value = txtGia.Text }
        };

                var result = db.ExeCute("sp_SuaDichVu", lstPara);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!");
                    LoadDichVu();
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật!");
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow != null)
            {
                string maDV = dgvDichVu.CurrentRow.Cells["MaDichVu"].Value.ToString();

                DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    var lstPara = new List<CustomParameter>()
            {
                new CustomParameter() { key = "@MaDichVu", value = maDV }
            };

                    var result = db.ExeCute("sp_XoaDichVu", lstPara);
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa dịch vụ thành công!");
                        LoadDichVu();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi xóa!");
                    }
                }
            }
        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtTenDichVu.Text = dgvDichVu.Rows[e.RowIndex].Cells["TenDichVu"].Value.ToString();
                txtMoTa.Text = dgvDichVu.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
                txtGia.Text = dgvDichVu.Rows[e.RowIndex].Cells["Gia"].Value.ToString();
            }
        }

        private void btnThue_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count > 0)
            {
                var row = dgvDichVu.SelectedRows[0];
                int maDV = Convert.ToInt32(row.Cells["MaDichVu"].Value);
                decimal gia = Convert.ToDecimal(row.Cells["Gia"].Value);

                var f = new FormThueDichVu(maDV, gia);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần thuê.");
            }
        }
    }   
}
