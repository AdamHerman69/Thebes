using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        PictureBox[] exhibitions = new PictureBox[3];
        List<PlayerDisplay> playerDisplays;

        public GameForm(UIGame game)
        {
            InitializeComponent();
            Initialize(game);
        }

        public void Initialize(UIGame game)
        {
            this.SuspendLayout();
            this.game = game;
            playerDisplays = new List<PlayerDisplay>();

            ThebesUI.Layout layout = ThebesUI.Layout.ParseLayout("layout.json");

            // initialize player displays
            playerDisplay1.Initialize(game.Players[0], layout);
            playerDisplays.Add(playerDisplay1);
            playerDisplay2.Initialize(game.Players[1], layout);
            playerDisplays.Add(playerDisplay2);
            if (game.Players.Count >= 3)
            {
                playerDisplay3.Initialize(game.Players[2], layout);
                playerDisplays.Add(playerDisplay3);
            }
            if (game.Players.Count >= 4)
            {
                playerDisplay4.Initialize(game.Players[3], layout);
                playerDisplays.Add(playerDisplay4);
            }
            foreach (PlayerDisplay playerDisplay in playerDisplays)
            {
                Controls.Add(playerDisplay);
            }

            // background
            pbBoard.Image = Image.FromFile(UIConfig.IMG_FOLDER + "board.jpg");
            pbBoard.SendToBack();
            Controls.Add(pbBoard);

            // display cards
            Rectangle pbDims;
            for (int i = 0; i < displayCards.Length; i++)
            {
                pbDims = BoardToForm(layout.DisplayedCards[i]);
                displayCards[i] = new PictureBox()
                {
                    Location = pbDims.topLeft,
                    Width = pbDims.Width,
                    Height = pbDims.Height,
                    Visible = true,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                displayCards[i].Click += pbCard_Click;
                Controls.Add(displayCards[i]);
            }

            // exhibitions
            for (int i = 0; i < exhibitions.Length; i++)
            {
                pbDims = BoardToForm(layout.DisplayedExhibitions[i]);
                exhibitions[i] = new PictureBox()
                {
                    Location = pbDims.topLeft,
                    Width = pbDims.Width,
                    Height = pbDims.Height,
                    Visible = true,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                exhibitions[i].Click += pbExhibition_Click;
                Controls.Add(exhibitions[i]);
            }

            UpdateBoard();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ResumeLayout(false);
        }

        private Rectangle BoardToForm(Rectangle rec)
        {
            Point boardTopLeft = pbBoard.Location;
            return new Rectangle(new Point(boardTopLeft.X + rec.topLeft.X, boardTopLeft.Y + rec.topLeft.Y), new Point(boardTopLeft.X + rec.bottomRight.X, boardTopLeft.Y + rec.bottomRight.Y));
        }

        public void UpdateBoard()
        {
            // player displays
            foreach (PlayerDisplay playerDisplay in playerDisplays)
            {
                playerDisplay.UpdateInfo();
            }

            // display cards
            for (int i = 0; i < displayCards.Length; i++)
            {
                displayCards[i].Image = GetImage(game.DisplayedCards[i]);
            }

            // exhibitions
            for (int i = 0; i < exhibitions.Length; i++)
            {
                if (game.DisplayedExhibitions[i] != null)
                {
                    exhibitions[i].Image = GetImage(game.DisplayedExhibitions[i]);
                }
            }
            pbBoard.SendToBack();

            // TODO move player pieces etc
        }

        private Image GetImage(ICardView card)
        {
            try
            {
                return Image.FromFile(UIConfig.IMG_FOLDER + card.FileName);
            }
            catch (FileNotFoundException e)
            {
                return Image.FromFile(UIConfig.IMG_FOLDER + "c_not_found.png");
            }
        }

        private void pbCard_Click(object sender, EventArgs e)
        {
            game.ExecuteAction(new TakeCardAction(game.DisplayedCards[Array.IndexOf(displayCards, sender)].Card));
            UpdateBoard();
        }

        private void pbExhibition_Click(object sender, EventArgs e)
        {
            game.ExecuteAction(new ExecuteExhibitionAction((IExhibitionCard)game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)].Card));
            UpdateBoard();
        }

        private void bUseZeppelin_Click(object sender, EventArgs e)
        {
            game.ExecuteAction(new ZeppelinAction());
            UpdateBoard();
        }
    }
}
