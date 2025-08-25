namespace BIUK9000.UI
{
    partial class ImportQuestionForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportQuestionForm));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            replaceButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            insertButton = new System.Windows.Forms.Button();
            asLayersButton = new System.Windows.Forms.Button();
            freshButton = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            spreadCountNUD = new System.Windows.Forms.NumericUpDown();
            layerRepeatRB = new System.Windows.Forms.RadioButton();
            layerSpreadRB = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            insertHereRB = new System.Windows.Forms.RadioButton();
            insertEndRB = new System.Windows.Forms.RadioButton();
            insertStartRB = new System.Windows.Forms.RadioButton();
            panel1 = new System.Windows.Forms.Panel();
            newMaxSideLengthNUD = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            originalSizeLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            reduceSizeCB = new System.Windows.Forms.CheckBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spreadCountNUD).BeginInit();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newMaxSideLengthNUD).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.06265F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0626545F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.8120327F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0626583F));
            tableLayoutPanel1.Controls.Add(replaceButton, 2, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(insertButton, 3, 1);
            tableLayoutPanel1.Controls.Add(asLayersButton, 1, 1);
            tableLayoutPanel1.Controls.Add(freshButton, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox1, 1, 2);
            tableLayoutPanel1.Controls.Add(groupBox2, 3, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 3);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(324, 353);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // replaceButton
            // 
            replaceButton.Dock = System.Windows.Forms.DockStyle.Fill;
            replaceButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            replaceButton.Location = new System.Drawing.Point(165, 103);
            replaceButton.Name = "replaceButton";
            replaceButton.Size = new System.Drawing.Size(74, 94);
            replaceButton.TabIndex = 7;
            replaceButton.Text = "Import as replace";
            toolTip.SetToolTip(replaceButton, resources.GetString("replaceButton.ToolTip"));
            replaceButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 4);
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(318, 100);
            label1.TabIndex = 0;
            label1.Text = "What do you want to do?";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // insertButton
            // 
            insertButton.Dock = System.Windows.Forms.DockStyle.Fill;
            insertButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            insertButton.Location = new System.Drawing.Point(245, 103);
            insertButton.Name = "insertButton";
            insertButton.Size = new System.Drawing.Size(76, 94);
            insertButton.TabIndex = 1;
            insertButton.Text = "Insert";
            toolTip.SetToolTip(insertButton, "Dragged images will be inserted as new frames\r\nat the location specified below");
            insertButton.UseVisualStyleBackColor = true;
            // 
            // asLayersButton
            // 
            asLayersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            asLayersButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            asLayersButton.Location = new System.Drawing.Point(84, 103);
            asLayersButton.Name = "asLayersButton";
            asLayersButton.Size = new System.Drawing.Size(75, 94);
            asLayersButton.TabIndex = 2;
            asLayersButton.Text = "Import as layers";
            toolTip.SetToolTip(asLayersButton, "New layer will be created from the dragged image(s)");
            asLayersButton.UseVisualStyleBackColor = true;
            // 
            // freshButton
            // 
            freshButton.Dock = System.Windows.Forms.DockStyle.Fill;
            freshButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            freshButton.Location = new System.Drawing.Point(3, 103);
            freshButton.Name = "freshButton";
            freshButton.Size = new System.Drawing.Size(75, 94);
            freshButton.TabIndex = 3;
            freshButton.Text = "Start fresh";
            toolTip.SetToolTip(freshButton, "This will delete everything and import whatever you dragged as a new gif");
            freshButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            tableLayoutPanel1.SetColumnSpan(groupBox1, 2);
            groupBox1.Controls.Add(spreadCountNUD);
            groupBox1.Controls.Add(layerRepeatRB);
            groupBox1.Controls.Add(layerSpreadRB);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(84, 203);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(155, 94);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "settings";
            // 
            // spreadCountNUD
            // 
            spreadCountNUD.Location = new System.Drawing.Point(72, 18);
            spreadCountNUD.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            spreadCountNUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            spreadCountNUD.Name = "spreadCountNUD";
            spreadCountNUD.Size = new System.Drawing.Size(72, 23);
            spreadCountNUD.TabIndex = 2;
            spreadCountNUD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // layerRepeatRB
            // 
            layerRepeatRB.AutoSize = true;
            layerRepeatRB.Location = new System.Drawing.Point(6, 47);
            layerRepeatRB.Name = "layerRepeatRB";
            layerRepeatRB.Size = new System.Drawing.Size(61, 19);
            layerRepeatRB.TabIndex = 1;
            layerRepeatRB.Text = "Repeat";
            toolTip.SetToolTip(layerRepeatRB, "Frames are assigned layers 1:1,\r\nIf original gif has more frames than new, the new frames are repeated until the end");
            layerRepeatRB.UseVisualStyleBackColor = true;
            // 
            // layerSpreadRB
            // 
            layerSpreadRB.AutoSize = true;
            layerSpreadRB.Checked = true;
            layerSpreadRB.Location = new System.Drawing.Point(6, 22);
            layerSpreadRB.Name = "layerSpreadRB";
            layerSpreadRB.Size = new System.Drawing.Size(61, 19);
            layerSpreadRB.TabIndex = 0;
            layerSpreadRB.TabStop = true;
            layerSpreadRB.Text = "Spread";
            toolTip.SetToolTip(layerSpreadRB, "First frame - first frame as layer\r\nLast frame - last frame as layer");
            layerSpreadRB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(insertHereRB);
            groupBox2.Controls.Add(insertEndRB);
            groupBox2.Controls.Add(insertStartRB);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(245, 203);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(76, 94);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "settings";
            // 
            // insertHereRB
            // 
            insertHereRB.AutoSize = true;
            insertHereRB.Location = new System.Drawing.Point(6, 72);
            insertHereRB.Name = "insertHereRB";
            insertHereRB.Size = new System.Drawing.Size(50, 19);
            insertHereRB.TabIndex = 2;
            insertHereRB.Text = "Here";
            toolTip.SetToolTip(insertHereRB, "Insert after current frame");
            insertHereRB.UseVisualStyleBackColor = true;
            // 
            // insertEndRB
            // 
            insertEndRB.AutoSize = true;
            insertEndRB.Location = new System.Drawing.Point(6, 47);
            insertEndRB.Name = "insertEndRB";
            insertEndRB.Size = new System.Drawing.Size(45, 19);
            insertEndRB.TabIndex = 1;
            insertEndRB.Text = "End";
            toolTip.SetToolTip(insertEndRB, "Insert at the end");
            insertEndRB.UseVisualStyleBackColor = true;
            // 
            // insertStartRB
            // 
            insertStartRB.AutoSize = true;
            insertStartRB.Checked = true;
            insertStartRB.Location = new System.Drawing.Point(6, 22);
            insertStartRB.Name = "insertStartRB";
            insertStartRB.Size = new System.Drawing.Size(49, 19);
            insertStartRB.TabIndex = 0;
            insertStartRB.TabStop = true;
            insertStartRB.Text = "Start";
            toolTip.SetToolTip(insertStartRB, "Insert at the start\r\n");
            insertStartRB.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            tableLayoutPanel1.SetColumnSpan(panel1, 4);
            panel1.Controls.Add(newMaxSideLengthNUD);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(originalSizeLabel);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(reduceSizeCB);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 303);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(318, 47);
            panel1.TabIndex = 8;
            // 
            // newMaxSideLengthNUD
            // 
            newMaxSideLengthNUD.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            newMaxSideLengthNUD.Location = new System.Drawing.Point(153, 21);
            newMaxSideLengthNUD.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            newMaxSideLengthNUD.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            newMaxSideLengthNUD.Name = "newMaxSideLengthNUD";
            newMaxSideLengthNUD.Size = new System.Drawing.Size(74, 23);
            newMaxSideLengthNUD.TabIndex = 4;
            newMaxSideLengthNUD.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(153, 3);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(149, 15);
            label3.TabIndex = 3;
            label3.Text = "New maximum side length";
            // 
            // originalSizeLabel
            // 
            originalSizeLabel.AutoSize = true;
            originalSizeLabel.Location = new System.Drawing.Point(81, 27);
            originalSizeLabel.Name = "originalSizeLabel";
            originalSizeLabel.Size = new System.Drawing.Size(0, 15);
            originalSizeLabel.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 26);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(77, 15);
            label2.TabIndex = 1;
            label2.Text = "Original size: ";
            // 
            // reduceSizeCB
            // 
            reduceSizeCB.AutoSize = true;
            reduceSizeCB.Checked = true;
            reduceSizeCB.CheckState = System.Windows.Forms.CheckState.Checked;
            reduceSizeCB.Location = new System.Drawing.Point(3, 5);
            reduceSizeCB.Name = "reduceSizeCB";
            reduceSizeCB.Size = new System.Drawing.Size(87, 19);
            reduceSizeCB.TabIndex = 0;
            reduceSizeCB.Text = "Reduce size";
            reduceSizeCB.UseVisualStyleBackColor = true;
            // 
            // ImportQuestionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(324, 353);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportQuestionForm";
            Text = "ImportQuestionForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)spreadCountNUD).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)newMaxSideLengthNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button asLayersButton;
        private System.Windows.Forms.Button freshButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton layerRepeatRB;
        private System.Windows.Forms.RadioButton layerSpreadRB;
        private System.Windows.Forms.RadioButton insertHereRB;
        private System.Windows.Forms.RadioButton insertEndRB;
        private System.Windows.Forms.RadioButton insertStartRB;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.NumericUpDown spreadCountNUD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox reduceSizeCB;
        private System.Windows.Forms.NumericUpDown newMaxSideLengthNUD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label originalSizeLabel;
        private System.Windows.Forms.Label label2;
    }
}