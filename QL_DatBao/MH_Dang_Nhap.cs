﻿using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_DatBao
{
    public partial class MH_Dang_Nhap : Form
    {
        public bool is_login = false;

        public MH_Dang_Nhap()
        {
            InitializeComponent();
        }

        private void frm_Dang_Nhap_Load(object sender, EventArgs e)
        {
            cb_Authentication.SelectedIndex = 0;
            //bt_Dang_Nhap.PerformClick();
        }

        private void cb_Authentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hide = true;
            if (cb_Authentication.SelectedIndex == 0)
                hide = false;
            txtUserName.Enabled = hide;
            txtPassword.Enabled = hide;
        }

        private void bt_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bt_Dang_Nhap_Click(object sender, EventArgs e)
        {
            if (cb_Authentication.SelectedIndex == 0)
                Class.XL_BANG.connectionString = "Data Source=" + txtServerName.Text
                    + ";Initial Catalog=" + txt_database.Text
                    + ";Integrated Security=True";
            else
                Class.XL_BANG.connectionString = "Data Source=" + txtServerName.Text
                    + ";Initial Catalog=" + txt_database.Text
                    + ";User ID=" + txtUserName.Text
                    + ";Password=" + txtPassword.Text;
            SqlConnection cnn = new SqlConnection(Class.XL_BANG.connectionString);
            try
            {
                cnn.Open();
                is_login = true;
                cnn.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void form_on_keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bt_Dang_Nhap.PerformClick();
            else if (e.KeyCode == Keys.Escape)
                bt_Thoat.PerformClick();
        }

    }
}
