using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static Simple_image_viewer_C_Sharp.Utils;

namespace Simple_image_viewer_C_Sharp
{
    public partial class Form1 : Form
    {
        private Image image;
        private readonly ImageFileList fileList = new ImageFileList();
        private string currentImageFilePath;
        private Point oldPoint;
        private bool canDrag = false;
        private int timerInterval = 1000;
        private int stepSize = 1;
        private string defaultImageFilePath = null;
        private Rectangle scaledImageRect;
        private Rectangle originalImageRect;

        private readonly Random random = new Random((int)DateTime.Now.Ticks);
        public const string TITLE = "Simple image viewer";
        private readonly Keys[] additionalInputKeys = new[] { Keys.Up, Keys.Right, Keys.Down, Keys.Left, Keys.Tab };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileList.ItemRemoved += (s, index, item) =>
            {
                if (fileList.Count == 0)
                {
                    fileList.Clear();
                }
                else
                {
                    int id = index >= fileList.Count ? fileList.Count - 1 : index;
                    if (fileList.Position != id)
                    {
                        fileList.Position = id;
                    }
                    else
                    {
                        currentImageFilePath = fileList[fileList.Position];
                        LoadImage(currentImageFilePath);
                    }

                    miZoomToolStripMenuItem.Checked = false;
                    UpdateIndicators();
                }
            };
            fileList.PositionIndexChanged += (s, index) =>
            {
                if (index >= 0)
                {
                    miZoomToolStripMenuItem.Checked = false;
                    progressBar1.Value = fileList.Position + 1;
                    toolStripStatusLabelCounter.Text = $"{fileList.Position + 1} / {fileList.Count}";
                    currentImageFilePath = fileList[index];
                    LoadImage(currentImageFilePath);
                    UpdateIndicators();
                }
            };
            fileList.Cleared += (s) =>
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                pictureBox1.Image = null;
                progressBar1.Value = 0;
                currentImageFilePath = null;
                toolStripStatusLabelCounter.Text = "Список пуст";
                toolStripStatusLabelImageSize.Text = "No image";
                toolStripStatusLabelFilePath.Text = null;
                miZoomToolStripMenuItem.Checked = false;
                Text = TITLE;
            };

            SupportedFileTypes.AddRange(new string[] { ".siv", ".sivr" });
            SupportedFileTypes.AddRange(ImageFileTypes);


            config = new MainConfiguration(Path.GetDirectoryName(Application.ExecutablePath) + "\\config_siv.json");
            config.Saving += (object _sender, JObject json) =>
            {
                json["windowState"] = WindowState == FormWindowState.Maximized ? "maximized" : "normal";
                if (WindowState == FormWindowState.Normal)
                {
                    json["left"] = Left;
                    json["top"] = Top;
                    json["width"] = Width;
                    json["height"] = Height;
                }

                if (!string.IsNullOrEmpty(defaultImageFilePath))
                {
                    json["defaultImageFileName"] = defaultImageFilePath;
                }
                json["closeByMouseClick"] = miCloseByMouseClickToolStripMenuItem.Checked;
                json["useMouseWheel"] = miUseMouseWheelToolStripMenuItem.Checked;

                JArray jsonArr = new JArray();
                for (int i = 3; i < miRelativePathListToolStripMenuItem.DropDownItems.Count; i++)
                {
                    jsonArr.Add(miRelativePathListToolStripMenuItem.DropDownItems[i].Text);
                }
                json["relativeLists"] = jsonArr;
            };

            config.Loading += (object _sender, JObject json) =>
            {
                JToken jt = json.Value<JToken>("left");
                if (jt != null)
                {
                    Left = jt.Value<int>();
                }
                jt = json.Value<JToken>("top");
                if (jt != null)
                {
                    Top = jt.Value<int>();
                }
                jt = json.Value<JToken>("width");
                if (jt != null)
                {
                    Width = jt.Value<int>();
                }
                jt = json.Value<JToken>("height");
                if (jt != null)
                {
                    Height = jt.Value<int>();
                }

                jt = json.Value<JToken>("windowState");
                if (jt != null)
                {
                    string t = jt.Value<string>();
                    if (t == "normal")
                    {
                        WindowState = FormWindowState.Normal;
                    }
                    else if (t == "maximized")
                    {
                        WindowState = FormWindowState.Maximized;
                    }
                    else
                    {
                        throw new ArgumentException(t);
                    }
                }

                jt = json.Value<JToken>("defaultImageFileName");
                defaultImageFilePath = jt == null ? null : jt.Value<string>();

                jt = json.Value<JToken>("closeByMouseClick");
                miCloseByMouseClickToolStripMenuItem.Checked = jt != null && jt.Value<bool>();
                jt = json.Value<JToken>("useMouseWheel");
                miUseMouseWheelToolStripMenuItem.Checked = jt != null && jt.Value<bool>();

                jt = json.Value<JToken>("relativeLists");
                if (jt != null)
                {
                    JArray jsonArr = jt.Value<JArray>();
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(jsonArr[i].Value<string>());
                        menuItem.Click += OnMenuItemRelativeListClick;
                        miRelativePathListToolStripMenuItem.DropDownItems.Add(menuItem);
                    }
                }
            };

            config.Load();

            if (Directory.Exists(config.ListsDirPath))
            {
                string[] files = Directory.GetFiles(config.ListsDirPath);
                foreach (string fn in files)
                {
                    string ext = Path.GetExtension(fn).ToLower();
                    if (ext == ".siv")
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(Path.GetFileName(fn));
                        menuItem.Click += OnMenuItemAbsoluteListClick;
                        miAbsolutePathListsToolStripMenuItem.DropDownItems.Add(menuItem);
                    }
                }
            }

            string[] args = Environment.GetCommandLineArgs();
            currentImageFilePath = args.Length > 1 ? args[1] : null;
            if (string.IsNullOrEmpty(currentImageFilePath) &&
                !string.IsNullOrEmpty(defaultImageFilePath) &&
                !string.IsNullOrWhiteSpace(defaultImageFilePath))
            {
                currentImageFilePath = defaultImageFilePath;
            }
            if (File.Exists(currentImageFilePath))
            {
                string ext = Path.GetExtension(currentImageFilePath).ToLower();
                if (ext == ".siv" || ext == ".sivr")
                {
                    AppendList(currentImageFilePath);
                }
                else
                {
                    string dir = Path.GetDirectoryName(currentImageFilePath);
                    string[] files = Directory.GetFiles(dir);
                    progressBar1.Value = 0;
                    if (files.Length > 0)
                    {
                        foreach (string fn in files)
                        {
                            if (IsImageFile(fn))
                            {
                                fileList.Add(fn);
                            }
                        }
                    }
                }
                if (fileList.Count > 0)
                {
                    progressBar1.Maximum = fileList.Count;
                    if (IsImageFile(currentImageFilePath))
                    {
                        fileList.Position = fileList.IndexOf(currentImageFilePath);
                    }
                    else
                    {
                        fileList.Position = 0;
                        currentImageFilePath = fileList[0];
                    }
                    progressBar1.Maximum = fileList.Count;
                    progressBar1.Value = fileList.Position + 1;

                    toolStripStatusLabelCounter.Text = $"{fileList.Position + 1} / {fileList.Count}";
                    LoadImage(currentImageFilePath);
                }
                else
                {
                    fileList.Clear();
                }
            }
            else
            {
                fileList.Clear();
            }

            if (!UacHelper.IsUacEnabled || UacHelper.IsProcessElevated)
            {
                miAssociateExtToolStripMenuItem.Enabled = true;
            }
            else
            {
                miAssociateExtToolStripMenuItem.Enabled = false;
                miAssociateExtToolStripMenuItem.Text += " (администратор)";
            }

            MouseWheel += Form1_MouseWheel;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (image != null)
            {
                if (miZoomToolStripMenuItem.Checked)
                {
                    int zoomStep = 20;
                    if (e.Delta > 0)
                    {
                        Rectangle zoomRect = new Rectangle(scaledImageRect.X - zoomStep, scaledImageRect.Y - zoomStep,
                            scaledImageRect.Width + zoomStep * 2, scaledImageRect.Height + zoomStep * 2);
                        Point location = new Point(scaledImageRect.X, scaledImageRect.Y);
                        scaledImageRect = originalImageRect.ResizeTo(new Size(zoomRect.Width, zoomRect.Height)).CenterIn(zoomRect);
                        scaledImageRect.X = location.X - zoomStep;
                        scaledImageRect.Y = location.Y - zoomStep;
                        pictureBox1.Refresh();
                    }
                    else if (e.Delta < 0 && scaledImageRect.Width >= 70 && scaledImageRect.Height >= 70)
                    {
                        Rectangle zoomRect = new Rectangle(scaledImageRect.X + zoomStep, scaledImageRect.Y + zoomStep,
                            scaledImageRect.Width - zoomStep * 2, scaledImageRect.Height - zoomStep * 2);
                        Point location = new Point(scaledImageRect.X, scaledImageRect.Y);
                        scaledImageRect = originalImageRect.ResizeTo(new Size(zoomRect.Width, zoomRect.Height)).CenterIn(zoomRect);
                        scaledImageRect.X = location.X + zoomStep;
                        scaledImageRect.Y = location.Y + zoomStep;
                        pictureBox1.Refresh();
                    }
                }
                else if (progressBar1.Visible && miUseMouseWheelToolStripMenuItem.Checked && fileList.Count > 1)
                {
                    if (e.Delta > 0)
                    {
                        PreviousImage();
                    }
                    else if (e.Delta < 0)
                    {
                        NextImage();
                    }
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            config.Save();
            if (image != null)
            {
                image.Dispose();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveControl != null)
            {
                return;
            }

            if (additionalInputKeys.Contains(e.KeyCode))
            {
                e.Handled = true;
            }

            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Escape:
                    Close();
                    return;

                case Keys.PageUp:
                    if (progressBar1.Visible && fileList.Count > 1)
                    {
                        PreviousImage();
                    }
                    break;

                case Keys.PageDown:
                case Keys.Space:
                    if (progressBar1.Visible && fileList.Count > 1)
                    {
                        NextImage();
                    }
                    break;

                case Keys.Left:
                    if (WindowState == FormWindowState.Maximized && progressBar1.Visible && fileList.Count > 1)
                    {
                        PreviousImage();
                    }
                    break;

                case Keys.Right:
                    if (WindowState == FormWindowState.Maximized && progressBar1.Visible && fileList.Count > 1)
                    {
                        NextImage();
                    }
                    break;

                case Keys.Home:
                    if (progressBar1.Visible && fileList.Count > 1)
                    {
                        fileList.Position = 0;
                    }
                    break;

                case Keys.End:
                    if (progressBar1.Visible && fileList.Count > 1)
                    {
                        fileList.Position = fileList.Count - 1;
                    }
                    break;

                case Keys.Add:
                case Keys.Oemplus:
                    if (timer1.Enabled)
                    {
                        timerInterval += 100;
                        timer1.Interval = timerInterval;
                        toolStripStatusLabelStepSize.Text = $"Interval: {timer1.Interval}ms";
                    }
                    else
                    {
                        stepSize++;
                        toolStripStatusLabelStepSize.Text = $"Step size: {stepSize}";
                    }
                    break;

                case Keys.Subtract:
                case Keys.OemMinus:
                    if (timer1.Enabled)
                    {
                        if (timerInterval >= 200)
                        {
                            timerInterval -= 100;
                            timer1.Interval = timerInterval;
                            toolStripStatusLabelStepSize.Text = $"Interval: {timer1.Interval}ms";
                        }
                    }
                    else if (stepSize > 1)
                    {
                        stepSize--;
                        toolStripStatusLabelStepSize.Text = $"Step size: {stepSize}";
                    }
                    break;

                case Keys.D:
                    RequestToRemoveImageFromList(fileList.Position);
                    break;

                case Keys.S:
                    progressBar1.Visible = !progressBar1.Visible;
                    checkBoxTimer.Visible = progressBar1.Visible;
                    btnPrevImage.Visible = progressBar1.Visible;
                    btnNextImage.Visible = progressBar1.Visible;
                    pictureBox1.Height = progressBar1.Visible ? progressBar1.Top : statusStrip1.Top;
                    break;

                case Keys.V:
                    if (!miZoomToolStripMenuItem.Checked)
                    {
                        miTiledViewToolStripMenuItem.Checked = !miTiledViewToolStripMenuItem.Checked;
                        pictureBox1.Refresh();
                    }
                    break;

                case Keys.T:
                    checkBoxTimer.Checked = !checkBoxTimer.Checked;
                    break;

                case Keys.Multiply:
                    switch (WindowState)
                    {
                        case FormWindowState.Maximized:
                            WindowState = FormWindowState.Normal;
                            break;

                        case FormWindowState.Normal:
                            WindowState = FormWindowState.Maximized;
                            break;
                    }
                    break;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return additionalInputKeys.Contains(keyData) || base.IsInputKey(keyData);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                int countBefore = fileList.Count;
                string[] strings = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fn in strings)
                {
                    string ext = Path.GetExtension(fn).ToLower();
                    if (ext == ".siv" || ext == ".sivr")
                    {
                        AppendList(fn);
                    }
                    else if (File.Exists(fn))
                    {
                        if (IsImageFile(fn))
                        {
                            fileList.Add(fn);
                        }
                    }
                    else if (Directory.Exists(fn))
                    {
                        string[] files = Directory.GetFiles(fn);
                        foreach (string fileName in files)
                        {
                            if (IsImageFile(fileName))
                            {
                                fileList.Add(fileName);
                            }
                        }
                    }
                }
                int id = countBefore == 0 ? 0 : fileList.IndexOf(currentImageFilePath);
                if (fileList.Position != id)
                {
                    fileList.Position = id;
                }
                else
                {
                    UpdateIndicators();
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (image != null)
            {
                if (!miZoomToolStripMenuItem.Checked)
                {
                    scaledImageRect = miFitToScreenToolStripMenuItem.Checked ?
                        originalImageRect.ResizeTo(pictureBox1.Size) : originalImageRect;

                    if (miTiledViewToolStripMenuItem.Checked)
                    {
                        for (int x = 0; x < pictureBox1.Width; x += scaledImageRect.Width)
                        {
                            for (int y = 0; y < pictureBox1.Height; y += scaledImageRect.Height)
                            {
                                e.Graphics.DrawImage(image, x, y, scaledImageRect.Width, scaledImageRect.Height);
                            }
                        }
                    }
                    else
                    {
                        Rectangle r = scaledImageRect.CenterIn(pictureBox1.ClientRectangle);
                        e.Graphics.DrawImage(image, r.X, r.Y, r.Width, r.Height);
                    }
                }
                else
                {
                    e.Graphics.DrawImage(image, scaledImageRect.X, scaledImageRect.Y, scaledImageRect.Width, scaledImageRect.Height);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ActiveControl = null;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (miZoomToolStripMenuItem.Checked)
                    {
                        oldPoint = new Point(e.X, e.Y);
                        canDrag = true;
                    }
                    else if (miCloseByMouseClickToolStripMenuItem.Checked)
                    {
                        Close();
                        return;
                    }
                    break;

                case MouseButtons.Right:
                    contextMenuStrip1.Show(Cursor.Position);
                    break;

                case MouseButtons.Middle:
                    miZoomToolStripMenuItem.Checked = !miZoomToolStripMenuItem.Checked;
                    break;
            }
        }

        private bool LoadImage(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                image?.Dispose();
                try
                {
                    image = Image.FromFile(filePath);
                }
                catch (Exception ex)
                {
                    image = null;
                    originalImageRect = new Rectangle();
                    Debug.WriteLine(ex.StackTrace);
                }
                if (image != null)
                {
                    originalImageRect = new Rectangle(0, 0, image.Width, image.Height);
                    scaledImageRect = originalImageRect.ResizeTo(pictureBox1.Size).CenterIn(pictureBox1.ClientRectangle);
                    Text = $"{Path.GetFileName(filePath)} | {TITLE}";
                    toolStripStatusLabelImageSize.Text = $"{image.Width}x{image.Height}";
                    toolStripStatusLabelFilePath.Text = filePath;
                    pictureBox1.Refresh();
                    return true;
                }
            }
            toolStripStatusLabelImageSize.Text = "Ошибка!";
            toolStripStatusLabelFilePath.Text = $"Ошибка! {filePath}";
            pictureBox1.Image = null;
            return false;
        }

        private void NextImage()
        {
            if (fileList.Count > 1)
            {
                miZoomToolStripMenuItem.Checked = false;
                int id = fileList.Position + stepSize;
                if (id >= fileList.Count)
                {
                    id = 0;
                }
                fileList.Position = id;
            }
        }

        private void PreviousImage()
        {
            if (fileList.Count > 1)
            {
                miZoomToolStripMenuItem.Checked = false;
                int id = fileList.Position - stepSize;
                if (id < 0)
                {
                    id = fileList.Count - 1;
                }
                fileList.Position = id;
            }
        }

        private void RequestToRemoveImageFromList(int imagePositionIndex)
        {
            if (fileList.Count > 0 && imagePositionIndex >= 0 && imagePositionIndex < fileList.Count)
            {
                if (timer1.Enabled)
                {
                    MessageBox.Show("Нельзя удалить картинку из списка, пока включен таймер!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MessageBox.Show("Удалить картинку из списка?", "Удалятор картинок из списка",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fileList.RemoveAt(fileList.Position);
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void copyFullImageFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentImageFilePath))
            {
                SetClipboardText(currentImageFilePath);
            }
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBoxTimer.Checked = false;
            fileList.Clear();
        }

        private void miItemAssociateExtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Associate(Application.ExecutablePath);
        }

        private void miSaveAbsoluteListAssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileList.Count == 0)
            {
                MessageBox.Show("Список пуст!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить список";
            sfd.Filter = "Списки с абсолютными путями|*.siv";
            sfd.DefaultExt = ".siv";
            sfd.AddExtension = true;
            if (!Directory.Exists(config.ListsDirPath))
            {
                Directory.CreateDirectory(config.ListsDirPath);
            }
            sfd.InitialDirectory = config.ListsDirPath;
            sfd.FileName = "_list.siv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                JArray jArray = new JArray();
                foreach (string name in fileList.ToArray())
                {
                    jArray.Add(name);
                }
                JObject json = new JObject();
                json["list"] = jArray;
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                File.WriteAllText(sfd.FileName, json.ToString());

                string t = Path.GetFileName(sfd.FileName);
                int id = miAbsolutePathListsToolStripMenuItem.DropDownItems.FindName(t);
                if (id == -1)
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(t);
                    menuItem.Click += OnMenuItemAbsoluteListClick;
                    miAbsolutePathListsToolStripMenuItem.DropDownItems.Add(menuItem);
                }
            }
            sfd.Dispose();
        }

        private void miSaveRelativeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileList.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить список";
                sfd.Filter = "Списки с относительными путями|*.sivr";
                sfd.DefaultExt = ".sivr";
                sfd.AddExtension = true;
                string path = Path.GetDirectoryName(fileList[fileList.Position]) + "\\";
                sfd.InitialDirectory = path;
                sfd.FileName = "_list.sivr";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    JArray jArray = new JArray();
                    path = Path.GetDirectoryName(sfd.FileName) + "\\";
                    int len = path.Length;
                    foreach (string name in fileList.ToArray())
                    {
                        string fn = name.StartsWith(path) ? name.Substring(len) : name;
                        jArray.Add(fn);
                    }

                    JObject json = new JObject();
                    json["relativePath"] = path;
                    json["list"] = jArray;
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }
                    File.WriteAllText(sfd.FileName, json.ToString());

                    int id = miRelativePathListToolStripMenuItem.DropDownItems.FindName(sfd.FileName);
                    if (id == -1)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(sfd.FileName);
                        item.Click += OnMenuItemRelativeListClick;
                        miRelativePathListToolStripMenuItem.DropDownItems.Add(item);
                    }
                }
                sfd.Dispose();
            }
        }

        private int AppendList(string listFilePath)
        {
            List<string> lst = LoadImageList(listFilePath);
            if (lst.Count > 0)
            {
                int countBefore = fileList.Count;
                foreach (string filePath in lst)
                {
                    if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrWhiteSpace(filePath) &&
                        IsImageFile(filePath) && File.Exists(filePath))
                    {
                        fileList.Add(filePath);
                    }
                }
                if (fileList.Count > 0)
                {
                    progressBar1.Maximum = fileList.Count;
                    if (countBefore == 0 || fileList.Position < 0)
                    {
                        fileList.Position = 0;
                    }
                    else if (currentImageFilePath != fileList[fileList.Position])
                    {
                        fileList.Position = fileList.IndexOf(currentImageFilePath);
                    }
                    progressBar1.Value = fileList.Position + 1;
                    toolStripStatusLabelCounter.Text = $"{fileList.Position + 1} / {fileList.Count}";
                }
            }
            return fileList.Count;
        }

        private void OnMenuItemAbsoluteListClick(object sender, EventArgs e)
        {
            string filePath = config.ListsDirPath + (sender as ToolStripMenuItem).Text;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Список не найден!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Загрузить список из файла?\n{filePath}", "Загружатор списка из файла",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Список не найден!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppendList(filePath);
        }

        private void OnMenuItemRelativeListClick(object sender, EventArgs e)
        {
            string filePath = (sender as ToolStripMenuItem).Text;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Список не найден!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Загрузить список из файла?\n{filePath}", "Загружатор списка из файла",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Список не найден!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppendList(filePath);
        }

        private void btnPrevImage_Click(object sender, EventArgs e)
        {
            PreviousImage();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void btnNextImage_Click(object sender, EventArgs e)
        {
            NextImage();
        }

        private void btnPrevImage_Leave(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void miCloseByMouseCliickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miCloseByMouseClickToolStripMenuItem.Checked = !miCloseByMouseClickToolStripMenuItem.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int id = random.Next(fileList.Count);
            if (id != fileList.Position)
            {
                fileList.Position = id;
            }
        }

        private void UpdateIndicators()
        {
            progressBar1.Maximum = fileList.Count;
            progressBar1.Value = fileList.Position + 1;
            toolStripStatusLabelCounter.Text = $"{fileList.Position + 1} / {fileList.Count}";
            toolStripStatusLabelStepSize.Text = timer1.Enabled ?
                $"Interval: {timer1.Interval}ms" : $"Step size: {stepSize}";
            toolStripStatusLabelFilePath.Text = currentImageFilePath;
        }

        private void checkBoxTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (fileList.Count > 1 && checkBoxTimer.Checked)
            {
                miTimerEnabledToolStripMenuItem.Checked = true;
                toolStripStatusLabelStepSize.Text = $"Interval: {timer1.Interval}ms";
                timer1.Interval = timerInterval;
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                toolStripStatusLabelStepSize.Text = $"Step size: {stepSize}";
                miTimerEnabledToolStripMenuItem.Checked = false;
                checkBoxTimer.Checked = false;
            }
        }

        private void timerEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBoxTimer.Checked = !checkBoxTimer.Checked;
        }

        private void miTiledViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miTiledViewToolStripMenuItem.Checked = !miTiledViewToolStripMenuItem.Checked;
            if (miTiledViewToolStripMenuItem.Checked)
            {
                miZoomToolStripMenuItem.Checked = false;
            }
            pictureBox1.Refresh();
        }

        private void miFitToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miFitToScreenToolStripMenuItem.Checked = !miFitToScreenToolStripMenuItem.Checked;
            pictureBox1.Refresh();
        }

        private void miAddRelativeListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileList.Count > 0)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Path.GetDirectoryName(currentImageFilePath);
                ofd.Filter = "Списки с относительными путями|*.sivr";
                ofd.FileName = "_list.sivr";
                ofd.AddExtension = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    int id = miRelativePathListToolStripMenuItem.DropDownItems.FindName(ofd.FileName);
                    if (id == -1)
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(ofd.FileName);
                        menuItem.Click += OnMenuItemRelativeListClick;
                        miRelativePathListToolStripMenuItem.DropDownItems.Add(menuItem);
                    }
                }
                ofd.Dispose();
            }
        }

        private void miSetDefaultImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentImageFilePath))
            {
                defaultImageFilePath = currentImageFilePath;
            }
        }

        private void miRemoveDefaultImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            defaultImageFilePath = null;
        }

        private void miRemoveImageFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RequestToRemoveImageFromList(fileList.Position);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (fileList.Count > 0)
            {
                miSaveAbsoluteListAssToolStripMenuItem.Enabled = true;
                miSaveRelativeListToolStripMenuItem.Enabled = true;
                miTimerEnabledToolStripMenuItem.Enabled = true;
                miCopyFullImageFileNameToolStripMenuItem.Enabled = true;
                miOpenContainingFolderToolStripMenuItem.Enabled = true;
                miZoomToolStripMenuItem.Enabled = true;
                miRemoveImageFromListToolStripMenuItem.Enabled = true;
                miClearListToolStripMenuItem.Enabled = true;
                miSetDefaultImageToolStripMenuItem.Enabled = true;
                miRemoveDefaultImageToolStripMenuItem.Enabled = true;
            }
            else
            {
                miSaveAbsoluteListAssToolStripMenuItem.Enabled = false;
                miSaveRelativeListToolStripMenuItem.Enabled = false;
                miTimerEnabledToolStripMenuItem.Enabled = false;
                miCopyFullImageFileNameToolStripMenuItem.Enabled = false;
                miOpenContainingFolderToolStripMenuItem.Enabled = false;
                miZoomToolStripMenuItem.Enabled = false;
                miRemoveImageFromListToolStripMenuItem.Enabled = false;
                miClearListToolStripMenuItem.Enabled = false;
                miSetDefaultImageToolStripMenuItem.Enabled = false;
                miRemoveDefaultImageToolStripMenuItem.Enabled = false;
            }
        }

        private void miUseMouseWheelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miUseMouseWheelToolStripMenuItem.Checked = !miUseMouseWheelToolStripMenuItem.Checked;
        }

        private void miOpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentImageFilePath))
            {
                string path = Path.GetDirectoryName(currentImageFilePath);
                if (!string.IsNullOrEmpty(path))
                {
                    Process process = new Process();
                    process.StartInfo.FileName = path;
                    process.Start();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            canDrag = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDrag)
            {
                scaledImageRect.X += e.X - oldPoint.X;
                scaledImageRect.Y += e.Y - oldPoint.Y;
                oldPoint = new Point(e.X, e.Y);
                pictureBox1.Refresh();
            }
        }

        private void checkBoxTimer_Enter(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void miZoomToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            string imageFileName = Path.GetFileName(currentImageFilePath);
            if (miZoomToolStripMenuItem.Checked)
            {
                miTiledViewToolStripMenuItem.Checked = false;
                scaledImageRect = originalImageRect.ResizeTo(pictureBox1.Size).CenterIn(pictureBox1.ClientRectangle);
                Text = $"{imageFileName} | {TITLE} [ZOOM]";
            }
            else
            {
                canDrag = false;
                Text = $"{imageFileName} | {TITLE}";
            }
            pictureBox1.Refresh();
        }

        private void miZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            miZoomToolStripMenuItem.Checked = !miZoomToolStripMenuItem.Checked;
            string imageFileName = Path.GetFileName(currentImageFilePath);
            if (miZoomToolStripMenuItem.Checked)
            {
                miTiledViewToolStripMenuItem.Checked = false;
                scaledImageRect = originalImageRect.ResizeTo(pictureBox1.Size).CenterIn(pictureBox1.ClientRectangle);
                Text = $"{imageFileName} | {TITLE} [ZOOM]";
            }
            else
            {
                canDrag = false;
                Text = $"{imageFileName} | {TITLE}";
            }
            pictureBox1.Refresh();
        }
    }
}
