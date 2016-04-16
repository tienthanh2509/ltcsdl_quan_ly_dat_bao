using System;
using System.Windows.Forms;

namespace QL_DatBao
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MH_Dang_Nhap f = new MH_Dang_Nhap();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);

            if(!f.is_login)
                Application.Exit();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            Main_Load(sender, e);
        }

        private void btn_form_dat_Bao_Click(object sender, EventArgs e)
        {
            MH_Dat_Bao f = new MH_Dat_Bao();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            MH_Khach_Hang f = new MH_Khach_Hang();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }
    }
}
