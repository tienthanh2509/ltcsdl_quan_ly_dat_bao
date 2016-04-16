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
            //
            Bang_KHACHHANG.Columns["MAKH"].ReadOnly = true;
            cb_makh.DataSource = Bang_KHACHHANG;
            cb_makh.DisplayMember = cb_makh.ValueMember = "MAKH";
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
            if(cb_makh.SelectedValue != null)
            DS_KHACHHANG_PositionChanged(sender, e);
        }

        private void DS_KHACHHANG_PositionChanged(object sender, EventArgs e)
        {
            if (btn_ghi.Enabled)
                btn_khongghi.PerformClick();

            // Load chi tiết đặt báo vào gridview
            dg_chitietdatbao.DataSource = Bang_CTDATBAO.layDsChiTiet(txt_sophieu.Text, cb_makh.SelectedValue.ToString());
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
            DS_PHIEUDATBAO.Position = DS_PHIEUDATBAO.Count;
        }

        private void dg_chitietdatbao_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tong = 0;

            foreach (DataGridViewRow r in dg_chitietdatbao.Rows)
            {
                r.Cells["STT"].Value = r.Index + 1;

                if(r.Cells["SOTIEN"].Value != null)
                tong += long.Parse(r.Cells["SOTIEN"].Value.ToString(), NumberStyles.Currency);
            }

            txt_tongsotien.Text = tong.ToString("#,0");
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

        }

        private void btn_sua_Click(object sender, EventArgs e)
        {

        }

        private void btn_ghi_Click(object sender, EventArgs e)
        {

        }

        private void btn_khongghi_Click(object sender, EventArgs e)
        {

        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(Bang_KHACHHANG.Copy());
            DataTable dt = Bang_CTDATBAO.layDsChiTiet(txt_sophieu.Text, cb_makh.SelectedValue.ToString());
            dt.TableName = "CHITIET";
            ds.Tables.Add(dt.Copy());
            dt.Dispose();

            MH_In_BC f = new MH_In_BC(ds);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }
    }
}
