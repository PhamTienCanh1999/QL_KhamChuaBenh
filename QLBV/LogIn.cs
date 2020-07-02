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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            lbTB.Visible = false;
        }

        bool DangNhap(string TK, string MK)
        {
            return DangNhapDAO.Khoa.DangNhap(TK, MK);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtTK.Text == "Tài Khoản" && txtMK.Text == "Mật Khẩu")
            {
                thongbao("Xin hãy điền Tài khoản và Mật Khẩu!");
            }
            else if(DangNhap(txtTK.Text, txtMK.Text))
            {
                thongbao("Đăng nhập thành công!");
                this.Hide();
                Form f = new frmMainForm();
                f.Show();
            }
            else
            {
                thongbao("Sai Tài khoản hoặc Mật Khẩu!");
            }
        }

        private void thongbao(string tb)
        {
            lbTB.Text = tb;
            lbTB.Visible = true;
        }

        private void txtTK_Enter(object sender, EventArgs e)
        {
            if (txtTK.Text == "Tài Khoản")
                txtTK.Text = "";
            txtTK.ForeColor = Color.White;
        }

        private void txtMK_Enter(object sender, EventArgs e)
        {
            if (txtMK.Text == "Mật Khẩu")
                txtMK.Text = "";
            txtMK.ForeColor = Color.White;
            txtMK.UseSystemPasswordChar = true;
        }

        private void txtTK_Leave(object sender, EventArgs e)
        {
            if (txtTK.Text == "")
            {
                txtTK.Text = "Tài Khoản";
                txtTK.ForeColor = Color.DarkGray;
            }
        }

        private void txtMK_Leave(object sender, EventArgs e)
        {
            if (txtMK.Text == "")
            {
                txtMK.Text = "Mật Khẩu";
                txtMK.ForeColor = Color.DarkGray;
                txtMK.UseSystemPasswordChar = false;
            }
        }

        private void picThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
