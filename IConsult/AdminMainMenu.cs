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
    public partial class AdminMainMenu : Form
    {
        OleDbCommand cmd;
        OleDbDataReader dr;
        public static AdminMainMenu adminMainMenu;
        public String userID;
        public AdminMainMenu()
        {
            InitializeComponent();
            displayInfo();
            adminMainMenu = this;
        }
       
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");

        public Boolean validate(bool n) 
        {
            if(!radLect.Checked && !radStud.Checked) 
            {
                n = false;
                MessageBox.Show("Select user type","ERROR");
            }
            if (txtUserId.Text == "")
            {
                n = false;
                MessageBox.Show("Enter user ID", "ERROR");
            }
            return n;
        }

        private void displayInfo()
        {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Admin where AdminNo='" + Login.login.studNo + "'", conn);
            da.Fill(dt);
            lblFName.Text = dt.Rows[0]["FName"].ToString() + " " + dt.Rows[0]["LName"].ToString();
            lblAdminNo.Text = Login.login.studNo;



        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            userID=txtUserId.Text.ToString();
            bool n = true;
            n=validate(n);
            if (n)
            {
                if (radLect.Checked)
                {

                    cmd = new OleDbCommand("select * from Lecturer where LecturerNo='" + userID + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        
                        LecturerUpdate b1 = new LecturerUpdate();
                        this.Visible = false;
                        // show new instance of the Booking form
                        b1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("User does not exist", "ERROR");
                    }
                }
                else if (radStud.Checked)
                {

                    cmd = new OleDbCommand("select * from Student where StudentNo='" + userID + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {

                        StudentUpdate b1 = new StudentUpdate();
                        this.Visible = false;
                        // show new instance of the Booking form
                        b1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("User does not exist", "ERROR");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login b1 = new Login();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Report b1 = new Report();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void btnResetAdmin_Click(object sender, EventArgs e)
        {
            ChangePassword b1 = new ChangePassword();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }
    }
    }

