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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }


        private bool validate(bool blnValid) {

            if (txtFName.Text == "") 
            {
                blnValid = false;
                MessageBox.Show("Please enter first name.", "ERROR");
            }
            if (txtLName.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter last name.", "ERROR");
            }
            if (txtStudNo.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter student number.", "ERROR");
            }
            if (txtDegType.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter degree type.", "ERROR");
            }
            if (txtPassword.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter password.", "ERROR");
            }
            if (txtConfirm.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please confirm password.", "ERROR");
            }
            if (txtConfirm.Text != txtPassword.Text)
            {
                blnValid = false;
                MessageBox.Show("Passwords do not match.", "ERROR");
                txtConfirm.Text = "";
                txtPassword.Text = "";
                txtPassword.Focus();
            }

            return blnValid;
        
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

            bool blnValid = true;

            blnValid = validate(blnValid);

            if (blnValid == true) {

                StudentClass.fName.Add(txtFName.Text.ToString());
                StudentClass.lName.Add(txtLName.Text.ToString());
                StudentClass.studNo.Add(txtStudNo.Text.ToString());
                StudentClass.degType.Add(txtDegType.Text.ToString());
                StudentClass.password.Add(txtPassword.Text.ToString());

                Login f1 = new Login();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();


            }
            
        }
    }
}
