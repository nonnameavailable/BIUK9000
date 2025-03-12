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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            insertButton = new System.Windows.Forms.Button();
            asLayersButton = new System.Windows.Forms.Button();
            freshButton = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            layerRepeatRB = new System.Windows.Forms.RadioButton();
            layerSpreadRB = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            insertHereRB = new System.Windows.Forms.RadioButton();
            insertEndRB = new System.Windows.Forms.RadioButton();
            insertStartRB = new System.Windows.Forms.RadioButton();
            groupBox3 = new System.Windows.Forms.GroupBox();
            freshAsFramesRB = new System.Windows.Forms.RadioButton();
            toolTip = new System.Windows.Forms.ToolTip(components);
            replaceButton = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0626545F));
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
            tableLayoutPanel1.Controls.Add(groupBox3, 0, 2);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(324, 302);
            tableLayoutPanel1.TabIndex = 0;
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
            freshButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            tableLayoutPanel1.SetColumnSpan(groupBox1, 2);
            groupBox1.Controls.Add(layerRepeatRB);
            groupBox1.Controls.Add(layerSpreadRB);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(84, 203);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(155, 96);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "settings";
            // 
            // layerRepeatRB
            // 
            layerRepeatRB.AutoSize = true;
            layerRepeatRB.Location = new System.Drawing.Point(6, 47);
            layerRepeatRB.Name = "layerRepeatRB";
            layerRepeatRB.Size = new System.Drawing.Size(58, 19);
            layerRepeatRB.TabIndex = 1;
            layerRepeatRB.Text = "repeat";
            toolTip.SetToolTip(layerRepeatRB, "Frames are assigned layers 1:1,\r\nIf original gif has more frames than new, the new frames are repeated until the end");
            layerRepeatRB.UseVisualStyleBackColor = true;
            // 
            // layerSpreadRB
            // 
            layerSpreadRB.AutoSize = true;
            layerSpreadRB.Checked = true;
            layerSpreadRB.Location = new System.Drawing.Point(6, 22);
            layerSpreadRB.Name = "layerSpreadRB";
            layerSpreadRB.Size = new System.Drawing.Size(60, 19);
            layerSpreadRB.TabIndex = 0;
            layerSpreadRB.TabStop = true;
            layerSpreadRB.Text = "spread";
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
            groupBox2.Size = new System.Drawing.Size(76, 96);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "settings";
            // 
            // insertHereRB
            // 
            insertHereRB.AutoSize = true;
            insertHereRB.Location = new System.Drawing.Point(6, 72);
            insertHereRB.Name = "insertHereRB";
            insertHereRB.Size = new System.Drawing.Size(48, 19);
            insertHereRB.TabIndex = 2;
            insertHereRB.Text = "here";
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
            insertEndRB.Text = "end";
            toolTip.SetToolTip(insertEndRB, "Insert at the end");
            insertEndRB.UseVisualStyleBackColor = true;
            // 
            // insertStartRB
            // 
            insertStartRB.AutoSize = true;
            insertStartRB.Checked = true;
            insertStartRB.Location = new System.Drawing.Point(6, 22);
            insertStartRB.Name = "insertStartRB";
            insertStartRB.Size = new System.Drawing.Size(48, 19);
            insertStartRB.TabIndex = 0;
            insertStartRB.TabStop = true;
            insertStartRB.Text = "start";
            toolTip.SetToolTip(insertStartRB, "Insert at the start\r\n");
            insertStartRB.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(freshAsFramesRB);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox3.Location = new System.Drawing.Point(3, 203);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(75, 96);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "settings";
            // 
            // freshAsFramesRB
            // 
            freshAsFramesRB.AutoSize = true;
            freshAsFramesRB.Checked = true;
            freshAsFramesRB.Location = new System.Drawing.Point(6, 22);
            freshAsFramesRB.Name = "freshAsFramesRB";
            freshAsFramesRB.Size = new System.Drawing.Size(75, 19);
            freshAsFramesRB.TabIndex = 1;
            freshAsFramesRB.TabStop = true;
            freshAsFramesRB.Text = "as frames";
            freshAsFramesRB.UseVisualStyleBackColor = true;
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
            replaceButton.UseVisualStyleBackColor = true;
            // 
            // ImportQuestionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(324, 302);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportQuestionForm";
            Text = "ImportQuestionForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton freshAsFramesRB;
        private System.Windows.Forms.Button replaceButton;
    }
}