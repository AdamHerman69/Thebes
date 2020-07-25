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
    public partial class DigForm : Form
    {
        IDigSite digSite;
        IPlayer player;
        int weeksToDig;
        int tokenBonus;
        int tokenAmount;
        int totalKnowledge;
        List<ICard> singleUseCards;
        Func<IAction, Task> executeAction;

        public DigForm(IDigSite digSite, IPlayer player, Func<IAction, Task> executeAction, List<IToken> tokens)
        {
            InitializeComponent();
            Initialize(digSite, player, executeAction, tokens);
            UpdateInfo();
        }

        /// <summary>
        /// Called in a constructor to fill with data
        /// </summary>
        /// <param name="digSite">where to dig</param>
        /// <param name="player">player who is ready to dig</param>
        /// <param name="executeAction">method to execute action once a dig starts</param>
        /// <param name="tokens">tokens present at the digsite</param>
        private void Initialize(IDigSite digSite, IPlayer player, Func<IAction, Task> executeAction, List<IToken> tokens)
        {
            this.digSite = digSite;
            this.player = player;
            this.executeAction = executeAction;
            this.Text = digSite.Name;

            lDigSiteName.Text = digSite.Name;

            // display knowledge points
            lSpecializedKnowledgeAmount.Text = player.SpecializedKnowledge[digSite].ToString();
            lGeneralKnowledgeAmount.Text = player.GeneralKnowledge.ToString();
            lAssistantKnowledgeAmount.Text = player.GetAssistantKnowledge().ToString();

            // display single use cards
            List<ICard> singleUseCards = player.GetUsableSingleUseCards(digSite);
            clSingleUseCards.Initialize(singleUseCards.ConvertAll(UIGame.ToView), UpdateInfo);

            // display tokens
            foreach (ITokenView token in tokens.ConvertAll(UIGame.ToView))
            {
                if (! (token is DirtTokenView))
                {
                    flpTokens.Controls.Add(new PictureBox()
                    {
                        Width = 50,
                        Height = 50,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = token,
                        Image = Image.FromFile(UIConfig.IMG_FOLDER + token.FileName)
                    });
                }
            }

            weeksToDig = 0;
            singleUseCards = new List<ICard>();
            player.GetDigStats(digSite, singleUseCards, out totalKnowledge, out tokenBonus);

            // token table
            int rowIndex;
            DataGridViewRow row;
            for (int weeks = 1; weeks <= 12; weeks++)
            {
                rowIndex = this.dgvTokenTable.Rows.Add();
                row = this.dgvTokenTable.Rows[rowIndex];

                row.Cells["weeksSpent"].Value = weeks;
                row.Cells["tokensDrawn"].Value = GameSettings.DugTokenCount(totalKnowledge, weeks);
            }

            dgvTokenTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Called to update form data when something changes
        /// </summary>
        public void UpdateInfo()
        {
            weeksToDig = (int)nudWeeks.Value;
               
            // get chosen single-use cards
            singleUseCards = new List<ICard>();
            foreach (ICardView card in clSingleUseCards.Selected)
            {
                singleUseCards.Add(card.Card);
            }


            player.GetDigStats(digSite, singleUseCards, out totalKnowledge, out tokenBonus);
            tokenAmount = GameSettings.DugTokenCount(totalKnowledge, weeksToDig) + tokenBonus;

            lTotalKnowledge.Text = $"Total knowledge: {totalKnowledge}";
            lDrawAmount.Text = $"You'll draw {tokenAmount}";

            // token table
            DataGridViewRow row;
            for (int weeks = 1; weeks <= 12; weeks++)
            {
                row = this.dgvTokenTable.Rows[weeks - 1];

                row.Cells["weeksSpent"].Value = weeks;
                row.Cells["tokensDrawn"].Value = GameSettings.DugTokenCount(totalKnowledge, weeks) + tokenBonus;
            }
        }

        private void nudWeeks_ValueChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        private void lvSingleUseCards_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateInfo();
        }

        private void bDigButton_Click(object sender, EventArgs e)
        {    
            if (!player.Permissions[digSite] && player.SpecialPermissions > 0)
            {
                var usePermission = MessageBox.Show("You don't have a valid permission. Do you want to use your special permission?",
                                     "Use special permission?",
                                     MessageBoxButtons.YesNo);
                if (usePermission == DialogResult.Yes)
                {
                    List<IToken> tokens = new List<IToken>();
                    executeAction(new DigAction(digSite, weeksToDig, singleUseCards, DisplayDigResult));
                }
            }
            else
            {
                List<IToken> tokens = new List<IToken>();
                executeAction(new DigAction(digSite, weeksToDig, singleUseCards, DisplayDigResult));
            }
        }

        public void DisplayDigResult(List<IToken> tokens)
        {
            DigResult resultForm = new DigResult(tokens);
            resultForm.ShowDialog();
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            // Close form
            this.Close();
        }
    }
}
