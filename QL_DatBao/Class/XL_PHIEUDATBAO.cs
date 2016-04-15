using System;
using System.Data;

namespace QL_DatBao.Class
{
    class XL_PHIEUDATBAO : XL_BANG
    {
        #region Phương thức khởi tạo
        public XL_PHIEUDATBAO() : base("PHIEUDATBAO") { }
        public XL_PHIEUDATBAO(string sql) : base("PHIEUDATBAO", sql) { }
        #endregion

        public DataTable layDsChiTiet(String MAKH)
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
                                  + " WHERE MAKH = '" + MAKH + "'");
        }
    }
}
