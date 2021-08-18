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

    }
}
