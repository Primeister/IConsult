using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IConsult
{
    public partial class AdminSignUp : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
        public AdminSignUp()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private bool validate(bool blnValid)
        {

            if (txtFName.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter first name.", "ERROR");
            }
            if(txtAdminNo.Text.Length < 8)
            {
                blnValid = false;
                MessageBox.Show("Minimum length of the admin number should be 8 characters", "ERROR");
            }
            if (txtLName.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter last name.", "ERROR");
            }
            if (txtAdminNo.Text == "")
            {
                blnValid = false;
                MessageBox.Show("Please enter student number.", "ERROR");
            }
            if(txtPassword.Text.Length < 8)
            {
                blnValid = false;
                MessageBox.Show("The minimum password length should be eight characters", "ERROR");
                txtPassword.Focus();
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

            if (blnValid == true)
            {
                conn.Close();
                conn.Open();
                OleDbCommand cmd1 = new OleDbCommand("select * from Admin where AdminNo ='" + txtAdminNo + "'", conn);
                OleDbDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Profile already exists", "ERROR");
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("insert into Admin values (@1, @2, @3, @4)", conn);
                    cmd.Parameters.AddWithValue("@1", txtAdminNo.Text);
                    cmd.Parameters.AddWithValue("@2", txtFName.Text);
                    cmd.Parameters.AddWithValue("@3", txtLName.Text);
                    cmd.Parameters.AddWithValue("@4", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("You have successfully registered your profile");

                    Login f1 = new Login();
                    this.Visible = false;
                    // show new instance of the Login form
                    f1.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login f1 = new Login();
            this.Visible = false;
            // show new instance of the Login form
            f1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
