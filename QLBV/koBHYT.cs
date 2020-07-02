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
    public partial class frmkoBHYT : Form
    {
        public delegate void FireEventOpenChildForm(object sender,  OpenFrom e);
        public class OpenFrom: EventArgs
        {
            public string ma { get; set; }
        }
        public event FireEventOpenChildForm OpenChildForm = null;

        bool them = false;
        string cot = "Bệnh nhân";
        string giatri = "Không BHYT";
        string truong = "";

        public frmkoBHYT()
        {
            InitializeComponent();
        }

        #region hàm load thông tin cho form

        private void frmkoBHYT_Load(object sender, EventArgs e)
        {
            ttBN();
            NapCT();
        }

        private void ttBN()
        {
            DataTable dt = khongBHYT_DAO.Khoa.DuLieuBN();
            grBN.DataSource = dt;
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

        #endregion

        #region hàm chuyển thời gian, tên

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

        #region hàm thao tác với text của textbox

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
            }
        }

        private void DeTrong()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cboGioi.Text = "Nam";
            dateNgay.Text = "";
            txtNoio.Text = "";
            cboDTuong.Text = "Không BHYT";
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
            cboDTuong.Enabled = false;
            them = true;
            tabControl3.SelectedIndex = 1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Việc xóa bệnh nhân sẽ xóa toàn bộ các thông tin kèm theo!\nBạn chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    khongBHYT_DAO.Khoa.XoaBN(txtMa.Text.ToString());
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
                    khongBHYT_DAO.Khoa.ThemBN(BiendoiBN());
                    MessageBox.Show("Thêm mới thành công!");
                    ttBN();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
                tabControl3.SelectedIndex = 0;
                txtMa.Enabled = false;
                cboDTuong.Enabled = true;
                NapCT();
            }
            else
            {
                try
                {
                    khongBHYT_DAO.Khoa.SuaBN(BiendoiBN());
                    MessageBox.Show("Đã cập nhật chỉnh sửa!");
                    ttBN();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể sửa!");
                }
            }
        }

        private void cboDTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDTuong.Text == "Có BHYT")
            {
                if (MessageBox.Show("Bạn có muốn thêm BHYT cho bệnh nhân không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(OpenChildForm != null)
                    {
                        OpenChildForm(this, new OpenFrom { ma = txtMa.Text});
                    }
                    this.Close();
                }
                else
                {
                    cboDTuong.Text = "Không BHYT";
                }
            }
        }

        #endregion

        #region tìm kiếm và lọc dữ liệu

        private string chuyendoi()
        {
            string truong = "";
            if (cboCot.Text == "Giới tính")
                truong = "gioi";
            return truong;
        }

        private void cboCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = khongBHYT_DAO.Khoa.LayTruong(chuyendoi());
            cboGiatri.DataSource = dt;
            cboGiatri.DisplayMember = chuyendoi();
            cboGiatri.ValueMember = chuyendoi();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (cboCot.Text != "")
            {
                DataTable dt = khongBHYT_DAO.Khoa.DuLieuDK(chuyendoi(), cboGiatri.Text.ToString());
                cot = cboCot.Text;
                giatri = cboGiatri.Text;
                truong = chuyendoi();
                grBN.DataSource = dt;
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            txtTKten.Text = "";
            cot = "Bệnh nhân";
            giatri = "Không BHYT";
            truong = "";
            ttBN();
        }

        private void txtTKten_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = khongBHYT_DAO.Khoa.TimKiemTen(txtTKten.Text.ToString());
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

        private void btnIn_Click_1(object sender, EventArgs e)
        {
            if (cot == "Bệnh nhân" && giatri == "Không BHYT")
            {
                DataTable dt = khongBHYT_DAO.Khoa.DuLieuBN();
                hienBaoCao(dt);
            }
            else if (cot == "Giới tính")
            {
                DataTable dt = khongBHYT_DAO.Khoa.DuLieuDK(truong, giatri);
                hienBaoCao(dt);
            }
        }

        #endregion

        
    }
}
