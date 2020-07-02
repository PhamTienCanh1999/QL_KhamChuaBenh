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
    public partial class frmChildFormBS : Form
    {
        bool themmoi = false;
        string truong = "";
        string cot = "Chức vụ";
        string giatri = "Bác sĩ";

        public frmChildFormBS()
        {
            InitializeComponent();
        }

        #region các hàm load thông tin cho form

        private void frmChildFormBS_Load(object sender, EventArgs e)
        {
            ttBS();
            NapCT();
        }

        private void ttBS()
        {
            DataTable dt = childFormBS_DAO.Khoa.DuLieuBS();
            grBS.DataSource = dt;
        }

        private void grBS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grBS_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grBS.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    grBS.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
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
            int n=0;
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
            if(grBS.CurrentRow != null)
            {
                int i = grBS.CurrentRow.Index;
                txtMa.Text = grBS[0, i].Value.ToString();
                txtTen.Text = grBS[1, i].Value.ToString() + " " + grBS[2, i].Value.ToString();
                cboGioi.Text = grBS[3, i].Value.ToString();
                dateNgay.Text = grBS[4, i].Value.ToString();
                txtNsinh.Text = grBS[5, i].Value.ToString();
                txtDchi.Text = grBS[6, i].Value.ToString();
                txtDantoc.Text = grBS[7, i].Value.ToString();
                txtTrinhdo.Text = grBS[8, i].Value.ToString();
                txtDonvi.Text = grBS[9, i].Value.ToString();
            }
        }

        private void DeTrong()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            cboGioi.Text = "Nam";
            dateNgay.Text = "";
            txtNsinh.Text = "";
            txtDchi.Text = "";
            txtDantoc.Text = "";
            txtTrinhdo.Text = "";
            txtDonvi.Text = "";
        }

        private NhanVien_DTO Biendoi()
        {
            NhanVien_DTO nv = new NhanVien_DTO();
            nv.Ma_nv = txtMa.Text;
            nv.Ho_nv = layho(txtTen.Text.ToString());
            nv.Ten_nv = layten(txtTen.Text.ToString());
            nv.Gioi = cboGioi.Text;
            nv.Ngay_sinh = chuyenkieu(dateNgay.Text.ToString());
            nv.Noi_sinh = txtNsinh.Text;
            nv.Dia_chi = txtDchi.Text;
            nv.Dan_toc = txtDantoc.Text;
            nv.Trinh_do = txtTrinhdo.Text;
            nv.Don_vi = txtDonvi.Text;
            return nv;
        }

        #endregion

        #region các pic điều hướng

        private void picDau_Click(object sender, EventArgs e)
        {
            grBS.ClearSelection();
            grBS.CurrentCell = grBS[0, 0];
            NapCT();
        }

        private void picTruoc_Click(object sender, EventArgs e)
        {
            int i = grBS.CurrentRow.Index;
            if (i > 0)
            {
                grBS.CurrentCell = grBS[0, i - 1];
                NapCT();
            }
        }

        private void picSau_Click(object sender, EventArgs e)
        {
            int i = grBS.CurrentRow.Index;
            if (i < grBS.RowCount - 1)
            {
                grBS.CurrentCell = grBS[0, i + 1];
                NapCT();
            }
        }

        private void picCuoi_Click(object sender, EventArgs e)
        {
            grBS.ClearSelection();
            grBS.CurrentCell = grBS[0, grBS.RowCount - 2];
            NapCT();
        }


        #endregion

        #region các nút thêm xóa lưu

        private void btnThem_Click(object sender, EventArgs e)
        {
            DeTrong();
            txtMa.Enabled = true;
            themmoi = true;
            tabControl1.SelectedIndex = 1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                childFormBS_DAO.Khoa.XoaBS(txtMa.Text.ToString());
                MessageBox.Show("Đã xóa thành công!");
                ttBS();
            }
            catch
            {
                MessageBox.Show("Lỗi không thể xóa!");
            }
            NapCT();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (themmoi == true)
            {
                try
                {
                    childFormBS_DAO.Khoa.ThemBS(Biendoi());
                    MessageBox.Show("Thêm mới thành công!");
                    ttBS();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
                themmoi = false;
                tabControl1.SelectedIndex = 0;
                txtMa.Enabled = false;
                NapCT();
            }
            else
            {
                try
                {
                    childFormBS_DAO.Khoa.SuaBS(Biendoi());
                    MessageBox.Show("Đã cập nhật chỉnh sửa!");
                    ttBS();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thay đổi!");
                }
            }
        }


        #endregion

        #region tìm kiếm và lọc dữ liệu

        private string chuyendoi()
        {
            string truong = "";
            if(cboCot.Text == "Giới tính")
                truong = "gioi";
            else if (cboCot.Text == "Trình độ")
                truong = "trinh_do";
            else if(cboCot.Text == "Dân tộc")
                truong = "dan_toc";
            else if(cboCot.Text == "Đơn vị")
                truong = "don_vi";
            return truong;
        }

        private void cboCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = childFormBS_DAO.Khoa.LayTruong(chuyendoi());
            cboGiatri.DataSource = dt;
            cboGiatri.DisplayMember = chuyendoi();
            cboGiatri.ValueMember = chuyendoi();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if(cboCot.Text != "")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuDK(chuyendoi(), cboGiatri.Text.ToString());
                truong = chuyendoi();
                cot = cboCot.Text;
                giatri = cboGiatri.Text;
                grBS.DataSource = dt;
            }
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            txtTKten.Text = "";
            cot = "Chức vụ";
            giatri = "Bác sĩ";
            ttBS();
        }

        private void txtTKten_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = childFormBS_DAO.Khoa.TimKiemTen(txtTKten.Text.ToString());
            grBS.DataSource = dt;
        }

        #endregion

        #region các hàm tạo báo cáo

        private void hienBaoCao(DataTable dt)
        {
            NhanVien rpt = new NhanVien();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            TextObject txtcot = (TextObject)rptObjects["txtCot"];
            txtcot.Text = cot;
            TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
            txtgiatri.Text = giatri;
            rpt.SetDataSource(dt);
            NhanVien_RPT bc = new NhanVien_RPT(rpt);
            bc.Show();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if(cot == "Chức vụ")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuBS();
                hienBaoCao(dt);
            }
            else if(cot == "Giới tính")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuDKBC(truong, giatri);
                hienBaoCao(dt);
            }
            else if (cot == "Trình độ")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuDKBC(truong, giatri);
                hienBaoCao(dt);
            }
            else if (cot == "Dân tộc")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuDKBC(truong, giatri);
                hienBaoCao(dt);
            }
            else if (cot == "Đơn vị")
            {
                DataTable dt = childFormBS_DAO.Khoa.DuLieuDKBC(truong, giatri);
                hienBaoCao(dt);
            }
        }

        #endregion

    }
}
