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
    public partial class MainMenu : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbCommand cmd = new OleDbCommand("select * from Consultation where StudentNo='" + Login.login.studNo + "' and NOT Status = 'Cancelled'", conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Consultation where StudentNo='" + Login.login.studNo + "' and NOT Status = 'Cancelled'", conn);

                    da.Fill(dt);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "ConsultationDate";
                    comboBox1.ValueMember = "ConsultationID";
                    String id = dt.Rows[0]["ConsultationID"].ToString();
                   displayBooking(id);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Connection:" + e);
            }
            finally
            {
                conn.Close();
            }
        }

        private void displayInfo() {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Student where StudentNo='" + Login.login.studNo + "'", conn);
            da.Fill(dt);
            lblFName.Text = dt.Rows[0]["FName"].ToString() + " " + dt.Rows[0]["LName"].ToString();
            lblStudNo.Text = Login.login.studNo;
            lblDegType.Text = dt.Rows[0]["DegreeType"].ToString();



        }

        private void displayBooking(String id)
        {

            String query = "Select * from Consultation where ConsultationID = " + id;
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
            da.Fill(dt);
            string Lecturer = dt.Rows[0]["LecturerNo"].ToString();
            DataTable dt2 = new DataTable();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select * from Lecturer where LecturerNo = '"+ Lecturer+"' ", conn);
            da2.Fill(dt2);
            lblLectName.Text = dt2.Rows[0]["FName"].ToString();
            
            lblDate.Text = dt.Rows[0]["ConsultationDate"].ToString();
            lblConType.Text = dt.Rows[0]["ConsultationType"].ToString();
            lblTime.Text = dt.Rows[0]["Timeslot"].ToString();
            lblVenue.Text = dt.Rows[0]["Venue"].ToString();
            label7.Text = dt.Rows[0]["Status"].ToString();

        }

        public MainMenu()
        {
            InitializeComponent();
            refreshGrid();
            displayInfo();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Booking b1 = new Booking();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String SelectedValue = comboBox1.SelectedValue.ToString();
            displayBooking(SelectedValue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login b1 = new Login();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "") {
                {
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand("update Consultation set Status = 'Cancelled' where ConsultationID = @1", conn);
                    cmd.Parameters.AddWithValue("@1", comboBox1.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    refreshGrid();
                    MessageBox.Show("Consultation has been cancelled");
                }
            }
        
        }

        private void btnResetStud_Click(object sender, EventArgs e)
        {
            ChangePassword b1 = new ChangePassword();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }
    }
    }

