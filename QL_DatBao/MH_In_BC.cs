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
    public partial class MH_In_BC : Form
    {
        private DataSet ds;
        public MH_In_BC(DataSet ds)
        {
            this.ds = ds;
            InitializeComponent();
        }

        private void MH_In_BC_Load(object sender, EventArgs e)
        {
            RP_PhieuDatBao rp = new RP_PhieuDatBao();
            rp.SetDataSource(this.ds);
            crystalReportViewer1.ReportSource = rp;
        }
    }
}
