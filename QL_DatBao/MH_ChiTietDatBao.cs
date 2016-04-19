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
    public partial class MH_ChiTietDatBao : Form
    {
        // 0 = add, 1 = new
        private int mode = 0;
        public MH_Dat_Bao f;

        public MH_ChiTietDatBao(int mode = 0)
        {
            this.mode = mode;
            InitializeComponent();
        }

        private void MH_ChiTietDatBao_Load(object sender, EventArgs e)
        {
            if(mode == 0)
            {

            }
            else if(mode == 1)
            {

            }
            else
            {
                MessageBox.Show("Error!");
                this.Close();
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {

        }
    }
}
