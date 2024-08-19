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
    public partial class StudentUpdate : Form
    {
        public StudentUpdate()
        {
            InitializeComponent();
            displayInfo();
        }
        OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Geneva\Desktop\IConsult Final-20231009T104045Z-001\IConsult Final\IConsult 5\IConsult\IConsult\IS .accdb");


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void displayInfo()
        {

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Student where StudentNo='" + AdminMainMenu.adminMainMenu.userID + "'", conn);
            da.Fill(dt);

            txtStudNo.Text = dt.Rows[0]["StudentNo"].ToString();
            txtFName.Text = dt.Rows[0]["FName"].ToString();
            txtLName.Text = dt.Rows[0]["LName"].ToString();
            txtDegType.Text = dt.Rows[0]["DegreeType"].ToString();
            


        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand cmd = new OleDbCommand("update Student set FName = @1, LName = @2, DegreeType = @4 where StudentNo = '" + AdminMainMenu.adminMainMenu.userID + "' ", conn);
            cmd.Parameters.AddWithValue("@1", txtFName.Text.ToString());
            cmd.Parameters.AddWithValue("@2", txtLName.Text.ToString());
            cmd.Parameters.AddWithValue("@4", txtDegType.Text.ToString());

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Student profile has been successfully updated");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("delete from Student where StudentNo = '"+AdminMainMenu.adminMainMenu.userID+"'", conn);
            DialogResult ans = MessageBox.Show("Delete this profile?", "Confirmation", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student profile has been deleted");
                AdminMainMenu b1 = new AdminMainMenu();
                this.Visible = false;
                // show new instance of the Booking form
                b1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Student profile not deleted");
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminMainMenu b1 = new AdminMainMenu();
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
