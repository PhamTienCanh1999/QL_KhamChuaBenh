using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBV.DAO;
using QLBV.DTO;
using QLBV.RPT;
using CrystalDecisions.CrystalReports.Engine;

namespace QLBV
{
    public partial class ChildformBC : Form
    {
        bool tatca = true;
        bool tatca1 = true;

        public ChildformBC()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region báo cáo bệnh nhân

        private void cboCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>() { "Nam", "Nữ" };
            cboGiatri.DataSource = list;
            tatca = false;
        }

        private void hienBaoCao(DataTable dt)
        {
            BenhNhan rpt = new BenhNhan();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            if (tatca == true)
            {
                TextObject txtcot = (TextObject)rptObjects["txtCot"];
                txtcot.Text = "Bệnh nhân";
                TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
                txtgiatri.Text = "Tất cả";
            }
            else
            {
                TextObject txtcot = (TextObject)rptObjects["txtCot"];
                txtcot.Text = cboCot.Text;
                TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
                txtgiatri.Text = cboGiatri.Text;
            }
            rpt.SetDataSource(dt);
            BenhNhan_RPT bc = new BenhNhan_RPT(rpt);
            bc.Show();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            tatca = true;
            cboCot.Text = "";
            cboGiatri.Text = "";
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cboCot.Text == "")
            {
                DataTable dt = ChildformBC_DAO.Khoa.BN();
                hienBaoCao(dt);
            }
            else if (cboCot.Text == "Giới tính")
            {
                DataTable dt = ChildformBC_DAO.Khoa.DuLieuDK("gioi", cboGiatri.Text.ToString());
                hienBaoCao(dt);
            }
        }

        #endregion

        #region báo cáo bác sĩ

        private string chuyendoi()
        {
            string truong = "";
            if (cboCot1.Text == "Giới tính")
                truong = "gioi";
            else if (cboCot1.Text == "Trình độ")
                truong = "trinh_do";
            else if (cboCot1.Text == "Dân tộc")
                truong = "dan_toc";
            else if (cboCot1.Text == "Đơn vị")
                truong = "don_vi";
            return truong;
        }

        private void cboCot1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = ChildformBC_DAO.Khoa.LayTruong(chuyendoi());
            cboGiatri1.DataSource = dt;
            cboGiatri1.DisplayMember = chuyendoi();
            cboGiatri1.ValueMember = chuyendoi();
            tatca1 = false;
        }

        private void btnNap1_Click(object sender, EventArgs e)
        {
            tatca1 = true;
            cboCot1.Text = "";
            cboGiatri1.Text = "";
        }

        private void hienBaoCaoNV(DataTable dt)
        {
            NhanVien rpt = new NhanVien();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            if (tatca1 == true)
            {
                TextObject txtcot = (TextObject)rptObjects["txtCot"];
                txtcot.Text = "Nhân viên";
                TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
                txtgiatri.Text = "Tất cả";
            }
            else
            {
                TextObject txtcot = (TextObject)rptObjects["txtCot"];
                txtcot.Text = cboCot1.Text;
                TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
                txtgiatri.Text = cboGiatri1.Text;
            }
            rpt.SetDataSource(dt);
            NhanVien_RPT bc = new NhanVien_RPT(rpt);
            bc.Show();
        }

        private void btnIn1_Click(object sender, EventArgs e)
        {
            if (cboCot1.Text == "")
            {
                DataTable dt = ChildformBC_DAO.Khoa.NV();
                hienBaoCaoNV(dt);
            }
            else if (cboCot1.Text == "Giới tính")
            {
                DataTable dt = ChildformBC_DAO.Khoa.DuLieuDKNV(chuyendoi(), cboGiatri1.Text.ToString());
                hienBaoCaoNV(dt);
            }
            else if (cboCot1.Text == "Trình độ")
            {
                DataTable dt = ChildformBC_DAO.Khoa.DuLieuDKNV(chuyendoi(), cboGiatri1.Text.ToString());
                hienBaoCaoNV(dt);
            }
            else if (cboCot1.Text == "Dân tộc")
            {
                DataTable dt = ChildformBC_DAO.Khoa.DuLieuDKNV(chuyendoi(), cboGiatri1.Text.ToString());
                hienBaoCaoNV(dt);
            }
            else if (cboCot1.Text == "Đơn vị")
            {
                DataTable dt = ChildformBC_DAO.Khoa.DuLieuDKNV(chuyendoi(), cboGiatri1.Text.ToString());
                hienBaoCaoNV(dt);
            }
        }

        #endregion

        #region báo cáo doanh thu

        private string chuyenkieu(string date)
        {
            string[] s = date.Split('/');
            for (int i = 0; i < 3; i++)
            {
                if (s[i].Length == 1)
                {
                    s[i] = "0" + s[i];
                }
            }
            string dateSQL = s[2] + s[0] + s[1];
            return dateSQL;
        }

        private void hienBaoCaoDT(DataTable dt)
        {
            DoanhThu rpt = new DoanhThu();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            TextObject txtcot = (TextObject)rptObjects["Dau"];
            txtcot.Text = dateDau.Text.ToString();
            TextObject txtgiatri = (TextObject)rptObjects["Cuoi"];
            txtgiatri.Text = dateCuoi.Text.ToString();
            rpt.SetDataSource(dt);
            DoanhThu_RPT bc = new DoanhThu_RPT(rpt);
            bc.Show();
        }

        private void btnIn2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ChildformBC_DAO.Khoa.BaocaoDT(chuyenkieu(dateDau.Text.ToString()), chuyenkieu(dateCuoi.Text.ToString()));
                hienBaoCaoDT(dt);
            }
            catch
            {
                MessageBox.Show("Chọn sai thứ tự ngày tháng!");
            }
        }

        #endregion

        

    }
}
