using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;

namespace ThebesUI
{
    public struct Rectangle
    {
        int XtopLeft, YtopLeft, XbottomRight, YbottomRight;
        public int Width { get { return XbottomRight - XtopLeft; } }
        public int Height { get { return YbottomRight - YtopLeft; } }

        public Rectangle(int value)
        {
            XtopLeft = 69;
            YtopLeft = 420;

            XbottomRight = 20;
            YbottomRight = 30;
        }
    }

    public class Layout
    {
        public Rectangle[] WeekCounter { get; private set; } = new Rectangle[52];
        public Rectangle[] YearCounter { get; private set; } = new Rectangle[3];
        public Rectangle[] DisplayedCards { get; private set; } = new Rectangle[4];
        public Rectangle[] DisplayedExhibitions { get; private set; } = new Rectangle[3];
        public Dictionary<string, Rectangle> Places;

        // player display, relative to player_display_background.png
        public Rectangle PlayerName { get; private set; }
        public Rectangle Points { get; private set; }
        public Rectangle Time { get; private set; }
        public Rectangle GeneralKnowledge { get; private set; }
        public Rectangle Shovel { get; private set; }
        public Rectangle Assistent { get; private set; }
        public Rectangle SpecialPermission { get; private set; }
        public Rectangle Congress { get; private set; }
        public Rectangle Car { get; private set; }
        public Rectangle Zeppelin { get; private set; }

        public Dictionary<string, Rectangle> SpecializedKnowledgeLs { get; private set; }
        public Dictionary<string, Rectangle> SingleUseKnowledgeTLs { get; private set; }


        public Layout()
        {
            for (int i = 0; i < WeekCounter.Length; i++)
            {
                WeekCounter[i] = new Rectangle(10);
            }
            for (int i = 0; i < YearCounter.Length; i++)
            {
                YearCounter[i] = new Rectangle(10);
            }
            for (int i = 0; i < DisplayedCards.Length; i++)
            {
                DisplayedCards[i] = new Rectangle(10);
            }
            for (int i = 0; i < DisplayedExhibitions.Length; i++)
            {
                DisplayedExhibitions[i] = new Rectangle(10);
            }
            Places = new Dictionary<string, Rectangle>();
            Places.Add("London",new Rectangle(5));
        }
    }

}
