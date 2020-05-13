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
        Action<IAction> executeAction;

        public DigForm(IDigSite digSite, IPlayer player, Action<IAction> executeAction)
        {
            InitializeComponent();
            Initialize(digSite, player, executeAction);
        }

        public void Initialize(IDigSite digSite, IPlayer player, Action<IAction> executeAction)
        {
            this.digSite = digSite;
            this.player = player;
            this.executeAction = executeAction;

            lDigSiteName.Text = digSite.Name;

            // display knowledge points
            lSpecializedKnowledgeAmount.Text = player.SpecializedKnowledge[digSite].ToString();
            lGeneralKnowledgeAmount.Text = player.GeneralKnowledge.ToString();
            lAssistantKnowledgeAmount.Text = player.GetAssistantKnowledge().ToString();

            // display single use cards
            List<ICard> singleUseCards = player.GetUsableSingleUseCards(digSite);
            clSingleUseCards.Initialize(singleUseCards.ConvertAll(UIGame.ToView), UpdateInfo);

            // display tokens
            foreach (ITokenView token in digSite.Tokens.ConvertAll(UIGame.ToView))
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
        }

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
        }

        private void nudWeeks_ValueChanged(object sender, EventArgs e)
        {
            UpdateInfo();
            lDrawAmount.Show();
        }

        private void lvSingleUseCards_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateInfo();
        }

        private void bDigButton_Click(object sender, EventArgs e)
        {
            if (!player.Permissions[digSite] && player.SpecialPermissions > 0)
            {
                var usePermission = MessageBox.Show("Do you want to use your special permission?",
                                     "Use special permission?",
                                     MessageBoxButtons.YesNo);
                if (usePermission == DialogResult.Yes)
                {
                    player.UseSpecialPermission(digSite);
                }
            }

            List<IToken> tokens = new List<IToken>();
            executeAction(new DigAction(digSite, weeksToDig, singleUseCards, tokens));
            if (tokens.Count != 0)
            {
                DigResult resultForm = new DigResult(tokens);
                resultForm.ShowDialog();
                this.Close();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            // Close form
            this.Close();
        }
    }
}
