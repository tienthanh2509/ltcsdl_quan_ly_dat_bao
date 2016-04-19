using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class MH_Dat_Bao : Form
    {
        Class.XL_PHIEUDATBAO Bang_PHIEUDATBAO;
        BindingManagerBase DS_PHIEUDATBAO;
        //
        Class.XL_KHACHHANG Bang_KHACHHANG;
        BindingManagerBase DS_KHACHHANG;
        //
        Class.XL_CTDATBAO Bang_CTDATBAO;
        Class.XL_TAPCHI Bang_TAPCHI;
        //
        private bool is_editting = false;

        public MH_Dat_Bao()
        {
            InitializeComponent();
        }

        private void MH_Dat_Bao_Load(object sender, EventArgs e)
        {
            // Khởi tạo
            Bang_PHIEUDATBAO = new Class.XL_PHIEUDATBAO();
            Bang_KHACHHANG = new Class.XL_KHACHHANG();
            Bang_CTDATBAO = new Class.XL_CTDATBAO();
            Bang_TAPCHI = new Class.XL_TAPCHI();
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
            // Load chi tiết đặt báo vào gridview
            dg_chitietdatbao.DataSource = Bang_CTDATBAO.layDsChiTiet(txt_sophieu.Text, cb_makh.SelectedValue.ToString());
            dg_chitietdatbao.Columns["SOPHIEU"].Visible = false;
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
            DS_PHIEUDATBAO.Position = DS_PHIEUDATBAO.Count -1;
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
            txt_sophieu.Enabled = true;
            txt_sophieu.Text = "";
            cb_makh.SelectedIndex = -1;
            txt_tenkh.Text = "";
            txt_diachi.Text = "";
            txt_sdt.Text = "";
            txt_tongsotien.Text = "N/a";
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = false;
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
            txt_sophieu.Enabled = false;
            is_editting = true;
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = false;
        }

        private void btn_ghi_Click(object sender, EventArgs e)
        {
            try
            {
                if (is_editting)
                {
                    //Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position]["MAKH"] = cb_makh.SelectedValue;
                    //Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position]["NGAYDAT"] = dtp_ngaydat.Value;
                    //Bang_PHIEUDATBAO.Rows[DS_PHIEUDATBAO.Position].AcceptChanges();
                    //Bang_PHIEUDATBAO.Write();
                    DataRow r = Bang_PHIEUDATBAO.NewRow();
                    r["MAKH"] = cb_makh.SelectedValue;
                    r["SOPHIEU"] = txt_sophieu.Text.Trim();
                    r["NGAYDAT"] = dtp_ngaydat.Value;
                    //
                    System.Data.SqlClient.SqlConnection con = Bang_PHIEUDATBAO.con;
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UPDATE PHIEUDATBAO SET MAKH = @MAKH, NGAYDAT = @NGAYDAT WHERE SOPHIEU = @SOPHIEU", con);
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
                    System.Data.SqlClient.SqlConnection con = Bang_PHIEUDATBAO.con;
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO PHIEUDATBAO (SOPHIEU, MAKH, NGAYDAT) VALUES(@SOPHIEU, @MAKH, @NGAYDAT)", con);
                    cmd.Parameters.AddWithValue("@SOPHIEU", r["SOPHIEU"]);
                    cmd.Parameters.AddWithValue("@NGAYDAT", r["NGAYDAT"]);
                    cmd.Parameters.AddWithValue("@MAKH", r["MAKH"]);
                    cmd.ExecuteNonQuery();
                    //
                    Bang_PHIEUDATBAO.Read();
                    btn_cuoi.PerformClick();
                    //
                    //Bang_PHIEUDATBAO.Rows.Add(r);
                    //Bang_PHIEUDATBAO.AcceptChanges();
                    //Bang_PHIEUDATBAO.Write();
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
                txt_sophieu.Enabled = false;
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
            txt_sophieu.Enabled = false;
            //
            Bang_PHIEUDATBAO.RejectChanges();
            Bang_KHACHHANG.RejectChanges();
            Bang_TAPCHI.RejectChanges();
            Bang_CTDATBAO.RejectChanges();
            //
            btn_dau.Enabled = btn_cuoi.Enabled = btn_lui.Enabled = btn_toi.Enabled = true;
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
    }
}
