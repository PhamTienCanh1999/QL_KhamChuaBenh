using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class BaoHiem_DTO
    {
        private string _so_the;
        private string _ma_bn;
        private string _ma_cq;
        private string _thoi_gian;
        private string _hieu_luc;
        private float _ptram;
        private string _noi_kham;

        public string So_the
        {
            get
            {
                return _so_the;
            }

            set
            {
                _so_the = value;
            }
        }

        public string Ma_bn
        {
            get
            {
                return _ma_bn;
            }

            set
            {
                _ma_bn = value;
            }
        }

        public string Ma_cq
        {
            get
            {
                return _ma_cq;
            }

            set
            {
                _ma_cq = value;
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

        public string Hieu_luc
        {
            get
            {
                return _hieu_luc;
            }

            set
            {
                _hieu_luc = value;
            }
        }

        public float Ptram
        {
            get
            {
                return _ptram;
            }

            set
            {
                _ptram = value;
            }
        }

        public string Noi_kham
        {
            get
            {
                return _noi_kham;
            }

            set
            {
                _noi_kham = value;
            }
        }
    }
}
