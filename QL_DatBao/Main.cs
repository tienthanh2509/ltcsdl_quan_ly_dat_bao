using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            MH_Dat_Bao ff2 = new MH_Dat_Bao();
            ff2.ShowDialog(this);
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
    }
}
