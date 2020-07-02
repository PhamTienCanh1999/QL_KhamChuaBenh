using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QLBV.DTO;

namespace QLBV.DAO
{
    public class ChildformCTKCB_DAO
    {
        private static ChildformCTKCB_DAO khoa;

        public static ChildformCTKCB_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new ChildformCTKCB_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private ChildformCTKCB_DAO() { }

        // lấy một giá trị theo tên trường
        public string Lay1gt(string truong, string ten)
        {
            string sql = "SELECT " + truong + " FROM khambenh WHERE ma_so = N'" + ten + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy danh sách bác sĩ đang khám
        public DataTable LayDsBs(string ma)
        {
            string sql = @"SELECT nhanvien.ma_nv, (nhanvien.ho_nv + ' ' + nhanvien.ten_nv) AS ten_bs, ctkhambenh.vai_tro
            FROM khambenh INNER JOIN ctkhambenh ON khambenh.ma_so = ctkhambenh.ma_so INNER JOIN nhanvien ON ctkhambenh.ma_nv = nhanvien.ma_nv
            WHERE khambenh.ma_so = '" + ma + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy danh sách các dịch vụ
        public DataTable LayDsDv(string ma)
        {
            string sql = @"SELECT hoadon.ma_hd, dichvu.ma_dv, dichvu.ten_dv, dichvu.don_gia, cthoadon.so_luong, cthoadon.thanh_tien FROM cthoadon INNER JOIN dichvu ON cthoadon.ma_dv = dichvu.ma_dv INNER JOIN hoadon ON hoadon.ma_hd = cthoadon.ma_hd WHERE cthoadon.ma_hd IN (SELECT ma_hd FROM hoadon WHERE ma_so = '" + ma + "')";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm bác sĩ
        public void ThemBS(CtKhamBenh_DTO bs)
        {
            string sql = @"INSERT INTO ctkhambenh (ma_so, ma_nv, vai_tro) VALUES('" + bs.Ma_so + "','" + bs.Ma_nv + "',N'" + bs.Vai_tro + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa bác sĩ
        public void SuaBS(string ma, string mabs, string vt)
        {
            string sql = @"UPDATE ctkhambenh SET vai_tro = N'" + vt + "' WHERE ma_so = '" + ma + "' AND ma_nv = '" + mabs + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // tìm bác sĩ đang phụ trách
        public string TimBSpt(string ma)
        {
            string sql = @"SELECT ma_nv  FROM ctkhambenh WHERE ma_so = '" + ma + "' AND vai_tro = N'Phụ trách'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // xóa bác sĩ
        public void XoaBS(string ma, string mabs)
        {
            string sql = "DELETE FROM ctkhambenh WHERE ma_so = '" + ma + "' AND ma_nv = '" + mabs + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // cập nhật chi tiết khám bệnh
        public void SuaKB(KhamBenh_DTO kb)
        {
            string sql = @"UPDATE khambenh SET cd_vao = N'" + kb.Cd_vao + "', cd_ra = N'" + kb.Cd_ra + "', qua_trinh = N'" + kb.Qua_trinh + "', phuong_phap = N'" + kb.Phuong_phap + "', ket_qua = N'" + kb.Ket_qua + "' WHERE ma_so = '" + kb.Ma_so + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // hoàn thành khám bệnh
        public void HoanThanh(KhamBenh_DTO kb)
        {
            string sql = "UPDATE khambenh SET ket_thuc = GETDATE(), cd_vao = N'" + kb.Cd_vao + "', cd_ra = N'" + kb.Cd_ra + "', qua_trinh = N'" + kb.Qua_trinh + "', phuong_phap = N'" + kb.Phuong_phap + "', ket_qua = N'" + kb.Ket_qua + "' WHERE ma_so = '" + kb.Ma_so + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // thêm hóa đơn
        public void ThemHD(string kb, string hd)
        {
            string sql = "INSERT INTO hoadon (ma_hd, ma_so, ma_nv, tong_tien, tinh_trang, thoi_gian) VALUES ('" + hd + "','" + kb + "','NV0001',NULL,N'Chưa thanh toán',NULL)";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy tên tất cả các dịch vụ
        public DataTable Tendv()
        {
            string sql = "SELECT ten_dv FROM dichvu WHERE loai = N'Bình thường'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm dịch vụ phát sinh
        public void Themdvps(DichVu_DTO dv)
        {
            string sql = "INSERT INTO dichvu (ten_dv, don_gia, loai) VALUES(N'" + dv.Ten_dv + "'," + dv.Don_gia + ",N'Phát sinh')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // tìm mã dịch vụ phát sinh
        public string TimMaps(DichVu_DTO dv)
        {
            string sql = "SELECT ma_dv FROM dichvu WHERE ten_dv = N'" + dv.Ten_dv + "' AND don_gia = " + dv.Don_gia + " AND loai = N'Phát sinh'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // thêm dịch vụ phát sinh vào chi tiết hóa đơn
        public void Themctps(string mahd, string madv, DichVu_DTO dv)
        {
            string sql = "INSERT INTO cthoadon (ma_hd, ma_dv, so_luong, thanh_tien) VALUES('" + mahd + "'," + madv + ",1," + dv.Don_gia + ")";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // tìm mã dịch vụ bình thường
        public string TimMabt(string dv)
        {
            string sql = "SELECT ma_dv FROM dichvu WHERE ten_dv = N'" + dv + "' AND loai = N'Bình thường'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // thêm dịch vụ bình thường vào ct hóa đơn
        public void Themdvbt(CtHoaDon_DTO cthd, string madv)
        {
            try
            {
                string sql = "INSERT INTO cthoadon (ma_hd, ma_dv, so_luong, thanh_tien) VALUES('" + cthd.Ma_hd + "'," + madv + "," + cthd.So_luong + ",NULL)";
                KetNoiDB.Khoa.ChayLenh(sql);
            }
            catch
            {
                string sql1 = "UPDATE cthoadon SET so_luong = so_luong + " + cthd.So_luong + " WHERE ma_hd = '" + cthd.Ma_hd + "' AND ma_dv = " + madv + "";
                KetNoiDB.Khoa.ChayLenh(sql1);
            }
        }


        // cập nhật thành tiền cho ct hóa đơn
        public void CnThanhtien(string mahd, string madv)
        {
            string sql = @"UPDATE cthoadon SET cthoadon.thanh_tien = cthoadon.so_luong * dichvu.don_gia
            FROM cthoadon INNER JOIN dichvu ON cthoadon.ma_dv = dichvu.ma_dv
            WHERE  cthoadon.ma_hd = '" + mahd + "' AND cthoadon.ma_dv = " + madv + "";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // xóa ct khám bệnh
        public void Xoacthd(string hd, string dv)
        {
            string sql = "DELETE FROM cthoadon WHERE ma_hd = '" + hd + "' AND ma_dv = " + dv + "";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // vào hiện lên hóa đơn chưa thanh toán
        // nếu không có hóa đơn chưa thanh toán: không hiện mã hóa đơn
        // nếu có hóa đơn chưa thanh toán: hiện mã hóa đơn
        // bấm thêm dịch vụ, nếu có hd thì thêm dv, nếu chưa thì thêm hd, thêm dv
        // thêm hóa đơn thì số hóa đơn = (số hóa đơn đã thanh toán + 1) + đầu mã khám

        //hàm lấy tên hóa đơn chưa thanh toán
        public string HDctt(string ma)
        {
            string sql = "SELECT ma_hd FROM hoadon WHERE ma_so = '" + ma + "' AND tinh_trang = N'Chưa thanh toán'";
            string kq = KetNoiDB.Khoa.LayGiaTrị(sql);
            if(kq != null)
            {
                return kq;
            }
            else
            {
                return "";
            }
        }


        // hàm lấy số lượng hóa đơn đã thanh toán
        public int SoHDtt(string ma)
        {
            string sql = "SELECT COUNT(*) FROM hoadon WHERE ma_so = '" + ma + "' AND tinh_trang = N'Đã thanh toán'";
            return int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
        }

        // lấy phần trăm của bệnh nhân
        public float LayPtram(string ma)
        {
            string sql = "SELECT baohiem.ptram FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn WHERE benhnhan.ma_bn = '" + ma + "'";
            return float.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
        }

        // hàm kiểm tra hóa đơn đã thanh toán chưa
        public bool KiemtraHD(string ma)
        {
            bool kt = false;
            string sql = "SELECT tinh_trang FROM hoadon WHERE ma_hd = '" + ma + "'";
            if(KetNoiDB.Khoa.LayGiaTrị(sql) == "Đã thanh toán")
            {
                kt = true;
            }
            return kt;
        }

        // hàm kiểm tra xem hóa đơn có dịch vụ nào không
        public int KiemtraDvHD(string ma)
        {
            string sql = "SELECT COUNT(cthoadon.ma_dv) FROM hoadon INNER JOIN cthoadon ON hoadon.ma_hd = cthoadon.ma_hd WHERE hoadon.ma_hd = '" + ma + "'";
            return int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
        }

        // hàm kiểm tra xem hồ sơ khám chữa bệnh đã hoàn thành chưa
        public bool KiemtraHS(string ma)
        {
            bool a = true;
            string sql = "SELECT COUNT(*) FROM khambenh WHERE ket_thuc IS NOT NULL AND ma_so = '" + ma + "'";
            int i = int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
            if (i == 0)
            {
                a = false;
            }
            return a;
        }

        // in tóm tắt bệnh án
        public DataTable InttBA(string ma)
        {
            string sql = @"SELECT * FROM khambenh INNER JOIN benhnhan ON khambenh.ma_bn = benhnhan.ma_bn WHERE khambenh.ma_so = '" + ma + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy số thẻ bảo hiểm bệnh nhân
        public string LaySoBH(string ma)
        {
            string sql = @"SELECT so_the FROM baohiem WHERE ma_bn = '" + ma + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy cơ quan bệnh nhân
        public string LayTenCQ(string ma)
        {
            string sql = @"SELECT ten_cq FROM baohiem LEFT JOIN coquan ON baohiem.ma_cq = coquan.ma_cq WHERE ma_bn = '" + ma + "'";
            string cq = KetNoiDB.Khoa.LayGiaTrị(sql);
            return cq;
        }

        // hàm kiểm tra dịch vụ có phải phát sinh không
        public bool KiemtraPS(string ma)
        {
            bool a = true;
            string sql = "SELECT COUNT(*) FROM dichvu WHERE loai = N'Phát sinh' AND ma_dv = '" + ma + "'";
            int i = int.Parse(KetNoiDB.Khoa.LayGiaTrị(sql));
            if (i == 0)
            {
                a = false;
            }
            return a;
        }

    }
}
