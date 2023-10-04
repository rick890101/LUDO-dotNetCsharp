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
    public partial class Game_setting : Form
    {
        
        public Game_setting()
        {
            InitializeComponent();
        }
        public short GetMode(short Mode)
        {
            if (radioButton1.Checked == true)
            {
                Mode = 0;
            }
            else if (radioButton2.Checked == true)
            {
                Mode = 1;
            }
            return (short)(Mode);
        }
        public short GetPlayer(short Player)
        {
            if (radioButton1.Checked == true)
            {   if (radioButton3.Checked == true)
                    Player = 2;
                if (radioButton4.Checked == true)
                    Player = 3;
                if (radioButton5.Checked == true)
                    Player = 4;
            }
            else if (radioButton2.Checked == true)
            {
                if (radioButton6.Checked == true)
                    Player = 2;
                if (radioButton7.Checked == true)
                    Player = 3;
                if (radioButton8.Checked == true)
                    Player = 4;
            }
            return (short)(Player);
        }
        public short GetSound(short Sound)
        {
            if (radioButton9.Checked == true)
            {
                Sound = 1;
            }
            else if (radioButton10.Checked == true)
            {
                Sound = 10;
            }
            return (short)(Sound);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
                radioButton6.Checked = false;
                radioButton7.Checked = false;
                radioButton8.Checked = false;
            } else if (radioButton2.Checked == true)
            {
                groupBox2.Enabled = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                groupBox3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        private void Game_setting_Load(object sender, EventArgs e)
        {
            
        }
    }
}
