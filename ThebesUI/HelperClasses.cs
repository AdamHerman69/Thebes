using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThebesUI
{
    /// <summary>
    /// Provides a transparent picutre box, that can be on top of other controls (not only just parent)
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

    public static class UIConfig
    {
        public const string IMG_FOLDER = @"..\..\..\img\";
        public static void ReplaceImage(PictureBox pb, Image newImg)
        {
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = newImg;
        }

        public static void RemoveImage(PictureBox pb)
        {
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = null;
        }

        public static void ErrorDialog(string message)
        {
            MessageBox.Show(message);
        }
    }
}
