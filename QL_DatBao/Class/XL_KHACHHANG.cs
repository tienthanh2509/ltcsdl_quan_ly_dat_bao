using System.Data;

namespace QL_DatBao.Class
{
    class XL_KHACHHANG : XL_BANG
    {
        #region Phương thức khởi tạo
        public XL_KHACHHANG() : base("KHACHHANG") { }
        public XL_KHACHHANG(string sql) : base("KHACHHANG", sql) { }
        #endregion

        public DataTable FindByMAKH(string MAKH)
        {
            return ExecuteQuery("SELECT * FROM KHACHHANG WHERE MAKH = '" + MAKH + "'");
        }
    }
}
