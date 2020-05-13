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
    public partial class DigSiteKnowledge : UserControl
    {
        public IDigSite digSite;

        public DigSiteKnowledge()
        {
            InitializeComponent();
        }

        public void Initialize(IDigSite digSite)
        {
            this.digSite = digSite;
            lDigSiteName.Text = digSite.Name;
        }

        public void UpdateInfo(int specializedKnowledge, int rumors, bool validPermission)
        {
            lSpecializedKnowledge.Text = specializedKnowledge.ToString();
            lRumors.Text = rumors.ToString();

            if (validPermission)
            {
                lDigSiteName.Font = new Font(lDigSiteName.Font, FontStyle.Regular);
            }
            else
            {
                lDigSiteName.Font = new Font(lDigSiteName.Font, FontStyle.Strikeout);
            }
        }
    }
}
