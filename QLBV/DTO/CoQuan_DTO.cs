using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class CoQuan_DTO
    {
        private string _ma_cq;
        private string _ten_cq;
        private string _dia_chi;
        private string _sdt;
        private string _fax;

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

        public string Ten_cq
        {
            get
            {
                return _ten_cq;
            }

            set
            {
                _ten_cq = value;
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

        public string Sdt
        {
            get
            {
                return _sdt;
            }

            set
            {
                _sdt = value;
            }
        }

        public string Fax
        {
            get
            {
                return _fax;
            }

            set
            {
                _fax = value;
            }
        }
    }
}
