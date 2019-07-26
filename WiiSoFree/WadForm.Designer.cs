namespace WiiSoFree
{
    partial class WadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WadForm));
            this.wadFilenameTextBox = new System.Windows.Forms.TextBox();
            this.wadFilenameLabel = new System.Windows.Forms.Label();
            this.wadGroupBox = new System.Windows.Forms.GroupBox();
            this.makerCodeCoTextBox = new System.Windows.Forms.TextBox();
            this.platformTextBox = new System.Windows.Forms.TextBox();
            this.platformLabel = new System.Windows.Forms.Label();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.gameNameTextBox = new System.Windows.Forms.TextBox();
            this.regionCodeTextBox = new System.Windows.Forms.TextBox();
            this.regionCodelabel = new System.Windows.Forms.Label();
            this.regionStringTextBox = new System.Windows.Forms.TextBox();
            this.regionStringLabel = new System.Windows.Forms.Label();
            this.makerCodeLabel = new System.Windows.Forms.Label();
            this.makerCodeTextBox = new System.Windows.Forms.TextBox();
            this.originalRegionLabel = new System.Windows.Forms.Label();
            this.originalRegionTextBox = new System.Windows.Forms.TextBox();
            this.gamecodeTextBox = new System.Windows.Forms.TextBox();
            this.gamecodeLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWadVCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchWadVCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wadPatchGroupBox = new System.Windows.Forms.GroupBox();
            this.newRegionStringLabel = new System.Windows.Forms.Label();
            this.newRegionStringTextBox = new System.Windows.Forms.TextBox();
            this.patchWadButton = new System.Windows.Forms.Button();
            this.newRegionStringComboBox = new System.Windows.Forms.ComboBox();
            this.newRegionCodeTextBox = new System.Windows.Forms.TextBox();
            this.newRegionCodeLabel = new System.Windows.Forms.Label();
            this.newRegionLabel = new System.Windows.Forms.Label();
            this.wadGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.wadPatchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // wadFilenameTextBox
            // 
            this.wadFilenameTextBox.Location = new System.Drawing.Point(106, 13);
            this.wadFilenameTextBox.Name = "wadFilenameTextBox";
            this.wadFilenameTextBox.Size = new System.Drawing.Size(304, 20);
            this.wadFilenameTextBox.TabIndex = 0;
            // 
            // wadFilenameLabel
            // 
            this.wadFilenameLabel.AutoSize = true;
            this.wadFilenameLabel.Location = new System.Drawing.Point(6, 16);
            this.wadFilenameLabel.Name = "wadFilenameLabel";
            this.wadFilenameLabel.Size = new System.Drawing.Size(78, 13);
            this.wadFilenameLabel.TabIndex = 1;
            this.wadFilenameLabel.Text = "Wad Filename:";
            // 
            // wadGroupBox
            // 
            this.wadGroupBox.Controls.Add(this.makerCodeCoTextBox);
            this.wadGroupBox.Controls.Add(this.platformTextBox);
            this.wadGroupBox.Controls.Add(this.platformLabel);
            this.wadGroupBox.Controls.Add(this.gameNameLabel);
            this.wadGroupBox.Controls.Add(this.gameNameTextBox);
            this.wadGroupBox.Controls.Add(this.regionCodeTextBox);
            this.wadGroupBox.Controls.Add(this.regionCodelabel);
            this.wadGroupBox.Controls.Add(this.regionStringTextBox);
            this.wadGroupBox.Controls.Add(this.regionStringLabel);
            this.wadGroupBox.Controls.Add(this.makerCodeLabel);
            this.wadGroupBox.Controls.Add(this.makerCodeTextBox);
            this.wadGroupBox.Controls.Add(this.originalRegionLabel);
            this.wadGroupBox.Controls.Add(this.originalRegionTextBox);
            this.wadGroupBox.Controls.Add(this.gamecodeTextBox);
            this.wadGroupBox.Controls.Add(this.gamecodeLabel);
            this.wadGroupBox.Controls.Add(this.wadFilenameLabel);
            this.wadGroupBox.Controls.Add(this.wadFilenameTextBox);
            this.wadGroupBox.Location = new System.Drawing.Point(12, 26);
            this.wadGroupBox.Name = "wadGroupBox";
            this.wadGroupBox.Size = new System.Drawing.Size(416, 173);
            this.wadGroupBox.TabIndex = 2;
            this.wadGroupBox.TabStop = false;
            // 
            // makerCodeCoTextBox
            // 
            this.makerCodeCoTextBox.Location = new System.Drawing.Point(215, 91);
            this.makerCodeCoTextBox.Name = "makerCodeCoTextBox";
            this.makerCodeCoTextBox.Size = new System.Drawing.Size(195, 20);
            this.makerCodeCoTextBox.TabIndex = 16;
            // 
            // platformTextBox
            // 
            this.platformTextBox.Location = new System.Drawing.Point(106, 66);
            this.platformTextBox.Name = "platformTextBox";
            this.platformTextBox.Size = new System.Drawing.Size(100, 20);
            this.platformTextBox.TabIndex = 15;
            // 
            // platformLabel
            // 
            this.platformLabel.AutoSize = true;
            this.platformLabel.Location = new System.Drawing.Point(6, 69);
            this.platformLabel.Name = "platformLabel";
            this.platformLabel.Size = new System.Drawing.Size(86, 13);
            this.platformLabel.TabIndex = 14;
            this.platformLabel.Text = "Original Platform:";
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Location = new System.Drawing.Point(212, 42);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(69, 13);
            this.gameNameLabel.TabIndex = 13;
            this.gameNameLabel.Text = "Game Name:";
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Location = new System.Drawing.Point(287, 39);
            this.gameNameTextBox.Name = "gameNameTextBox";
            this.gameNameTextBox.Size = new System.Drawing.Size(123, 20);
            this.gameNameTextBox.TabIndex = 12;
            // 
            // regionCodeTextBox
            // 
            this.regionCodeTextBox.Location = new System.Drawing.Point(106, 143);
            this.regionCodeTextBox.Name = "regionCodeTextBox";
            this.regionCodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.regionCodeTextBox.TabIndex = 11;
            // 
            // regionCodelabel
            // 
            this.regionCodelabel.AutoSize = true;
            this.regionCodelabel.Location = new System.Drawing.Point(6, 146);
            this.regionCodelabel.Name = "regionCodelabel";
            this.regionCodelabel.Size = new System.Drawing.Size(72, 13);
            this.regionCodelabel.TabIndex = 10;
            this.regionCodelabel.Text = "Region Code:";
            // 
            // regionStringTextBox
            // 
            this.regionStringTextBox.Location = new System.Drawing.Point(106, 117);
            this.regionStringTextBox.Name = "regionStringTextBox";
            this.regionStringTextBox.Size = new System.Drawing.Size(304, 20);
            this.regionStringTextBox.TabIndex = 9;
            // 
            // regionStringLabel
            // 
            this.regionStringLabel.AutoSize = true;
            this.regionStringLabel.Location = new System.Drawing.Point(6, 120);
            this.regionStringLabel.Name = "regionStringLabel";
            this.regionStringLabel.Size = new System.Drawing.Size(74, 13);
            this.regionStringLabel.TabIndex = 8;
            this.regionStringLabel.Text = "Region String:";
            // 
            // makerCodeLabel
            // 
            this.makerCodeLabel.AutoSize = true;
            this.makerCodeLabel.Location = new System.Drawing.Point(6, 94);
            this.makerCodeLabel.Name = "makerCodeLabel";
            this.makerCodeLabel.Size = new System.Drawing.Size(68, 13);
            this.makerCodeLabel.TabIndex = 7;
            this.makerCodeLabel.Text = "Maker Code:";
            // 
            // makerCodeTextBox
            // 
            this.makerCodeTextBox.Location = new System.Drawing.Point(106, 91);
            this.makerCodeTextBox.Name = "makerCodeTextBox";
            this.makerCodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.makerCodeTextBox.TabIndex = 6;
            // 
            // originalRegionLabel
            // 
            this.originalRegionLabel.AutoSize = true;
            this.originalRegionLabel.Location = new System.Drawing.Point(212, 146);
            this.originalRegionLabel.Name = "originalRegionLabel";
            this.originalRegionLabel.Size = new System.Drawing.Size(82, 13);
            this.originalRegionLabel.TabIndex = 5;
            this.originalRegionLabel.Text = "Original Region:";
            // 
            // originalRegionTextBox
            // 
            this.originalRegionTextBox.Location = new System.Drawing.Point(300, 143);
            this.originalRegionTextBox.Name = "originalRegionTextBox";
            this.originalRegionTextBox.Size = new System.Drawing.Size(100, 20);
            this.originalRegionTextBox.TabIndex = 4;
            // 
            // gamecodeTextBox
            // 
            this.gamecodeTextBox.Location = new System.Drawing.Point(106, 39);
            this.gamecodeTextBox.Name = "gamecodeTextBox";
            this.gamecodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.gamecodeTextBox.TabIndex = 3;
            // 
            // gamecodeLabel
            // 
            this.gamecodeLabel.AutoSize = true;
            this.gamecodeLabel.Location = new System.Drawing.Point(6, 42);
            this.gamecodeLabel.Name = "gamecodeLabel";
            this.gamecodeLabel.Size = new System.Drawing.Size(66, 13);
            this.gamecodeLabel.TabIndex = 2;
            this.gamecodeLabel.Text = "Game Code:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(440, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWadVCToolStripMenuItem,
            this.patchWadVCToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openWadVCToolStripMenuItem
            // 
            this.openWadVCToolStripMenuItem.Name = "openWadVCToolStripMenuItem";
            this.openWadVCToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openWadVCToolStripMenuItem.Text = "&Open WAD / VC";
            this.openWadVCToolStripMenuItem.Click += new System.EventHandler(this.OpenWadVCToolStripMenuItem_Click);
            // 
            // patchWadVCToolStripMenuItem
            // 
            this.patchWadVCToolStripMenuItem.Name = "patchWadVCToolStripMenuItem";
            this.patchWadVCToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.patchWadVCToolStripMenuItem.Text = "&Patch WAD / VC";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // wadPatchGroupBox
            // 
            this.wadPatchGroupBox.Controls.Add(this.newRegionStringLabel);
            this.wadPatchGroupBox.Controls.Add(this.newRegionStringTextBox);
            this.wadPatchGroupBox.Controls.Add(this.patchWadButton);
            this.wadPatchGroupBox.Controls.Add(this.newRegionStringComboBox);
            this.wadPatchGroupBox.Controls.Add(this.newRegionCodeTextBox);
            this.wadPatchGroupBox.Controls.Add(this.newRegionCodeLabel);
            this.wadPatchGroupBox.Controls.Add(this.newRegionLabel);
            this.wadPatchGroupBox.Location = new System.Drawing.Point(12, 205);
            this.wadPatchGroupBox.Name = "wadPatchGroupBox";
            this.wadPatchGroupBox.Size = new System.Drawing.Size(416, 150);
            this.wadPatchGroupBox.TabIndex = 4;
            this.wadPatchGroupBox.TabStop = false;
            // 
            // newRegionStringLabel
            // 
            this.newRegionStringLabel.AutoSize = true;
            this.newRegionStringLabel.Location = new System.Drawing.Point(6, 89);
            this.newRegionStringLabel.Name = "newRegionStringLabel";
            this.newRegionStringLabel.Size = new System.Drawing.Size(99, 13);
            this.newRegionStringLabel.TabIndex = 6;
            this.newRegionStringLabel.Text = "New Region String:";
            // 
            // newRegionStringTextBox
            // 
            this.newRegionStringTextBox.Location = new System.Drawing.Point(106, 86);
            this.newRegionStringTextBox.Name = "newRegionStringTextBox";
            this.newRegionStringTextBox.Size = new System.Drawing.Size(304, 20);
            this.newRegionStringTextBox.TabIndex = 5;
            // 
            // patchWadButton
            // 
            this.patchWadButton.Location = new System.Drawing.Point(335, 121);
            this.patchWadButton.Name = "patchWadButton";
            this.patchWadButton.Size = new System.Drawing.Size(75, 23);
            this.patchWadButton.TabIndex = 4;
            this.patchWadButton.Text = "Patch Wad";
            this.patchWadButton.UseVisualStyleBackColor = true;
            this.patchWadButton.Click += new System.EventHandler(this.PatchWadButton_Click);
            // 
            // newRegionStringComboBox
            // 
            this.newRegionStringComboBox.FormattingEnabled = true;
            this.newRegionStringComboBox.Location = new System.Drawing.Point(106, 33);
            this.newRegionStringComboBox.Name = "newRegionStringComboBox";
            this.newRegionStringComboBox.Size = new System.Drawing.Size(122, 21);
            this.newRegionStringComboBox.TabIndex = 3;
            this.newRegionStringComboBox.SelectedIndexChanged += new System.EventHandler(this.NewRegionStringComboBox_SelectedIndexChanged);
            // 
            // newRegionCodeTextBox
            // 
            this.newRegionCodeTextBox.Location = new System.Drawing.Point(106, 60);
            this.newRegionCodeTextBox.Name = "newRegionCodeTextBox";
            this.newRegionCodeTextBox.Size = new System.Drawing.Size(100, 20);
            this.newRegionCodeTextBox.TabIndex = 2;
            // 
            // newRegionCodeLabel
            // 
            this.newRegionCodeLabel.AutoSize = true;
            this.newRegionCodeLabel.Location = new System.Drawing.Point(6, 63);
            this.newRegionCodeLabel.Name = "newRegionCodeLabel";
            this.newRegionCodeLabel.Size = new System.Drawing.Size(97, 13);
            this.newRegionCodeLabel.TabIndex = 1;
            this.newRegionCodeLabel.Text = "New Region Code:";
            // 
            // newRegionLabel
            // 
            this.newRegionLabel.AutoSize = true;
            this.newRegionLabel.Location = new System.Drawing.Point(6, 36);
            this.newRegionLabel.Name = "newRegionLabel";
            this.newRegionLabel.Size = new System.Drawing.Size(69, 13);
            this.newRegionLabel.TabIndex = 0;
            this.newRegionLabel.Text = "New Region:";
            // 
            // WadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 367);
            this.Controls.Add(this.wadPatchGroupBox);
            this.Controls.Add(this.wadGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "WadForm";
            this.Text = "WiiSoFree Wad Manager";
            this.Load += new System.EventHandler(this.WadFormLoad);
            this.wadGroupBox.ResumeLayout(false);
            this.wadGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.wadPatchGroupBox.ResumeLayout(false);
            this.wadPatchGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox wadFilenameTextBox;
        private System.Windows.Forms.Label wadFilenameLabel;
        private System.Windows.Forms.GroupBox wadGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWadVCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patchWadVCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label makerCodeLabel;
        private System.Windows.Forms.TextBox makerCodeTextBox;
        private System.Windows.Forms.Label originalRegionLabel;
        private System.Windows.Forms.TextBox originalRegionTextBox;
        private System.Windows.Forms.TextBox gamecodeTextBox;
        private System.Windows.Forms.Label gamecodeLabel;
        private System.Windows.Forms.TextBox regionStringTextBox;
        private System.Windows.Forms.Label regionStringLabel;
        private System.Windows.Forms.TextBox regionCodeTextBox;
        private System.Windows.Forms.Label regionCodelabel;
        private System.Windows.Forms.GroupBox wadPatchGroupBox;
        private System.Windows.Forms.ComboBox newRegionStringComboBox;
        private System.Windows.Forms.TextBox newRegionCodeTextBox;
        private System.Windows.Forms.Label newRegionCodeLabel;
        private System.Windows.Forms.Label newRegionLabel;
        private System.Windows.Forms.Button patchWadButton;
        private System.Windows.Forms.TextBox newRegionStringTextBox;
        private System.Windows.Forms.Label newRegionStringLabel;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.TextBox gameNameTextBox;
        private System.Windows.Forms.TextBox platformTextBox;
        private System.Windows.Forms.Label platformLabel;
        private System.Windows.Forms.TextBox makerCodeCoTextBox;
    }
}