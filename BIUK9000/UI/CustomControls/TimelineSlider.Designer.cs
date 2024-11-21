namespace BIUK9000.UI
{
    partial class TimelineSlider
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
            timeLineTrackBar = new MyTrackBar();
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            playButton = new System.Windows.Forms.Button();
            addMarkButton = new System.Windows.Forms.Button();
            frameDelayNUD = new System.Windows.Forms.NumericUpDown();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)frameDelayNUD).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(timeLineTrackBar, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(482, 100);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // timeLineTrackBar
            // 
            timeLineTrackBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            timeLineTrackBar.Location = new System.Drawing.Point(3, 53);
            timeLineTrackBar.Maximum = 9;
            timeLineTrackBar.Name = "timeLineTrackBar";
            timeLineTrackBar.Size = new System.Drawing.Size(476, 44);
            timeLineTrackBar.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(playButton);
            panel1.Controls.Add(addMarkButton);
            panel1.Controls.Add(frameDelayNUD);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(476, 44);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(108, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "delay:";
            // 
            // playButton
            // 
            playButton.Location = new System.Drawing.Point(3, 3);
            playButton.Name = "playButton";
            playButton.Size = new System.Drawing.Size(48, 38);
            playButton.TabIndex = 1;
            playButton.Text = "play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // addMarkButton
            // 
            addMarkButton.Location = new System.Drawing.Point(57, 3);
            addMarkButton.Name = "addMarkButton";
            addMarkButton.Size = new System.Drawing.Size(45, 38);
            addMarkButton.TabIndex = 0;
            addMarkButton.Text = "mark";
            addMarkButton.UseVisualStyleBackColor = true;
            // 
            // frameDelayNUD
            // 
            frameDelayNUD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            frameDelayNUD.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            frameDelayNUD.Location = new System.Drawing.Point(152, 11);
            frameDelayNUD.Maximum = new decimal(new int[] { 50000, 0, 0, 0 });
            frameDelayNUD.Name = "frameDelayNUD";
            frameDelayNUD.Size = new System.Drawing.Size(82, 25);
            frameDelayNUD.TabIndex = 0;
            frameDelayNUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // TimelineSlider
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "TimelineSlider";
            Size = new System.Drawing.Size(482, 100);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)timeLineTrackBar).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)frameDelayNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MyTrackBar timeLineTrackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button addMarkButton;
        private System.Windows.Forms.NumericUpDown frameDelayNUD;
    }
}
