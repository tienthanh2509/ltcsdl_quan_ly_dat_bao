using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QL_DatBao.Class
{
    class XL_BANG : DataTable
    {
        //
        public static string connectionString;
        //
        public SqlConnection con;
        public SqlDataAdapter da;
        public DataTable dt;
        public SqlCommand cmd;
        //
        protected string sql;
        public string tableName;
        #region Set Get thuộc tính
        //public string Sql
        //{
        //    get { return sql; }
        //    set { sql = value; }
        //}
        //public string TableName
        //{
        //    get { return tableName; }
        //    set { tableName = value; }
        //}
        public int Count
        {
            get { return this.DefaultView.Count; }
        }
        #endregion

        #region Phương thức khởi tạo
        public XL_BANG() : base()
        {
            this.tableName = this.sql = "";
            con = new SqlConnection(XL_BANG.connectionString);
        }

        public XL_BANG(string tableName) : base()
        {
            this.tableName = tableName;
            this.sql = "";
            con = new SqlConnection(XL_BANG.connectionString);
            Read();
        }

        public XL_BANG(string tableName, string sql) : base()
        {
            this.tableName = tableName;
            this.sql = sql;
            con = new SqlConnection(XL_BANG.connectionString);
            Read();
        }
        #endregion

        #region Hàm xử lý truy vấn
        public void close()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public SqlCommand getCMD(string sql)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();

            cmd = new SqlCommand(sql, con);

            return cmd;
        }
        public DataTable ExecuteQuery(string sql)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da = new SqlDataAdapter(sql, con);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int ExecuteNonQuery(string strSQL)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand(strSQL, con);

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return -1;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public Object ExecuteScalar(string function)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand(function, con);
                Object result = cmd.ExecuteScalar();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        #endregion

        #region Các phương thức xử lý, thao tác trên bảng: đọc, ghi, lọc, cập nhật
        // Đọc Dữ Liệu
        public void Read()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (sql == "")
                    sql = "SELECT * FROM " + tableName;

                da = new SqlDataAdapter(sql, con);
                da.FillSchema(this, SchemaType.Mapped);
                da.Fill(this);
                da.RowUpdated += new SqlRowUpdatedEventHandler(SqlRowUpdatedEventHandler_RowUpdated);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Ghi Dữ Liệu
        public bool Write()
        {
            try
            {
                da.Update(this);
                this.AcceptChanges();

                return true;
            }
            catch (SqlException ex)
            {
                this.RejectChanges();
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        //Lọc Dữ Liệu
        public void Filter(string dieu_kien)
        {
            try
            {
                this.DefaultView.RowFilter = dieu_kien;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

        #region Xử lý xự kiện
        // Xử lý sự kiện Rowupdated đối với trường khóa chính có kiểu Autonumber
        private void SqlRowUpdatedEventHandler_RowUpdated(Object sender, SqlRowUpdatedEventArgs e)
        {
            if (this.PrimaryKey[0].AutoIncrement)
            {
                if ((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
                {
                    SqlCommand cmd = new SqlCommand("Select @@IDENTITY ", con);
                    e.Row.ItemArray[0] = cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }
        #endregion
    }
}
