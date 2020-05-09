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
        IPlayerData player;

        // Data display
        Dictionary<IDigSiteSimpleView, Label> specializedKnowledgeDisplay;
        Dictionary<IDigSiteSimpleView, Label> singleUseKnowledgeDisplay;

        Label lPlayerName, lTime, lPoints, lGeneralKnowledge, lShovels, lAssistants, lSpecialPermissions, lCongress, lCar, lZeppelin;

        public PlayerDisplay()
        {
            InitializeComponent();
        }

        public void Initialize(IPlayerData player, Layout layout)
        {
            this.player = player;

            // knowledge display
            // TODO maybe add relative positioning wtr. background.png
            specializedKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            singleUseKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            Label label = new Label();
            Rectangle labelDims;
            foreach (KeyValuePair<IDigSiteSimpleView, int> digSite_knowledge in player.SpecializedKnowledge)
            {
                // specialized knowledge
                label = new Label();
                label.Name = $"lSpecializedKnowledge{digSite_knowledge.Key}";
                label.Text = "0";

                labelDims = layout.SpecializedKnowledgeLs[digSite_knowledge.Key.Name];
                label.Size = new Size(labelDims.Width, labelDims.Height);
                label.Location = labelDims.topLeft;
                label.Show();
                Controls.Add(label);
                specializedKnowledgeDisplay.Add(digSite_knowledge.Key, label);

                // rumors
                label = new Label();
                label.Name = $"lRumors{digSite_knowledge.Key}";
                label.Text = "0";

                labelDims = layout.SingleUseKnowledgeLs[digSite_knowledge.Key.Name];
                label.Size = new Size(labelDims.Width, labelDims.Height);
                label.Location = labelDims.topLeft;
                label.Show();
                Controls.Add(label);
                singleUseKnowledgeDisplay.Add(digSite_knowledge.Key, label);
            }

            // player name
            labelDims = layout.PlayerName;
            lPlayerName = new Label()
            {
                Name = $"lPlayerName",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lPlayerName);

            // time
            labelDims = layout.Time;
            lTime = new Label()
            {
                Name = $"lTime",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lTime);

            // points
            labelDims = layout.Time;
            lPoints = new Label()
            {
                Name = $"lTime",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lPoints);

            // general knowledge
            labelDims = layout.GeneralKnowledge;
            lGeneralKnowledge = new Label()
            {
                Name = $"lGeneralKnowledge",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lGeneralKnowledge);

            // shovels
            labelDims = layout.Shovel;
            lShovels = new Label()
            {
                Name = $"lShovels",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lShovels);

            // assistants
            labelDims = layout.Assistant;
            lAssistants = new Label()
            {
                Name = $"lAssistants",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lAssistants);

            // special permissions
            labelDims = layout.SpecialPermission;
            lSpecialPermissions = new Label()
            {
                Name = $"lSpecialPermissions",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lSpecialPermissions);

            // congress
            labelDims = layout.Congress;
            lCongress = new Label()
            {
                Name = $"lCongress",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lCongress);

            // car
            labelDims = layout.Car;
            lCar = new Label()
            {
                Name = $"lCar",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lCar);

            // zeppelin
            labelDims = layout.Zeppelin;
            lZeppelin = new Label()
            {
                Name = $"lZeppelin",
                Text = "0",
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            Controls.Add(lZeppelin);

            UpdateInfo();
            this.Show();
        }

        public void UpdateInfo()
        {
            lPlayerName.Text = player.Name;
            lTime.Text = player.Time.ToString();
            lPoints.Text = player.Points.ToString();

            lGeneralKnowledge.Text = player.GeneralKnowledge.ToString();
            lShovels.Text = player.Shovels.ToString();
            lAssistants.Text = player.Assistants.ToString();

            lSpecialPermissions.Text = player.SpecialPermissions.ToString();
            lCongress.Text = player.Congresses.ToString();
            lCar.Text = player.Cars.ToString();
            lZeppelin.Text = player.Zeppelins.ToString();

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

        }
    }
}
