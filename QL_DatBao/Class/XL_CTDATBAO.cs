using System;
using System.Data;

namespace QL_DatBao.Class
{
    class XL_CTDATBAO : XL_BANG
    {
        #region Phương thức khởi tạo
        public XL_CTDATBAO() : base("CTDATBAO") { }
        public XL_CTDATBAO(string sql) : base("CTDATBAO", sql) { }
        #endregion

        public DataTable layDsChiTiet(string SOPHIEU, string MAKH)
        {
            return ExecuteQuery("SELECT"
                                  + "   PHIEUDATBAO.SOPHIEU,"
                                  + "   CTDATBAO.THANGBD, "
                                  + "   CTDATBAO.THANGKT, "
                                  + "   CTDATBAO.SOTIEN, "
                                  + "   TAPCHI.MATC, "
                                  + "   TAPCHI.TENTC"
                                  + " FROM dbo.PHIEUDATBAO"
                                  + " INNER JOIN dbo.CTDATBAO"
                                  + "   ON CTDATBAO.SOPHIEU = PHIEUDATBAO.SOPHIEU"
                                  + " INNER JOIN dbo.TAPCHI"
                                  + "  ON CTDATBAO.MATC = TAPCHI.MATC"
                                  + " WHERE PHIEUDATBAO.SOPHIEU = '" + SOPHIEU + "' AND PHIEUDATBAO.MAKH = '" + MAKH + "'");
        }

        public DataTable dulieureport(string SOPHIEU, string MAKH)
        {
            return ExecuteQuery("SELECT "
                                + "  PHIEUDATBAO.SOPHIEU, "
                                + "  PHIEUDATBAO.NGAYDAT, "
                                + "  PHIEUDATBAO.TONGSOTIEN, "
                                + "  CTDATBAO.THANGBD, "
                                + "  CTDATBAO.THANGKT, "
                                + "  CTDATBAO.SOTIEN, "
                                + "  TAPCHI.MATC, "
                                + "  TAPCHI.TENTC, "
                                + "  KHACHHANG.TENKH, "
                                + "  KHACHHANG.MAKH "
                                + "FROM dbo.CTDATBAO "
                                + "INNER JOIN dbo.PHIEUDATBAO "
                                + "  ON CTDATBAO.SOPHIEU = PHIEUDATBAO.SOPHIEU "
                                + "INNER JOIN dbo.TAPCHI "
                                + "  ON CTDATBAO.MATC = TAPCHI.MATC "
                                + "INNER JOIN dbo.KHACHHANG "
                                + "  ON PHIEUDATBAO.MAKH = KHACHHANG.MAKH "
                                + " WHERE PHIEUDATBAO.SOPHIEU = '" + SOPHIEU + "' AND PHIEUDATBAO.MAKH = '" + MAKH + "'");
        }
    }
}
