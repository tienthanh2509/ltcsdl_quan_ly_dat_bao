using System;
using System.Data;

namespace QL_DatBao.Class
{
    class XL_TAPCHI : XL_BANG
    {
        #region Phương thức khởi tạo
        public XL_TAPCHI() : base("TAPCHI") { }
        public XL_TAPCHI(string sql) : base("TAPCHI", sql) { }
        #endregion

        public DataTable FindBySOPHIEU(string SOPHIEU)
        {
            return ExecuteQuery("SELECT"
                            + "   TAPCHI.MATC,"
                            + "   TAPCHI.TENTC,"
                            + "   TAPCHI.LOAITC,"
                            + "   TAPCHI.NGAYPH,"
                            + "   TAPCHI.NHAXB"
                            + " FROM PHIEUDATBAO"
                            + " INNER JOIN CTDATBAO"
                            + "   ON CTDATBAO.SOPHIEU = PHIEUDATBAO.SOPHIEU"
                            + " INNER JOIN TAPCHI"
                            + "   ON CTDATBAO.MATC = TAPCHI.MATC"
                            + " WHERE PHIEUDATBAO.SOPHIEU = '" + SOPHIEU + "'");
        }
    }
}
