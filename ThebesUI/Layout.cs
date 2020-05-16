using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThebesCore;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace ThebesUI
{
    /// <summary>
    /// Represents a reclantle shape in a 2D cartesian system. Stored as top left and bottom right points.
    /// Only supports rectangles with sides parallel to x and y axis.
    /// </summary>
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

        /// <summary>
        /// Decides whether a given point lies within the rectanlge (including borders)
        /// </summary>
        /// <param name="point"></param>
        /// <returns>true if point is inside the rectangle</returns>
        public bool IsInside(Point point)
        {
            return (point.X >= topLeft.X && point.X <= bottomRight.X && point.Y >= topLeft.Y && point.Y <= bottomRight.Y);
        }

        /// <summary>
        /// The center point of the rectangle (might be pixel off, because of integer rounding)
        /// </summary>
        public Point Center { get { return new Point(topLeft.X + Width / 2, topLeft.Y + Height / 2); } }
        
        /// <summary>
        /// Centers a given rectangle (specified by width and height) inside this rectangle.
        /// </summary>
        /// <param name="width">width of the rectanlge being centered</param>
        /// <param name="height">height of the rectangle being centered</param>
        /// <returns>top left point of the given rectangle so that it's centered inside this rectangle</returns>
        public Point RectanglePositionCenter(int width, int height)
        {
            return new Point(Center.X - width / 2, Center.Y - height / 2);
        }
    }

    /// <summary>
    /// Holds all necessary information about the layout of controls.
    /// </summary>
    public class Layout
    {
        // All properties below are located on the game board. 
        //The positioning is specified by Rectangle objects and should be inside 1064x766 2D integer cartesian system.

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

        // All properties below are located on the playerDisplay card.
        //The positioning is specified by Rectangle objects and should be inside 368x152 2D integer cartesian system.

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


        public Layout() {} 

        /// <summary>
        /// Creates a new Layout object from the given json file
        /// </summary>
        /// <param name="jsonFilePath">file path to a .json file</param>
        /// <returns>Layout object, holding data from the json file</returns>
        public static Layout ParseLayout(string jsonFilePath)
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            Layout layout =  JsonConvert.DeserializeObject<Layout>(jsonString);
            return layout;
        }
    }

}
