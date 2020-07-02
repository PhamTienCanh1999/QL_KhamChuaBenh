using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class DichVu_DTO
    {
        private string _ma_dv;
        private string _ten_dv;
        private float _don_gia;
        private string _loai;

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

        public string Ten_dv
        {
            get
            {
                return _ten_dv;
            }

            set
            {
                _ten_dv = value;
            }
        }

        public float Don_gia
        {
            get
            {
                return _don_gia;
            }

            set
            {
                _don_gia = value;
            }
        }

        public string Loai
        {
            get
            {
                return _loai;
            }

            set
            {
                _loai = value;
            }
        }
    }
}
