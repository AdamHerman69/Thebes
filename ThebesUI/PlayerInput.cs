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
        public PlayerColor Color { get { return (PlayerColor)cbColor.SelectedIndex; } set { cbColor.SelectedIndex = (int)value; } }
        public Type Type { get { return (Type)cbAI.SelectedItem; } }
        public PlayerInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds new Type to the list
        /// </summary>
        /// <param name="type"></param>
        public void AddDefaultAI(Type type)
        {
            cbAI.Items.Add(type);
            cbAI.SelectedItem = type;
        }

        public bool Selected()
        {
            return cbSelected.Checked;
        }

        public string PlayerName()
        {
            return tbName.Text;
        }

        public bool IsHuman()
        {
            return rbHuman.Checked;
        }

        public void AddAI(Type type)
        {
            cbAI.Items.Add(type);
        }

        /// <summary>
        /// Checkes this player if his name is filled in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (!rbHuman.Checked && !rbAI.Checked)
            {
                rbHuman.Checked = true;
            }
            cbSelected.Checked = true;
        }

        /// <summary>
        /// Displays the AI picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAI.Checked)
            {
                cbAI.Visible = true;
            }
        }

        /// <summary>
        /// Hides the AI picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbHuman_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHuman.Checked)
            {
                cbAI.Visible = false;
            }
        }
    }
}
