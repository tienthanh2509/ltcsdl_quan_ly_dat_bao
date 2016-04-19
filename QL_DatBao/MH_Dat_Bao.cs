using QL_DatBao.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class MH_Dat_Bao : Form
    {
        XL_PHIEUDATBAO Bang_PHIEUDATBAO;
        BindingManagerBase DS_PHIEUDATBAO;
        //
        XL_KHACHHANG Bang_KHACHHANG;
        BindingManagerBase DS_KHACHHANG;
        //
        XL_CTDATBAO Bang_CTDATBAO;
        XL_TAPCHI Bang_TAPCHI;
        //
        private bool is_editting = false;

        internal XL_TAPCHI _Bang_TAPCHI
        {
            get
            {
                return Bang_TAPCHI;
            }

            set
            {
                Bang_TAPCHI = value;
            }
        }

        public MH_Dat_Bao()
        {
            InitializeComponent();
        }

        private void MH_Dat_Bao_Load(object sender, EventArgs e)
        {
            // Khởi tạo
            Bang_PHIEUDATBAO = new XL_PHIEUDATBAO();
            Bang_KHACHHANG = new XL_KHACHHANG();
            Bang_CTDATBAO = new XL_CTDATBAO();
            Bang_TAPCHI = new XL_TAPCHI();
            //
            Bang_KHACHHANG.Columns["MAKH"].ReadOnly = true;
            cb_makh.DataSource = Bang_KHACHHANG;
            cb_makh.DisplayMember = "MAKH";
            cb_makh.ValueMember = "MAKH";
            cb_makh.SelectedIndex = -1;

            // Liên kết dữ liệu
            txt_sophieu.DataBindings.Add("text", Bang_PHIEUDATBAO, "SOPHIEU");
            dtp_ngaydat.DataBindings.Add("Value", Bang_PHIEUDATBAO, "NGAYDAT");
            //
            txt_sdt.DataBindings.Add("text", Bang_KHACHHANG, "DIENTHOAI");
            txt_tenkh.DataBindings.Add("text", Bang_KHACHHANG, "TENKH");
            txt_diachi.DataBindings.Add("text", Bang_KHACHHANG, "DIACHI");

            // Thiết lập sự kiện khi liên kết dữ liệu
            DS_KHACHHANG = this.BindingContext[Bang_KHACHHANG];
            DS_KHACHHANG.PositionChanged += DS_KHACHHANG_PositionChanged;
            //
            DS_PHIEUDATBAO = this.BindingContext[Bang_PHIEUDATBAO];
            DS_PHIEUDATBAO.PositionChanged += DS_PHIEUDATBAO_PositionChanged;

            // Default value
            if (Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position][1] != null)
                DS_PHIEUDATBAO_PositionChanged(sender, e);
            if (cb_makh.SelectedValue != null)
                DS_KHACHHANG_PositionChanged(sender, e);
        }

        private void DS_KHACHHANG_PositionChanged(object sender, EventArgs e)
        {
            update_control_ctdatbao();
        }

        private void DS_PHIEUDATBAO_PositionChanged(object sender, EventArgs e)
        {

            cb_makh.SelectedValue = Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position][1].ToString();
        }

        private void btn_dau_Click(object sender, EventArgs e)
        {
            DS_PHIEUDATBAO.Position = 0;
        }

        private void btn_lui_Click(object sender, EventArgs e)
        {
            DS_PHIEUDATBAO.Position += -1;
        }

        private void btn_toi_Click(object sender, EventArgs e)
        {
            DS_PHIEUDATBAO.Position += 1;
        }

        private void btn_cuoi_Click(object sender, EventArgs e)
        {
            DS_PHIEUDATBAO.Position = DS_PHIEUDATBAO.Count - 1;
        }

        private void dg_chitietdatbao_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tong = 0;

            foreach (DataGridViewRow r in dg_chitietdatbao.Rows)
            {
                r.Cells["STT"].Value = r.Index + 1;

                if (r.Cells["SOTIEN"].Value != null)
                    tong += long.Parse(r.Cells["SOTIEN"].Value.ToString(), NumberStyles.Currency);
            }

            txt_tongsotien.Text = tong.ToString("#,0");
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            btn_them.Enabled = !btn_them.Enabled;
            btn_sua.Enabled = !btn_sua.Enabled;
            btn_xoa.Enabled = !btn_xoa.Enabled;
            btn_ghi.Enabled = !btn_ghi.Enabled;
            btn_khongghi.Enabled = !btn_khongghi.Enabled;
            btn_in.Enabled = !btn_in.Enabled;
            //
            cb_makh.Enabled = !cb_makh.Enabled;
            //dtp_ngaydat.Enabled = false;
            dtp_ngaydat.Value = DateTime.Now;
            txt_sophieu.ReadOnly = false;
            txt_sophieu.Text = "";
            cb_makh.SelectedIndex = -1;
            txt_tenkh.Text = "";
            txt_diachi.Text = "";
            txt_sdt.Text = "";
            txt_tongsotien.Text = "N/a";
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = false;
            btn_chitiet_them.Enabled = btn_chitiet_sua.Enabled = btn_chitiet_xoa.Enabled = false;
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (Bang_PHIEUDATBAO.Count < 1)
                return;

            DialogResult r = MessageBox.Show("Bạn có muốn xóa đơn hàng đang chọn không?", "Hỏi???", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (r == DialogResult.Yes)
            {
                Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position].Delete();
                Bang_PHIEUDATBAO.Write();

                update_control_ctdatbao();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            btn_them.Enabled = !btn_them.Enabled;
            btn_sua.Enabled = !btn_sua.Enabled;
            btn_xoa.Enabled = !btn_xoa.Enabled;
            btn_ghi.Enabled = !btn_ghi.Enabled;
            btn_khongghi.Enabled = !btn_khongghi.Enabled;
            btn_in.Enabled = !btn_in.Enabled;
            //
            cb_makh.Enabled = !cb_makh.Enabled;
            dtp_ngaydat.Enabled = true;
            //
            txt_sophieu.ReadOnly = true;
            is_editting = true;
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = false;
            btn_chitiet_them.Enabled = btn_chitiet_sua.Enabled = btn_chitiet_xoa.Enabled = false;
        }

        private void btn_ghi_Click(object sender, EventArgs e)
        {
            try
            {
                if (is_editting)
                {
                    DataRow r = Bang_PHIEUDATBAO.NewRow();
                    r["MAKH"] = cb_makh.SelectedValue;
                    r["SOPHIEU"] = txt_sophieu.Text.Trim();
                    r["NGAYDAT"] = dtp_ngaydat.Value;
                    //
                    SqlConnection con = Bang_PHIEUDATBAO.con;
                    SqlCommand cmd = new SqlCommand("UPDATE PHIEUDATBAO SET MAKH = @MAKH, NGAYDAT = @NGAYDAT WHERE SOPHIEU = @SOPHIEU", con);
                    cmd.Parameters.AddWithValue("@SOPHIEU", r["SOPHIEU"]);
                    cmd.Parameters.AddWithValue("@NGAYDAT", r["NGAYDAT"]);
                    cmd.Parameters.AddWithValue("@MAKH", r["MAKH"]);
                    cmd.ExecuteNonQuery();
                    //
                    Bang_PHIEUDATBAO.Read();
                    is_editting = false;
                }
                else
                {
                    DataRow r = Bang_PHIEUDATBAO.NewRow();
                    r["MAKH"] = cb_makh.SelectedValue;
                    r["SOPHIEU"] = txt_sophieu.Text.Trim();
                    r["NGAYDAT"] = dtp_ngaydat.Value;
                    //
                    SqlConnection con = Bang_PHIEUDATBAO.con;
                    SqlCommand cmd = new SqlCommand("INSERT INTO PHIEUDATBAO (SOPHIEU, MAKH, NGAYDAT) VALUES(@SOPHIEU, @MAKH, @NGAYDAT)", con);
                    cmd.Parameters.AddWithValue("@SOPHIEU", r["SOPHIEU"]);
                    cmd.Parameters.AddWithValue("@NGAYDAT", r["NGAYDAT"]);
                    cmd.Parameters.AddWithValue("@MAKH", r["MAKH"]);
                    cmd.ExecuteNonQuery();
                    //
                    Bang_PHIEUDATBAO.Read();
                    btn_cuoi.PerformClick();
                }

                btn_them.Enabled = !btn_them.Enabled;
                btn_sua.Enabled = !btn_sua.Enabled;
                btn_xoa.Enabled = !btn_xoa.Enabled;
                btn_ghi.Enabled = !btn_ghi.Enabled;
                btn_khongghi.Enabled = !btn_khongghi.Enabled;
                btn_in.Enabled = !btn_in.Enabled;
                //
                cb_makh.Enabled = !cb_makh.Enabled;
                dtp_ngaydat.Enabled = false;
                txt_sophieu.ReadOnly = false;
                //
                btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = true;
                //
                Bang_KHACHHANG.Read();
                Bang_TAPCHI.Read();
                Bang_PHIEUDATBAO.Read();
                Bang_CTDATBAO.Read();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_khongghi_Click(object sender, EventArgs e)
        {
            btn_them.Enabled = !btn_them.Enabled;
            btn_sua.Enabled = !btn_sua.Enabled;
            btn_xoa.Enabled = !btn_xoa.Enabled;
            btn_ghi.Enabled = !btn_ghi.Enabled;
            btn_khongghi.Enabled = !btn_khongghi.Enabled;
            btn_in.Enabled = !btn_in.Enabled;
            //
            cb_makh.Enabled = !cb_makh.Enabled;
            dtp_ngaydat.Enabled = false;
            txt_sophieu.ReadOnly = true;
            //
            Bang_PHIEUDATBAO.RejectChanges();
            Bang_KHACHHANG.RejectChanges();
            Bang_TAPCHI.RejectChanges();
            Bang_CTDATBAO.RejectChanges();
            //
            DS_KHACHHANG.CancelCurrentEdit();
            DS_PHIEUDATBAO.CancelCurrentEdit();
            cb_makh.SelectedValue = Bang_KHACHHANG.Rows[DS_KHACHHANG.Position]["MAKH"];
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = true;

            if (dg_chitietdatbao.Rows.Count > 0)
            {
                btn_chitiet_them.Enabled = true;
                btn_chitiet_sua.Enabled = true;
                btn_chitiet_xoa.Enabled = true;
            }
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            string MAKH = cb_makh.SelectedValue.ToString();
            string SOPHIEU = txt_sophieu.Text.Trim();
            DataSet ds = new DataSet();
            DataTable[] dt = new DataTable[4];
            // Rút trích dữ liệu bảng PHIEUDATBAO
            dt[0] = Bang_PHIEUDATBAO.FindBySOPHIEU(SOPHIEU);
            dt[0].TableName = "PHIEUDATBAO";
            ds.Tables.Add(dt[0].Copy());
            dt[0].Dispose();
            // Rút trích dữ liệu bảng CTDATBAO
            dt[1] = Bang_CTDATBAO.FindBySOPHIEU(SOPHIEU);
            dt[1].TableName = "CTDATBAO";
            //
            if (dt[1].Rows.Count < 1)
            {
                MessageBox.Show("Không có dữ liệu gì để in!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ds.Tables.Add(dt[1].Copy());
            dt[1].Dispose();
            // Rút trích dữ liệu bảng TAPCHI
            dt[2] = Bang_TAPCHI.FindBySOPHIEU(SOPHIEU);
            dt[2].TableName = "TAPCHI";
            ds.Tables.Add(dt[2].Copy());
            dt[2].Dispose();
            // Rút trích dữ liệu bảng KHACHHANG
            dt[3] = Bang_KHACHHANG.FindByMAKH(MAKH);
            dt[3].TableName = "KHACHHANG";
            ds.Tables.Add(dt[3].Copy());
            dt[3].Dispose();
            // Free Memory
            dt = null;
            //
            MH_In_BC f = new MH_In_BC(ds);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        private void btn_refresh_khachhang_Click(object sender, EventArgs e)
        {
            Bang_KHACHHANG.Read();
        }

        // Cập nhật trạng thái nút thao tác với chi tiết đặt báo
        private void update_control_ctdatbao()
        {
            // Load chi tiết đặt báo vào gridview
            dg_chitietdatbao.DataSource = Bang_CTDATBAO.layDsChiTiet(txt_sophieu.Text, cb_makh.SelectedValue.ToString());
            dg_chitietdatbao.Columns["SOPHIEU"].Visible = false;

            if (dg_chitietdatbao.Rows.Count > 0)
            {
                btn_chitiet_sua.Enabled = true;
                btn_chitiet_xoa.Enabled = true;
            }
            else
            {
                btn_chitiet_sua.Enabled = false;
                btn_chitiet_xoa.Enabled = false;
            }
        }

        private void btn_chitiet_them_Click(object sender, EventArgs e)
        {
            MH_ChiTietDatBao f = new MH_ChiTietDatBao(0, txt_sophieu.Text, dg_chitietdatbao.CurrentRow);
            f.ShowDialog();

            update_control_ctdatbao();
        }

        private void btn_chitiet_sua_Click(object sender, EventArgs e)
        {
            MH_ChiTietDatBao f = new MH_ChiTietDatBao(1, txt_sophieu.Text, dg_chitietdatbao.CurrentRow);
            f.ShowDialog(this);

            update_control_ctdatbao();
        }

        private void btn_chitiet_xoa_Click(object sender, EventArgs e)
        {
            if (dg_chitietdatbao.Rows.Count < 1)
                return;

            DialogResult r = MessageBox.Show("Bạn có muốn xóa chi tiết đơn hàng đang chọn không?", "Hỏi???", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (r == DialogResult.Yes)
            {
                // Kết nối CSDL
                SqlConnection con = Bang_CTDATBAO.con;

                try
                {
                    // Mở kết nối nếu chưa mở
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    // Khởi tạo sql cmd
                    SqlCommand cmd = con.CreateCommand();
                    // Gán chuỗi lệnh sql
                    cmd.CommandText = "DELETE FROM CTDATBAO WHERE SOPHIEU = @SOPHIEU AND MATC = @MATC";
                    // Gán tham số đi kèm
                    cmd.Parameters.AddWithValue("@SOPHIEU", txt_sophieu.Text);
                    cmd.Parameters.AddWithValue("@MATC", dg_chitietdatbao.CurrentRow.Cells["MATC"].Value.ToString());
                    // Thực thi câu lệnh và không trả dữ liệu về
                    cmd.ExecuteNonQuery();
                    // Cập nhật lưới
                    update_control_ctdatbao();
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
        }
    }
}
