using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBV.DTO;
using System.Data;
using System.Data.SqlClient;

namespace QLBV.DAO
{
    public class ChildformBC_DAO
    {
        private static ChildformBC_DAO khoa;

        public static ChildformBC_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new ChildformBC_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private ChildformBC_DAO() { }

        // lấy dữ liệu tất cả bệnh nhân
        public DataTable BN()
        {
            string sql = @"SELECT * FROM benhnhan";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDK(string truong, string giatri)
        {
            string sql = @"SELECT * FROM benhnhan WHERE " + truong + " = N'" + giatri + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy dữ liệu tất cả bệnh nhân
        public DataTable NV()
        {
            string sql = @"SELECT * FROM nhanvien";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = "SELECT DISTINCT " + truong + " FROM nhanvien";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDKNV(string truong, string giatri)
        {
            string sql = "SELECT * FROM nhanvien WHERE " + truong + " = N'" + giatri + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lệnh lấy báo cáo doanh thu theo thời gian
        public DataTable BaocaoDT(string dau, string cuoi)
        {
            string sql = @"EXEC dbo.USP_doanhthu '"+dau+"', '"+cuoi+"'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

    }
}
