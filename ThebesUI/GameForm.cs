using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ThebesCore;

namespace ThebesUI
{
    public partial class GameForm : Form
    {
        IUIGame game;
        PictureBox[] displayCards = new PictureBox[4];
        PictureBox[] exhibitions = new PictureBox[3];
        Dictionary<IPlayer, TransparentPictureBox> smallPieces;
        Dictionary<IPlayer, TransparentPictureBox> bigPieces;
        TransparentPictureBox yearCounter;
        List<PlayerDisplay> playerDisplays;
        Layout layout;

        public GameForm(IUIGame game)
        {
            InitializeComponent();
            Initialize(game);
            game.ExecuteAction(null);
            UpdateBoard();
        }

        public void Initialize(IUIGame game)
        {
            this.SuspendLayout();
            this.game = game;
            playerDisplays = new List<PlayerDisplay>();

            layout = ThebesUI.Layout.ParseLayout("layout.json");

            // initialize player displays
            playerDisplay1.Initialize(game.Players[0], layout, game.Colors[game.Players[0]]);
            playerDisplays.Add(playerDisplay1);
            playerDisplay2.Initialize(game.Players[1], layout, game.Colors[game.Players[1]]);
            playerDisplays.Add(playerDisplay2);
            if (game.Players.Count >= 3)
            {
                playerDisplay3.Initialize(game.Players[2], layout, game.Colors[game.Players[2]]);
                playerDisplays.Add(playerDisplay3);
            }
            if (game.Players.Count >= 4)
            {
                playerDisplay4.Initialize(game.Players[3], layout, game.Colors[game.Players[3]]);
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

            // week counter
            smallPieces = new Dictionary<IPlayer, TransparentPictureBox>();
            foreach (IPlayer player in game.Players)
            {
                pbDims = layout.WeekCounter[0];
                smallPieces.Add(player, new TransparentPictureBox()
                {
                    Location = pbDims.RectanglePositionCenter(34, 30),
                    Width = 34,
                    Height = 30,
                    Visible = true,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Image = Image.FromFile(UIConfig.IMG_FOLDER + $"p_small_{game.Colors[player]}.png")
                });
                pBoard.Controls.Add(smallPieces[player]);
            }

            // player pieces
            bigPieces = new Dictionary<IPlayer, TransparentPictureBox>();
            foreach (IPlayer player in game.Players)
            {
                pbDims = layout.Places[player.CurrentPlace.Name];
                bigPieces.Add(player, new TransparentPictureBox()
                {
                    Location = pbDims.RectanglePositionCenter(35, 71),
                    Width = 35,
                    Height = 71,
                    Visible = true,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Image = Image.FromFile(UIConfig.IMG_FOLDER + $"p_big_{game.Colors[player]}.png")
                });
                pBoard.Controls.Add(bigPieces[player]);
            }

            // year counter
            pbDims = layout.YearCounter[(game.ActivePlayer.Time.CurrentYear % 10) - 1];
            yearCounter = new TransparentPictureBox()
            {
                Location = pbDims.RectanglePositionCenter(34, 30),
                Width = 34,
                Height = 30,
                Visible = true,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Image = Image.FromFile(UIConfig.IMG_FOLDER + $"p_small_black.png")
            };
            pBoard.Controls.Add(yearCounter);

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
                else
                {
                    exhibitions[i].Image = null;
                }
            }

            // week counter
            foreach (KeyValuePair<IPlayer, TransparentPictureBox> player_pb in smallPieces.OrderByDescending(kvp => kvp.Key))
            {
                player_pb.Value.Location = layout.WeekCounter[player_pb.Key.Time.CurrentWeek - 1].RectanglePositionCenter(player_pb.Value.Width, player_pb.Value.Height);
                player_pb.Value.Top -= 10 * (player_pb.Key.Time.SameWeekOrder - 1);
                player_pb.Value.BringToFront();
            }

            // player pieces
            int offset = 0;
            foreach (KeyValuePair<IPlayer, TransparentPictureBox> player_pb in bigPieces)
            {
                player_pb.Value.Location = layout.Places[player_pb.Key.CurrentPlace.Name].RectanglePositionCenter(player_pb.Value.Width, player_pb.Value.Height);
                player_pb.Value.Left += offset;
                offset += 20;
            }

            // year counter
            yearCounter.Location = layout.YearCounter[(game.ActivePlayer.Time.CurrentYear % 10) - 1].RectanglePositionCenter(yearCounter.Width, yearCounter.Height);

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
            ExecuteAction(new TakeCardAction(game.DisplayedCards[Array.IndexOf(displayCards, sender)].Card));

        }

        private void pbExhibition_Click(object sender, EventArgs e)
        {
            if(game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)] != null)
            {
                ExecuteAction(new ExecuteExhibitionAction((IExhibitionCard)game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)].Card));

            }
        }

        private void bUseZeppelin_Click(object sender, EventArgs e)
        {
            ExecuteAction(new ZeppelinAction());
        }

        private void ChangeCards(ICardChangePlace place)
        {
            ExecuteAction(new ChangeCardsAction(place));
        }

        private void OpenDigForm(IDigSiteSimpleView digSite)
        {
            DigForm digForm = new DigForm((IDigSiteFullView)digSite, game.ActivePlayer, ExecuteAction);
            DialogResult result = digForm.ShowDialog();

            UpdateBoard();
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

        public void ExecuteAction(IAction action)
        {
            if (!game.ExecuteAction(action))
            {
                UpdateBoard();
            }
            else
            {
                ResultsForm resultsForm = new ResultsForm(game.Players);
                resultsForm.ShowDialog();
                this.Close();
            }
        }

        private void bEndYear_Click(object sender, EventArgs e)
        {
            ExecuteAction(new EndYearAction());
        }
    }

    class TransparentPictureBox : PictureBox
    {
        public TransparentPictureBox()
        {
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent != null && this.BackColor == Color.Transparent)
            {
                using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.Controls.Cast<Control>()
                          .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                          .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                          .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                          .ToList()
                          .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));

                    e.Graphics.DrawImage(bmp, -Left, -Top);

                }
            }
            base.OnPaint(e);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
