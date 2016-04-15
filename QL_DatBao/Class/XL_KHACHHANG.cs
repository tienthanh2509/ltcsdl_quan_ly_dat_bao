using System.Data;

namespace QL_DatBao.Class
{
    class XL_KHACHHANG : XL_BANG
    {
        #region Phương thức khởi tạo
        public XL_KHACHHANG() : base("KHACHHANG") { }
        public XL_KHACHHANG(string sql) : base("KHACHHANG", sql) { }
        #endregion
    }
}
