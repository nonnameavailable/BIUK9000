namespace BIUK9000.UI.LayerParamControls
{
    partial class TextGFLParamControl
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
            textLayerParamsGB = new System.Windows.Forms.GroupBox();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            fontColorButton = new ColorButton();
            borderColorButton = new ColorButton();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            TextLayerBorderWidthNUD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            textLayerFontCBB = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            textLayerTextTB = new System.Windows.Forms.TextBox();
            textLayerParamsGB.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TextLayerBorderWidthNUD).BeginInit();
            SuspendLayout();
            // 
            // textLayerParamsGB
            // 
            textLayerParamsGB.Controls.Add(tableLayoutPanel2);
            textLayerParamsGB.Dock = System.Windows.Forms.DockStyle.Fill;
            textLayerParamsGB.Location = new System.Drawing.Point(0, 0);
            textLayerParamsGB.Name = "textLayerParamsGB";
            textLayerParamsGB.Size = new System.Drawing.Size(514, 150);
            textLayerParamsGB.TabIndex = 8;
            textLayerParamsGB.TabStop = false;
            textLayerParamsGB.Text = "text layer params";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(panel1, 0, 0);
            tableLayoutPanel2.Controls.Add(textLayerTextTB, 1, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(508, 128);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(fontColorButton);
            panel1.Controls.Add(borderColorButton);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(TextLayerBorderWidthNUD);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textLayerFontCBB);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(194, 122);
            panel1.TabIndex = 0;
            // 
            // fontColorButton
            // 
            fontColorButton.Color = System.Drawing.Color.White;
            fontColorButton.Location = new System.Drawing.Point(19, 83);
            fontColorButton.Name = "fontColorButton";
            fontColorButton.Size = new System.Drawing.Size(35, 35);
            fontColorButton.TabIndex = 7;
            // 
            // borderColorButton
            // 
            borderColorButton.Color = System.Drawing.Color.Black;
            borderColorButton.Location = new System.Drawing.Point(116, 84);
            borderColorButton.Name = "borderColorButton";
            borderColorButton.Size = new System.Drawing.Size(35, 35);
            borderColorButton.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label4.Location = new System.Drawing.Point(98, 64);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(83, 17);
            label4.TabIndex = 5;
            label4.Text = "border color";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label3.Location = new System.Drawing.Point(5, 64);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(65, 17);
            label3.TabIndex = 4;
            label3.Text = "font color";
            // 
            // TextLayerBorderWidthNUD
            // 
            TextLayerBorderWidthNUD.Location = new System.Drawing.Point(98, 34);
            TextLayerBorderWidthNUD.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            TextLayerBorderWidthNUD.Name = "TextLayerBorderWidthNUD";
            TextLayerBorderWidthNUD.Size = new System.Drawing.Size(82, 23);
            TextLayerBorderWidthNUD.TabIndex = 3;
            TextLayerBorderWidthNUD.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label2.Location = new System.Drawing.Point(5, 34);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 17);
            label2.TabIndex = 2;
            label2.Text = "border width";
            // 
            // textLayerFontCBB
            // 
            textLayerFontCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            textLayerFontCBB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            textLayerFontCBB.FormattingEnabled = true;
            textLayerFontCBB.Location = new System.Drawing.Point(47, 3);
            textLayerFontCBB.Name = "textLayerFontCBB";
            textLayerFontCBB.Size = new System.Drawing.Size(121, 25);
            textLayerFontCBB.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(3, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 17);
            label1.TabIndex = 0;
            label1.Text = "font";
            // 
            // textLayerTextTB
            // 
            textLayerTextTB.Dock = System.Windows.Forms.DockStyle.Fill;
            textLayerTextTB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            textLayerTextTB.Location = new System.Drawing.Point(203, 3);
            textLayerTextTB.Multiline = true;
            textLayerTextTB.Name = "textLayerTextTB";
            textLayerTextTB.Size = new System.Drawing.Size(302, 122);
            textLayerTextTB.TabIndex = 1;
            // 
            // TextGFLParamControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(textLayerParamsGB);
            Name = "TextGFLParamControl";
            Size = new System.Drawing.Size(514, 150);
            textLayerParamsGB.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TextLayerBorderWidthNUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox textLayerParamsGB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private ColorButton fontColorButton;
        private ColorButton borderColorButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown TextLayerBorderWidthNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox textLayerFontCBB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textLayerTextTB;
    }
}
