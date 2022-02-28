
namespace Simple_image_viewer_C_Sharp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStepSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFilePath = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAbsolutePathListsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAbsoluteListAssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miRelativePathListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveRelativeListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddRelativeListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miViewModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miTiledViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miFitToScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miTimerEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miCopyFullImageFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCloseByMouseClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miUseMouseWheelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveImageFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miSetDefaultImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveDefaultImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miAssociateExtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPrevImage = new System.Windows.Forms.Button();
            this.btnNextImage = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxTimer = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(492, 212);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCounter,
            this.toolStripStatusLabelStepSize,
            this.toolStripStatusLabelImageSize,
            this.toolStripStatusLabelFilePath});
            this.statusStrip1.Location = new System.Drawing.Point(0, 236);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCounter
            // 
            this.toolStripStatusLabelCounter.Name = "toolStripStatusLabelCounter";
            this.toolStripStatusLabelCounter.Size = new System.Drawing.Size(92, 20);
            this.toolStripStatusLabelCounter.Text = "Список пуст";
            // 
            // toolStripStatusLabelStepSize
            // 
            this.toolStripStatusLabelStepSize.Name = "toolStripStatusLabelStepSize";
            this.toolStripStatusLabelStepSize.Size = new System.Drawing.Size(83, 20);
            this.toolStripStatusLabelStepSize.Text = "Step size: 1";
            // 
            // toolStripStatusLabelImageSize
            // 
            this.toolStripStatusLabelImageSize.Name = "toolStripStatusLabelImageSize";
            this.toolStripStatusLabelImageSize.Size = new System.Drawing.Size(75, 20);
            this.toolStripStatusLabelImageSize.Text = "No image";
            // 
            // toolStripStatusLabelFilePath
            // 
            this.toolStripStatusLabelFilePath.Name = "toolStripStatusLabelFilePath";
            this.toolStripStatusLabelFilePath.Size = new System.Drawing.Size(0, 20);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 213);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(412, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbsolutePathListsToolStripMenuItem,
            this.miRelativePathListToolStripMenuItem,
            this.toolStripMenuItem1,
            this.miViewModeToolStripMenuItem,
            this.miTimerEnabledToolStripMenuItem,
            this.toolStripMenuItem3,
            this.miCopyFullImageFileNameToolStripMenuItem,
            this.miOpenContainingFolderToolStripMenuItem,
            this.miCloseByMouseClickToolStripMenuItem,
            this.miZoomToolStripMenuItem,
            this.miUseMouseWheelToolStripMenuItem,
            this.miRemoveImageFromListToolStripMenuItem,
            this.miClearListToolStripMenuItem,
            this.toolStripSeparator1,
            this.miSetDefaultImageToolStripMenuItem,
            this.miRemoveDefaultImageToolStripMenuItem,
            this.toolStripMenuItem4,
            this.miAssociateExtToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(413, 414);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // miAbsolutePathListsToolStripMenuItem
            // 
            this.miAbsolutePathListsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSaveAbsoluteListAssToolStripMenuItem,
            this.toolStripMenuItem2});
            this.miAbsolutePathListsToolStripMenuItem.Name = "miAbsolutePathListsToolStripMenuItem";
            this.miAbsolutePathListsToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miAbsolutePathListsToolStripMenuItem.Text = "Списки с абсолютными путями";
            // 
            // miSaveAbsoluteListAssToolStripMenuItem
            // 
            this.miSaveAbsoluteListAssToolStripMenuItem.Name = "miSaveAbsoluteListAssToolStripMenuItem";
            this.miSaveAbsoluteListAssToolStripMenuItem.Size = new System.Drawing.Size(246, 26);
            this.miSaveAbsoluteListAssToolStripMenuItem.Text = "Сохранить список как...";
            this.miSaveAbsoluteListAssToolStripMenuItem.Click += new System.EventHandler(this.miSaveAbsoluteListAssToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(243, 6);
            // 
            // miRelativePathListToolStripMenuItem
            // 
            this.miRelativePathListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSaveRelativeListToolStripMenuItem,
            this.miAddRelativeListToolStripMenuItem,
            this.toolStripMenuItem5});
            this.miRelativePathListToolStripMenuItem.Name = "miRelativePathListToolStripMenuItem";
            this.miRelativePathListToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miRelativePathListToolStripMenuItem.Text = "Списки с относительными путями";
            // 
            // miSaveRelativeListToolStripMenuItem
            // 
            this.miSaveRelativeListToolStripMenuItem.Name = "miSaveRelativeListToolStripMenuItem";
            this.miSaveRelativeListToolStripMenuItem.Size = new System.Drawing.Size(309, 26);
            this.miSaveRelativeListToolStripMenuItem.Text = "Сохранить список как...";
            this.miSaveRelativeListToolStripMenuItem.Click += new System.EventHandler(this.miSaveRelativeListToolStripMenuItem_Click);
            // 
            // miAddRelativeListToolStripMenuItem
            // 
            this.miAddRelativeListToolStripMenuItem.Name = "miAddRelativeListToolStripMenuItem";
            this.miAddRelativeListToolStripMenuItem.Size = new System.Drawing.Size(309, 26);
            this.miAddRelativeListToolStripMenuItem.Text = "Добавить существущий список..";
            this.miAddRelativeListToolStripMenuItem.Click += new System.EventHandler(this.miAddRelativeListToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(306, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(409, 6);
            // 
            // miViewModeToolStripMenuItem
            // 
            this.miViewModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTiledViewToolStripMenuItem,
            this.miFitToScreenToolStripMenuItem});
            this.miViewModeToolStripMenuItem.Name = "miViewModeToolStripMenuItem";
            this.miViewModeToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miViewModeToolStripMenuItem.Text = "Режим просмотра";
            // 
            // miTiledViewToolStripMenuItem
            // 
            this.miTiledViewToolStripMenuItem.Name = "miTiledViewToolStripMenuItem";
            this.miTiledViewToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.miTiledViewToolStripMenuItem.Text = "Плитка (Tiled)";
            this.miTiledViewToolStripMenuItem.Click += new System.EventHandler(this.miTiledViewToolStripMenuItem_Click);
            // 
            // miFitToScreenToolStripMenuItem
            // 
            this.miFitToScreenToolStripMenuItem.Checked = true;
            this.miFitToScreenToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miFitToScreenToolStripMenuItem.Name = "miFitToScreenToolStripMenuItem";
            this.miFitToScreenToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.miFitToScreenToolStripMenuItem.Text = "Вписать в размер окна";
            this.miFitToScreenToolStripMenuItem.Click += new System.EventHandler(this.miFitToScreenToolStripMenuItem_Click);
            // 
            // miTimerEnabledToolStripMenuItem
            // 
            this.miTimerEnabledToolStripMenuItem.Name = "miTimerEnabledToolStripMenuItem";
            this.miTimerEnabledToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miTimerEnabledToolStripMenuItem.Text = "Таймер активен";
            this.miTimerEnabledToolStripMenuItem.Click += new System.EventHandler(this.timerEnabledToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(409, 6);
            // 
            // miCopyFullImageFileNameToolStripMenuItem
            // 
            this.miCopyFullImageFileNameToolStripMenuItem.Name = "miCopyFullImageFileNameToolStripMenuItem";
            this.miCopyFullImageFileNameToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miCopyFullImageFileNameToolStripMenuItem.Text = "Скопировать полное имя файла";
            this.miCopyFullImageFileNameToolStripMenuItem.Click += new System.EventHandler(this.copyFullImageFileNameToolStripMenuItem_Click);
            // 
            // miOpenContainingFolderToolStripMenuItem
            // 
            this.miOpenContainingFolderToolStripMenuItem.Name = "miOpenContainingFolderToolStripMenuItem";
            this.miOpenContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miOpenContainingFolderToolStripMenuItem.Text = "Открыть папку с текущей картинкой";
            this.miOpenContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.miOpenContainingFolderToolStripMenuItem_Click);
            // 
            // miCloseByMouseClickToolStripMenuItem
            // 
            this.miCloseByMouseClickToolStripMenuItem.Name = "miCloseByMouseClickToolStripMenuItem";
            this.miCloseByMouseClickToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miCloseByMouseClickToolStripMenuItem.Text = "Закрывать щелчком мыши";
            this.miCloseByMouseClickToolStripMenuItem.Click += new System.EventHandler(this.miCloseByMouseCliickToolStripMenuItem_Click);
            // 
            // miZoomToolStripMenuItem
            // 
            this.miZoomToolStripMenuItem.Name = "miZoomToolStripMenuItem";
            this.miZoomToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miZoomToolStripMenuItem.Text = "Zoom zoom";
            this.miZoomToolStripMenuItem.CheckedChanged += new System.EventHandler(this.miZoomToolStripMenuItem_CheckedChanged);
            this.miZoomToolStripMenuItem.Click += new System.EventHandler(this.miZoomToolStripMenuItem_Click);
            // 
            // miUseMouseWheelToolStripMenuItem
            // 
            this.miUseMouseWheelToolStripMenuItem.Name = "miUseMouseWheelToolStripMenuItem";
            this.miUseMouseWheelToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miUseMouseWheelToolStripMenuItem.Text = "Использовать колесо мыши";
            this.miUseMouseWheelToolStripMenuItem.Click += new System.EventHandler(this.miUseMouseWheelToolStripMenuItem_Click);
            // 
            // miRemoveImageFromListToolStripMenuItem
            // 
            this.miRemoveImageFromListToolStripMenuItem.Name = "miRemoveImageFromListToolStripMenuItem";
            this.miRemoveImageFromListToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miRemoveImageFromListToolStripMenuItem.Text = "Убрать картинку из списка";
            this.miRemoveImageFromListToolStripMenuItem.Click += new System.EventHandler(this.miRemoveImageFromListToolStripMenuItem_Click);
            // 
            // miClearListToolStripMenuItem
            // 
            this.miClearListToolStripMenuItem.Name = "miClearListToolStripMenuItem";
            this.miClearListToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miClearListToolStripMenuItem.Text = "Очистить список";
            this.miClearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(409, 6);
            // 
            // miSetDefaultImageToolStripMenuItem
            // 
            this.miSetDefaultImageToolStripMenuItem.Name = "miSetDefaultImageToolStripMenuItem";
            this.miSetDefaultImageToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miSetDefaultImageToolStripMenuItem.Text = "Установить текущую картинку по-умолчанию";
            this.miSetDefaultImageToolStripMenuItem.Click += new System.EventHandler(this.miSetDefaultImageToolStripMenuItem_Click);
            // 
            // miRemoveDefaultImageToolStripMenuItem
            // 
            this.miRemoveDefaultImageToolStripMenuItem.Name = "miRemoveDefaultImageToolStripMenuItem";
            this.miRemoveDefaultImageToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miRemoveDefaultImageToolStripMenuItem.Text = "Убрать картинку по-умолчанию";
            this.miRemoveDefaultImageToolStripMenuItem.Click += new System.EventHandler(this.miRemoveDefaultImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(409, 6);
            // 
            // miAssociateExtToolStripMenuItem
            // 
            this.miAssociateExtToolStripMenuItem.Name = "miAssociateExtToolStripMenuItem";
            this.miAssociateExtToolStripMenuItem.Size = new System.Drawing.Size(412, 26);
            this.miAssociateExtToolStripMenuItem.Text = "Ассоциировать файлы";
            this.miAssociateExtToolStripMenuItem.Click += new System.EventHandler(this.miItemAssociateExtToolStripMenuItem_Click);
            // 
            // btnPrevImage
            // 
            this.btnPrevImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevImage.Location = new System.Drawing.Point(438, 213);
            this.btnPrevImage.Name = "btnPrevImage";
            this.btnPrevImage.Size = new System.Drawing.Size(23, 23);
            this.btnPrevImage.TabIndex = 4;
            this.btnPrevImage.Text = "<";
            this.toolTip1.SetToolTip(this.btnPrevImage, "Предыдущая картинка");
            this.btnPrevImage.UseVisualStyleBackColor = true;
            this.btnPrevImage.Click += new System.EventHandler(this.btnPrevImage_Click);
            this.btnPrevImage.Enter += new System.EventHandler(this.btnPrevImage_Leave);
            // 
            // btnNextImage
            // 
            this.btnNextImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextImage.Location = new System.Drawing.Point(461, 213);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(23, 23);
            this.btnNextImage.TabIndex = 6;
            this.btnNextImage.Text = ">";
            this.toolTip1.SetToolTip(this.btnNextImage, "Следующая картинка");
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.Click += new System.EventHandler(this.btnNextImage_Click);
            this.btnNextImage.Enter += new System.EventHandler(this.btnPrevImage_Leave);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxTimer
            // 
            this.checkBoxTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTimer.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxTimer.AutoSize = true;
            this.checkBoxTimer.Location = new System.Drawing.Point(414, 213);
            this.checkBoxTimer.Name = "checkBoxTimer";
            this.checkBoxTimer.Size = new System.Drawing.Size(24, 23);
            this.checkBoxTimer.TabIndex = 7;
            this.checkBoxTimer.Text = "T";
            this.toolTip1.SetToolTip(this.checkBoxTimer, "Таймер вкл/выкл");
            this.checkBoxTimer.UseVisualStyleBackColor = true;
            this.checkBoxTimer.CheckedChanged += new System.EventHandler(this.checkBoxTimer_CheckedChanged);
            this.checkBoxTimer.Enter += new System.EventHandler(this.checkBoxTimer_Enter);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.checkBoxTimer);
            this.Controls.Add(this.btnNextImage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPrevImage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(16, 150);
            this.Name = "Form1";
            this.Text = "Simple image viewer";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCounter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilePath;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCopyFullImageFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miClearListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAssociateExtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAbsolutePathListsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSaveAbsoluteListAssToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Button btnPrevImage;
        private System.Windows.Forms.Button btnNextImage;
        private System.Windows.Forms.ToolStripMenuItem miRelativePathListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSaveRelativeListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCloseByMouseClickToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBoxTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStepSize;
        private System.Windows.Forms.ToolStripMenuItem miTimerEnabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miViewModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miTiledViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miFitToScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miAddRelativeListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageSize;
        private System.Windows.Forms.ToolStripMenuItem miSetDefaultImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miRemoveDefaultImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miRemoveImageFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miUseMouseWheelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOpenContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miZoomToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

