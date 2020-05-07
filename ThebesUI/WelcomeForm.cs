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
    public partial class WelcomeForm : Form
    {
        OpenFileDialog ofd = new OpenFileDialog() { Filter = "THB|*.thb" };

        public WelcomeForm()
        {
            InitializeComponent();
        }

        public static void NotEnoughTimeDialog()
        {
            MessageBox.Show("You don't have enough time for that action");
        }

        private void bStartNew_Click(object sender, EventArgs e)
        {
            GameSettings.Initialize();

            // get player count
            int playerCount = 0;
            foreach (Control control in newGameBox.Controls)
            {
                if (control is PlayerInput && ((PlayerInput)control).Selected())
                {
                    playerCount++;
                }
            }

            UIGame game = new UIGame(playerCount);
            
            // Create Players
            List<IPlayer> players = new List<IPlayer>();
            foreach (Control control in newGameBox.Controls)
            {
                if (control is PlayerInput && ((PlayerInput)control).Selected())
                {
                    PlayerInput pi = (PlayerInput)control;
                    Player player = new Player(
                        pi.PlayerName(),
                        GameSettings.Places.OfType<IDigSiteSimpleView>().ToList(),
                        GameSettings.StartingPlace,
                        NotEnoughTimeDialog,
                        game.AvailableCards.ChangeDisplayedCards,
                        game.AvailableCards.GiveCard,
                        game.Deck.Discard,
                        game.ActiveExhibitions.GiveExhibition,
                        game.PlayersOnWeek
                        );
                    players.Add(player);
                }
            }
            game.Initialize(players);

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
            // TODO start game from serialized file
        }
    }
}
