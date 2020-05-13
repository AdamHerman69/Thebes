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
using ThebesAI;
using System.IO;

namespace ThebesUI
{
    public partial class WelcomeForm : Form
    {
        OpenFileDialog ofd = new OpenFileDialog() { Filter = "THB|*.thb" };

        public WelcomeForm()
        {
            InitializeComponent();

            playerInput1.Color = PlayerColor.red;
            playerInput2.Color = PlayerColor.green;
            playerInput3.Color = PlayerColor.blue;
            playerInput4.Color = PlayerColor.yellow;
        }

        private int GetPlayerCount()
        {
            int playerCount = 0;
            foreach (Control control in newGameBox.Controls)
            {
                if (control is PlayerInput && ((PlayerInput)control).Selected())
                {
                    playerCount++;
                }
            }
            return playerCount;
        }

        private void bStartNew_Click(object sender, EventArgs e)
        {
            int playerCount;
            if ((playerCount = GetPlayerCount()) < 2 || playerCount > 4)
            {
                MessageBox.Show("Unsupported number of players");
                return;
            }

            try
            {
                GameSettings.LoadFromFile(@"thebes_config.thc");
            }
            catch (Exception exception)
            {
                if (exception is FileNotFoundException)
                {
                    UIConfig.ErrorDialog("File not found error:\n" + exception.Message);
                }
                else if (exception is FormatException)
                {
                    UIConfig.ErrorDialog("Error processing the config file:\n" + exception.Message);
                }
                else
                {
                    UIConfig.ErrorDialog("Unknown error reading the config file:\n" + exception.Message);
                }
                return;
            }
            




            UIGame game = new UIGame(playerCount);

            // Create Players
            Dictionary<IPlayer, PlayerColor> players = new Dictionary<IPlayer, PlayerColor>();
            Player player;
            PlayerInput pi;
            foreach (Control control in newGameBox.Controls)
            {
                if (control is PlayerInput && ((PlayerInput)control).Selected())
                {
                    pi = (PlayerInput)control;
                    
                    if (pi.IsHuman())
                    {
                        player = new Player(
                            pi.PlayerName(),
                            GameSettings.Places.OfType<IDigSite>().ToList(),
                            GameSettings.StartingPlace,
                            UIConfig.ErrorDialog,
                            game.AvailableCards.ChangeDisplayedCards,
                            game.AvailableCards.GiveCard,
                            game.Deck.Discard,
                            game.ActiveExhibitions.GiveExhibition,
                            game.PlayersOnWeek
                            );
                    }
                    else
                    {
                        player = new AIPlayer(
                            pi.PlayerName(),
                            GameSettings.Places.OfType<IDigSite>().ToList(),
                            GameSettings.StartingPlace,
                            GameSettings.Places,
                            UIConfig.ErrorDialog,
                            game.AvailableCards.ChangeDisplayedCards,
                            game.AvailableCards.GiveCard,
                            game.Deck.Discard,
                            game.ActiveExhibitions.GiveExhibition,
                            game.PlayersOnWeek
                            );
                        ((AIPlayer)player).Init(new TestAI(player, game));
                    }
                    
                    players.Add(player, pi.Color);
                }
            }
            try
            {
                game.Initialize(players);
            }
            catch (Exception exception)
            {
                UIConfig.ErrorDialog("Error initializing the game:\n" + exception.Message);
                return;
            }
            

            this.Hide();
            GameForm gameForm = new GameForm(game);
            gameForm.ShowDialog();
            this.Close();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofd.SafeFileName;
                tbFilePath.Text = ofd.FileName;
            }
        }

        private void bStartLoaded_Click(object sender, EventArgs e)
        {
            string filePath = tbFilePath.Text;

            IUIGame game;
            try
            {
                game = GameState.Deserialize(filePath);
            }
            catch (Exception exception)
            {
                UIConfig.ErrorDialog("Error loading the game from file:\n" + exception.Message);
                return;
            }

            this.Hide();
            GameForm gameForm = new GameForm(game);
            gameForm.ShowDialog();

            this.Close();
        }
    }
}
