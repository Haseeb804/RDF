using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDF
{
    public partial class ProgressForm : Form
    {

        private readonly Timer timer;
        private int progressValue = 0;

        public ProgressForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            progressValue += 2;

            progressbar.ProgressColor = Color.FromArgb(101, 104, 184);

            progressbar.Increment(2);

            if (progressValue >= 100)
            {
                timer.Stop();
                await Task.Delay(1000);
                this.Hide();
                Login loginForm = new Login();
                loginForm.ShowDialog();
                this.Close();


            }
 
        }
}   }         
