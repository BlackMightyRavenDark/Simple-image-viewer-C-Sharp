using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Simple_image_viewer_C_Sharp
{
    public class Utils
    {

        public class MyConfiguration
        {
            public string fileName;
            public string selfPath;
            public string listsPath;
          
            public delegate void SavingDelegate(object sender, JObject root);
            public delegate void LoadingDelegate(object sender, JObject root);
            public SavingDelegate Saving;
            public LoadingDelegate Loading;

            public MyConfiguration(string fileName)
            {
                this.fileName = fileName;
                this.selfPath = Path.GetDirectoryName(Application.ExecutablePath);
                LoadDefaults();
            }

            public void Save()
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                JObject json = new JObject();
                Saving?.Invoke(this, json);
                File.WriteAllText(fileName, json.ToString());
            }

            public void LoadDefaults()
            {
                listsPath = $"{selfPath}\\Lists\\";
            }

            public void Load()
            {
                if (File.Exists(fileName))
                {
                    JObject json = JObject.Parse(File.ReadAllText(fileName));
                    if (json != null)
                    {
                        Loading?.Invoke(this, json);
                    }
                }
            }
        }

        public static List<string> imageFileTypes = new List<string>() { ".bmp", ".jpg", ".jpeg", ".gif", ".png", ".tif", ".jfif", ".ico" };
        public static List<string> supportedFileTypes = new List<string>();
        
        public static MyConfiguration config;







        public static void Associate(string exe)
        {
            try
            {
                foreach (string t in supportedFileTypes)
                {
                    string ext = t.Substring(1);
                    using (RegistryKey keyExtension = Registry.ClassesRoot.CreateSubKey(t))
                    {
                        keyExtension.SetValue(string.Empty, $"{ext}file");
                        using (RegistryKey key0 = Registry.ClassesRoot.CreateSubKey($"{ext}file"))
                        {
                            key0.SetValue(string.Empty, ext);
                            using (RegistryKey key = key0.CreateSubKey("shell"))
                                key.SetValue(string.Empty, "SIVOpen");
                            using (RegistryKey key = key0.CreateSubKey(@"shell\SIVOpen"))
                                key.SetValue(string.Empty, "Open");
                            using (RegistryKey key = key0.CreateSubKey(@"shell\SIVOpen\command"))
                                key.SetValue(string.Empty, $"\"{exe}\" \"%1\"");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        public static Rectangle ResizeRect(Rectangle source, Size newSize)
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

        public static Rectangle CenterRect(Rectangle source, Rectangle dest)
        {
            int x = dest.Width / 2 - source.Width / 2;
            int y = dest.Height / 2 - source.Height / 2;
            return new Rectangle(x, y, source.Width, source.Height);
        }

        public static List<string> LoadImageList(string fileName)
        {
            List<string> resList = new List<string>();
            JObject json = JObject.Parse(File.ReadAllText(fileName));
            if (json != null)
            {
                JArray ja = json.Value<JArray>("list");
                if (ja != null && ja.Count > 0)
                {
                    JToken jt = json.Value<JToken>("relativePath");
                    string path = jt == null ? string.Empty : jt.Value<string>();
                    if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                    {
                        return resList;
                    }
                    for (int i = 0; i < ja.Count; i++)
                    {
                        string t = ja[i].Value<string>();
                        string fn = t.Contains(":\\") ? t : path + t;
                        if (File.Exists(fn))
                        {
                            resList.Add(fn);
                        }
                    }
                }
            }
            return resList;
        }

        public static bool IsImageFile(string fn)
        {
            string ext = Path.GetExtension(fn).ToLower();
            return imageFileTypes.Contains(ext);
        }

        public static void SetClipboardText(string text)
        {
            bool res;
            do
            {
                try
                {
                    Clipboard.SetText(text);
                    res = true;
                    return;
                }
                catch
                {
                    res = false;
                }
            } while (!res);
        }
    }
}
