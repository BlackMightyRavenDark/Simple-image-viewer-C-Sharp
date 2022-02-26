using System.Drawing;
using System.Windows.Forms;

namespace Simple_image_viewer_C_Sharp
{
    public static class Helper
    {
        public static int FindName(this ToolStripItemCollection collection, string name)
        {
            name = name.ToLower();
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].Text.ToLower() == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public static Rectangle ResizeTo(this Rectangle source, Size newSize)
        {
            float aspectSource = source.Height / (float)source.Width;
            float aspectDest = newSize.Height / (float)newSize.Width;
            int w = newSize.Width;
            int h = newSize.Height;
            if (aspectSource > aspectDest)
            {
                w = (int)(newSize.Height / aspectSource);
            }
            else if (aspectSource < aspectDest)
            {
                h = (int)(newSize.Width * aspectSource);
            }
            return new Rectangle(0, 0, w, h);
        }

        public static Rectangle CenterIn(this Rectangle source, Rectangle dest)
        {
            int x = dest.Width / 2 - source.Width / 2;
            int y = dest.Height / 2 - source.Height / 2;
            return new Rectangle(x, y, source.Width, source.Height);
        }
    }
}
