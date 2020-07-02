using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using QLBV.DTO;

namespace QLBV.DAO
{
    public class childFormBS_DAO
    {
        private static childFormBS_DAO khoa;

        public static childFormBS_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new childFormBS_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private childFormBS_DAO() { }

        // lấy dữ liệu của nhân viên là bác sĩ
        public DataTable DuLieuBS()
        {
            string sql = "SELECT * FROM nhanvien WHERE chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = "SELECT DISTINCT " + truong + " FROM nhanvien WHERE chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDK(string truong, string giatri)
        {
            string sql = "SELECT * FROM nhanvien WHERE " + truong + " = N'" + giatri + "' AND chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // tìm kiếm bác sĩ bằng tên gần đúng
        public DataTable TimKiemTen(string ten)
        {
            string sql = "SELECT * FROM nhanvien WHERE (ho_nv + ' ' + ten_nv) LIKE N'%" + ten + "%' AND chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm bác sĩ
        public void ThemBS(NhanVien_DTO bs)
        {
            string sql = @"INSERT INTO nhanvien (ma_nv, ho_nv, ten_nv, gioi, ngay_sinh, noi_sinh, dia_chi, dan_toc, trinh_do, don_vi, chuc_vu) VALUES('" + bs.Ma_nv + "', N'" + bs.Ho_nv + "', N'" + bs.Ten_nv + "', N'" + bs.Gioi + "', '" + bs.Ngay_sinh + "', N'" + bs.Noi_sinh + "', N'" + bs.Dia_chi + "', N'" + bs.Dan_toc + "', N'" + bs.Trinh_do + "', N'" + bs.Don_vi + "', N'Bác sĩ')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa bác sĩ
        public void SuaBS(NhanVien_DTO bs)
        {
            string sql = "UPDATE nhanvien SET ho_nv = N'" + bs.Ho_nv + "', ten_nv = N'" + bs.Ten_nv + "', gioi = N'" + bs.Gioi + "', ngay_sinh = '" + bs.Ngay_sinh + "', noi_sinh = N'" + bs.Noi_sinh + "', dia_chi = N'" + bs.Dia_chi + "', dan_toc = N'" + bs.Dan_toc + "', trinh_do = N'" + bs.Trinh_do + "', don_vi = N'" + bs.Don_vi + "' WHERE ma_nv = '" + bs.Ma_nv + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa bác sĩ
        public void XoaBS(string ma)
        {
            string sql = "DELETE FROM nhanvien WHERE ma_nv = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy các dữ liệu với điều kiện để báo cáo
        public DataTable DuLieuDKBC(string truong, string giatri)
        {
            string sql = "SELECT ma_nv, ho_nv, ten_nv, gioi, ngay_sinh, noi_sinh FROM nhanvien WHERE " + truong + " = N'" + giatri + "' AND chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy dữ liệu bệnh nhân mà bác sĩ đang thăm khám
        /*public DataTable BenhNhan(string MaNV)
        {
            string sql = @"SELECT khambenh.ma_so, benhnhan.ma_bn, benhnhan.ho_bn, benhnhan.ten_bn, ctkhambenh.vai_tro
        FROM khambenh INNER JOIN ctkhambenh ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN benhnhan ON khambenh.ma_bn = benhnhan.ma_bn
        WHERE khambenh.ket_thuc = NULL AND ctkhambenh.ma_nv = '" + MaNV + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }*/
    }
}
