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

        public DataTable FindBySOPHIEU(string SOPHIEU)
        {
            return ExecuteQuery("SELECT * FROM PHIEUDATBAO WHERE SOPHIEU = '" + SOPHIEU + "'");
        }
    }
}
