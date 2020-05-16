using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThebesUI
{
    public partial class CardList : UserControl
    {
        List<PictureBox> cards = new List<PictureBox>();
        Action update;
        public List<ICardView> Selected { get; private set; }
        
        /// <summary>
        /// Needs to be called after the default constructor to populate it with data
        /// </summary>
        /// <param name="cards">cards to display</param>
        /// <param name="update">method to call when a card selection  has been altered</param>
        public void Initialize(List<ICardView> cards, Action update)
        {
            InitializeComponent();

            this.update = update;
            Selected = new List<ICardView>();
            int counter = 0;
            PictureBox pb;
            foreach (ICardView card in cards)
            {
                pb = new PictureBox()
                {
                    Width = 67,
                    Name = "pb" + counter.ToString(),
                    Height = 100,
                    Left = 74 * counter,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Tag = card,
                    Image = Image.FromFile(UIConfig.IMG_FOLDER + card.FileName)
                };
                counter++;
                pb.Click += pbCard_Click;
                this.cards.Add(pb);
                this.Controls.Add(pb);
            }
        }

        private bool IsSelected(ICardView card)
        {
            return Selected.Contains(card);
        }

        private void pbCard_Click(object sender, EventArgs e)
        {
            
            ICardView clickedCard = (ICardView)((PictureBox)sender).Tag;
            if (IsSelected(clickedCard)) 
            {
                // deselect
                Selected.Remove(clickedCard);
                ((PictureBox)sender).BorderStyle = BorderStyle.None;
                update();
            }
            else 
            {
                // select
                Selected.Add(clickedCard);
                ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
                update();
            }
        }
    }
}
