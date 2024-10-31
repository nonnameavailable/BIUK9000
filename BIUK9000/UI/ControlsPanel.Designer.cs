namespace BIUK9000.UI
{
    partial class ControlsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            SaveButton = new System.Windows.Forms.Button();
            exportTC = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ImageExportJpegQualNUD = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            ImageExportFormatCBB = new System.Windows.Forms.ComboBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            GifExportLossyNUD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            GifExportColorsNUD = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            settingsTC = new System.Windows.Forms.TabPage();
            drawHelpCB = new System.Windows.Forms.CheckBox();
            rotationSnapCB = new System.Windows.Forms.CheckBox();
            saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            positionSnapCB = new System.Windows.Forms.CheckBox();
            tableLayoutPanel1.SuspendLayout();
            exportTC.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).BeginInit();
            settingsTC.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(SaveButton, 0, 0);
            tableLayoutPanel1.Controls.Add(exportTC, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(150, 434);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // SaveButton
            // 
            SaveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            SaveButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            SaveButton.Location = new System.Drawing.Point(3, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new System.Drawing.Size(144, 137);
            SaveButton.TabIndex = 8;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // exportTC
            // 
            exportTC.Controls.Add(tabPage1);
            exportTC.Controls.Add(settingsTC);
            exportTC.Dock = System.Windows.Forms.DockStyle.Fill;
            exportTC.Location = new System.Drawing.Point(3, 146);
            exportTC.Name = "exportTC";
            exportTC.SelectedIndex = 0;
            exportTC.Size = new System.Drawing.Size(144, 285);
            exportTC.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.AutoScroll = true;
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(136, 257);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "export";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ImageExportJpegQualNUD);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ImageExportFormatCBB);
            groupBox2.Location = new System.Drawing.Point(6, 90);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(124, 141);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Single image";
            // 
            // ImageExportJpegQualNUD
            // 
            ImageExportJpegQualNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            ImageExportJpegQualNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            ImageExportJpegQualNUD.Location = new System.Drawing.Point(71, 51);
            ImageExportJpegQualNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ImageExportJpegQualNUD.Name = "ImageExportJpegQualNUD";
            ImageExportJpegQualNUD.Size = new System.Drawing.Size(47, 25);
            ImageExportJpegQualNUD.TabIndex = 5;
            ImageExportJpegQualNUD.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label3.Location = new System.Drawing.Point(6, 57);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 17);
            label3.TabIndex = 4;
            label3.Text = "jpg qual.";
            // 
            // ImageExportFormatCBB
            // 
            ImageExportFormatCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ImageExportFormatCBB.FormattingEnabled = true;
            ImageExportFormatCBB.IntegralHeight = false;
            ImageExportFormatCBB.Items.AddRange(new object[] { ".jpeg", ".png" });
            ImageExportFormatCBB.Location = new System.Drawing.Point(6, 22);
            ImageExportFormatCBB.Name = "ImageExportFormatCBB";
            ImageExportFormatCBB.Size = new System.Drawing.Size(112, 23);
            ImageExportFormatCBB.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(GifExportLossyNUD);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(GifExportColorsNUD);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(124, 78);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "gif";
            // 
            // GifExportLossyNUD
            // 
            GifExportLossyNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            GifExportLossyNUD.Location = new System.Drawing.Point(55, 44);
            GifExportLossyNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            GifExportLossyNUD.Name = "GifExportLossyNUD";
            GifExportLossyNUD.Size = new System.Drawing.Size(63, 25);
            GifExportLossyNUD.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label2.Location = new System.Drawing.Point(6, 50);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(37, 17);
            label2.TabIndex = 2;
            label2.Text = "lossy";
            // 
            // GifExportColorsNUD
            // 
            GifExportColorsNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            GifExportColorsNUD.Location = new System.Drawing.Point(55, 13);
            GifExportColorsNUD.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            GifExportColorsNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GifExportColorsNUD.Name = "GifExportColorsNUD";
            GifExportColorsNUD.Size = new System.Drawing.Size(63, 25);
            GifExportColorsNUD.TabIndex = 1;
            GifExportColorsNUD.Value = new decimal(new int[] { 256, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(6, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 17);
            label1.TabIndex = 0;
            label1.Text = "colors";
            // 
            // settingsTC
            // 
            settingsTC.Controls.Add(positionSnapCB);
            settingsTC.Controls.Add(drawHelpCB);
            settingsTC.Controls.Add(rotationSnapCB);
            settingsTC.Location = new System.Drawing.Point(4, 24);
            settingsTC.Name = "settingsTC";
            settingsTC.Padding = new System.Windows.Forms.Padding(3);
            settingsTC.Size = new System.Drawing.Size(136, 257);
            settingsTC.TabIndex = 1;
            settingsTC.Text = "settings";
            settingsTC.UseVisualStyleBackColor = true;
            // 
            // drawHelpCB
            // 
            drawHelpCB.AutoSize = true;
            drawHelpCB.Checked = true;
            drawHelpCB.CheckState = System.Windows.Forms.CheckState.Checked;
            drawHelpCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            drawHelpCB.Location = new System.Drawing.Point(6, 60);
            drawHelpCB.Name = "drawHelpCB";
            drawHelpCB.Size = new System.Drawing.Size(85, 21);
            drawHelpCB.TabIndex = 1;
            drawHelpCB.Text = "draw help";
            drawHelpCB.UseVisualStyleBackColor = true;
            // 
            // rotationSnapCB
            // 
            rotationSnapCB.AutoSize = true;
            rotationSnapCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            rotationSnapCB.Location = new System.Drawing.Point(6, 6);
            rotationSnapCB.Name = "rotationSnapCB";
            rotationSnapCB.Size = new System.Drawing.Size(105, 21);
            rotationSnapCB.TabIndex = 0;
            rotationSnapCB.Text = "rotation snap";
            rotationSnapCB.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "GIF files|*.gif";
            // 
            // positionSnapCB
            // 
            positionSnapCB.AutoSize = true;
            positionSnapCB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            positionSnapCB.Location = new System.Drawing.Point(6, 33);
            positionSnapCB.Name = "positionSnapCB";
            positionSnapCB.Size = new System.Drawing.Size(106, 21);
            positionSnapCB.TabIndex = 2;
            positionSnapCB.Text = "position snap";
            positionSnapCB.UseVisualStyleBackColor = true;
            // 
            // ControlsPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ControlsPanel";
            Size = new System.Drawing.Size(150, 434);
            tableLayoutPanel1.ResumeLayout(false);
            exportTC.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ImageExportJpegQualNUD).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)GifExportLossyNUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)GifExportColorsNUD).EndInit();
            settingsTC.ResumeLayout(false);
            settingsTC.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl exportTC;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ImageExportFormatCBB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown GifExportLossyNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown GifExportColorsNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage settingsTC;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.NumericUpDown ImageExportJpegQualNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox rotationSnapCB;
        private System.Windows.Forms.CheckBox drawHelpCB;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.CheckBox positionSnapCB;
    }
}
