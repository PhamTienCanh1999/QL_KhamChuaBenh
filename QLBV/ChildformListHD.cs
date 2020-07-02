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

namespace QLBV
{
    public partial class ChildformListHD : Form
    {
        public static float tong = 0;
        private string ghichu;

        public ChildformListHD()
        {
            InitializeComponent();
        }

        #region hàm load thông tin cho form

        private void ChildformListHD_Load(object sender, EventArgs e)
        {
            ttHD();
            NapCT();
        }

        private void ttHD()
        {
            DataTable dt = ChildformListHD_DAO.Khoa.HoaDon();
            grHD.DataSource = dt;
        }

        private void grHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();
        }

        private void grHD_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int n = grHD.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                if (i % 2 == 0)
                {
                    grHD.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        #endregion

        #region hàm thao tác với text của textbox

        private string laydau(string date)
        {
            string[] s = date.Split(' ');
            string dateSQL = s[0];
            return dateSQL;
        }

        private void NapCT()
        {
            if(grHD.CurrentRow != null)
            {
                int i = grHD.CurrentRow.Index;
                txtHD.Text = grHD[0, i].Value.ToString();
                txtMaBn.Text = grHD[1, i].Value.ToString();
                txtTenBn.Text = grHD[2, i].Value.ToString();
                txtNgay.Text = laydau(grHD[3, i].Value.ToString());
                txtMaNv.Text = grHD[4, i].Value.ToString();
                txtTenNv.Text = grHD[5, i].Value.ToString();
                txtTongtien.Text = grHD[6, i].Value.ToString();
                txtPhainop.Text = grHD[7, i].Value.ToString();
                if (txtTongtien.Text != "")
                {
                    tong = float.Parse(txtTongtien.Text);
                }
                string ma = ChildformListHD_DAO.Khoa.LayMaKB(txtHD.Text.ToString());
                ghichu = ChildformCTKCB_DAO.Khoa.Lay1gt("ghi_chu", ma);
            }
        }

        #endregion

        #region nút điều hướng

        private void picDau_Click(object sender, EventArgs e)
        {
            grHD.ClearSelection();
            grHD.CurrentCell = grHD[0, 0];
            NapCT();
        }

        private void picTruoc_Click(object sender, EventArgs e)
        {
            int i = grHD.CurrentRow.Index;
            if (i > 0)
            {
                grHD.CurrentCell = grHD[0, i - 1];
                NapCT();
            }
        }

        private void picSau_Click(object sender, EventArgs e)
        {
            int i = grHD.CurrentRow.Index;
            if (i < grHD.RowCount - 1)
            {
                grHD.CurrentCell = grHD[0, i + 1];
                NapCT();
            }
        }

        private void picCuoi_Click(object sender, EventArgs e)
        {
            grHD.ClearSelection();
            grHD.CurrentCell = grHD[0, grHD.RowCount - 2];
            NapCT();
        }

        #endregion

        #region nút chi tiết, đóng, tìm

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChitiet_Click(object sender, EventArgs e)
        {
            Form f = new ChildformHD(txtHD.Text.ToString(), txtMaBn.Text.ToString(), ghichu);
            f.ShowDialog();
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            string a = txtTKten.ToString();
            string b;
            int n = grHD.RowCount;
            for (int i = 0; i < n - 1; i++)
            {
                b = grHD[0, i].Value.ToString();
                if (b.Trim() == a.Trim())
                {
                    grHD.CurrentCell = grHD[0, i];
                    NapCT();
                }
            }
        }

        #endregion

    }
}
