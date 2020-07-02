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
    public partial class frmChildFormKCB : Form
    {
        string giatri = "Tất cả";

        public frmChildFormKCB()
        {
            InitializeComponent();
        }

        #region các hàm load thông tin cho form

        public void frmChildFormKCB_Load(object sender, EventArgs e)
        {
            ttKB();
            CboMaBN();
            CboMaBS();
            NapCT();
        }

        private void ttKB()
        {
            DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuKB();
            grKB.DataSource = dt;
        }

        private void grKB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grKB_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grKB.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    grKB.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        #endregion

        #region các hàm thao tác với text của textbox

        private string laydau(string date)
        {
            string[] s = date.Split(' ');
            string dateSQL = s[0];
            return dateSQL;
        }

        private void NapCT()
        {
            if(grKB.CurrentRow != null)
            {
                int i = grKB.CurrentRow.Index;
                txtSoHs.Text = grKB[0, i].Value.ToString();
                txtNgayvao.Text = laydau(grKB[5, i].Value.ToString());
                txtNgayra.Text = laydau(grKB[6, i].Value.ToString());
                cboGhichu.Text = grKB[8, i].Value.ToString();
                cboMabn.Text = grKB[1, i].Value.ToString();
                txtTenbn.Text = grKB[2, i].Value.ToString();
                cboMabs.Text = grKB[3, i].Value.ToString();
                txtTenbs.Text = grKB[4, i].Value.ToString();
                txtKetqua.Text = grKB[7, i].Value.ToString();
            }
        }

        private void DeTrong()
        {
            txtSoHs.Text = "";
            txtNgayvao.Text = "";
            txtNgayra.Text = "";
            cboGhichu.Text = "";
            cboMabn.Text = "";
            txtTenbn.Text = "";
            cboMabs.Text = "";
            txtTenbs.Text = "";
            txtKetqua.Text = "";
        }

        private KhamBenh_DTO BienDoiKB()
        {
            KhamBenh_DTO kb = new KhamBenh_DTO();
            kb.Ma_so = txtSoHs.Text;
            kb.Ma_bn = cboMabn.Text;
            kb.Ghi_chu = cboGhichu.Text;
            return kb;
        }

        private CtKhamBenh_DTO BienDoiCtKB()
        {
            CtKhamBenh_DTO ctkb = new CtKhamBenh_DTO();
            ctkb.Ma_so = txtSoHs.Text;
            ctkb.Ma_nv = cboMabs.Text;
            return ctkb;
        }

        private HoaDon_DTO BienDoiCtHD()
        {
            HoaDon_DTO hd = new HoaDon_DTO();
            hd.Ma_so = "HD" + txtSoHs.Text.ToString();
            return hd;
        }

        #endregion

        #region các pic điều hướng

        private void picDau_Click(object sender, EventArgs e)
        {
            grKB.ClearSelection();
            grKB.CurrentCell = grKB[0, 0];
            NapCT();
        }

        private void picTruoc_Click(object sender, EventArgs e)
        {
            int i = grKB.CurrentRow.Index;
            if (i > 0)
            {
                grKB.CurrentCell = grKB[0, i - 1];
                NapCT();
            }
        }

        private void picSau_Click(object sender, EventArgs e)
        {
            int i = grKB.CurrentRow.Index;
            if (i < grKB.RowCount - 1)
            {
                grKB.CurrentCell = grKB[0, i + 1];
                NapCT();
            }
        }

        private void picCuoi_Click(object sender, EventArgs e)
        {
            grKB.ClearSelection();
            grKB.CurrentCell = grKB[0, grKB.RowCount - 2];
            NapCT();
        }


        #endregion

        #region nút thêm, xóa hồ sơ, chi tiết

        private void btnTen_Click(object sender, EventArgs e)
        {
            DeTrong();
            txtSoHs.Enabled = true;
            cboMabn.Enabled = true;
            cboMabs.Enabled = true;
            tabControl4.SelectedIndex = 1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(/*"Việc xóa hồ sơ khám bệnh sẽ xóa toàn bộ các thông tin kèm theo!\n*/"Bạn chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if(!Khamchuabenh_DAO.Khoa.KiemtraHS(txtSoHs.Text.ToString()))
                    {
                        Khamchuabenh_DAO.Khoa.XoaCtKB(txtSoHs.Text.ToString());
                        Khamchuabenh_DAO.Khoa.XoaCtHD(txtSoHs.Text.ToString());
                        Khamchuabenh_DAO.Khoa.XoaHD(txtSoHs.Text.ToString());
                    }
                    Khamchuabenh_DAO.Khoa.XoaKB(txtSoHs.Text.ToString());
                    MessageBox.Show("Đã xóa thành công!");
                    ttKB();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể xóa!");
                }
            }
            NapCT();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                Khamchuabenh_DAO.Khoa.ThemKB(BienDoiKB());
                Khamchuabenh_DAO.Khoa.ThemCtKB(BienDoiCtKB());
                MessageBox.Show("Thêm mới thành công!");
                ttKB();
                if (MessageBox.Show("Bạn có muốn mở chi tiết hồ sơ khám bệnh không", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form f = new ChildformCTKCB(txtSoHs.Text);
                    f.ShowDialog();
                    ttKB();
                    NapCT();
                }
            }
            catch
            {
                MessageBox.Show("Lỗi không thể thêm!");
            }
            tabControl4.SelectedIndex = 0;
            txtSoHs.Enabled = false;
            cboGhichu.Enabled = false;
            cboMabn.Enabled = false;
            cboMabs.Enabled = false;
            cboGhichu.Enabled = false;
            NapCT();
        }

        private void btnChitiet_Click(object sender, EventArgs e)
        {
            if(txtSoHs.Enabled == false)
            {
                Form f = new ChildformCTKCB(txtSoHs.Text);
                f.ShowDialog();
                ttKB();
                NapCT();
            }
            else
            {
                MessageBox.Show("Hồ sơ khám bệnh chưa được lưu!");
            }
        }

        #endregion

        #region tìm kiếm dữ liệu

        private void cboCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lt = new List<string> { "Chưa hoàn thành", "Đã hoàn thành" };
            cboGiatri.DataSource = lt;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cboCot.Text != "")
            {
                if(cboGiatri.Text == "Đã hoàn thành")
                {
                    DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuTT(cboGiatri.Text.ToString());
                    giatri = cboGiatri.Text;
                    grKB.DataSource = dt;
                }
                else
                {
                    DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuTT(cboGiatri.Text.ToString());
                    giatri = cboGiatri.Text;
                    grKB.DataSource = dt;
                }
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            txtTKten.Text = "";
            giatri = "Tất cả";
            ttKB();
        }

        private void txtTKten_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Khamchuabenh_DAO.Khoa.TimKiemTen(txtTKten.Text.ToString());
            grKB.DataSource = dt;
        }

        #endregion

        #region load thông tin cho cbo

        private void CboMaBS()
        {
            DataTable dt = Khamchuabenh_DAO.Khoa.MaBS();
            cboMabs.DataSource = dt;
            cboMabs.DisplayMember = "ma_nv";
            cboMabs.ValueMember = "ma_nv";
        }

        private void CboMaBN()
        {
            DataTable dt = Khamchuabenh_DAO.Khoa.MaBN();
            cboMabn.DataSource = dt;
            cboMabn.DisplayMember = "ma_bn";
            cboMabn.ValueMember = "ma_bn";
        }

        private void cboMabn_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenbn.Text = Khamchuabenh_DAO.Khoa.TenBN(cboMabn.Text.ToString());
            if(txtSoHs.Enabled == true)
            {
                if (Khamchuabenh_DAO.Khoa.KiemTraBHYT(cboMabn.Text.ToString()) == "Có BHYT")
                {
                    List<string> list = new List<string>() { "Có BHYT", "Không BHYT" };
                    cboGhichu.DataSource = list;
                    cboGhichu.Enabled = true;
                }
                else
                {
                    cboGhichu.Enabled = false;
                    cboGhichu.Text = "Không BHYT";
                }
            }
        }

        private void cboMabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenbs.Text = Khamchuabenh_DAO.Khoa.TenBS(cboMabs.Text.ToString());
        }

        #endregion

        #region các hàm tạo báo cáo

        private void hienBaoCao(DataTable dt)
        {
            KhamChuaBenh rpt = new KhamChuaBenh();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
            txtgiatri.Text = giatri;
            rpt.SetDataSource(dt);
            KhamChuaBenh_RPT bc = new KhamChuaBenh_RPT(rpt);
            bc.Show();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (giatri == "Tất cả")
            {
                DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuKB();
                hienBaoCao(dt);
            }
            else if (giatri == "Chưa hoàn thành")
            {
                DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuTT(giatri);
                hienBaoCao(dt);
            }
            else if (giatri == "Đã hoàn thành")
            {
                DataTable dt = Khamchuabenh_DAO.Khoa.DuLieuTT(giatri);
                hienBaoCao(dt);
            }
        }

        #endregion

        private void btnChitiet1_Click(object sender, EventArgs e)
        {
            if (txtSoHs.Enabled == false)
            {
                Form f = new ChildformCTKCB(txtSoHs.Text);
                f.ShowDialog();
                ttKB();
                NapCT();
            }
            else
            {
                MessageBox.Show("Hồ sơ khám bệnh chưa được lưu!");
            }
        }
    }
}
