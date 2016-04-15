using System;
using System.Data;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class MH_Dat_Bao : Form
    {
        DataTable Bang_KHACHHANG;
        BindingManagerBase DS_KHACHHANG;
        //
        Class.XL_PHIEUDATBAO xlpbd;

        public MH_Dat_Bao()
        {
            InitializeComponent();
        }

        private void MH_Dat_Bao_Load(object sender, EventArgs e)
        {
            Bang_KHACHHANG = new Class.XL_KHACHHANG();
            Bang_KHACHHANG.Columns["MAKH"].ReadOnly = true;

            // Đổ dữ liệu
            cb_makh.DataSource = Bang_KHACHHANG;
            cb_makh.DisplayMember = cb_makh.ValueMember = "MAKH";
            // Liên kết dữ liệu
            txt_sdt.DataBindings.Add("text", Bang_KHACHHANG, "DIENTHOAI");
            txt_tenkh.DataBindings.Add("text", Bang_KHACHHANG, "TENKH");
            txt_diachi.DataBindings.Add("text", Bang_KHACHHANG, "DIACHI");
            //
            DS_KHACHHANG = this.BindingContext[Bang_KHACHHANG];
            DS_KHACHHANG.PositionChanged += DS_KHACHHANG_PositionChanged;

            //
            xlpbd = new Class.XL_PHIEUDATBAO();
            if (cb_makh.SelectedIndex >= 0)
                dg_chitietdatbao.DataSource = xlpbd.layDsChiTiet(cb_makh.SelectedValue.ToString());
        }

        private void DS_KHACHHANG_PositionChanged(object sender, EventArgs e)
        {
            if (btn_ghi.Enabled)
                btn_khongghi.PerformClick();

            // Load chi tiết đặt báo vào gridview
            dg_chitietdatbao.DataSource = xlpbd.layDsChiTiet(cb_makh.SelectedValue.ToString());
        }

        private void btn_dau_Click(object sender, EventArgs e)
        {
            DS_KHACHHANG.Position = 0;
        }

        private void btn_lui_Click(object sender, EventArgs e)
        {
            DS_KHACHHANG.Position += -1;
        }

        private void btn_toi_Click(object sender, EventArgs e)
        {
            DS_KHACHHANG.Position += 1;
        }

        private void btn_cuoi_Click(object sender, EventArgs e)
        {
            DS_KHACHHANG.Position = DS_KHACHHANG.Count;
        }

        private void dg_chitietdatbao_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dg_chitietdatbao.Rows)
            {
                r.Cells["Column1"].Value = r.Index + 1;
            }
        }
    }
}
