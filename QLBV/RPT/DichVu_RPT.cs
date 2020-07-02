using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV.RPT
{
    public partial class DichVu_RPT : Form
    {
        public DichVu_RPT(DichVu rpt)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
