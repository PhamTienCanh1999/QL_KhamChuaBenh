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
    public partial class frmcoBHYT : Form
    {
        public static string ma = null;
        string ma1 = null;

        bool them = false;
        bool themcq = false;
        string cot = "Bệnh nhân";
        string giatri = "Có BHYT";

        public frmcoBHYT()
        {
            InitializeComponent();
        }

        #region hàm load thông tin cho form

        private void frmcoBHYT_Load(object sender, EventArgs e)
        {
            ma1 = ma;
            ma = null;
            if(ma1 == null)
            {
                ttBN();
                NapCT();
            }
            else
            {
                DeTrong();
                tabControl2.SelectedIndex = 1;
                txtMa.Text = ma1;
                txtTen.Enabled = false;
                cboGioi.Enabled = false;
                txtNoio.Enabled = false;
                dateNgay.Enabled = false;
                txtTen.Text = ChildformHD_DAO.Khoa.LayBN("ho_bn + ' ' + ten_bn", ma1);
                txtNoio.Text = ChildformHD_DAO.Khoa.LayBN("dia_chi", ma1);
                cboGioi.Text = ChildformHD_DAO.Khoa.LayBN("gioi", ma1);
                dateNgay.Text = ChildformHD_DAO.Khoa.LayBN("ngay_sinh", ma1);
                txtBHYT.Enabled = true;
                btnLuu.Visible = false;
                btnLuu1.Visible = true;
            }
        }

        private void ttBN()
        {
            DataTable dt = coBHYT_DAO.Khoa.DuLieuBN();
            grBN.DataSource = dt;
        }

        private void cboTencq_Enter(object sender, EventArgs e)
        {
            if(themcq == false)
            {
                DataTable dt = coBHYT_DAO.Khoa.TenCq();
                cboTencq.DataSource = dt;
                cboTencq.DisplayMember = "ten_cq";
                cboTencq.ValueMember = "ten_cq";
            }
        }

        private void cboTencq_SelectedIndexChanged(object sender, EventArgs e)
        {
            string n = cboTencq.Text.ToString();
            txtDchicq.Text = coBHYT_DAO.Khoa.Lay1cq("dia_chi",n);
            txtSdt.Text = coBHYT_DAO.Khoa.Lay1cq("sdt", n);
            txtFax.Text = coBHYT_DAO.Khoa.Lay1cq("fax", n);
            txtMacq.Text = coBHYT_DAO.Khoa.Lay1cq("ma_cq", n);
            themcq = false;
        }

        private void grBN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grBN_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grBN.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    grBN.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        private void frmcoBHYT_Resize(object sender, EventArgs e)
        {
            int y = this.Size.Height;
            if (y >= 580)
            {
                if(this.tabPage3.Parent != null)
                {
                    this.tabPage2.Controls.Add(this.palCoquan);
                    this.palCoquan.Location = new System.Drawing.Point(0, 258);
                    this.tabPage3.Parent = null;
                }
            }
            else if (y < 580)
            {
                if(this.tabPage3.Parent == null)
                {
                    this.tabPage3.Parent = this.tabControl2;
                    this.tabPage3.Controls.Add(this.palCoquan);
                    this.palCoquan.Location = new System.Drawing.Point(0, 0);
                }
            }
        }

        #endregion

        #region hàm chuyển thời gian tên

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

        private string layho(string hoten)
        {
            string[] s = hoten.Split(' ');
            string ho = s[0];
            return ho.ToUpper();
        }

        private string layten(string hoten)
        {
            int n = 0;
            string ten = "";
            string[] s = hoten.Split(' ');
            foreach (string st in s)
                n += 1;
            for (int i = 1; i < n; i++)
                ten += s[i] + " ";
            ten = ten.TrimEnd();
            return ten.ToUpper();
        }

        #endregion

        #region các hàm thao tác với text của textbox

        private void NapCT()
        {
            if(grBN.CurrentRow != null)
            {
                int i = grBN.CurrentRow.Index;
                txtMa.Text = grBN[0, i].Value.ToString();
                txtTen.Text = grBN[1, i].Value.ToString() + " " + grBN[2, i].Value.ToString();
                cboGioi.Text = grBN[3, i].Value.ToString();
                dateNgay.Text = grBN[4, i].Value.ToString();
                txtNoio.Text = grBN[5, i].Value.ToString();
                cboDTuong.Text = grBN[6, i].Value.ToString();
                txtBHYT.Text = grBN[7, i].Value.ToString();
                dateBHYT.Text = grBN[8, i].Value.ToString();
                txtHLuuBHYT.Text = grBN[9, i].Value.ToString();
                txtPtram.Text = grBN[10, i].Value.ToString();
                txtNkhamBHYT.Text = grBN[11, i].Value.ToString();
                cboTencq.Text = grBN[12, i].Value.ToString();
                txtDchicq.Text = grBN[13, i].Value.ToString();
                txtSdt.Text = grBN[14, i].Value.ToString();
                txtFax.Text = grBN[15, i].Value.ToString();
                if (cboTencq.Text != "")
                {
                    txtMacq.Text = coBHYT_DAO.Khoa.LayMacq(BiendoiCQ());
                }
                else
                {
                    txtMacq.Text = "";
                }
            }
        }

        private void DeTrong()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cboGioi.Text = "Nam";
            dateNgay.Text = "";
            txtNoio.Text = "";
            //cboDTuong.Text = "Có BHYT";
            txtBHYT.Text = "";
            dateBHYT.Text = "";
            txtHLuuBHYT.Text = "";
            txtPtram.Text = "";
            txtNkhamBHYT.Text = "";
            cboTencq.Text = "";
            txtDchicq.Text = "";
            txtSdt.Text = "";
            txtFax.Text = "";
            //
            txtMacq.Text = "";
        }

        private BenhNhan_DTO BiendoiBN()
        {
            BenhNhan_DTO bn = new BenhNhan_DTO();
            bn.Ma_bn = txtMa.Text;
            bn.Ho_bn = layho(txtTen.Text.ToString());
            bn.Ten_bn = layten(txtTen.Text.ToString());
            bn.Gioi = cboGioi.Text;
            bn.Ngay_sinh = chuyenkieu(dateNgay.Text.ToString());
            bn.Dia_chi = txtNoio.Text;
            bn.Doi_tuong = cboDTuong.Text;
            return bn;
        }

        private CoQuan_DTO BiendoiCQ()
        {
            CoQuan_DTO cq = new CoQuan_DTO();
            cq.Ma_cq = txtMacq.Text;
            cq.Ten_cq = cboTencq.Text;
            cq.Dia_chi = txtDchicq.Text;
            cq.Sdt = txtSdt.Text;
            cq.Fax = txtFax.Text;
            return cq;
        }

        private BaoHiem_DTO BiendoiBH()
        {
            BaoHiem_DTO bh = new BaoHiem_DTO();
            bh.So_the = txtBHYT.Text;
            bh.Ma_bn = txtMa.Text;
            //
            bh.Thoi_gian = chuyenkieu(dateBHYT.Text.ToString());
            bh.Hieu_luc = txtHLuuBHYT.Text;
            bh.Ptram = float.Parse(txtPtram.Text);
            bh.Noi_kham = txtNkhamBHYT.Text;
            return bh;
        }

        #endregion

        #region các pic điều hướng

        private void picDau_Click(object sender, EventArgs e)
        {
            grBN.ClearSelection();
            grBN.CurrentCell = grBN[0, 0];
            NapCT();
        }

        private void picTruoc_Click(object sender, EventArgs e)
        {
            int i = grBN.CurrentRow.Index;
            if (i > 0)
            {
                grBN.CurrentCell = grBN[0, i - 1];
                NapCT();
            }
        }

        private void picSau_Click(object sender, EventArgs e)
        {
            int i = grBN.CurrentRow.Index;
            if (i < grBN.RowCount - 1)
            {
                grBN.CurrentCell = grBN[0, i + 1];
                NapCT();
            }
        }

        private void picCuoi_Click(object sender, EventArgs e)
        {
            grBN.ClearSelection();
            grBN.CurrentCell = grBN[0, grBN.RowCount - 2];
            NapCT();
        }




        #endregion

        #region các nút thêm xóa lưu

        private void btnThem_Click(object sender, EventArgs e)
        {
            DeTrong();
            txtMa.Enabled = true;
            txtBHYT.Enabled = true;
            them = true;
            tabControl2.SelectedIndex = 1;
        }

        private void btnTrongCq_Click(object sender, EventArgs e)
        {
                cboTencq.Text = "";
                txtDchicq.Text = "";
                txtSdt.Text = "";
                txtFax.Text = "";
                themcq = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Việc xóa bệnh nhân sẽ xóa toàn bộ các thông tin kèm theo!\nBạn chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    coBHYT_DAO.Khoa.XoaBH(txtBHYT.Text.ToString());
                    coBHYT_DAO.Khoa.XoaBN(txtMa.Text.ToString());
                    MessageBox.Show("Đã xóa thành công!");
                    ttBN();
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
            if (them == true)
            {
                try
                {
                    coBHYT_DAO.Khoa.ThemBN(BiendoiBN());
                    coBHYT_DAO.Khoa.ThemBH(BiendoiBH());
                    if (cboTencq.Text != "")
                    {
                        coBHYT_DAO.Khoa.ThemCQ(BiendoiCQ());
                        string macq = coBHYT_DAO.Khoa.LayMacq(BiendoiCQ());
                        coBHYT_DAO.Khoa.SuaBH(BiendoiBH(), macq);
                    }
                    MessageBox.Show("Thêm mới thành công!");
                    ttBN();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
                tabControl2.SelectedIndex = 0;
                txtMa.Enabled = false;
                txtBHYT.Enabled = false;
                NapCT();
                them = false;
            }
            else
            {
                try
                {
                    coBHYT_DAO.Khoa.SuaBN(BiendoiBN());
                    if(txtMacq.Text != "") // chỉnh sửa đối với bệnh nhân có cơ quan
                    {
                        if (themcq == true) // xóa hoặc thêm khác cơ quan với bệnh nhân đã có cơ quan
                        {
                            if (cboTencq.Text != "") // thêm khác cơ quan
                            {
                                coBHYT_DAO.Khoa.ThemCQ(BiendoiCQ());
                                string macq = coBHYT_DAO.Khoa.LayMacq(BiendoiCQ());
                                coBHYT_DAO.Khoa.SuaBH(BiendoiBH(), macq);
                                txtMacq.Text = macq;
                            }
                            else // xóa cơ quan hiện tại
                            {
                                coBHYT_DAO.Khoa.SuaBH(BiendoiBH());
                                txtMacq.Text = "";
                            }
                            themcq = false;
                        }
                        else // chỉnh sửa thông cơ quan
                        {
                            coBHYT_DAO.Khoa.SuaBH(BiendoiBH(), txtMacq.Text.ToString());
                            coBHYT_DAO.Khoa.SuaCQ(BiendoiCQ());
                        }
                        
                    }
                    else // chỉnh sửa đối với bệnh nhân không có cơ quan
                    {
                        if(cboTencq.Text != "") // thêm cơ quan
                        {
                            coBHYT_DAO.Khoa.ThemCQ(BiendoiCQ());
                            string macq = coBHYT_DAO.Khoa.LayMacq(BiendoiCQ());
                            coBHYT_DAO.Khoa.SuaBH(BiendoiBH(), macq);
                            txtMacq.Text = macq;
                        }
                        else // chỉ sửa thông tin bảo hiểm
                        {
                            coBHYT_DAO.Khoa.SuaBH(BiendoiBH());
                        }
                    }
                    MessageBox.Show("Đã cập nhật chỉnh sửa!");
                    ttBN();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể sửa!");
                }
            }
        }

        private void btnLuu1_Click(object sender, EventArgs e)
        {
            try
            {
                coBHYT_DAO.Khoa.ThemBH(BiendoiBH());
                coBHYT_DAO.Khoa.SuaBHYT(txtMa.Text.ToString());
                if (cboTencq.Text != "")
                {
                    coBHYT_DAO.Khoa.ThemCQ(BiendoiCQ());
                    string macq = coBHYT_DAO.Khoa.LayMacq(BiendoiCQ());
                    coBHYT_DAO.Khoa.SuaBH(BiendoiBH(), macq);
                }

                MessageBox.Show("Thêm mới thành công!");
            }
            catch
            {
                MessageBox.Show("Lỗi không thể thêm!");
            }
            tabControl2.SelectedIndex = 0;
            txtTen.Enabled = true;
            cboGioi.Enabled = true;
            txtNoio.Enabled = true;
            dateNgay.Enabled = true;
            txtBHYT.Enabled = false;
            btnLuu.Visible = true;
            btnLuu1.Visible = false;
            ttBN();
            NapCT();
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0 && btnLuu1.Visible == false)
            {
                txtTen.Enabled = true;
                cboGioi.Enabled = true;
                txtNoio.Enabled = true;
                dateNgay.Enabled = true;
                txtBHYT.Enabled = false;
                btnLuu.Visible = true;
                btnLuu1.Visible = false;
                ttBN();
                NapCT();
            }
        }

        #endregion

        #region tìm kiếm và lọc dữ liệu

        private void cboCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCot.Text == "Giới tính")
            {
                DataTable dt = coBHYT_DAO.Khoa.LayTruong("gioi");
                cboGiatri.DataSource = dt;
                cboGiatri.DisplayMember = "gioi";
                cboGiatri.ValueMember = "gioi";
            }
            else
            {
                List<string> dl = new List<string>() { "Có cơ quan", "Không cơ quan" };
                cboGiatri.DataSource = dl;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cboCot.Text != "")
            {
                if(cboCot.Text == "Giới tính")
                {
                    DataTable dt = coBHYT_DAO.Khoa.DuLieuDK("gioi", cboGiatri.Text.ToString());
                    cot = "Giới tính";
                    giatri = cboGiatri.Text;
                    grBN.DataSource = dt;
                }
                else
                {
                    if (cboGiatri.Text == "Có cơ quan")
                    {
                        DataTable dt = coBHYT_DAO.Khoa.LocBNcq(cboGiatri.Text.ToString());
                        cot = "Bệnh nhân";
                        giatri = cboGiatri.Text;
                        grBN.DataSource = dt;
                    }
                    else if (cboGiatri.Text == "Không cơ quan")
                    {
                        DataTable dt = coBHYT_DAO.Khoa.LocBNcq(cboGiatri.Text.ToString());
                        cot = "Bệnh nhân";
                        giatri = cboGiatri.Text;
                        grBN.DataSource = dt;
                    }
                }
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            txtTKten.Text = "";
            cot = "Bệnh nhân";
            giatri = "Có BHYT";
            ttBN();
        }

        private void txtTKten_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = coBHYT_DAO.Khoa.TimKiemTen(txtTKten.Text.ToString());
            grBN.DataSource = dt;
        }

        #endregion

        #region các hàm tạo báo cáo

        private void hienBaoCao(DataTable dt)
        {
            BenhNhan rpt = new BenhNhan();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            TextObject txtcot = (TextObject)rptObjects["txtCot"];
            txtcot.Text = cot;
            TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
            txtgiatri.Text = giatri;
            rpt.SetDataSource(dt);
            BenhNhan_RPT bc = new BenhNhan_RPT(rpt);
            bc.Show();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (cot == "Bệnh nhân" && giatri == "Có BHYT")
            {
                DataTable dt = coBHYT_DAO.Khoa.DuLieuBN();
                hienBaoCao(dt);
            }
            else if(cot == "Giới tính")
            {
                DataTable dt = coBHYT_DAO.Khoa.LocGT(giatri);
                hienBaoCao(dt);
            }
            else if (cot == "Bệnh nhân")
            {
                DataTable dt = coBHYT_DAO.Khoa.LocBNcqBC(giatri);
                hienBaoCao(dt);
            }
        }


        #endregion

        
    }
}
