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
        IUIGame game;
        PictureBox[] displayCards = new PictureBox[4];
        PictureBox[] exhibitions = new PictureBox[3];
        List<PlayerDisplay> playerDisplays;
        Layout layout;

        public GameForm(IUIGame game)
        {
            InitializeComponent();
            Initialize(game);
        }

        public void Initialize(IUIGame game)
        {
            this.SuspendLayout();
            this.game = game;
            playerDisplays = new List<PlayerDisplay>();

            layout = ThebesUI.Layout.ParseLayout("layout.json");

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
            pBoard.BackgroundImage = Image.FromFile(UIConfig.IMG_FOLDER + "board.jpg");
            Controls.Add(pBoard);

            // display cards
            Rectangle pbDims;
            for (int i = 0; i < displayCards.Length; i++)
            {
                pbDims = layout.DisplayedCards[i];
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
                pBoard.Controls.Add(displayCards[i]);
            }

            // exhibitions
            for (int i = 0; i < exhibitions.Length; i++)
            {
                pbDims = layout.DisplayedExhibitions[i];
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
                pBoard.Controls.Add(exhibitions[i]);
            }

            UpdateBoard();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ResumeLayout(false);
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
            //pbBoard.SendToBack();

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
            if(game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)] != null)
            {
                game.ExecuteAction(new ExecuteExhibitionAction((IExhibitionCard)game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)].Card));
                UpdateBoard();
            }
        }

        private void bUseZeppelin_Click(object sender, EventArgs e)
        {
            game.ExecuteAction(new ZeppelinAction());
            UpdateBoard();
        }

        private void ChangeCards(ICardChangePlace place)
        {
            game.ExecuteAction(new ChangeCardsAction(place));
            UpdateBoard();
        }

        private void OpenDigForm(IDigSiteSimpleView digSite)
        {
            DigForm digForm = new DigForm((IDigSiteFullView)digSite, game.ActivePlayer);
            digForm.ShowDialog();
        }

        private void pBoard_Click(object sender, EventArgs e)
        {
            Point clickPosition = pBoard.PointToClient(MousePosition);
            foreach (KeyValuePair<string, Rectangle> placeName_rec in layout.Places)
            {
                if (placeName_rec.Value.IsInside(clickPosition))
                {
                    IPlace place = GameSettings.getPlaceByName(placeName_rec.Key);
                    if (place is IDigSiteSimpleView)
                    {
                        OpenDigForm((IDigSiteSimpleView)place);
                    }
                    else if (place is ICardChangePlace)
                    {
                        ChangeCards((ICardChangePlace)place);
                    }
                    else if (place is IUniversity)
                    {
                        MessageBox.Show($"uni: {place}");
                    }
                }
            }
        }

        private void bSaveGame_Click(object sender, EventArgs e)
        {
            //open file browser
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "THB | *.thb",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GameState.Serialize(game, sfd.FileName);
            }
        }

        private void bExitGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Do you want to save the game before quitting?",
                                     "Save game?",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                bSaveGame_Click(null, null);
            }
            else
            {
                this.Close();
            }
        }
    }
}
