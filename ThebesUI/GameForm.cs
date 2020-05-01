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
    public partial class GameForm : Form
    {
        UIGame game;
        PictureBox[] displayCards = new PictureBox[4];
        public GameForm(UIGame game)
        {
            InitializeComponent();
            Initialize(game);
        }

        public void Initialize(UIGame game)
        {
            this.game = game;

            // initialize player displays
            playerDisplay1.Initialize(game.Players[0]);
            playerDisplay2.Initialize(game.Players[1]);
            if (game.Players.Count >= 3)
            {
                playerDisplay3.Initialize(game.Players[2]);
            }
            if (game.Players.Count >= 4)
            {
                playerDisplay4.Initialize(game.Players[3]);
            }

            // display cards events
            pbCard1.Click += pbCard_Click;
            pbCard2.Click += pbCard_Click;
            pbCard3.Click += pbCard_Click;
            pbCard4.Click += pbCard_Click;

            // exhibition events
            pbExhibition1.Click += pbExhibition_Click;
            pbExhibition2.Click += pbExhibition_Click;
            pbExhibition3.Click += pbExhibition_Click;


            UpdateBoard();
        }

        public void UpdateBoard()
        {
            // display cards
            pbCard1.Image = Image.FromFile(game.GetImgFilePath(game.AvailableCards.AvailableCards[0]));
            pbCard2.Image = Image.FromFile(game.GetImgFilePath(game.AvailableCards.AvailableCards[1]));
            pbCard3.Image = Image.FromFile(game.GetImgFilePath(game.AvailableCards.AvailableCards[2]));
            pbCard4.Image = Image.FromFile(game.GetImgFilePath(game.AvailableCards.AvailableCards[3]));

            // exhibitions
            if (game.ActiveExhibitions.Exhibitions[0] != null)
            {
                pbExhibition1.Image = Image.FromFile(game.GetImgFilePath(game.ActiveExhibitions.Exhibitions[0]));
            }
            else
            {
                pbExhibition1.Image = null;
            }

            if (game.ActiveExhibitions.Exhibitions[0] != null)
            {
                pbExhibition2.Image = Image.FromFile(game.GetImgFilePath(game.ActiveExhibitions.Exhibitions[1]));
            }
            else
            {
                pbExhibition2.Image = null;
            }

            if (game.ActiveExhibitions.Exhibitions[0] != null)
            {
                pbExhibition3.Image = Image.FromFile(game.GetImgFilePath(game.ActiveExhibitions.Exhibitions[2]));
            }
            else
            {
                pbExhibition3.Image = null;
            }

        }

        private void pbCard_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureBox)sender).AccessibleDescription);
            game.activePlayer.MoveAndTakeCard(game.AvailableCards.AvailableCards[index]);
            game.NextMove();
            this.UpdateBoard();
        }

        private void pbExhibition_Click(object sender, EventArgs e)
        {
            int index = int.Parse(((PictureBox)sender).AccessibleDescription);
            game.activePlayer.MoveAndTakeCard(game.ActiveExhibitions.Exhibitions[index]);
            game.NextMove();
            this.UpdateBoard();
        }

        private void bUseZeppelin_Click(object sender, EventArgs e)
        {
            game.activePlayer.UseZeppelin();
        }
    }
}
