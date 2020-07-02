using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBV
{
    public partial class frmChildFormHT : Form
    {

        public frmChildFormHT()
        {
            InitializeComponent();
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //this.Close();
                Form f = new frmMainForm();
                Form f1 = new LogIn();
                f1.Show();
                this.Hide();
                
                f.Hide();

            }
        }

        private void btnDichvu_Click(object sender, EventArgs e)
        {
            Form f = new ChildFormDV();
            f.ShowDialog();
        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            Form f = new ChildformBC();
            f.ShowDialog();
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            Form f = new ChildformListHD();
            f.ShowDialog();
        }

    }
}
