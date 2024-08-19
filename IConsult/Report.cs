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
    public partial class Report : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
        OleDbCommand cmd;
        OleDbDataReader dr;
        public Report()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close(); ;
            con.Open();
            if (comboBox1.Text == "Lecturer") {
           
                    
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter("select LecturerNo, FName , LName , Office , Course , School from Lecturer", con);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                
            }
            else if (comboBox1.Text == "Student")
            {

                
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select StudentNo , FName , LName , DegreeType from Student", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            else if (comboBox1.Text == "Timeslots")
            {

                
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Timeslot", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            else if (comboBox1.Text == "Consultation")
            {
                
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Consultation", con);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();        }

        private void button2_Click(object sender, EventArgs e)
        {

            AdminMainMenu b1 = new AdminMainMenu();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }
    }
}
