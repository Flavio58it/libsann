namespace libsannNETWorkbenchToolkit.Configuration
{
    partial class RPropUi
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
            if(disposing && (components != null))
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxUpdateValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.minUpdateValueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.growthFactorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.decreaseFactorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxUpdateValueNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minUpdateValueNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.growthFactorNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decreaseFactorNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.maxUpdateValueNumericUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.minUpdateValueNumericUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.growthFactorNumericUpDown, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.decreaseFactorNumericUpDown, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(351, 167);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Min update value:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max update value:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // maxUpdateValueNumericUpDown
            // 
            this.maxUpdateValueNumericUpDown.DecimalPlaces = 1;
            this.maxUpdateValueNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxUpdateValueNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.maxUpdateValueNumericUpDown.Location = new System.Drawing.Point(178, 3);
            this.maxUpdateValueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.maxUpdateValueNumericUpDown.Name = "maxUpdateValueNumericUpDown";
            this.maxUpdateValueNumericUpDown.Size = new System.Drawing.Size(170, 20);
            this.maxUpdateValueNumericUpDown.TabIndex = 1;
            this.maxUpdateValueNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Growth factor:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Decrease factor:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // minUpdateValueNumericUpDown
            // 
            this.minUpdateValueNumericUpDown.DecimalPlaces = 3;
            this.minUpdateValueNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minUpdateValueNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.minUpdateValueNumericUpDown.Location = new System.Drawing.Point(178, 33);
            this.minUpdateValueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.minUpdateValueNumericUpDown.Name = "minUpdateValueNumericUpDown";
            this.minUpdateValueNumericUpDown.Size = new System.Drawing.Size(170, 20);
            this.minUpdateValueNumericUpDown.TabIndex = 5;
            this.minUpdateValueNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // growthFactorNumericUpDown
            // 
            this.growthFactorNumericUpDown.DecimalPlaces = 2;
            this.growthFactorNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.growthFactorNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.growthFactorNumericUpDown.Location = new System.Drawing.Point(178, 63);
            this.growthFactorNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.growthFactorNumericUpDown.Minimum = new decimal(new int[] {
            101,
            0,
            0,
            131072});
            this.growthFactorNumericUpDown.Name = "growthFactorNumericUpDown";
            this.growthFactorNumericUpDown.Size = new System.Drawing.Size(170, 20);
            this.growthFactorNumericUpDown.TabIndex = 6;
            this.growthFactorNumericUpDown.Value = new decimal(new int[] {
            101,
            0,
            0,
            131072});
            // 
            // decreaseFactorNumericUpDown
            // 
            this.decreaseFactorNumericUpDown.DecimalPlaces = 2;
            this.decreaseFactorNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.decreaseFactorNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.decreaseFactorNumericUpDown.Location = new System.Drawing.Point(178, 93);
            this.decreaseFactorNumericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.decreaseFactorNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.decreaseFactorNumericUpDown.Name = "decreaseFactorNumericUpDown";
            this.decreaseFactorNumericUpDown.Size = new System.Drawing.Size(170, 20);
            this.decreaseFactorNumericUpDown.TabIndex = 7;
            this.decreaseFactorNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.save_diskette_floppy_disk;
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(178, 123);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(170, 39);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveClickHandler);
            // 
            // RPropUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 167);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(367, 205);
            this.MinimumSize = new System.Drawing.Size(367, 205);
            this.Name = "RPropUi";
            this.Text = "RPropUi";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxUpdateValueNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minUpdateValueNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.growthFactorNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decreaseFactorNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxUpdateValueNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown minUpdateValueNumericUpDown;
        private System.Windows.Forms.NumericUpDown growthFactorNumericUpDown;
        private System.Windows.Forms.NumericUpDown decreaseFactorNumericUpDown;
        private System.Windows.Forms.Button saveButton;
    }
}