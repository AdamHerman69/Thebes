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

        public static void NotEnoughTimeDialog(string message)
        {
            MessageBox.Show(message);
        }

        private void bStartNew_Click(object sender, EventArgs e)
        {
            GameSettings.LoadFromFile(@"C:\Users\admhe\source\repos\Thebes\ThebesConsole\bin\Debug\thebes_config_auto.txt");

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
            Dictionary<IPlayer, PlayerColor> players = new Dictionary<IPlayer, PlayerColor>();
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
                    players.Add(player, pi.Color());
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
            IUIGame game = GameState.Deserialize(filePath);

            this.Hide();
            GameForm gameForm = new GameForm(game);
            gameForm.ShowDialog();

            this.Close();
        }
    }
}
