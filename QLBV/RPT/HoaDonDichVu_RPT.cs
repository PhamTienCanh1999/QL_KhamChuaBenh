﻿using System;
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
    public partial class HoaDonDichVu_RPT : Form
    {
        public HoaDonDichVu_RPT(HoaDonDichVu rpt)
        {
            InitializeComponent();
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
