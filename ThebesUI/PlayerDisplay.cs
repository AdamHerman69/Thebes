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
        IPlayerView player;

        public PlayerDisplay()
        {
            InitializeComponent();
        }

        public void Initialize(IPlayerView player)
        {
            this.player = player;
            UpdateInfo();
            this.Show();

            // DATA BINDS
            lPlayerName.DataBindings.Add(new Binding("Text", player, "Name"));
            lTime.DataBindings.Add(new Binding("Text", player, "Time"));
            lPointsAmount.DataBindings.Add(new Binding("Text", player, "Points"));
        }

        public void UpdateInfo()
        {
            lPlayerName.Text = player.Name;
            lTime.Text = player.Time.ToString();
            lPointsAmount.Text = player.Points.ToString();

            //knowledgeStats1.UpdateInfo(player.SpecializedKnowledge, player.SingleUseKnowledge, player.Permissions);

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
