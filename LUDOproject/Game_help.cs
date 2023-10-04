using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUDOproject
{
    public partial class Game_help : Form
    {
        public Game_help()
        {
            InitializeComponent();
        }

        private void Game_help_Load(object sender, EventArgs e)
        {
            timer1.Interval = 80;
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Top--;
            if (label2.Top <= -190) label2.Top = 140;
        }
    }
}
