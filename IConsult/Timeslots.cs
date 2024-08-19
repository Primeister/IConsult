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
    public partial class Timeslots : Form
    {
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");
        public Timeslots()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("insert into Timeslot(ConsultationDay, Timeslot, LecturerNo) values (@1, @2, @4)", conn);
            cmd.Parameters.AddWithValue("@1", monthCalendar1.SelectionRange.Start.ToShortDateString());
            cmd.Parameters.AddWithValue("@2", startTime.Text+" - "+endTime.Text);
            cmd.Parameters.AddWithValue("@4", Login.login.studNo);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("You have successfully created a timeslot");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LecturerMainMenu f1 = new LecturerMainMenu();
            this.Visible = false;
            // show new instance of the Login form
            f1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
