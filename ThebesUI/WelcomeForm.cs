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
using System.Reflection;

namespace ThebesUI
{
    public partial class WelcomeForm : Form
    {
        OpenFileDialog ofdThb = new OpenFileDialog() { Filter = "THB|*.thb" };
        OpenFileDialog ofdDll = new OpenFileDialog() { Filter = "DLL|*.dll" };

        PlayerInput[] playerInputs = new PlayerInput[4];

        public WelcomeForm()
        {
            InitializeComponent();

            playerInput1.Color = PlayerColor.red;
            playerInput2.Color = PlayerColor.green;
            playerInput3.Color = PlayerColor.blue;
            playerInput4.Color = PlayerColor.yellow;

            playerInputs[0] = playerInput1;
            playerInputs[1] = playerInput2;
            playerInputs[2] = playerInput3;
            playerInputs[3] = playerInput4;

            foreach (PlayerInput playerInput in playerInputs)
            {
                playerInput.AddDefaultAI(typeof(TestAI));
                playerInput.AddAI(typeof(CheaterAI));
                playerInput.AddAI(typeof(HeuristicCheaterAI));
            }
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

        /// <summary>
        /// Starts a new game with the player data from this form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bStartNew_Click(object sender, EventArgs e)
        {
            // check player count
            int playerCount;
            if ((playerCount = GetPlayerCount()) < 2 || playerCount > 4)
            {
                MessageBox.Show("Unsupported number of players");
                return;
            }

            // load config file (.thc)
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

            // check for empty names
            if (AnyEmptyNames())
            {
                MessageBox.Show("All players have to have names");
                return;
            }



            UIGame game = new UIGame(playerCount);

            // Create Players
            Dictionary<IPlayer, PlayerColor> players = new Dictionary<IPlayer, PlayerColor>();
            Player player;

            foreach (PlayerInput playerInput in playerInputs)
            {
                if (playerInput.Selected())
                { 
                    if (playerInput.IsHuman())
                    {
                        player = new Player(
                            playerInput.PlayerName(),
                            GameSettings.Places.OfType<IDigSite>().ToList(),
                            GameSettings.StartingPlace,
                            UIConfig.ErrorDialog,
                            game.AvailableCards.ChangeDisplayedCards,
                            game.AvailableCards.GiveCard,
                            game.Deck.Discard,
                            game.ActiveExhibitions.GiveExhibition,
                            game.DrawTokens,
                            game.PlayersOnWeek
                            );
                    }
                    else
                    {
                        player = new AIPlayer(
                            playerInput.PlayerName(),
                            GameSettings.Places.OfType<IDigSite>().ToList(),
                            GameSettings.StartingPlace,
                            GameSettings.Places,
                            UIConfig.ErrorDialog,
                            game.AvailableCards.ChangeDisplayedCards,
                            game.AvailableCards.GiveCard,
                            game.Deck.Discard,
                            game.ActiveExhibitions.GiveExhibition,
                            game.DrawTokens,
                            game.PlayersOnWeek
                            );

                        IAI ai = (IAI)playerInput.Type.Assembly.CreateInstance(playerInput.Type.FullName, false, 0, null, new object[] { player, game }, null, null);
                        ((AIPlayer)player).Init(ai);
                    }
                    
                    players.Add(player, playerInput.Color);
                }
            }

            // initialize game
            try
            {
                game.Initialize(players);
            }
            catch (Exception exception)
            {
                UIConfig.ErrorDialog("Error initializing the game:\n" + exception.Message);
                return;
            }
            
            // open game form
            this.Hide();
            GameForm gameForm = new GameForm(game);
            gameForm.ShowDialog();
            this.Close();
        }

        private bool AnyEmptyNames()
        {
            foreach (PlayerInput playerInput in playerInputs)
            {
                if (playerInput.Selected() && playerInput.Name.Equals(""))
                {
                    return true;
                }
            }

            return false;
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            if (ofdThb.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofdThb.SafeFileName;
                tbFilePath.Text = ofdThb.FileName;
            }
        }

        /// <summary>
        /// Loads the game from file specified by tbFilePath.Text and starts it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Opens OpenFileDialog, checks the selected DLL for classes implementing the IAI interface and adds them to all four playerInputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddAI_Click(object sender, EventArgs e)
        {
            if (ofdDll.ShowDialog() == DialogResult.OK)
            {
                Assembly assembly = null;
                AssemblyName assemblyName = new AssemblyName();
                assemblyName.CodeBase = ofdDll.FileName;

                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error loading the dll:" + exception.Message);
                    return;
                }
                

                List<Type> typesAdded = new List<Type>();
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IAI).IsAssignableFrom(type))
                    {
                        foreach (PlayerInput playerInput in playerInputs)
                        {
                            playerInput.AddAI(type);
                            typesAdded.Add(type);
                        }
                    }
                }

                if (typesAdded.Count > 0)
                {
                    string aiList = "";
                    foreach (Type type in typesAdded)
                    {
                        aiList += type.ToString() + "\n";
                    }

                    MessageBox.Show("These AIs were succesfully added:\n" + aiList);
                }
                else
                {
                    MessageBox.Show("No classes implementing the IAI interface found in the provided dll");
                }

            }
        }
    }
}
