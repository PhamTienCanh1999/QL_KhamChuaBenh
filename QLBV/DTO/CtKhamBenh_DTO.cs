using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBV.DTO
{
    public class CtKhamBenh_DTO
    {
        private string _ma_so;
        private string _ma_nv;
        private string _vai_tro;

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

        public string Vai_tro
        {
            get
            {
                return _vai_tro;
            }

            set
            {
                _vai_tro = value;
            }
        }
    }
}
