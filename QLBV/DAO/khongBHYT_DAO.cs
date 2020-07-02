using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QLBV.DTO;

namespace QLBV.DAO
{
    public class khongBHYT_DAO
    {
        private static khongBHYT_DAO khoa;

        public static khongBHYT_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new khongBHYT_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private khongBHYT_DAO() { }

        // lấy dữ liệu của bệnh nhân có BHYT
        public DataTable DuLieuBN()
        {
            string sql = @"SELECT * FROM benhnhan WHERE doi_tuong = N'Không BHYT'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = "SELECT DISTINCT " + truong + " FROM benhnhan WHERE doi_tuong = N'Không BHYT'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDK(string truong, string giatri)
        {
            string sql = @"SELECT * FROM benhnhan WHERE doi_tuong = N'Không BHYT' AND " + truong + " = N'" + giatri + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // tìm kiếm bệnh nhân bằng tên gần đúng
        public DataTable TimKiemTen(string ten)
        {
            string sql = @"SELECT * FROM benhnhan WHERE doi_tuong = N'Không BHYT' AND (ho_bn + ' ' + ten_bn) LIKE N'%" + ten + "%'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm bệnh nhân
        public void ThemBN(BenhNhan_DTO bn)
        {
            string sql = @"INSERT INTO benhnhan (ma_bn, ho_bn, ten_bn, gioi, ngay_sinh, dia_chi, doi_tuong) VALUES('" + bn.Ma_bn + "',N'" + bn.Ho_bn + "',N'" + bn.Ten_bn + "',N'" + bn.Gioi + "','" + bn.Ngay_sinh + "',N'" + bn.Dia_chi + "',N'Không BHYT')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa bệnh nhân
        public void SuaBN(BenhNhan_DTO bn)
        {
            string sql = "UPDATE benhnhan SET ho_bn = N'" + bn.Ho_bn + "', ten_bn = N'" + bn.Ten_bn + "', gioi = N'" + bn.Gioi + "', ngay_sinh = '" + bn.Ngay_sinh + "', dia_chi = N'" + bn.Dia_chi + "', doi_tuong = N'" + bn.Doi_tuong + "' WHERE ma_bn = '" + bn.Ma_bn + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa bệnh nhân
        public void XoaBN(string ma)
        {
            string sql = "DELETE FROM benhnhan WHERE ma_bn = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy dữ liệu hồ sơ khám bệnh 
        public DataTable BenhNhan(string MaNV)
        {
            string sql = "";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }
    }
}
