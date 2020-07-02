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
    public class Khamchuabenh_DAO
    {
        private static Khamchuabenh_DAO khoa;

        public static Khamchuabenh_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new Khamchuabenh_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private Khamchuabenh_DAO() { }

        // lấy hồ sơ khám chữa bệnh tóm tắt
        public DataTable DuLieuKB()
        {
            string sql = @"SELECT khambenh.ma_so, benhnhan.ma_bn, (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) AS ten_bn,
            nhanvien.ma_nv, (nhanvien.ho_nv + ' ' + nhanvien.ten_nv) AS ten_pt, khambenh.bat_dau, khambenh.ket_thuc, khambenh.cd_ra, khambenh.ghi_chu
            FROM khambenh INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn INNER JOIN ctkhambenh
            ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN nhanvien ON ctkhambenh.ma_nv = nhanvien.ma_nv
            WHERE ctkhambenh.vai_tro = N'Phụ trách'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // tìm kiếm theo tên bệnh nhân
        public DataTable TimKiemTen(string hoten)
        {
            string sql = @"SELECT khambenh.ma_so, benhnhan.ma_bn, (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) AS ten_bn,
            nhanvien.ma_nv, (nhanvien.ho_nv + ' ' + nhanvien.ten_nv) AS ten_pt, khambenh.bat_dau, khambenh.ket_thuc, khambenh.cd_ra, khambenh.ghi_chu
            FROM khambenh INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn INNER JOIN ctkhambenh
            ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN nhanvien ON ctkhambenh.ma_nv = nhanvien.ma_nv
            WHERE ctkhambenh.vai_tro = N'Phụ trách' AND (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) LIKE N'%" + hoten + "%' ";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = @"SELECT DISTINCT " + truong + " FROM khambenh INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn INNER JOIN ctkhambenh ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN nhanvien ON ctkhambenh.ma_nv = nhanvien.ma_nv WHERE ctkhambenh.vai_tro = N'Phụ trách'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy dữ liệu với tình trạng
        public DataTable DuLieuTT(string tinhtrang)
        {
            string sql = @"SELECT khambenh.ma_so, benhnhan.ma_bn, (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) AS ten_bn,
            nhanvien.ma_nv, (nhanvien.ho_nv + ' ' + nhanvien.ten_nv) AS ten_pt, khambenh.bat_dau, khambenh.ket_thuc, khambenh.cd_ra, khambenh.ghi_chu
            FROM khambenh INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn INNER JOIN ctkhambenh
            ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN nhanvien ON ctkhambenh.ma_nv = nhanvien.ma_nv";
            if(tinhtrang == "Đã hoàn thành")
            {
                sql += " WHERE ctkhambenh.vai_tro = N'Phụ trách' AND khambenh.ket_thuc IS NOT NULL";
            }
            else
            {
                sql += " WHERE ctkhambenh.vai_tro = N'Phụ trách' AND khambenh.ket_thuc IS NULL";
            }
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy danh sách mã bác sĩ
        public DataTable MaBS()
        {
            string sql = @"SELECT ma_nv FROM nhanvien WHERE chuc_vu = N'Bác sĩ'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy danh sách mã bệnh nhân
        public DataTable MaBN()
        {
            string sql = @"SELECT ma_bn FROM benhnhan";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy tên bác sĩ
        public string TenBS(string ma)
        {
            string sql = "SELECT ho_nv + ' ' + ten_nv FROM nhanvien WHERE chuc_vu = N'Bác sĩ' AND ma_nv = '" + ma + "'";
            string ten = KetNoiDB.Khoa.LayGiaTrị(sql);
            return ten;
        }

        // lấy tên bệnh nhân
        public string TenBN(string ma)
        {
            string sql = "SELECT ho_bn + ' ' + ten_bn FROM benhnhan WHERE ma_bn = '" + ma + "'";
            string ten = KetNoiDB.Khoa.LayGiaTrị(sql);
            return ten;
        }

        // thêm hồ sơ khám bệnh
        public void ThemKB(KhamBenh_DTO kb)
        {
            string sql = @"INSERT INTO khambenh (ma_so, ma_bn, bat_dau, ket_thuc, cd_vao, cd_ra, qua_trinh, phuong_phap, ket_qua, ghi_chu) VALUES('" + kb.Ma_so + "','" + kb.Ma_bn + "',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,N'" + kb.Ghi_chu + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // thêm chi tiết khám bệnh
        public void ThemCtKB(CtKhamBenh_DTO ctkb)
        {
            string sql = @"INSERT INTO ctkhambenh (ma_so, ma_nv, vai_tro) VALUES('" + ctkb.Ma_so + "','" + ctkb.Ma_nv + "',N'Phụ trách')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa hồ sơ khám bệnh
        public void XoaKB(string ma)
        {
            string sql = "DELETE FROM khambenh WHERE ma_so = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa chi tiết khám bệnh
        public void XoaCtKB(string ma)
        {
            string sql = "DELETE FROM ctkhambenh WHERE ma_so = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }
        
        // xóa hóa đơn
        public void XoaHD(string ma)
        {
            string sql = "DELETE FROM hoadon WHERE ma_so = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa chi tiết hóa đơn
        public void XoaCtHD(string ma)
        {
            string sql = "DELETE FROM cthoadon WHERE ma_hd IN (SELECT ma_hd FROM hoadon WHERE ma_so = '" + ma + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // kiểm tra BHYT
        public string KiemTraBHYT(string ma)
        {
            string sql = "SELECT doi_tuong FROM benhnhan WHERE ma_bn = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // kiểm tra xem hồ sơ có hóa đơn nào đã thanh toán chưa
        public bool KiemtraHS(string ma)
        {
            bool a = true;
            string sql = @"SELECT COUNT(*) FROM hoadon WHERE tinh_trang = N'Đã thanh toán' AND ma_so = '" + ma + "'";
            int i = int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
            if(i == 0)
            {
                a = false;
            }
            return a;
        }

    }
}
