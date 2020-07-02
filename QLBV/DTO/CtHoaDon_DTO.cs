using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class CtHoaDon_DTO
    {
        private string _ma_hd;
        private string _ma_dv;
        private float _so_luong;
        private float _thanh_tien;

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

        public string Ma_dv
        {
            get
            {
                return _ma_dv;
            }

            set
            {
                _ma_dv = value;
            }
        }

        public float So_luong
        {
            get
            {
                return _so_luong;
            }

            set
            {
                _so_luong = value;
            }
        }

        public float Thanh_tien
        {
            get
            {
                return _thanh_tien;
            }

            set
            {
                _thanh_tien = value;
            }
        }
    }
}
