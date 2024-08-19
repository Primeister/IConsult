using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IConsult
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (comboBox1.Text == "Student") {
                //continue from here
                for (int i = 0; i < StudentClass.studNo.Count(); ++i) {
                    if (StudentClass.studNo[i].Equals(textBox5.Text.ToString()))
                    {
                        index = i; break;
                    }
                }
                if (StudentClass.password[index].Equals(textBox6.Text.ToString())) {
                    Booking b1 = new Booking();
                    this.Visible = false;
                    // show new instance of the Booking form
                    b1.ShowDialog();
                }
            }
        }
    }
}
