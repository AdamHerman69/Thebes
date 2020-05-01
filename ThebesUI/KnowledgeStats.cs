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
    public partial class KnowledgeStats : UserControl
    {
        public KnowledgeStats()
        {
            InitializeComponent();
        }

        public void Initialize(List<IDigSiteSimpleView> digSites)
        {
            // if number of DigSiteKnowledge controls is not digSite.Count
            if (digSites.Count != ((IEnumerable<Control>)Controls).ToList().Where(x => x is DigSiteKnowledge).Count())
            {
                throw new ArgumentOutOfRangeException("dig site count doesn't match up");
            }

            foreach (Control control in Controls)
            {
                if (control is DigSiteKnowledge)
                {
                    ((DigSiteKnowledge)control).Initialize(digSites[0]);
                    digSites.RemoveAt(0);
                }
            }
        }

        public void UpdateInfo(Dictionary<IDigSiteSimpleView, int> specializedKnowledge, Dictionary<IDigSiteSimpleView, int> rumors, Dictionary<IDigSiteSimpleView, bool> permissions)
        {
            foreach (Control control in Controls)
            {
                if (control is DigSiteKnowledge)
                {
                    DigSiteKnowledge digSiteKnowledge = (DigSiteKnowledge)control;
                    digSiteKnowledge.UpdateInfo(specializedKnowledge[digSiteKnowledge.digSite], rumors[digSiteKnowledge.digSite], permissions[digSiteKnowledge.digSite]);
                }
            }
        }
    }
}
