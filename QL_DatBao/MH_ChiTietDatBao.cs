using QL_DatBao.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class MH_ChiTietDatBao : Form
    {
        private int mode = 0;
        private string sophieu = "";
        private DataGridViewRow row = null;
        //
        private XL_TAPCHI Bang_TAPCHI;
        private BindingManagerBase DS_TAPCHI;


        public MH_ChiTietDatBao(int mode, string sophieu, DataGridViewRow row)
        {
            this.mode = mode;
            this.sophieu = sophieu;
            this.row = row;

            InitializeComponent();
        }

        private void MH_ChiTietDatBao_Load(object sender, EventArgs e)
        {
            try
            {
                //
                Bang_TAPCHI = new XL_TAPCHI();
                //
                cb_matc.DataSource = Bang_TAPCHI;
                cb_matc.DisplayMember = "MATC";
                cb_matc.ValueMember = "MATC";
                //
                txt_tentc.DataBindings.Add("text", Bang_TAPCHI, "TENTC");
                txt_nxb.DataBindings.Add("text", Bang_TAPCHI, "NHAXB");
                //
                DS_TAPCHI = BindingContext[Bang_TAPCHI];
                DS_TAPCHI.PositionChanged += DS_TAPCHI_PositionChanged;
                if (DS_TAPCHI.Count > 0)
                    DS_TAPCHI_PositionChanged(sender, e);

                txt_sophieu.Text = sophieu;

                if (mode == 0)
                {

                }
                else if (mode == 1)
                {
                    cb_matc.SelectedValue = row.Cells["MATC"].Value.ToString();
                    nb_thangbd.Value = int.Parse(row.Cells["THANGBD"].Value.ToString());
                    nb_thangkt.Value = int.Parse(row.Cells["THANGKT"].Value.ToString());
                    nb_sotien.Value = decimal.Parse(row.Cells["SOTIEN"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("Error!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            // Kết nối CSDL
            SqlConnection con = Bang_TAPCHI.con;

            try
            {
                // Mở kết nối nếu chưa mở
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Khởi tạo sql cmd
                SqlCommand cmd = con.CreateCommand();
                // Gán chuỗi lệnh sql
                if(mode == 0)
                {
                    // Kiểm tra tồn tại
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT MATC, SOPHIEU FROM CTDATBAO WHERE SOPHIEU = @SOPHIEU AND MATC = @MATC ";
                    cmd2.Parameters.AddWithValue("@SOPHIEU", txt_sophieu.Text);
                    cmd2.Parameters.AddWithValue("@MATC", cb_matc.SelectedValue);
                    SqlDataReader rd = cmd2.ExecuteReader();
                    bool ton_tai = rd.HasRows;
                    rd.Close();
                    cmd2.Dispose();

                    if (ton_tai)
                    {
                        DialogResult dr = MessageBox.Show("Đã tồn tại thông tin tạp chí này, ghi đè?", "Hỏi???", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                            // Cập nhật bản ghi
                            cmd.CommandText = "UPDATE CTDATBAO"
                            + " SET MATC = @MATC, THANGBD = @THANGBD, THANGKT = @THANGKT, SOTIEN = @SOTIEN "
                            + " WHERE SOPHIEU = @SOPHIEU AND MATC = @MATC ";
                        else
                            return;
                    }
                    else
                        // Chèn bản ghi mới
                        cmd.CommandText = "INSERT INTO CTDATBAO (SOPHIEU, MATC, THANGBD, THANGKT, SOTIEN) VALUES(@SOPHIEU, @MATC, @THANGBD, @THANGKT, @SOTIEN)";
                }
                else if(mode == 1)
                    cmd.CommandText = "UPDATE CTDATBAO"
                        + " SET MATC = @MATC, THANGBD = @THANGBD, THANGKT = @THANGKT, SOTIEN = @SOTIEN "
                        + " WHERE SOPHIEU = @SOPHIEU AND MATC = @MATC ";
                // Gán tham số đi kèm
                cmd.Parameters.AddWithValue("@SOPHIEU", txt_sophieu.Text);
                cmd.Parameters.AddWithValue("@MATC", cb_matc.SelectedValue);
                cmd.Parameters.AddWithValue("@THANGBD", nb_thangbd.Value);
                cmd.Parameters.AddWithValue("@THANGKT", nb_thangkt.Value);
                cmd.Parameters.AddWithValue("@SOTIEN", nb_sotien.Value);
                // Thực thi câu lệnh và không trả dữ liệu về
                cmd.ExecuteNonQuery();
                // Đóng cửa sổ
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Thử ngắt kết nối
                try
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
        }

        private void DS_TAPCHI_PositionChanged(object sender, EventArgs e)
        {
            lb_ngayphathanh.Text = string.Format("{0} - {1}", Bang_TAPCHI.Rows[DS_TAPCHI.Position]["LOAITC"].ToString(), Bang_TAPCHI.Rows[DS_TAPCHI.Position]["NGAYPH"].ToString());
        }
    }
}
