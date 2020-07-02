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
    public class childFormKhac_DAO
    {
        private static childFormKhac_DAO khoa;

        public static childFormKhac_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new childFormKhac_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private childFormKhac_DAO() { }

        // lấy dữ liệu của nhân viên khác
        public DataTable DuLieuNV()
        {
            string sql = "SELECT * FROM nhanvien WHERE chuc_vu NOT LIKE N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = "SELECT DISTINCT " + truong + " FROM nhanvien WHERE chuc_vu NOT LIKE N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDK(string truong, string giatri)
        {
            string sql = "SELECT * FROM nhanvien WHERE " + truong + " = N'" + giatri + "' AND chuc_vu NOT LIKE N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // tìm kiếm bác sĩ bằng tên gần đúng
        public DataTable TimKiemTen(string ten)
        {
            string sql = "SELECT * FROM nhanvien WHERE (ho_nv + ' ' + ten_nv) LIKE N'%" + ten + "%' AND chuc_vu NOT LIKE N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm nhân viên khác
        public void ThemNV(NhanVien_DTO nv)
        {
            string sql = @"INSERT INTO nhanvien (ma_nv, ho_nv, ten_nv, gioi, ngay_sinh, noi_sinh, dia_chi, dan_toc, trinh_do, don_vi, chuc_vu)
                VALUES('" + nv.Ma_nv + "', N'" + nv.Ho_nv + "', N'" + nv.Ten_nv + "',N'" + nv.Gioi + "', '" + nv.Ngay_sinh + "', N'" + nv.Noi_sinh + "', N'" + nv.Dia_chi + "', N'" + nv.Dan_toc + "', N'" + nv.Trinh_do + "', N'" + nv.Don_vi + "', N'" + nv.Chuc_vu + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa nhân viên khác
        public void SuaNV(NhanVien_DTO nv)
        {
            string sql = "UPDATE nhanvien SET ho_nv = N'" + nv.Ho_nv + "', ten_nv = N'" + nv.Ten_nv + "', gioi = N'" + nv.Gioi + "', ngay_sinh = '" + nv.Ngay_sinh + "', noi_sinh = N'" + nv.Noi_sinh + "', dia_chi = N'" + nv.Dia_chi + "', dan_toc = N'" + nv.Dan_toc + "', trinh_do = N'" + nv.Trinh_do + "', don_vi = N'" + nv.Don_vi + "', chuc_vu = N'" + nv.Chuc_vu + "' WHERE ma_nv = '" + nv.Ma_nv + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa nhân viên khác
        public void XoaNV(string nv)
        {
            string sql = "DELETE FROM nhanvien WHERE ma_nv = '" + nv + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy các dữ liệu với điều kiện để báo cáo
        public DataTable DuLieuDKBC(string truong, string giatri)
        {
            string sql = "SELECT ma_nv, ho_nv, ten_nv, gioi, ngay_sinh, noi_sinh FROM nhanvien WHERE " + truong + " = N'" + giatri + "' AND chuc_vu NOT LIKE N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }
    }
}
