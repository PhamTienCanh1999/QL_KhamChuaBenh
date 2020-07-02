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
    public class coBHYT_DAO
    {
        private static coBHYT_DAO khoa;

        public static coBHYT_DAO Khoa
        {
            get
            {
                if (khoa == null) khoa = new coBHYT_DAO { };
                return khoa;
            }

            private set
            {
                khoa = value;
            }
        }

        private coBHYT_DAO() { }

        // lấy dữ liệu của bệnh nhân có BHYT
        public DataTable DuLieuBN()
        {
            string sql = @"SELECT benhnhan.*, baohiem.so_the, baohiem.thoi_gian, baohiem.hieu_luc, baohiem.ptram, baohiem.noi_kham,
            coquan.ten_cq, coquan.dia_chi, coquan.sdt, coquan.fax FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các giá trị không lặp trong 1 trường
        public DataTable LayTruong(string truong)
        {
            string sql = "SELECT DISTINCT " + truong + " FROM benhnhan WHERE doi_tuong = N'Có BHYT'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lấy các dữ liệu với điều kiện
        public DataTable DuLieuDK(string truong, string giatri)
        {
            string sql = @"SELECT benhnhan.*, baohiem.so_the, baohiem.thoi_gian, baohiem.hieu_luc, baohiem.ptram, baohiem.noi_kham, coquan.ten_cq, coquan.dia_chi, coquan.sdt, coquan.fax
            FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq
            WHERE " + truong + " = N'" + giatri + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // tìm kiếm bệnh nhân bằng tên gần đúng
        public DataTable TimKiemTen(string ten)
        {
            string sql = @"SELECT benhnhan.*, baohiem.so_the, baohiem.thoi_gian, baohiem.hieu_luc, baohiem.ptram, baohiem.noi_kham, coquan.ten_cq, coquan.dia_chi, coquan.sdt, coquan.fax
            FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq
            WHERE (benhnhan.ho_bn + ' ' + benhnhan.ten_bn) LIKE N'%" + ten + "%'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lọc bệnh nhân với cơ quan để báo cáo
        public DataTable LocBNcq(string cq)
        {
            string sql;
            if (cq == "Có cơ quan")
            {
                sql = @"SELECT benhnhan.*, baohiem.so_the, baohiem.thoi_gian, baohiem.hieu_luc, baohiem.ptram, baohiem.noi_kham, coquan.ten_cq, coquan.dia_chi, coquan.sdt, coquan.fax
                FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq
                WHERE coquan.ten_cq LIKE N'%%'";
            }
            else
            {
                sql = @"SELECT benhnhan.*, baohiem.so_the, baohiem.thoi_gian, baohiem.hieu_luc, baohiem.ptram, baohiem.noi_kham, coquan.ten_cq, coquan.dia_chi, coquan.sdt, coquan.fax
                FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq
                WHERE coquan.ten_cq IS NULL";
            }
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // thêm bệnh nhân
        public void ThemBN(BenhNhan_DTO bn)
        {
            string sql = @"INSERT INTO benhnhan (ma_bn, ho_bn, ten_bn, gioi, ngay_sinh, dia_chi, doi_tuong) VALUES('" + bn.Ma_bn + "',N'" + bn.Ho_bn + "',N'" + bn.Ten_bn + "',N'" + bn.Gioi + "','" + bn.Ngay_sinh + "',N'" + bn.Dia_chi + "',N'Có BHYT')";
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

        // thêm dữ liệu BHYT
        public void ThemBH(BaoHiem_DTO bh)
        {
            string sql = @"INSERT INTO baohiem (so_the, ma_bn, ma_cq, thoi_gian, hieu_luc, ptram, noi_kham) VALUES ('" + bh.So_the + "','" + bh.Ma_bn + "',NULL,'" + bh.Thoi_gian + "'," + bh.Hieu_luc + "," + bh.Ptram + ",N'" + bh.Noi_kham + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa dữ liệu BHYT
        public void SuaBH(BaoHiem_DTO bh, string cq = null)
        {
            if(cq == null)
            {
                string sql = "UPDATE baohiem SET ma_cq = NULL, thoi_gian = '" + bh.Thoi_gian + "', hieu_luc = '" + bh.Hieu_luc + "', ptram = " + bh.Ptram + ", noi_kham = N'" + bh.Noi_kham + "' WHERE so_the = '" + bh.So_the + "'";
                KetNoiDB.Khoa.ChayLenh(sql);
            }
            else
            {
                string sql2 = "UPDATE baohiem SET ma_cq = '" + cq + "', thoi_gian = '" + bh.Thoi_gian + "', hieu_luc = '" + bh.Hieu_luc + "', ptram = " + bh.Ptram + ", noi_kham = N'" + bh.Noi_kham + "' WHERE so_the = '" + bh.So_the + "'";
                KetNoiDB.Khoa.ChayLenh(sql2);
            }
            
        }

        // xóa dữ liệu BHYT
        public void XoaBH(string ma)
        {
            string sql = "DELETE FROM baohiem WHERE so_the = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // thêm dữ liệu cơ quan
        public void ThemCQ(CoQuan_DTO cq)
        {
            string sql = "INSERT INTO coquan (ten_cq, dia_chi, sdt, fax) VALUES(N'" + cq.Ten_cq + "',N'" + cq.Dia_chi + "','" + cq.Sdt + "','" + cq.Fax + "')";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // sửa dữ liệu cơ quan
        public void SuaCQ(CoQuan_DTO cq)
        {
            string sql = "UPDATE coquan SET ten_cq = N'"+cq.Ten_cq+"', dia_chi = N'"+cq.Dia_chi+"', sdt = '"+cq.Sdt+"', fax = '"+cq.Fax+"' WHERE ma_cq = '"+cq.Ma_cq+"'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

        // lấy dữ liệu mã cơ quan
        public string LayMacq(CoQuan_DTO cq)
        {
            string sql = "SELECT ma_cq FROM coquan WHERE ten_cq = N'" + cq.Ten_cq + "' AND dia_chi = N'" + cq.Dia_chi + "' AND sdt = '" + cq.Sdt + "' AND fax = '" + cq.Fax + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy dữ liệu tên cơ quan
        public DataTable TenCq()
        {
            string sql = "SELECT ten_cq FROM coquan";
            return KetNoiDB.Khoa.LayBang(sql);
        }

        // lấy dữ liệu của một cơ quan
        public string Lay1cq(string truong ,string ten)
        {
            string sql = "SELECT " + truong + " FROM coquan WHERE ten_cq = N'" + ten + "'";
            return KetNoiDB.Khoa.LayGiaTrị(sql);
        }

        // lấy dữ liệu hồ sơ khám bệnh 
        public DataTable BenhNhan(string MaNV)
        {
            string sql = "";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lọc bệnh nhân với cơ quan để báo cáo
        public DataTable LocBNcqBC(string cq)
        {
            string sql;
            if (cq == "Có cơ quan")
            {
                sql = @"SELECT benhnhan.* FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq WHERE coquan.ten_cq LIKE N'%%'";
            }
            else
            {
                sql = @"SELECT benhnhan.* FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq WHERE coquan.ten_cq IS NULL";
            }
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // lọc giới tính
        public DataTable LocGT(string gt)
        {
            string sql = @"SELECT benhnhan.* FROM benhnhan INNER JOIN baohiem ON benhnhan.ma_bn = baohiem.ma_bn LEFT JOIN coquan ON coquan.ma_cq = baohiem.ma_cq WHERE benhnhan.gioi = N'" + gt + "'";
            DataTable dt = KetNoiDB.Khoa.LayBang(sql);
            return dt;
        }

        // cập nhật có bhyt
        public void SuaBHYT(string ma)
        {
            string sql = "UPDATE benhnhan SET doi_tuong = N'Có BHYT' WHERE ma_bn = '" + ma + "'";
            KetNoiDB.Khoa.ChayLenh(sql);
        }

    }
}
