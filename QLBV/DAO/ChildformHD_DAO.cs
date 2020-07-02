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
    public class ChildformHD_DAO
    {
        private static ChildformHD_DAO khoa;

        public static ChildformHD_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new ChildformHD_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private ChildformHD_DAO() { }

        // lấy các trường của bệnh nhân
        public string LayBN(string truong, string ma)
        {
            string sql = "SELECT " + truong + " FROM benhnhan WHERE ma_bn = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy các trường trong bảng bảo hiểm y tế của bệnh nhân
        public string LayBhBN(string truong, string ma)
        {
            string sql = "SELECT baohiem." + truong + " FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn WHERE benhnhan.ma_bn = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy chi tiết hóa đơn của 1 hóa đơn
        public DataTable LayCtHD(string ma)
        {
            string sql = "SELECT dichvu.ma_dv, dichvu.ten_dv, dichvu.don_gia, cthoadon.so_luong, cthoadon.thanh_tien FROM cthoadon INNER JOIN dichvu ON cthoadon.ma_dv = dichvu.ma_dv INNER JOIN hoadon ON hoadon.ma_hd = cthoadon.ma_hd WHERE cthoadon.ma_hd = '" + ma + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy danh sách nhân viên lễ tân
        public DataTable LayNvTN()
        {
            string sql = "SELECT ma_nv FROM nhanvien WHERE chuc_vu = N'Tiếp tân'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy tên nhân viên
        public string LaytenNV(string ma)
        {
            string sql = "SELECT ho_nv + ' ' + ten_nv FROM nhanvien WHERE ma_nv = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // thanh toán
        public void Thanhtoan(HoaDon_DTO hd)
        {
            string sql = "UPDATE hoadon SET ma_nv = '" + hd.Ma_nv + "', tong_tien = " + hd.Tong_tien + ", tinh_trang = N'Đã thanh toán', thoi_gian = GETDATE(), ngay = GETDATE() WHERE ma_hd = '" + hd.Ma_hd + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // in hóa đơn
        public DataTable InHD(string ma)
        {
            string sql = @"SELECT * FROM cthoadon INNER JOIN dichvu ON cthoadon.ma_dv = dichvu.ma_dv INNER JOIN hoadon ON hoadon.ma_hd = cthoadon.ma_hd INNER JOIN nhanvien ON nhanvien.ma_nv = hoadon.ma_nv
            INNER JOIN khambenh ON khambenh.ma_so = hoadon.ma_so INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn WHERE cthoadon.ma_hd = '" + ma + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // kiểm tra hóa đơn thanh toán chưa
        public bool KiemtraHD(string ma)
        {
            bool a = true;
            string sql = "SELECT COUNT(*) FROM hoadon WHERE tinh_trang = N'Đã thanh toán' AND ma_hd = '" + ma + "'";
            int i = int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
            if(i==0)
            {
                a = false;
            }
            return a;
        }

        // lấy các trường của hóa đơn
        public string LayHD(string truong, string ma)
        {
            string sql = "SELECT " + truong + " FROM hoadon WHERE ma_hd = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

    }
}
