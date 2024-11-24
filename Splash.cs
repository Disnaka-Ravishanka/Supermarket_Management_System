using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton;
using ComponentFactory.Krypton.Toolkit;

namespace Supermarket_app
{
    public partial class Splash : KryptonForm
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private int startpoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 5;
            ProgressBar.Value = startpoint;

            if (ProgressBar.Value == 100)
            {
                ProgressBar.Value = 0;
                timer1.Stop();
                Login login = new Login();
                this.Hide();
                login.Show();
            }
        }

        private void ProgressBar_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}