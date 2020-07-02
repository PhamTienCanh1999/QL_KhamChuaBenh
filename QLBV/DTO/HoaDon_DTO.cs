using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class HoaDon_DTO
    {
        private string _ma_hd;
        private string _ma_so;
        private string _ma_nv;
        private float _tong_tien;
        private string _tinh_trang;
        private string _thoi_gian;
        private string _ngay;

        public string Ma_hd
        {
            get
            {
                return _ma_hd;
            }

            set
            {
                _ma_hd = value;
            }
        }

        public string Ma_so
        {
            get
            {
                return _ma_so;
            }

            set
            {
                _ma_so = value;
            }
        }

        public string Ma_nv
        {
            get
            {
                return _ma_nv;
            }

            set
            {
                _ma_nv = value;
            }
        }

        public float Tong_tien
        {
            get
            {
                return _tong_tien;
            }

            set
            {
                _tong_tien = value;
            }
        }

        public string Tinh_trang
        {
            get
            {
                return _tinh_trang;
            }

            set
            {
                _tinh_trang = value;
            }
        }

        public string Thoi_gian
        {
            get
            {
                return _thoi_gian;
            }

            set
            {
                _thoi_gian = value;
            }
        }

        public string Ngay
        {
            get
            {
                return _ngay;
            }

            set
            {
                _ngay = value;
            }
        }
    }
}
