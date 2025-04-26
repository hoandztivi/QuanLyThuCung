using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlythucungv2._0
{
    public class Database
    {
        //chuỗi kết nối
        private string strCon = @"Data Source=HOANDEPTRAI;Initial Catalog=QLThuCung;Integrated Security=True";
        private SqlConnection sqlCon;
        private DataTable dt;
        private SqlCommand cmd;

        //hàm khởi tạo
        public Database()
        {
            try
            {
                sqlCon = new SqlConnection(strCon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thông tin chi tiết :" + ex);
            }
        }

        //hàm truy vấn dữ liệu của bảng
        public DataTable SelectData(string sql, List<CustomParameter> lstPara = null)
        {
            try
            {
                //mở kết nối
                sqlCon.Open();
                //nội dung sql đc truyền vào
                cmd = new SqlCommand(sql, sqlCon);
                //set command type cho cmd
                cmd.CommandType = CommandType.StoredProcedure;
                //gán các tham số cho cmd
                if (lstPara != null)
                {
                    //gán các tham số cho cmd
                    foreach (var para in lstPara)
                    {
                        cmd.Parameters.AddWithValue(para.key, para.value);
                    }

                }
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
                return null;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        //hàm update dữ liệu
        public int ExeCute(string sql, List<CustomParameter> lstPara = null)
        {
            /*
            try
            {
                //mở kết nối
                sqlCon.Open();
                //thực thi câu lệnh sql
                cmd = new SqlCommand(sql, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                //gán các tham số cho cmd
                foreach (var p in lstPara)
                {
                    cmd.Parameters.AddWithValue(p.key, p.value);
                }
                //lấy kết quả thực thi truy vấn
                var kq = cmd.ExecuteNonQuery();
                //trả về kết quả
                return (int)kq;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi câu lệnh: " + ex.Message);
                return -1;
            }
            finally
            {
                //đóng kết nối
                sqlCon.Close();
            }
            */
            try
            {
                sqlCon.Open();
                cmd = new SqlCommand(sql, sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;

                // foreach nếu lstPara khác null
                if (lstPara != null)
                {
                    foreach (var p in lstPara)
                    {
                        cmd.Parameters.AddWithValue(p.key, p.value);
                    }
                }

                var kq = cmd.ExecuteNonQuery();
                return kq;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi câu lệnh: " + ex.Message);
                return -1;
            }
            finally
            {
                sqlCon.Close();
            }
        }

        //hàm truy vấn một dòng dữ liệu
        public DataRow Select(string sql)
        {
            try
            {
                //mở kết nối
                sqlCon.Open();
                //truyền giá trị vào cmd
                cmd = new SqlCommand(sql, sqlCon);
                dt = new DataTable();
                //thực thi câu lệnh
                dt.Load(cmd.ExecuteReader());
                //trả về kết quả
                return dt.Rows[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thông tin chi tiết: " + ex.Message);
                return null;
            }
            finally
            {
                //đóng kết nối
                sqlCon.Close();
            }
        }
    }

    //lớp CustomParameter. key, value là từ khóa có giá trị
    public class CustomParameter
    {
        public string key
        {
            get;
            set;
        }
        public string value
        {
            get;
            set;

        }
    }
}
