namespace libsannNETWorkbenchToolkit.Configuration
{
    partial class BackPropUi
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
            this.lRateLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.momentumNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.learningRateNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.momentumNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lRateLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.momentumNumericUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.saveButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.learningRateNumericUpDown, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(292, 105);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lRateLabel
            // 
            this.lRateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lRateLabel.AutoSize = true;
            this.lRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lRateLabel.Location = new System.Drawing.Point(3, 8);
            this.lRateLabel.Name = "lRateLabel";
            this.lRateLabel.Size = new System.Drawing.Size(86, 13);
            this.lRateLabel.TabIndex = 0;
            this.lRateLabel.Text = "Learning rate:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Momentum K:";
            // 
            // momentumNumericUpDown
            // 
            this.momentumNumericUpDown.DecimalPlaces = 2;
            this.momentumNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.momentumNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.momentumNumericUpDown.Location = new System.Drawing.Point(149, 33);
            this.momentumNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.momentumNumericUpDown.Name = "momentumNumericUpDown";
            this.momentumNumericUpDown.Size = new System.Drawing.Size(140, 20);
            this.momentumNumericUpDown.TabIndex = 3;
            // 
            // learningRateNumericUpDown
            // 
            this.learningRateNumericUpDown.DecimalPlaces = 2;
            this.learningRateNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.learningRateNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.learningRateNumericUpDown.Location = new System.Drawing.Point(149, 3);
            this.learningRateNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.learningRateNumericUpDown.Name = "learningRateNumericUpDown";
            this.learningRateNumericUpDown.Size = new System.Drawing.Size(140, 20);
            this.learningRateNumericUpDown.TabIndex = 5;
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.save_diskette_floppy_disk;
            this.saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveButton.Location = new System.Drawing.Point(149, 63);
            this.saveButton.MaximumSize = new System.Drawing.Size(140, 39);
            this.saveButton.MinimumSize = new System.Drawing.Size(140, 39);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(140, 39);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveClickHandler);
            // 
            // BackPropUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 105);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BackPropUi";
            this.Text = "BackPropUi";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.momentumNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.learningRateNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lRateLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown momentumNumericUpDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.NumericUpDown learningRateNumericUpDown;
    }
}