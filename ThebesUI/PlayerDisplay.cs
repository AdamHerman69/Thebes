using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThebesCore;

namespace ThebesUI
{
    public partial class PlayerDisplay : UserControl
    {
        // Data
        IPlayerView player;

        // Data display
        Dictionary<IDigSiteSimpleView, Label> specializedKnowledgeDisplay;
        Dictionary<IDigSiteSimpleView, Label> singleUseKnowledgeDisplay;


        public PlayerDisplay(IPlayerView player, Layout layout)
        {
            InitializeComponent();


            this.player = player;

            // TODO RELATIVE positioning wtr. background.png
            // knowledge display
            specializedKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            singleUseKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            foreach (KeyValuePair<IDigSiteSimpleView, int> digSite_knowledge in player.SpecializedKnowledge)
            {
                Label label = new Label();
                label.Name = $"lSpecializedKnowledge{digSite_knowledge.Key}";
                label.Text = "0";

                Rectangle labelDims = layout.SpecializedKnowledgeLs[digSite_knowledge.Key.Name];
                label.Size = new Size(labelDims.Width, labelDims.Height);
                // TODO
            }

            UpdateInfo();
            this.Show();
        }

        public void Initialize(IPlayerView player)
        {
            
            

        }

        public void UpdateInfo()
        {
            lPlayerName.Text = player.Name;
            lTime.Text = player.Time.ToString();
            lPointsAmount.Text = player.Points.ToString();

            // specialized knowledge
            foreach (KeyValuePair<IDigSiteSimpleView, Label> digSite_label in specializedKnowledgeDisplay)
            {
                digSite_label.Value.Text = player.SpecializedKnowledge[digSite_label.Key].ToString();
            }

            // single-use knowledge
            foreach (KeyValuePair<IDigSiteSimpleView, Label> digSite_label in singleUseKnowledgeDisplay)
            {
                digSite_label.Value.Text = player.SingleUseKnowledge[digSite_label.Key].ToString();
            }



            lGeneralKnowledgeAmount.Text = player.GeneralKnowledge.ToString();
            lShovelsAmount.Text = player.Shovels.ToString();
            lAssistentsAmount.Text = player.Assistents.ToString();

            lSpecialPermissionAmount.Text = player.SpecialPermissions.ToString();
            lCongressAmount.Text = player.Congresses.ToString();
            lCarAmount.Text = player.Cars.ToString();
            lZeppelinAmount.Text = player.Zeppelins.ToString();

        }
    }
}
