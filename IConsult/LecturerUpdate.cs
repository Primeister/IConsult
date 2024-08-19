using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IConsult
{
    public partial class LecturerUpdate : Form
    {
        public LecturerUpdate()
        {
            InitializeComponent();
            comboBoxSchool.SelectedIndex= -1;
            comboBox2.SelectedIndex= -1;
            displayInfo();
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");


        private void displayInfo()
        {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Lecturer where LecturerNo='" + AdminMainMenu.adminMainMenu.userID + "'", conn);
            da.Fill(dt);

            txtLectNo.Text = dt.Rows[0]["LecturerNo"].ToString();
            txtFName.Text = dt.Rows[0]["FName"].ToString();
            txtLName.Text = dt.Rows[0]["LName"].ToString();
            txtCourse.Text = dt.Rows[0]["Course"].ToString();
            txtOffice.Text = dt.Rows[0]["Office"].ToString();
            txtSchool.Text = dt.Rows[0]["School"].ToString();


        }

        private void comboBoxSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSchool.Text = comboBoxSchool.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCourse.Text = comboBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            AdminMainMenu b1 = new AdminMainMenu();
            this.Visible = false;
            // show new instance of the Booking form
            b1.ShowDialog();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("update Lecturer set FName = @1, LName = @2, Office = @4, Course = @5,  School = @6 where LecturerNo = '" + AdminMainMenu.adminMainMenu.userID+"' ", conn);
            cmd.Parameters.AddWithValue("@1", txtFName.Text.ToString());
            cmd.Parameters.AddWithValue("@2", txtLName.Text.ToString());
            cmd.Parameters.AddWithValue("@4", txtOffice.Text.ToString());
            cmd.Parameters.AddWithValue("@5", txtCourse.Text.ToString());
            cmd.Parameters.AddWithValue("@6", txtSchool.Text.ToString());
            
            cmd.ExecuteNonQuery();
            conn.Close();
            
            MessageBox.Show("Lecturer profile has been successfully updated");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            conn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from Lecturer where LecturerNo = '" + AdminMainMenu.adminMainMenu.userID + "'", conn);
            DialogResult ans = MessageBox.Show("Delete this profile?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lecturer profile has been deleted");
                AdminMainMenu b1 = new AdminMainMenu();
                this.Visible = false;
                // show new instance of the Booking form
                b1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lecturer profile not deleted");
            }
            conn.Close();
        }
    }

}
