using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ThebesUI
{
    /// <summary>
    /// Provides a transparent picutre box, that can be on top of other controls (not just parent)
    /// 
    /// Source:
    /// Stack Overflow user Reza Aghaei
    /// https://stackoverflow.com/a/36102074/13426274
    /// </summary>
    class TransparentPictureBox : PictureBox
    {
        public TransparentPictureBox()
        {
            this.BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent != null && this.BackColor == Color.Transparent)
            {
                using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.Controls.Cast<Control>()
                          .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                          .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                          .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                          .ToList()
                          .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));

                    e.Graphics.DrawImage(bmp, -Left, -Top);

                }
            }
            base.OnPaint(e);
        }
    }

    /// <summary>
    /// Configuration and helper methods for the UI
    /// </summary>
    public static class UIConfig
    {
        /// <summary>
        /// Path to the img folder with all the graphic files
        /// </summary>
        public static string IMG_FOLDER = @"..\..\..\img\";

        /// <summary>
        /// Tries to find an img folder in the filesystem near the folder
        /// </summary>
        /// <param name="levelLimit">how far up the filesystem should we search</param>
        public static void FindImgFolder(int levelLimit)
        {
            string path = @".\img\";
            try
            {
                int counter = 0;
                while (counter < levelLimit && !Directory.Exists(path))
                {
                    path = @".\." + path;
                    counter++;
                }
                IMG_FOLDER = path;
            }
            catch (Exception)
            {
                throw new FileNotFoundException("Couldn't find the img folder");
            }
            
        }

        /// <summary>
        /// Replaces the image in a picture box
        /// </summary>
        /// <param name="pb">picture box</param>
        /// <param name="newImg">new image</param>
        public static void ReplaceImage(PictureBox pb, Image newImg)
        {
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = newImg;
        }

        /// <summary>
        /// Removes the image from a picture box
        /// </summary>
        /// <param name="pb"></param>
        public static void RemoveImage(PictureBox pb)
        {
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = null;
        }

        /// <summary>
        /// Displays a message box with the given message
        /// </summary>
        /// <param name="message">message to display</param>
        public static void ErrorDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
