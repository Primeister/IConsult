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
    public partial class LecturerMainMenu : Form
    {

        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");

        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbCommand cmd = new OleDbCommand("select * from Consultation where LecturerNo='" + Login.login.studNo + "'", conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from Consultation where LecturerNo='" + Login.login.studNo + "' ", conn);

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
        private void displayBooking(String id)
        {

            String query = "Select * from Consultation where ConsultationID = " + id;
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
            da.Fill(dt);
            string Student = dt.Rows[0]["StudentNo"].ToString();
            DataTable dt2 = new DataTable();
            OleDbDataAdapter da2 = new OleDbDataAdapter("select * from Student where StudentNo = '" + Student + "' ", conn);
            da2.Fill(dt2);
            lblStudName.Text = dt2.Rows[0]["FName"].ToString();
            lblDate.Text = dt.Rows[0]["ConsultationDate"].ToString();
            lblConType.Text = dt.Rows[0]["ConsultationType"].ToString();
            lblTime.Text = dt.Rows[0]["Timeslot"].ToString();
            lblVenue.Text = dt.Rows[0]["Venue"].ToString();
            label7.Text = dt.Rows[0]["Status"].ToString();

        }
        public LecturerMainMenu()
        {
            InitializeComponent();
            refreshGrid();
            displayInfo();
        }

        private void displayInfo()
        {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Lecturer where LecturerNo='" + Login.login.studNo + "'", conn);
            da.Fill(dt);
            lblFName.Text = dt.Rows[0]["FName"].ToString() + " " + dt.Rows[0]["LName"].ToString();
            lblLectNo.Text = Login.login.studNo;



        }


        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Timeslots f1 = new Timeslots();
            this.Visible = false;
            // show new instance of the Login form
            f1.ShowDialog();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {

                conn.Close();

                if (!radAccept.Checked && !radCancel.Checked && !radComplete.Checked) { MessageBox.Show("Select a status"); }
                else
                {
                    if (radAccept.Checked)
                    {
                        conn.Open();

                        OleDbCommand cmd = new OleDbCommand("update Consultation set Status = 'Upcoming' where ConsultationID = @1", conn);
                        cmd.Parameters.AddWithValue("@1", comboBox1.SelectedValue.ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        refreshGrid();
                        MessageBox.Show("Consultation has been accepted");
                    }
                    else if (radCancel.Checked)
                    {
                        conn.Open();

                        OleDbCommand cmd = new OleDbCommand("update Consultation set Status = 'Cancelled' where ConsultationID = @1", conn);
                        cmd.Parameters.AddWithValue("@1", comboBox1.SelectedValue.ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        refreshGrid();
                        MessageBox.Show("Consultation has been cancelled");
                    }
                    else if (radComplete.Checked)
                    {

                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("update Consultation set Status = 'Completed' where ConsultationID = @1", conn);
                        cmd.Parameters.AddWithValue("@1", comboBox1.SelectedValue.ToString());
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        refreshGrid();
                        MessageBox.Show("Consultation completed");
                    }
                }
            }
        }

        private void btnResetLect_Click(object sender, EventArgs e)
        {
            ChangePassword b1 = new ChangePassword();
            this.Visible = false;
            // show new instance of the ChangePassword form
            b1.ShowDialog();
        }
    }
}
