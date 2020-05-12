using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThebesCore;

namespace ThebesUI
{
    public partial class ResultsForm : Form
    {
        public ResultsForm(List<IPlayer> players)
        {
            InitializeComponent();

            players.Sort((p1, p2) => p2.Points.CompareTo(p1.Points));

            lName1.Text = players[0].Name;
            lPoints1.Text = players[0].Points.ToString();

            lName2.Text = players[1].Name;
            lPoints2.Text = players[1].Points.ToString();

            if (players.Count > 2)
            {
                lName3.Text = players[2].Name;
                lPoints3.Text = players[2].Points.ToString();

                lName3.Visible = true;
                lPoints3.Visible = true;
                pictureBox3.Visible = true;
            }     
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
