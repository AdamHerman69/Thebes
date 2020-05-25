using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ThebesCore;

namespace ThebesUI
{
    public partial class GameForm : Form
    {
        IUIGame game;
        PictureBox[] displayCards = new PictureBox[4];
        PictureBox[] exhibitions = new PictureBox[3];
        ToolTip[] cardToolTips = new ToolTip[4];
        ToolTip[] exhibitionToolTips = new ToolTip[3];
        Dictionary<IPlayer, TransparentPictureBox> smallPieces;
        Dictionary<IPlayer, TransparentPictureBox> bigPieces;
        TransparentPictureBox yearCounter;
        List<PlayerDisplay> playerDisplays;
        Layout layout;

        /// <summary>
        /// Enables double buffering for all the controls (to reduce flickering)
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        public GameForm(IUIGame game)
        {
            InitializeComponent();
            Initialize(game);

            game.ExecuteAction(null);
            UpdateBoard();
        }

        /// <summary>
        /// Called from constructor ot initialize the form
        /// </summary>
        /// <param name="game"></param>
        private void Initialize(IUIGame game)
        {
            this.SuspendLayout();
            this.game = game;
            playerDisplays = new List<PlayerDisplay>();

            layout = ThebesUI.Layout.ParseLayout("layout.json");

            // player displays
            foreach (IPlayer player in game.Players)
            {
                PlayerDisplay pd = new PlayerDisplay();
                pd.Width = 368;
                pd.Height = 152;
                pd.Initialize(player, layout, game.Colors[player]);
                playerDisplays.Add(pd);
                flpPlayerDisplay.Controls.Add(pd);
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

            //tooltips
            for (int i = 0; i < cardToolTips.Length; i++)
            {
                cardToolTips[i] = new ToolTip();
            }
            for (int i = 0; i < exhibitionToolTips.Length; i++)
            {
                exhibitionToolTips[i] = new ToolTip();
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

        /// <summary>
        /// Called after every action. Redraws the board with current data
        /// </summary>
        public void UpdateBoard()
        {
            //DrawingControl.SuspendDrawing(this);

            // zeppelin button
            if (game.ActivePlayer.Zeppelins > 0)
            {
                cbUseZeppelin.Visible = true;
            }
            else
            {
                cbUseZeppelin.Visible = false;
            }

            // player displays
            foreach (PlayerDisplay playerDisplay in playerDisplays)
            {
                playerDisplay.UpdateInfo();
            }

            // display cards
            for (int i = 0; i < displayCards.Length; i++)
            {
                UIConfig.ReplaceImage(displayCards[i], GetImage(game.DisplayedCards[i]));
                cardToolTips[i].SetToolTip(displayCards[i], game.DisplayedCards[i].Description);
            }

            // exhibitions
            for (int i = 0; i < exhibitions.Length; i++)
            {
                if (game.DisplayedExhibitions[i] != null)
                {
                    UIConfig.ReplaceImage(exhibitions[i], GetImage(game.DisplayedExhibitions[i]));
                    exhibitionToolTips[i].SetToolTip(exhibitions[i], game.DisplayedExhibitions[i].Description);
                }
                else
                {
                    UIConfig.RemoveImage(exhibitions[i]);
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
            int index = (game.ActivePlayer.Time.CurrentYear % 10) - 1;
            if (index > 2) index = 2;
            yearCounter.Location = layout.YearCounter[index].RectanglePositionCenter(yearCounter.Width, yearCounter.Height);

            //DrawingControl.ResumeDrawing(this);
        }

        /// <summary>
        /// Gets the appropriate image for a card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private Image GetImage(ICardView card)
        {
            try
            {
                return Image.FromFile(UIConfig.IMG_FOLDER + card.FileName);
            }
            catch (FileNotFoundException)
            {
                return Image.FromFile(UIConfig.IMG_FOLDER + "c_not_found.png");
            }
        }

        /// <summary>
        /// Active player takes the card clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCard_Click(object sender, EventArgs e)
        {
            ExecuteAction(new TakeCardAction(game.DisplayedCards[Array.IndexOf(displayCards, sender)].Card));

        }

        /// <summary>
        /// Active player executes the exhibition clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbExhibition_Click(object sender, EventArgs e)
        {
            if(game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)] != null)
            {
                ExecuteAction(new ExecuteExhibitionAction((IExhibitionCard)game.DisplayedExhibitions[Array.IndexOf(exhibitions, sender)].Card));

            }
        }


        /// <summary>
        /// Active player changes cards
        /// </summary>
        /// <param name="place"></param>
        private void ChangeCards(ICardChangePlace place)
        {
            ExecuteAction(new ChangeCardsAction(place));
        }

        /// <summary>
        /// Opens the dig form
        /// </summary>
        /// <param name="digSite"></param>
        private void OpenDigForm(IDigSite digSite)
        {
            DigForm digForm = new DigForm(digSite, game.ActivePlayer, ExecuteAction);
            DialogResult result = digForm.ShowDialog();

            UpdateBoard();
        }

        /// <summary>
        /// Handles all clicks on the board. Then executes the appropriate action based on click location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pBoard_Click(object sender, EventArgs e)
        {
            Point clickPosition = pBoard.PointToClient(MousePosition);
            foreach (KeyValuePair<string, Rectangle> placeName_rec in layout.Places)
            {
                if (placeName_rec.Value.IsInside(clickPosition))
                {
                    IPlace place = GameSettings.getPlaceByName(placeName_rec.Key);
                    if (place is IDigSite)
                    {
                        OpenDigForm((IDigSite)place);
                    }
                    else if (place is ICardChangePlace)
                    {
                        ChangeCards((ICardChangePlace)place);
                    }
                    else if (place is IUniversity)
                    {
                        MessageBox.Show($"Uni: {place}");
                    }
                }
            }
        }

        /// <summary>
        /// Opens file browser and then saves the current game state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                try
                {
                    GameState.Serialize(game, sfd.FileName);
                    MessageBox.Show("Game state successfully saved");
                }
                catch (Exception exception)
                {
                    UIConfig.ErrorDialog("Error saving the game state:\n" + exception.Message);
                    return;
                }   
            }
        }

        /// <summary>
        /// Exits the game with a prompt to save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bExitGame_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Do you want to save the game before quitting?",
                                     "Save game?",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                bSaveGame_Click(null, null);
            }
            this.Close();
        }

        /// <summary>
        /// Executes the given action
        /// </summary>
        /// <param name="action"></param>
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

        /// <summary>
        /// Ends the year
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bEndYear_Click(object sender, EventArgs e)
        {
            ExecuteAction(new EndYearAction());
        }

        /// <summary>
        /// Toggles Zeppelin for the active player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUseZeppelin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseZeppelin.Checked)
            {
                ExecuteAction(new ZeppelinAction(true));
            }
            else
            {
                ExecuteAction(new ZeppelinAction(false));
            }
        }
    }
}
