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

namespace IConsult
{
    public partial class Login : Form
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        public static Login login;
        public string studNo;
        public string UserType;


        public Login()
        {
            InitializeComponent();
            login = this;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
            con.Open();
            studNo = textBox5.Text.ToString();

            if (comboBox1.Text == "Student")
            {
                //continue from here
                
                cmd = new OleDbCommand("select * from Student where StudentNo='" + textBox5.Text + "' and Password='" + textBox6.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    UserType = comboBox1.Text;
                    
                    MainMenu f1 = new MainMenu();
                    this.Visible = false;
                    // show new instance of the Login form
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
                

            }
            else if(comboBox1.Text == "Lecturer") 
            {
                cmd = new OleDbCommand("select * from Lecturer where LecturerNo='" + textBox5.Text + "' and Password='" + textBox6.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    UserType = comboBox1.Text;
                    
                    LecturerMainMenu f1 = new LecturerMainMenu();
                    this.Visible = false;
                    // show new instance of the Login form
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
            }
            else if(comboBox1.Text == "Admin") 
            {
                cmd = new OleDbCommand("select * from Admin where AdminNo='" + textBox5.Text + "' and Password='" + textBox6.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    UserType = comboBox1.Text;
                    
                    AdminMainMenu f1 = new AdminMainMenu();
                    this.Visible = false;
                    // show new instance of the Login form
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials, Please Re-Enter");
                }
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SignupType b1 = new SignupType();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }
    }
}
