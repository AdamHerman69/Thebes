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
    public partial class DigResult : Form
    {
        public DigResult(List<IToken> dugTokens)
        {
            InitializeComponent();

            List<ITokenView> tokens = dugTokens.ConvertAll(UIGame.ToView);
            foreach (ITokenView token in tokens)
            {
                flpTokens.Controls.Add(new PictureBox
                {
                    Width = 85,
                    Height = 85,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = token,
                    Image = Image.FromFile(UIConfig.IMG_FOLDER + token.FileName)
                });
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
