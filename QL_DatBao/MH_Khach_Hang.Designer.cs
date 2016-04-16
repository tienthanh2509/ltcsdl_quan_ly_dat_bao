namespace QL_DatBao
{
    partial class MH_Khach_Hang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dg_danhsachkhachhang = new System.Windows.Forms.DataGridView();
            this.MAKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TENKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIACHI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIENTHOAI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_luu = new System.Windows.Forms.Button();
            this.btn_lamtuoi = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_danhsachkhachhang)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_danhsachkhachhang
            // 
            this.dg_danhsachkhachhang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_danhsachkhachhang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAKH,
            this.TENKH,
            this.DIACHI,
            this.DIENTHOAI});
            this.dg_danhsachkhachhang.Location = new System.Drawing.Point(16, 87);
            this.dg_danhsachkhachhang.Name = "dg_danhsachkhachhang";
            this.dg_danhsachkhachhang.Size = new System.Drawing.Size(658, 324);
            this.dg_danhsachkhachhang.TabIndex = 12;
            // 
            // MAKH
            // 
            this.MAKH.DataPropertyName = "MAKH";
            this.MAKH.HeaderText = "Mã KH";
            this.MAKH.Name = "MAKH";
            // 
            // TENKH
            // 
            this.TENKH.DataPropertyName = "TENKH";
            this.TENKH.HeaderText = "Tên khách hàng";
            this.TENKH.Name = "TENKH";
            this.TENKH.Width = 150;
            // 
            // DIACHI
            // 
            this.DIACHI.DataPropertyName = "DIACHI";
            this.DIACHI.HeaderText = "Địa chỉ";
            this.DIACHI.Name = "DIACHI";
            this.DIACHI.Width = 250;
            // 
            // DIENTHOAI
            // 
            this.DIENTHOAI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DIENTHOAI.DataPropertyName = "DIENTHOAI";
            this.DIENTHOAI.HeaderText = "Số điện thoại";
            this.DIENTHOAI.Name = "DIENTHOAI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(213, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "Danh sách khách hàng";
            // 
            // btn_luu
            // 
            this.btn_luu.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btn_luu.Location = new System.Drawing.Point(518, 58);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(75, 23);
            this.btn_luu.TabIndex = 14;
            this.btn_luu.Text = "Lưu";
            this.btn_luu.UseVisualStyleBackColor = true;
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // btn_lamtuoi
            // 
            this.btn_lamtuoi.ForeColor = System.Drawing.Color.ForestGreen;
            this.btn_lamtuoi.Location = new System.Drawing.Point(437, 58);
            this.btn_lamtuoi.Name = "btn_lamtuoi";
            this.btn_lamtuoi.Size = new System.Drawing.Size(75, 23);
            this.btn_lamtuoi.TabIndex = 15;
            this.btn_lamtuoi.Text = "Làm tươi";
            this.btn_lamtuoi.UseVisualStyleBackColor = true;
            this.btn_lamtuoi.Click += new System.EventHandler(this.btn_lamtuoi_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.ForeColor = System.Drawing.Color.Red;
            this.btn_xoa.Location = new System.Drawing.Point(599, 58);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(75, 23);
            this.btn_xoa.TabIndex = 16;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // MH_Khach_Hang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 423);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.btn_lamtuoi);
            this.Controls.Add(this.btn_luu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dg_danhsachkhachhang);
            this.Name = "MH_Khach_Hang";
            this.Text = "MH_Khach_Hang";
            this.Load += new System.EventHandler(this.MH_Khach_Hang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_danhsachkhachhang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dg_danhsachkhachhang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_luu;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TENKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIACHI;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIENTHOAI;
        private System.Windows.Forms.Button btn_lamtuoi;
        private System.Windows.Forms.Button btn_xoa;
    }
}