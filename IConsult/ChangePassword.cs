using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IConsult
{
    public partial class ChangePassword : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\Shared drives\IS Project\IS .accdb");
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Login.login.UserType == "Student")
            {
                MainMenu f1 = new MainMenu();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }
            else if(Login.login.UserType == "Lecturer")
            {
                LecturerMainMenu f1 = new LecturerMainMenu();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }

            else if (Login.login.UserType == "Admin")
            {
                AdminMainMenu f1 = new AdminMainMenu();
                this.Visible = false;
                // show new instance of the Login form
                f1.ShowDialog();
            }
        }

        private bool validate(bool valid) {

            if (txtPassword.Text != txtPassword.Text) {
                
                valid= false;
                MessageBox.Show("Password do not match", "ERROR");

            }
            else if (txtPassword.Text.Length < 8)
            {

                valid = false;
                MessageBox.Show("Minimum password length should be 8 characters", "ERROR");

            }
            else if (txtPassword.Text == "" || txtConfirm.Text == "")
            {

                valid = false;
                MessageBox.Show("Enter password", "ERROR");

            }
            return valid;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            
            bool valid = true;
            valid = validate(true);
            if (valid)
            {

                if (Login.login.UserType == "Student")
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("update Student set Password = @1 where StudentNo ='"+Login.login.studNo+"'", conn);
                    cmd.Parameters.AddWithValue("@1", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    
                    MessageBox.Show("Password has been succesfully reset");
                }
                else if (Login.login.UserType == "Lecturer")
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("update Lecturer set Password = '12345678 where LecturerNo = '24680' ", conn);
                    cmd.Parameters.AddWithValue("@1", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                        
                    MessageBox.Show("Password has been succesfully reset");
                }
                else if (Login.login.UserType == "Admin")
                {

                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand("update Admin set Password = @1 where AdminNo ='" + Login.login.studNo + "'", conn);
                    cmd.Parameters.AddWithValue("@1", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Password has been succesfully reset");
                }
            }
        }
    }
}
