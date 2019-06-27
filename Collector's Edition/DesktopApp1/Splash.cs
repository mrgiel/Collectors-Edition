using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp1
{
    public partial class Splash : Form
    {
        int count = 0;
        public Splash()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 60;
            count += 5;

            if (count == 300)
            {
                timer1.Stop();
                this.Hide();
                MainPage main = new MainPage();
                main.Show();
            }
        }
    }
}
