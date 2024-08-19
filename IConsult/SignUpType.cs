using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IConsult
{
    public partial class SignupType : Form
    {
        public SignupType()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (radAdmin.Checked)
            {
                AdminSignUp f1 = new AdminSignUp();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }
            else if (radLect.Checked)
            {

                LecturerSignup f1 = new LecturerSignup();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }
            else if (radStud.Checked)
            {

                SignUp f1 = new SignUp();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }
            else { MessageBox.Show("User type not selected"); }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login b1 = new Login();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
