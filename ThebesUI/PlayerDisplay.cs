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
        Dictionary<IDigSiteSimpleView, PictureBox> permissionDisplay;

        Label lPlayerName, lTime, lPoints, lGeneralKnowledge, lShovels, lAssistants, lSpecialPermissions, lCongress, lCar, lZeppelin;

        public PlayerDisplay()
        {
            InitializeComponent();
        }

        public void Initialize(IPlayerData player, Layout layout, PlayerColor color)
        {
            this.player = player;
            this.BackColor = Color.Transparent;
            //this.BackgroundImage = Image.FromFile(UIConfig.IMG_FOLDER + $"playerDisplay_{color}.png");
            panel.BackgroundImage = Image.FromFile(UIConfig.IMG_FOLDER + $"playerDisplay_{color}.png");

            // knowledge display
            // TODO maybe add relative positioning wtr. background.png
            specializedKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            singleUseKnowledgeDisplay = new Dictionary<IDigSiteSimpleView, Label>();
            permissionDisplay = new Dictionary<IDigSiteSimpleView, PictureBox>();
            Label label = new Label();
            Rectangle labelDims;
            PictureBox pb;
            foreach (KeyValuePair<IDigSiteSimpleView, int> digSite_knowledge in player.SpecializedKnowledge)
            {
                // specialized knowledge
                label = new Label();
                label.Name = $"lSpecializedKnowledge{digSite_knowledge.Key}";
                label.Text = "0";
                label.Font = new Font("Arial", 12, FontStyle.Bold);

                labelDims = layout.SpecializedKnowledgeLs[digSite_knowledge.Key.Name];
                label.Size = new Size(labelDims.Width, labelDims.Height);
                label.Location = labelDims.topLeft;
                label.BackColor = Color.Transparent;
                label.Show();
                panel.Controls.Add(label);
                specializedKnowledgeDisplay.Add(digSite_knowledge.Key, label);

                // rumors
                label = new Label();
                label.Name = $"lRumors{digSite_knowledge.Key}";
                label.Text = "0";
                label.Font = new Font("Arial", 12, FontStyle.Bold);

                labelDims = layout.SingleUseKnowledgeLs[digSite_knowledge.Key.Name];
                label.Size = new Size(labelDims.Width, labelDims.Height);
                label.Location = labelDims.topLeft;
                label.BackColor = Color.Transparent;
                label.Show();
                panel.Controls.Add(label);
                singleUseKnowledgeDisplay.Add(digSite_knowledge.Key, label);

                // permissions
                labelDims = layout.Permissions[digSite_knowledge.Key.Name];
                pb = new PictureBox
                    {
                        Location = labelDims.topLeft,
                        Size = new Size(labelDims.Width, labelDims.Height),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                panel.Controls.Add(pb);
                permissionDisplay.Add(digSite_knowledge.Key, pb);
                
            }

            // player name
            labelDims = layout.PlayerName;
            lPlayerName = new Label()
            {
                Name = $"lPlayerName",
                Text = "0",
                Font = new Font("Arial", 11, FontStyle.Bold),
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                BackColor = Color.Transparent,
            Visible = true
            };
            panel.Controls.Add(lPlayerName);

            //// time
            //labelDims = layout.Time;
            //lTime = new Label()
            //{
            //    Name = $"lTime",
            //    Text = "0",
            //    BackColor = Color.Transparent,
            //    Size = new Size(labelDims.Width, labelDims.Height),
            //    Location = labelDims.topLeft,
            //    Visible = true
            //};
            //panel.Controls.Add(lTime);

            // points
            labelDims = layout.Points;
            lPoints = new Label()
            {
                Name = $"lPoints",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lPoints);

            // general knowledge
            labelDims = layout.GeneralKnowledge;
            lGeneralKnowledge = new Label()
            {
                Name = $"lGeneralKnowledge",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lGeneralKnowledge);

            // shovels
            labelDims = layout.Shovel;
            lShovels = new Label()
            {
                Name = $"lShovels",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lShovels);

            // assistants
            labelDims = layout.Assistant;
            lAssistants = new Label()
            {
                Name = $"lAssistants",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lAssistants);

            // special permissions
            labelDims = layout.SpecialPermission;
            lSpecialPermissions = new Label()
            {
                Name = $"lSpecialPermissions",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lSpecialPermissions);

            // congress
            labelDims = layout.Congress;
            lCongress = new Label()
            {
                Name = $"lCongress",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lCongress);

            // car
            labelDims = layout.Car;
            lCar = new Label()
            {
                Name = $"lCar",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lCar);

            // zeppelin
            labelDims = layout.Zeppelin;
            lZeppelin = new Label()
            {
                Name = $"lZeppelin",
                Text = "0",
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Transparent,
                Size = new Size(labelDims.Width, labelDims.Height),
                Location = labelDims.topLeft,
                Visible = true
            };
            panel.Controls.Add(lZeppelin);

            

            UpdateInfo();
            this.Show();
        }

        public void UpdateInfo()
        {
            lPlayerName.Text = player.Name;
            //lTime.Text = player.Time.ToString();
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

            // tokens
            List<ITokenView> tokens = player.Tokens.SelectMany(t => t.Value).ToList().ConvertAll(UIGame.ToView);
            flpTokens.Controls.Clear();
            foreach (ITokenView token in tokens)
            {
                flpTokens.Controls.Add(new PictureBox
                {
                    Width = 30,
                    Height = 30,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = token,
                    Image = Image.FromFile(UIConfig.IMG_FOLDER + token.FileName)
                });
            }

            // permissions
            foreach (KeyValuePair<IDigSiteSimpleView, bool> digSite_bool in player.Permissions)
            {
                if (digSite_bool.Value)
                {
                    permissionDisplay[digSite_bool.Key].Image = null;
                }
                else
                {
                    permissionDisplay[digSite_bool.Key].Image = Image.FromFile(UIConfig.IMG_FOLDER + $"p_no_{digSite_bool.Key.Name}.png");
                }
                
            }

            // height
            this.Height = 152 + 45 * (flpTokens.Controls.Count / 10);
            if (flpTokens.Controls.Count % 10 > 0)
            {
                this.Height += 45;
            }

        }
    }
}
