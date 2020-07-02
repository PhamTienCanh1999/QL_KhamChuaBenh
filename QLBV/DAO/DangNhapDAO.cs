using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLBV.DAO
{
    public class DangNhapDAO
    {
        private static DangNhapDAO khoa;

        public static DangNhapDAO Khoa
        {
            get
            {
                if(khoa == null) khoa = new DangNhapDAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }
        private DangNhapDAO() { }

        public bool DangNhap(string TK, string MK)
        {
            string sql = "SELECT * FROM taikhoan WHERE tai_khoan = N'" + TK + "' AND mat_khau = N'" + MK + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt.Rows.Count > 0;
        }
    }
}
