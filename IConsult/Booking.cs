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
using static IConsult.IS_DataSet;
using System.Xml.Linq;
using System.Collections;

namespace IConsult
{
    public partial class Booking : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
        public Booking()
        {
            InitializeComponent();
            refreshGrid();
        }

        public void refreshGrid()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                OleDbCommand cmd = new OleDbCommand("select LecturerNo, FName & ' ' & LName as[name] from Lecturer", conn);
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select LecturerNo, FName & ' ' & LName as[name] from Lecturer", conn);

                    da.Fill(dt);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "name";
                    comboBox1.ValueMember = "LecturerNo";
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

        private void label2_Click(object sender, EventArgs e)
        {
           
        }
        private Boolean validate(bool n)
        {
            if(!radGroup.Checked && !radIndividual.Checked) {
                n = false;
                MessageBox.Show("Please select consultation type");
            }
            if (comboBox1.Text == "")
            {
                n = false;
                MessageBox.Show("Please select timeslot ");
            }
            return n;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Boolean n = true;
            n=validate(n);
            if (n)
            {
                string conType = "";
                if (radGroup.Checked)
                {
                    conType = "Group";
                }
                else if (radIndividual.Checked)
                {
                    conType = "Individual";
                }
                string venue = "";
                conn.Close();
                conn.Open();
                DataTable dt = new DataTable();
                OleDbCommand cmd1 = new OleDbCommand("select Office from Lecturer where LecturerNo ='" + comboBox1.SelectedValue + "' ", conn);
                OleDbDataReader dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Office from Lecturer where LecturerNo ='" + comboBox1.SelectedValue + "' ", conn);
                    da.Fill(dt);

                    venue = dt.Rows[0]["Office"].ToString();

                }


                OleDbCommand cmd = new OleDbCommand("insert into Consultation(StudentNo, LecturerNo, Timeslot, ConsultationType, Venue, ConsultationDate, Status) values (@1, @2, @3, @4, @5, @6, @7)", conn);
                cmd.Parameters.AddWithValue("@1", Login.login.studNo);
                cmd.Parameters.AddWithValue("@2", comboBox1.SelectedValue);
                cmd.Parameters.AddWithValue("@3", comboBox2.Text);
                cmd.Parameters.AddWithValue("@4", conType);
                cmd.Parameters.AddWithValue("@5", venue);
                cmd.Parameters.AddWithValue("@6", monthCalendar1.SelectionRange.Start.ToShortDateString());
                cmd.Parameters.AddWithValue("@7", "Pending");


                cmd.ExecuteNonQuery();
          

                MessageBox.Show("You have successfully booked a consultation");
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            monthCalendar1.BoldedDates = null;
            List<DateTime> dates = new List<DateTime>();
            conn.Close();
            conn.Open();
            if (comboBox1.Text != "")
            {
                
                DataTable dt = new DataTable();
                
                
                OleDbCommand cmd = new OleDbCommand("select ConsultationDay from Timeslot where LecturerNo ='" + comboBox1.SelectedValue + "' ", conn);
                
                OleDbDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select ConsultationDay from Timeslot where LecturerNo ='" + comboBox1.SelectedValue + "' ", conn);
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        string date = row["ConsultationDay"].ToString();
                        DateTime newDate = DateTime.Parse(date);
                        dates.Add(newDate);
                    }


                    DateTime[] moreDates = new DateTime[dates.Count];
                    for (int i = 0; i < dates.Count; i++)
                    {
                        moreDates[i] = dates[i];
                    }
                    var highlighted = moreDates;
                    monthCalendar1.BoldedDates = moreDates;
                    monthCalendar1.SetDate(moreDates[0]);


                }
                
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            DataTable dt = new DataTable();
            OleDbCommand cmd = new OleDbCommand("select * from Timeslot where ConsultationDay ='" + monthCalendar1.SelectionRange.Start.ToShortDateString() + "' ", conn);
            
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from Timeslot where ConsultationDay ='" + monthCalendar1.SelectionRange.Start.ToShortDateString() + "' ", conn);
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "Timeslot";
                comboBox2.ValueMember = "TimeslotID";
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainMenu b1 = new MainMenu();
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
