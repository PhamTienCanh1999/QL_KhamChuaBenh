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
    public class ChildformListHD_DAO
    {
        private static ChildformListHD_DAO khoa;

        public static ChildformListHD_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new ChildformListHD_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private ChildformListHD_DAO() { }

        // hàm lấy thông tin hóa đơn đã thanh toán
        public DataTable HoaDon()
        {
            string sql = @"SELECT hoadon.ma_hd, benhnhan.ma_bn, (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) AS ten_bn, hoadon.ngay, hoadon.ma_nv,
            (nhanvien.ho_nv + ' ' + nhanvien.ten_nv) AS ten_nv, SUM(cthoadon.thanh_tien) AS tong, hoadon.tong_tien
            FROM hoadon INNER JOIN khambenh ON hoadon.ma_so = khambenh.ma_so INNER JOIN benhnhan ON benhnhan.ma_bn = khambenh.ma_bn
            INNER JOIN nhanvien ON nhanvien.ma_nv = hoadon.ma_nv INNER JOIN cthoadon ON cthoadon.ma_hd = hoadon.ma_hd
            WHERE hoadon.tinh_trang = N'Đã thanh toán'
            GROUP BY hoadon.ma_hd, (benhnhan.ho_bn + ' ' + benhnhan.ten_bn),
            hoadon.ngay, hoadon.ma_nv, hoadon.tong_tien, benhnhan.ma_bn, hoadon.ma_nv, (nhanvien.ho_nv + ' ' + nhanvien.ten_nv)";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // hàm lấy thông tin ghi chú khám chữa bệnh
        public string LayMaKB(string ma)
        {
            string sql = @"SELECT khambenh.ma_so FROM khambenh INNER JOIN hoadon ON khambenh.ma_so = hoadon.ma_so WHERE hoadon.ma_hd = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql); 
        }

    }
}
