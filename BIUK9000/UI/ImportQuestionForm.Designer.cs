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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            insertButton = new System.Windows.Forms.Button();
            asLayersButton = new System.Windows.Forms.Button();
            freshButton = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(insertButton, 2, 1);
            tableLayoutPanel1.Controls.Add(asLayersButton, 1, 1);
            tableLayoutPanel1.Controls.Add(freshButton, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(348, 214);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 3);
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(342, 100);
            label1.TabIndex = 0;
            label1.Text = "Looks like there is already something here. What do you want to do?";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // insertButton
            // 
            insertButton.Dock = System.Windows.Forms.DockStyle.Fill;
            insertButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            insertButton.Location = new System.Drawing.Point(235, 103);
            insertButton.Name = "insertButton";
            insertButton.Size = new System.Drawing.Size(110, 108);
            insertButton.TabIndex = 1;
            insertButton.Text = "Insert after current frame";
            insertButton.UseVisualStyleBackColor = true;
            // 
            // asLayersButton
            // 
            asLayersButton.Dock = System.Windows.Forms.DockStyle.Fill;
            asLayersButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            asLayersButton.Location = new System.Drawing.Point(119, 103);
            asLayersButton.Name = "asLayersButton";
            asLayersButton.Size = new System.Drawing.Size(110, 108);
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
            freshButton.Size = new System.Drawing.Size(110, 108);
            freshButton.TabIndex = 3;
            freshButton.Text = "Start fresh";
            freshButton.UseVisualStyleBackColor = true;
            // 
            // ImportQuestionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(348, 214);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportQuestionForm";
            Text = "ImportQuestionForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button insertButton;
        private System.Windows.Forms.Button asLayersButton;
        private System.Windows.Forms.Button freshButton;
    }
}