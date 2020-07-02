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
    public partial class ChildformHD : Form
    {
        string ghichu = "";
        float tong = 0;
        float thanhtoan = 0;

        public ChildformHD()
        {
            InitializeComponent();
        }

        #region các hàm load thông tin cho form

        public ChildformHD(string Truyen, string Truyen2, string Truyen3): this()
        {
            txtMaHD.Text = Truyen;
            txtMaBN.Text = Truyen2;
            ghichu = Truyen3;
        }

        private void ChildformHD_Load(object sender, EventArgs e)
        {
            ttHD();
            ttBN();
            if (ChildformHD_DAO.Khoa.KiemtraHD(txtMaHD.Text.ToString()))
            {
                dateNgay.Text = ChildformHD_DAO.Khoa.LayHD("ngay", txtMaHD.Text.ToString());
                cboMa.Enabled = false;
                cboMa.Text = ChildformHD_DAO.Khoa.LayHD("ma_nv", txtMaHD.Text.ToString());
                txtTenNV.Text = ChildformHD_DAO.Khoa.LaytenNV(cboMa.Text.ToString());
                txtThanhtoan.Text = ChildformHD_DAO.Khoa.LayHD("tong_tien", txtMaHD.Text.ToString()) + " đ";
                txtTong.Text = ChildformListHD.tong.ToString() + " đ";
                btnThanhtoan.Visible = false;
                btnIn.Visible = true;
                //tong = float.Parse(ChildformListHD.tong.ToString());
                //thanhtoan = float.Parse(ChildformHD_DAO.Khoa.LayHD("tong_tien", txtMaHD.Text.ToString()));
            }
            else
            {
                Tien();
                ttNvTN();
            }
        }

        private void ttHD()
        {
            DataTable dt = ChildformHD_DAO.Khoa.LayCtHD(txtMaHD.Text.ToString());
            grCtHD.DataSource = dt;
            int n = grCtHD.RowCount;
            for(int i=0; i<n-1; i++)
            {
                grCtHD[0, i].Value = i + 1;
            }
        }

        private void Tien()
        {
            int n = grCtHD.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                tong = tong + float.Parse(grCtHD[5, i].Value.ToString());
            }
            txtTong.Text = tong.ToString() + " đ";
            if (txtBH.Text != "Không có")
            {
                float ptram = ChildformCTKCB_DAO.Khoa.LayPtram(txtMaBN.Text.ToString());
                thanhtoan = tong * (1 - (ptram / 100));
                txtThanhtoan.Text = thanhtoan.ToString() + " đ";
            }
            else
            {
                thanhtoan = tong;
                txtThanhtoan.Text = txtTong.Text;
            }
        }

        public void ttNvTN()
        {
            DataTable dt = ChildformHD_DAO.Khoa.LayNvTN();
            cboMa.DataSource = dt;
            cboMa.DisplayMember = "ma_nv";
            cboMa.ValueMember = "ma_nv";
        }

        #endregion

        #region các hàm thao tác với text của textbox

        private void ttBN()
        {
            txtTen.Text = ChildformHD_DAO.Khoa.LayBN("ho_bn + ' ' + ten_bn", txtMaBN.Text.ToString());
            txtDiachi.Text = ChildformHD_DAO.Khoa.LayBN("dia_chi", txtMaBN.Text.ToString());
            if(ghichu == "Có BHYT")
            {
                txtBH.Text = ChildformHD_DAO.Khoa.LayBhBN("so_the", txtMaBN.Text.ToString());
            }
            else
            {
                txtBH.Text = "Không có";
            }
        }

        private HoaDon_DTO BienDoiHD()
        {
            HoaDon_DTO hd = new HoaDon_DTO();
            hd.Ma_hd = txtMaHD.Text;
            hd.Tong_tien = thanhtoan;
            hd.Ma_nv = cboMa.Text;
            return hd;
        }

        #endregion

        #region các button thanh toán, thoát


        private void cboMa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenNV.Text = ChildformHD_DAO.Khoa.LaytenNV(cboMa.Text.ToString());
        }

        private void btnThanhtoan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn thanh toán hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ChildformHD_DAO.Khoa.Thanhtoan(BienDoiHD());
                    MessageBox.Show("Đã thanh toán thành công!");
                    if (MessageBox.Show("Bạn có muốn in hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataTable dt = ChildformHD_DAO.Khoa.InHD(txtMaHD.Text.ToString());
                        hienBaoCao(dt, false);
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thanh toán!");
                }
            }
        }

        private void btnKetthuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region in hóa đơn

        private void hienBaoCao(DataTable dt, bool a)
        {
            HoaDonDichVu rpt = new HoaDonDichVu();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            //TextObject txttc1 = (TextObject)rptObjects["tc1"];
            //txttc1.Text = DocSo.ChuyenSoSangChuoi(tong);
            //TextObject txttc2 = (TextObject)rptObjects["tc2"];
            //txttc2.Text = DocSo.ChuyenSoSangChuoi(thanhtoan);
            rpt.SetDataSource(dt);
            HoaDonDichVu_RPT bc = new HoaDonDichVu_RPT(rpt);
            if (a == true)
            {
                bc.Show();
            }
            else
            {
                bc.ShowDialog();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataTable dt = ChildformHD_DAO.Khoa.InHD(txtMaHD.Text.ToString());
            hienBaoCao(dt, true);
        }

        #endregion

    }
}
