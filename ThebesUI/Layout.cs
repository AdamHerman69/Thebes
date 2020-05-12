using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using Newtonsoft.Json;
using System.IO;

namespace ThebesUI
{
    public static class UIConfig
    {
        public const string IMG_FOLDER = @"C:\Users\admhe\source\repos\Thebes\img\";
    }

    public struct Rectangle
    {
        public Point topLeft, bottomRight;
        [JsonIgnore]
        public int Width { get { return bottomRight.X - topLeft.X; } }
        [JsonIgnore]
        public int Height { get { return bottomRight.Y - topLeft.Y; } }

        public Rectangle(Point topLeft, Point bottomRight)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public bool IsInside(Point point)
        {
            return (point.X >= topLeft.X && point.X <= bottomRight.X && point.Y >= topLeft.Y && point.Y <= bottomRight.Y);
        }

        public Point Center { get { return new Point(topLeft.X + Width / 2, topLeft.Y + Height / 2); } }
        public Point RectanglePositionCenter(int width, int height)
        {
            return new Point(Center.X - width / 2, Center.Y - height / 2);
        }
    }

    public class Layout
    {
        [JsonProperty]
        public Rectangle[] WeekCounter { get; private set; } = new Rectangle[52];
        [JsonProperty]
        public Rectangle[] YearCounter { get; private set; } = new Rectangle[3];
        [JsonProperty]
        public Rectangle[] DisplayedCards { get; private set; } = new Rectangle[4];
        [JsonProperty]
        public Rectangle[] DisplayedExhibitions { get; private set; } = new Rectangle[3];
        [JsonProperty]
        public Dictionary<string, Rectangle> Places;

        // player display, relative to player_display_background.png
        [JsonProperty]
        public Rectangle PlayerName { get; private set; }
        [JsonProperty]
        public Rectangle Points { get; private set; }
        [JsonProperty]
        public Rectangle Time { get; private set; }
        [JsonProperty]
        public Rectangle GeneralKnowledge { get; private set; }
        [JsonProperty]
        public Rectangle Shovel { get; private set; }
        [JsonProperty]
        public Rectangle Assistant { get; private set; }
        [JsonProperty]
        public Rectangle SpecialPermission { get; private set; }
        [JsonProperty]
        public Rectangle Congress { get; private set; }
        [JsonProperty]
        public Rectangle Car { get; private set; }
        [JsonProperty]
        public Rectangle Zeppelin { get; private set; }

        [JsonProperty]
        public Dictionary<string, Rectangle> SpecializedKnowledgeLs { get; private set; }
        [JsonProperty]
        public Dictionary<string, Rectangle> SingleUseKnowledgeLs { get; private set; }
        [JsonProperty]
        public Dictionary<string, Rectangle> Permissions { get; private set; }


        public Layout()
        {
            
        }



        public static Layout ParseLayout(string jsonFilePath)
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            Layout layout =  JsonConvert.DeserializeObject<Layout>(jsonString);
            return layout;
        }
    }

}
