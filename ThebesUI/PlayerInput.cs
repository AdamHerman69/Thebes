using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThebesUI
{
    public enum PlayerColor {
        red,
        green,
        blue,
        yellow
    }
    
    public partial class PlayerInput : UserControl
    {
        public PlayerInput()
        {
            InitializeComponent();
        }

        public bool Selected()
        {
            return cbSelected.Checked;
        }

        public string PlayerName()
        {
            return tbName.Text;
        }

        public PlayerColor Color()
        {
            return (PlayerColor)cbColor.SelectedIndex;
        }

        public bool IsHuman()
        {
            return rbHuman.Checked;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            cbSelected.Checked = true;
            rbHuman.Checked = true;
        }
    }
}
