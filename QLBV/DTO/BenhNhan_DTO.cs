using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class BenhNhan_DTO
    {
        private string _ma_bn;
        private string _ho_bn;
        private string _ten_bn;
        private string _gioi;
        private string _ngay_sinh;
        private string _dia_chi;
        private string _doi_tuong;

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

        public string Ho_bn
        {
            get
            {
                return _ho_bn;
            }

            set
            {
                _ho_bn = value;
            }
        }

        public string Ten_bn
        {
            get
            {
                return _ten_bn;
            }

            set
            {
                _ten_bn = value;
            }
        }

        public string Gioi
        {
            get
            {
                return _gioi;
            }

            set
            {
                _gioi = value;
            }
        }

        public string Ngay_sinh
        {
            get
            {
                return _ngay_sinh;
            }

            set
            {
                _ngay_sinh = value;
            }
        }

        public string Dia_chi
        {
            get
            {
                return _dia_chi;
            }

            set
            {
                _dia_chi = value;
            }
        }

        public string Doi_tuong
        {
            get
            {
                return _doi_tuong;
            }

            set
            {
                _doi_tuong = value;
            }
        }
    }
}
