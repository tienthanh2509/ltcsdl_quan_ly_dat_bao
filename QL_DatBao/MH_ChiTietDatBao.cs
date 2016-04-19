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
        public MH_ChiTietDatBao()
        {
            InitializeComponent();
        }

        private void MH_ChiTietDatBao_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dSR_PhieuDatBao.TAPCHI' table. You can move, or remove it, as needed.
            this.tAPCHITableAdapter.Fill(this.dSR_PhieuDatBao.TAPCHI);

        }
    }
}
