using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class MH_Chinh : Form
    {
        public MH_Chinh()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            btn_dang_nhap_Click(sender, e);
        }

        //private void buttonItem3_Click(object sender, EventArgs e)
        //{
        //    MH_Khach_Hang f = new MH_Khach_Hang();
        //    //f.Dock = DockStyle.Fill;
        //    //f.FormBorderStyle = FormBorderStyle.None;
        //    //f.TopLevel = false;
        //    f.Show();
        //}

        private void btn_dang_nhap_Click(object sender, EventArgs e)
        {
            MH_Dang_Nhap f = new MH_Dang_Nhap();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);

            if (!f.is_login)
                Application.Exit();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool is_btn_dattapchi_open = false;
        private void btn_dattapchi_Click(object sender, EventArgs e)
        {
            // Reset tab
            if (is_btn_dattapchi_open)
                return;
            TabItem tab_datbao = tabControl1.CreateTab("Đặt Tạp Chí");
            // Create tab
            MH_Dat_Bao f = new MH_Dat_Bao();
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            f.TopLevel = false;
            tab_datbao.AttachedControl.Controls.Add(f);
            f.Show();
            tabControl1.SelectedTabIndex = tabControl1.Tabs.Count - 1;
            //
            is_btn_dattapchi_open = true;
        }

        bool is_btn_khachhang_open = false;
        private void btn_khachhang_Click(object sender, EventArgs e)
        {
            // Reset tab
            if (is_btn_khachhang_open)
                return;
            TabItem tab_datbao = tabControl1.CreateTab("DS Khách Hàng");
            // Create tab
            MH_Khach_Hang f = new MH_Khach_Hang();
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;
            f.TopLevel = false;
            tab_datbao.AttachedControl.Controls.Add(f);
            f.Show();
            tabControl1.SelectedTabIndex = tabControl1.Tabs.Count - 1;
            //
            is_btn_khachhang_open = true;
        }
    }
}
