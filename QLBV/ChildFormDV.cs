using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBV.DTO;
using QLBV.DAO;
using QLBV.RPT;
using CrystalDecisions.CrystalReports.Engine;

namespace QLBV
{
    public partial class ChildFormDV : Form
    {
        bool bt = true;
        bool them = false;

        public ChildFormDV()
        {
            InitializeComponent();
        }

        #region các hàm load form

        private void ChildFormDV_Load(object sender, EventArgs e)
        {
            ttBT();
            NapCT();
            cboLoai.Text = "Bình thường";
            txtLoai.Text = cboLoai.Text;
        }

        private void ttBT()
        {
            DataTable dt = ChildformDV_DAO.Khoa.DvBT();
            grDV.DataSource = dt;
        }

        private void ttPS()
        {
            DataTable dt = ChildformDV_DAO.Khoa.DvPS();
            grDV.DataSource = dt;
        }

        private void grDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grDV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grDV.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    grDV.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        #endregion

        #region hàm thao tác với text của textbox

        private void NapCT()
        {
            if(grDV.CurrentRow != null)
            {
                int i = grDV.CurrentRow.Index;
                txtMa.Text = grDV[0, i].Value.ToString();
                txtTen.Text = grDV[1, i].Value.ToString();
                txtDongia.Text = grDV[2, i].Value.ToString();
                txtLoai.Text = cboLoai.Text;
            }
            else
            {
                txtMa.Text = "";
                txtTen.Text = "";
                txtDongia.Text = "";
                txtLoai.Text = cboLoai.Text;
            }
            
        }

        #endregion

        #region hàm sửa xóa lưu đóng

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region hàm tìm kiếm, nạp, in

        private void txtTKten_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if(bt == true)
            {
                dt = ChildformDV_DAO.Khoa.TKtenDvBT(txtTKten.Text.ToString());
            }
            else
            {
                dt = ChildformDV_DAO.Khoa.TKtenDvPS(txtTKten.Text.ToString());
            }
            grDV.DataSource = dt;
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            if(cboLoai.Text == "Phát sinh")
            {
                ttPS();
                bt = false;
                txtMa.ReadOnly = true;
                txtTen.ReadOnly = true;
                txtDongia.ReadOnly = true;
                txtLoai.ReadOnly = true;
                btnThem.Visible = false;
                btnLuu.Visible = false;
                btnXoa.Visible = false;
            }
            else
            {
                ttBT();
                bt = true;
            }
            NapCT();
        }

        private void hienBaoCao(DataTable dt)
        {
            DichVu rpt = new DichVu();
            ReportObjects rptObjects = rpt.ReportDefinition.ReportObjects;
            TextObject txtcot = (TextObject)rptObjects["txtCot"];
            txtcot.Text = "Loại";
            TextObject txtgiatri = (TextObject)rptObjects["txtGiatri"];
            txtgiatri.Text = txtLoai.Text;
            rpt.SetDataSource(dt);
            DichVu_RPT bc = new DichVu_RPT(rpt);
            bc.Show();
        }

        private void btnChitiet_Click(object sender, EventArgs e)
        {
            if (bt == true)
            {
                DataTable dt = ChildformDV_DAO.Khoa.DvBT();
                hienBaoCao(dt);
            }
            else
            {
                DataTable dt = ChildformDV_DAO.Khoa.DvPS();
                hienBaoCao(dt);
            }
        }

        #endregion

        #region nút thêm sửa xóa

        private void btnThem_Click(object sender, EventArgs e)
        {
            them = true;
            txtMa.Text = "";
            txtTen.Text = "";
            txtDongia.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(them == true)
            {
                try
                {
                    float gia = float.Parse(txtDongia.Text.ToString());
                    ChildformDV_DAO.Khoa.ThemDV(txtTen.Text.ToString(), gia);
                    MessageBox.Show("Đã thêm thành công!");
                    ttBT();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể thêm!");
                }
                them = false;
            }
            else
            {
                try
                {
                    float gia = float.Parse(txtDongia.Text.ToString());
                    ChildformDV_DAO.Khoa.SuaDV(txtMa.Text.ToString(), txtTen.Text.ToString(), gia);
                    MessageBox.Show("Đã cập nhật chỉnh sửa!");
                    ttBT();
                }
                catch
                {
                    MessageBox.Show("Lỗi không thể sửa!");
                }
            }
            NapCT();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                ChildformDV_DAO.Khoa.XoaDV(txtMa.Text.ToString());
                MessageBox.Show("Đã xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Lỗi không thể xóa!");
            }
            ttBT();
            NapCT();
        }

        #endregion

        #region các nút điều hướng

        private void picDau_Click(object sender, EventArgs e)
        {
            grDV.ClearSelection();
            grDV.CurrentCell = grDV[0, 0];
            NapCT();
        }

        private void picTruoc_Click(object sender, EventArgs e)
        {
            int i = grDV.CurrentRow.Index;
            if (i > 0)
            {
                grDV.CurrentCell = grDV[0, i - 1];
                NapCT();
            }
        }

        private void picSau_Click(object sender, EventArgs e)
        {
            int i = grDV.CurrentRow.Index;
            if (i < grDV.RowCount - 1)
            {
                grDV.CurrentCell = grDV[0, i + 1];
                NapCT();
            }
        }

        private void picCuoi_Click(object sender, EventArgs e)
        {
            grDV.ClearSelection();
            grDV.CurrentCell = grDV[0, grDV.RowCount - 2];
            NapCT();
        }

        #endregion

        
    }
}
