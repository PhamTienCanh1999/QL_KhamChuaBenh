using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLBV.DAO
{
    public class ChildformDV_DAO
    {
        private static ChildformDV_DAO khoa;

        public static ChildformDV_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new ChildformDV_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private ChildformDV_DAO() { }

        // lấy các dịch vụ bình thường
        public DataTable DvBT()
        {
            string sql = @"SELECT * FROM dichvu WHERE loai = N'Bình thường'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dịch vụ phát sinh
        public DataTable DvPS()
        {
            string sql = @"SELECT * FROM dichvu WHERE loai = N'Phát sinh'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm dịch vụ bình thường
        public void ThemDV(string ten, float gia)
        {
            string sql = @"INSERT INTO dichvu (ten_dv, don_gia, loai) VALUES (N'" + ten + "'," + gia + ",N'Bình thường')";
            KetNoiDB.Khoa.ChayLenh(sql); 
        }
        // sửa dịch vụ bình thường
        public void SuaDV(string ma, string ten, float gia)
        {
            string sql = @"UPDATE dichvu SET  ten_dv = N'" + ten + "', don_gia = " + gia + " WHERE ma_dv = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa dịch vụ bình thường
        public void XoaDV(string ma)
        {
            string sql = @"DELETE FROM dichvu WHERE ma_dv = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy các dịch vụ bình thường với tên
        public DataTable TKtenDvBT(string ten)
        {
            string sql = @"SELECT * FROM dichvu WHERE loai = N'Bình thường' AND ten_dv LIKE N'%" + ten + "%'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dịch vụ phát sinh
        public DataTable TKtenDvPS(string ten)
        {
            string sql = @"SELECT * FROM dichvu WHERE loai = N'Phát sinh' AND ten_dv LIKE N'%" + ten + "%'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

    }
}
