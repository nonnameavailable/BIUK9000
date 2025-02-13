namespace BIUK9000.UI.LayerParamControls
{
    partial class ShapeGFLParamControl
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
            colorButton = new ColorButton();
            shapesGB = new System.Windows.Forms.GroupBox();
            rectangleRB = new System.Windows.Forms.RadioButton();
            ellipseRB = new System.Windows.Forms.RadioButton();
            tableLayoutPanel1.SuspendLayout();
            shapesGB.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(colorButton, 0, 0);
            tableLayoutPanel1.Controls.Add(shapesGB, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(384, 124);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // colorButton
            // 
            colorButton.Color = System.Drawing.Color.Black;
            colorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            colorButton.Location = new System.Drawing.Point(3, 3);
            colorButton.Name = "colorButton";
            colorButton.Size = new System.Drawing.Size(186, 118);
            colorButton.TabIndex = 1;
            // 
            // shapesGB
            // 
            shapesGB.Controls.Add(ellipseRB);
            shapesGB.Controls.Add(rectangleRB);
            shapesGB.Dock = System.Windows.Forms.DockStyle.Fill;
            shapesGB.Location = new System.Drawing.Point(195, 3);
            shapesGB.Name = "shapesGB";
            shapesGB.Size = new System.Drawing.Size(186, 118);
            shapesGB.TabIndex = 2;
            shapesGB.TabStop = false;
            shapesGB.Text = "shapes";
            // 
            // rectangleRB
            // 
            rectangleRB.AutoSize = true;
            rectangleRB.Location = new System.Drawing.Point(6, 22);
            rectangleRB.Name = "rectangleRB";
            rectangleRB.Size = new System.Drawing.Size(74, 19);
            rectangleRB.TabIndex = 0;
            rectangleRB.TabStop = true;
            rectangleRB.Text = "rectangle";
            rectangleRB.UseVisualStyleBackColor = true;
            // 
            // ellipseRB
            // 
            ellipseRB.AutoSize = true;
            ellipseRB.Location = new System.Drawing.Point(6, 47);
            ellipseRB.Name = "ellipseRB";
            ellipseRB.Size = new System.Drawing.Size(58, 19);
            ellipseRB.TabIndex = 1;
            ellipseRB.TabStop = true;
            ellipseRB.Text = "ellipse";
            ellipseRB.UseVisualStyleBackColor = true;
            // 
            // PlainGFLParamControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "PlainGFLParamControl";
            Size = new System.Drawing.Size(384, 124);
            tableLayoutPanel1.ResumeLayout(false);
            shapesGB.ResumeLayout(false);
            shapesGB.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ColorButton colorButton;
        private System.Windows.Forms.GroupBox shapesGB;
        private System.Windows.Forms.RadioButton ellipseRB;
        private System.Windows.Forms.RadioButton rectangleRB;
    }
}
