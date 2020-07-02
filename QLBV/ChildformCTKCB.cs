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
    public partial class ChildformCTKCB : Form
    {
        bool binhthuong = true;
        int dem = 0;
        bool themmoi = false;

        public ChildformCTKCB()
        {
            InitializeComponent();
        }

        #region hàm load thông tin cho form

        public ChildformCTKCB(string Truyen): this()
        {
            lbMaso.Text = Truyen;
        }

        private void ChildformCTKCB_Load(object sender, EventArgs e)
        {
            ttKB();
            DsBs();
            DsDv();
            if(ChildformCTKCB_DAO.Khoa.KiemtraHS(lbMaso.Text.ToString()))
            {
                KhoaTT();
            }
            else
            {
                NapCT();
                dem = 1 + ChildformCTKCB_DAO.Khoa.SoHDtt(lbMaso.Text.ToString());
                lbHoadon.Text = ChildformCTKCB_DAO.Khoa.HDctt(lbMaso.Text.ToString());
            }
        }

        private void KhoaTT()
        {
            txtCDvao.ReadOnly = true;
            txtCDra.ReadOnly = true;
            txtQuatrinh.ReadOnly = true;
            txtPhuongphap.ReadOnly = true;
            txtKetqua.ReadOnly = true;
            txtGhichu.ReadOnly = true;
            cboMabs.Enabled = false;
            cboVaitro.Enabled = false;
            btnThembs.Enabled = false;
            btnXoabs.Enabled = false;
            btnLuubs.Enabled = false;
            cboDichvu.Enabled = false;
            txtPhatsinh.Enabled = false;
            txtChiphi.Enabled = false;
            btnThemdv.Enabled = false;
            btnXoadv.Enabled = false;
            btnHoadon.Enabled = false;
            btnInlai.Visible = true;
            btnHoanthanh.Visible = false;
            btnCapnhat.Enabled = false;
        }

        private void grBS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void DsBs()
        {
            DataTable dt = ChildformCTKCB_DAO.Khoa.LayDsBs(lbMaso.Text.ToString());
            grBS.DataSource = dt;
        }

        private void DsDv()
        {
            DataTable dt = ChildformCTKCB_DAO.Khoa.LayDsDv(lbMaso.Text.ToString());
            grDV.DataSource = dt;
            Tien();
        }

        private void Tien()
        {
            int n = grDV.RowCount;
            float tong = 0;
            for (int i = 0; i < n - 1; i++)
            {
                tong = tong + float.Parse(grDV[5, i].Value.ToString());
            }
            txtTong.Text = tong.ToString() + " đ";
            if(txtGhichu.Text == "Có BHYT")
            {
                float ptram = ChildformCTKCB_DAO.Khoa.LayPtram(txtMa.Text.ToString());
                txtPhaitra.Text = (tong * (1 - (ptram / 100))).ToString() + " đ";
            }
            else
            {
                txtPhaitra.Text = txtTong.Text;
            }
        }

        private void grDV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grDV.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (ChildformCTKCB_DAO.Khoa.KiemtraHD(grDV[0, i].Value.ToString()) == true)
                {
                    grDV.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        #endregion

        #region hàm thao tác với các text của textbox

        private string laydau(string date)
        {
            string[] s = date.Split(' ');
            string dateSQL = s[0];
            return dateSQL;
        }

        private void NapCT()
        {
            int i = grBS.CurrentRow.Index;
            cboMabs.Text = grBS[0, i].Value.ToString();
            txtTenbs.Text = grBS[1, i].Value.ToString();
            cboVaitro.Text = grBS[2, i].Value.ToString();
        }

        private void ttKB()
        {
            string n = lbMaso.Text.ToString();
            txtMa.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("ma_bn", lbMaso.Text.ToString());
            txtTen.Text = Khamchuabenh_DAO.Khoa.TenBN(txtMa.Text.ToString());
            txtNgayvao.Text = laydau(ChildformCTKCB_DAO.Khoa.Lay1gt("bat_dau", lbMaso.Text.ToString()));
            txtNgayra.Text = laydau(ChildformCTKCB_DAO.Khoa.Lay1gt("ket_thuc", lbMaso.Text.ToString()));
            txtCDvao.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("cd_vao", lbMaso.Text.ToString());
            txtCDra.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("cd_ra", lbMaso.Text.ToString());
            txtQuatrinh.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("qua_trinh", lbMaso.Text.ToString());
            txtPhuongphap.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("phuong_phap", lbMaso.Text.ToString());
            txtKetqua.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("ket_qua", lbMaso.Text.ToString());
            txtGhichu.Text = ChildformCTKCB_DAO.Khoa.Lay1gt("ghi_chu", lbMaso.Text.ToString());
        }

        private KhamBenh_DTO BienDoiKB()
        {
            KhamBenh_DTO kb = new KhamBenh_DTO();
            kb.Ma_so = lbMaso.Text;
            kb.Cd_vao = txtCDvao.Text;
            kb.Cd_ra = txtCDra.Text;
            kb.Qua_trinh = txtQuatrinh.Text;
            kb.Phuong_phap = txtPhuongphap.Text;
            kb.Ket_qua = txtKetqua.Text;
            return kb;
        }

        private CtKhamBenh_DTO BienDoiCtKB()
        {
            CtKhamBenh_DTO ctkb = new CtKhamBenh_DTO();
            ctkb.Ma_so = lbMaso.Text;
            ctkb.Ma_nv = cboMabs.Text;
            ctkb.Vai_tro = cboVaitro.Text;
            return ctkb;
        }

        private DichVu_DTO BienDoiDV()
        {
            DichVu_DTO dv = new DichVu_DTO();
            dv.Ten_dv = txtPhatsinh.Text;
            dv.Don_gia = float.Parse(txtChiphi.Text);
            return dv;
        }

        private CtHoaDon_DTO BienDoiCtHD()
        {
            CtHoaDon_DTO cthd = new CtHoaDon_DTO();
            cthd.Ma_hd = lbHoadon.Text;
            cthd.So_luong = (float)NubSoluong.Value;
            return cthd;
        }

        #endregion

        #region các hàm của danh sách bác sĩ khám

        private void cboMa_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenbs.Text = Khamchuabenh_DAO.Khoa.TenBS(cboMabs.Text.ToString());
        }

        private void cboMa_Enter(object sender, EventArgs e)
        {
            DataTable dt = Khamchuabenh_DAO.Khoa.MaBS();
            cboMabs.DataSource = dt;
            cboMabs.DisplayMember = "ma_nv";
            cboMabs.ValueMember = "ma_nv";
        }

        #endregion

        #region hàm thêm xóa lưu danh sách bác sĩ khám

        private void btnThembs_Click(object sender, EventArgs e)
        {
            cboMabs.Text = "";
            txtTenbs.Text = "";
            cboVaitro.Text = "Hỗ trợ";
            cboMabs.Enabled = true;
            themmoi = true;
        }

        private void btnXoabs_Click(object sender, EventArgs e)
        {
            try
            {
                int i = grBS.CurrentRow.Index;
                if (grBS[2, i].Value.ToString() != "Phụ trách")
                {
                    ChildformCTKCB_DAO.Khoa.XoaBS(lbMaso.Text.ToString(), cboMabs.Text.ToString());
                    MessageBox.Show("Đã xóa thành công!");
                    DsBs();
                }
                else
                {
                    MessageBox.Show("Không thể xóa bác sĩ phụ trách!");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi không thể xóa!");
            }
            NapCT();
        }

        private void btnLuubs_Click(object sender, EventArgs e)
        {
            if(themmoi == true)
            {
                try
                {
                    if (cboVaitro.Text == "Phụ trách")
                    {
                        string bsht = ChildformCTKCB_DAO.Khoa.TimBSpt(lbMaso.Text.ToString());
                        ChildformCTKCB_DAO.Khoa.ThemBS(BienDoiCtKB());
                        if (MessageBox.Show("Bạn muốn xóa bác sĩ phụ trách hiện tại không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ChildformCTKCB_DAO.Khoa.XoaBS(lbMaso.Text.ToString(), bsht);
                            MessageBox.Show("Bác sĩ phụ trách hiện tại đã bị xóa!");
                        }
                        else
                        {
                            ChildformCTKCB_DAO.Khoa.SuaBS(lbMaso.Text.ToString(), bsht, "Hỗ trợ");
                            MessageBox.Show("Bác sĩ phụ trách hiện tại trở thành hỗ trợ!");
                        }
                    }
                    else
                    {
                        ChildformCTKCB_DAO.Khoa.ThemBS(BienDoiCtKB());
                    }
                    MessageBox.Show("Thêm mới thành công!");
                    DsBs();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
                cboMabs.Enabled = false;
                themmoi = false;
                NapCT();
            }
            else
            {
                try
                {
                    string bsht = ChildformCTKCB_DAO.Khoa.TimBSpt(lbMaso.Text.ToString());
                    if (cboVaitro.Text == "Phụ trách")
                    {
                        ChildformCTKCB_DAO.Khoa.SuaBS(lbMaso.Text.ToString(), cboMabs.Text.ToString(), "Phụ trách");
                        if (MessageBox.Show("Bạn muốn xóa bác sĩ phụ trách hiện tại không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ChildformCTKCB_DAO.Khoa.XoaBS(lbMaso.Text.ToString(), bsht);
                            MessageBox.Show("Bác sĩ phụ trách hiện tại đã bị xóa!");
                        }
                        else
                        {
                            ChildformCTKCB_DAO.Khoa.SuaBS(lbMaso.Text.ToString(), bsht, "Hỗ trợ");
                            MessageBox.Show("Bác sĩ phụ trách hiện tại trở thành hỗ trợ!");
                        }
                        MessageBox.Show("Đã cập nhật chỉnh sửa!");
                    }
                    else if(cboVaitro.Text == "Hỗ trợ" && bsht == cboMabs.Text.ToString())
                    {
                        MessageBox.Show("Không thể sửa bác sĩ phụ trách!");
                    }
                    DsBs();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể sửa!");
                }
                NapCT();
            }
        }


        #endregion

        #region nút cập nhật, hoàn thành, kết thúc, hóa đơn

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            try
            {
                ChildformCTKCB_DAO.Khoa.SuaKB(BienDoiKB());
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
            catch
            {
                MessageBox.Show("Cập nhật không thành công!", "Thông báo");
            }
        }

        private void btnHoanthanh_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Hồ sơ khám chữa bệnh này sẽ được lưu lại và chỉ có thể đọc!\nBạn có chắc chắn muốn hoàn thành không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ChildformCTKCB_DAO.Khoa.HoanThanh(BienDoiKB());
                    MessageBox.Show("Hồ sơ khám chữa bệnh đã được lưu!");
                    if (MessageBox.Show("Bạn có muốn in tóm tắt bệnh án không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataTable dt = ChildformCTKCB_DAO.Khoa.InttBA(lbMaso.Text.ToString());
                        hienBaoCao(dt, false);
                        KhoaTT();
                    }
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể lưu hồ sơ khám chữa bệnh!");
                }
            }
        }

        private void btnKetthuc_Click(object sender, EventArgs e)
        {
            Form f = new frmChildFormKCB();
            this.Close();
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            if (ChildformCTKCB_DAO.Khoa.KiemtraDvHD(lbHoadon.Text.ToString()) == 0)
            {
                MessageBox.Show("Hóa đơn hiện tại không có dịch vụ nào!");
            }
            else if(lbHoadon.Text == "")
            {
                MessageBox.Show("Hiện tại không có hóa đơn nào!");
            }
            else
            {
                Form f = new ChildformHD(lbHoadon.Text, txtMa.Text, txtGhichu.Text);
                f.ShowDialog();
                DsDv();
                dem = 1 + ChildformCTKCB_DAO.Khoa.SoHDtt(lbMaso.Text.ToString());
                lbHoadon.Text = ChildformCTKCB_DAO.Khoa.HDctt(lbMaso.Text.ToString());
            }

        }

        #endregion

        #region các hàm của danh sách dịch vụ khám

        private void cboDichvu_Enter(object sender, EventArgs e)
        {
            txtPhatsinh.Text = "";
            txtChiphi.Text = "";
            binhthuong = true;
            DataTable dt = ChildformCTKCB_DAO.Khoa.Tendv();
            cboDichvu.DataSource = dt;
            cboDichvu.DisplayMember = "ten_dv";
            cboDichvu.ValueMember = "ten_dv";
        }

        private void txtPhatsinh_Enter(object sender, EventArgs e)
        {
            cboDichvu.Text = "";
            NubSoluong.Value = 1;
            binhthuong = false;
        }

        private void txtChiphi_Enter(object sender, EventArgs e)
        {
            cboDichvu.Text = "";
            NubSoluong.Value = 1;
            binhthuong = false;
        }

        private void btnThemdv_Click(object sender, EventArgs e)
        {
            if(lbHoadon.Text == "")
            {
                lbHoadon.Text = dem.ToString() + lbMaso.Text;
                ChildformCTKCB_DAO.Khoa.ThemHD(lbMaso.Text.ToString(), lbHoadon.Text.ToString());
            }
            if (binhthuong == true)
            {
                try
                {
                    string ma = ChildformCTKCB_DAO.Khoa.TimMabt(cboDichvu.Text.ToString());
                    ChildformCTKCB_DAO.Khoa.Themdvbt(BienDoiCtHD(), ma);
                    ChildformCTKCB_DAO.Khoa.CnThanhtien(lbHoadon.Text.ToString(), ma);
                    MessageBox.Show("Đã thêm thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
            }
            else
            {
                try
                {
                    ChildformCTKCB_DAO.Khoa.Themdvps(BienDoiDV());
                    string ma = ChildformCTKCB_DAO.Khoa.TimMaps(BienDoiDV());
                    ChildformCTKCB_DAO.Khoa.Themctps(lbHoadon.Text.ToString(), ma, BienDoiDV());
                    MessageBox.Show("Đã thêm thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
            }
            DsDv();
        }

        private void btnXoadv_Click(object sender, EventArgs e)
        {
            int i = grDV.CurrentRow.Index;
            if(ChildformCTKCB_DAO.Khoa.KiemtraHD(grDV[0, i].Value.ToString()) == true)
            {
                MessageBox.Show("Không thể xóa dịch vụ đã thanh toán!");
            }
            else
            {
                try
                {
                    string a = grDV[1, i].Value.ToString();
                    ChildformCTKCB_DAO.Khoa.Xoacthd(lbHoadon.Text.ToString(), a);
                    if(ChildformCTKCB_DAO.Khoa.KiemtraPS(a))
                    {
                        ChildformDV_DAO.Khoa.XoaDV(a);
                    }
                    MessageBox.Show("Đã xóa thành công!");
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể xóa!");
                }
            }
            DsDv();
        }


        #endregion

        #region các hàm in báo cáo

        private void btnInlai_Click(object sender, EventArgs e)
        {
            DataTable dt = ChildformCTKCB_DAO.Khoa.InttBA(lbMaso.Text.ToString());
            hienBaoCao(dt, true);
        }

        private void hienBaoCao(DataTable dt, bool a)
        {
            TomTatBenhAn rpt = new TomTatBenhAn();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            if(txtGhichu.Text == "Có BHYT")
            {
                TextObject BH = (TextObject)rptObjects["txtBH"];
                BH.Text = ChildformCTKCB_DAO.Khoa.LaySoBH(txtMa.Text.ToString());
                TextObject DV = (TextObject)rptObjects["txtDonvi"];
                DV.Text = ChildformCTKCB_DAO.Khoa.LayTenCQ(txtMa.Text.ToString());
            }
            else
            {
                TextObject BH = (TextObject)rptObjects["txtBH"];
                BH.Text = "";
                TextObject DV = (TextObject)rptObjects["txtDonvi"];
                DV.Text = "";
            }
            rpt.SetDataSource(dt);
            TomTatBenhAn_RPT bc = new TomTatBenhAn_RPT(rpt);
            if (a == true)
            {
                bc.Show();
            }
            else
            {
                bc.ShowDialog();
            }
        }

        #endregion

    }
}
