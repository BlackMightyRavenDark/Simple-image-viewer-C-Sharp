using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Simple_image_viewer_C_Sharp
{
    public static class Utils
    {
        public static List<string> ImageFileTypes { get; private set; } =
            new List<string>() { ".bmp", ".jpg", ".jpeg", ".gif", ".png", ".tif", ".jfif", ".ico" };
        public static List<string> SupportedFileTypes { get; private set; } = new List<string>();
        
        public static MainConfiguration config;

        public static void Associate(string exe)
        {
            try
            {
                foreach (string t in SupportedFileTypes)
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
            return ImageFileTypes.Contains(ext);
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

    public class MainConfiguration
    {
        public string FilePath { get; private set; }
        public string SelfDirPath { get; private set; }
        public string ListsDirPath { get; set; }

        public delegate void SavingDelegate(object sender, JObject root);
        public delegate void LoadingDelegate(object sender, JObject root);
        public SavingDelegate Saving;
        public LoadingDelegate Loading;

        public MainConfiguration(string filePath)
        {
            FilePath = filePath;
            SelfDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            LoadDefaults();
        }

        public void Save()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            JObject json = new JObject();
            Saving?.Invoke(this, json);
            File.WriteAllText(FilePath, json.ToString());
        }

        public void LoadDefaults()
        {
            ListsDirPath = $"{SelfDirPath}\\Lists\\";
        }

        public void Load()
        {
            if (File.Exists(FilePath))
            {
                JObject json = JObject.Parse(File.ReadAllText(FilePath));
                if (json != null)
                {
                    Loading?.Invoke(this, json);
                }
            }
        }
    }
}
