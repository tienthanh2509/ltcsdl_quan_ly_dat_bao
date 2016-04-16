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
    public partial class MH_Khach_Hang : Form
    {
        Class.XL_KHACHHANG Bang_KhachHang;
        public MH_Khach_Hang()
        {
            InitializeComponent();
        }

        private void MH_Khach_Hang_Load(object sender, EventArgs e)
        {
            Bang_KhachHang = new Class.XL_KHACHHANG();
            dg_danhsachkhachhang.DataSource = Bang_KhachHang;
        }

        private void btn_lamtuoi_Click(object sender, EventArgs e)
        {
            Bang_KhachHang.Dispose();
            Bang_KhachHang = new Class.XL_KHACHHANG();
            dg_danhsachkhachhang.DataSource = Bang_KhachHang;
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            Bang_KhachHang.Write();
            //MessageBox.Show("Đã thêm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (dg_danhsachkhachhang.Rows[0].Cells[0].Value == null)
                return;

            int CurrentIndex = dg_danhsachkhachhang.CurrentCell.RowIndex;
            string MAKH = dg_danhsachkhachhang.Rows[CurrentIndex].Cells[0].Value.ToString();
            string TENKH = dg_danhsachkhachhang.Rows[CurrentIndex].Cells[1].Value.ToString();
            if (MAKH == null)
                return;

            DialogResult r = MessageBox.Show("Bạn có muốn xóa khách hàng ["+ CurrentIndex + " - " + MAKH + " - " + TENKH + "] đang chọn không?", "Hỏi???", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(r == DialogResult.Yes)
            {
                dg_danhsachkhachhang.Rows.RemoveAt(CurrentIndex);
                btn_luu.PerformClick();
            }
        }
    }
}
