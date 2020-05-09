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
        IDigSiteFullView digSite;
        IPlayer player;
        int weeksToDig;
        int tokenBonus;
        int tokenAmount;
        int totalKnowledge;
        List<ICard> singleUseCards;

        public DigForm()
        {
            InitializeComponent();
        }

        public void Initialize(IDigSiteFullView digSite, IPlayer player)
        {
            this.digSite = digSite;
            this.player = player;

            lDigSiteName.Text = digSite.Name;

            // display knowledge points
            lSpecializedKnowledgeAmount.Text = player.SpecializedKnowledge[digSite].ToString();
            lGeneralKnowledgeAmount.Text = player.GeneralKnowledge.ToString();
            lAssistantKnowledgeAmount.Text = player.GetAssistantKnowledge().ToString();

            // display single use cards
            List<ICard> singleUseCards = player.GetUsableSingleUseCards(digSite);
            foreach (ICard card in singleUseCards)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Tag = card;
                lvItem.Text = card.ToString();
                lvSingleUseCards.Items.Add(lvItem);
            }

            // display tokens
            foreach (IToken token in digSite.Tokens)
            {
                if (! (token is DirtToken))
                {
                    lvTokens.Items.Add(new ListViewItem(token.ToString()));
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
            foreach (ListViewItem item in lvSingleUseCards.Items)
            {
                if (item.Checked)
                {
                    singleUseCards.Add((ICard)item.Tag);
                }
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
            List<IToken> tokens = player.Dig(digSite, weeksToDig, singleUseCards);
            string resultStriing = $"Congratulations! You found these artifacts:\n";
            foreach (IToken token in tokens)
            {
                resultStriing += token.ToString() + "\n";
            }

            MessageBox.Show(resultStriing);
            // TODO close form
        }
    }
}
