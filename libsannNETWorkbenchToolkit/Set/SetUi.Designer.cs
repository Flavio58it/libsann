namespace libsannNETWorkbenchToolkit.Set
{
    partial class SetUi
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputSetDataGridView = new System.Windows.Forms.DataGridView();
            this.outputSetDataGridView = new System.Windows.Forms.DataGridView();
            this.inValidationSetDataGridView = new System.Windows.Forms.DataGridView();
            this.saveInputBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.saveOutputBtn = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.saveInValBtn = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.outValidationSetDataGridView = new System.Windows.Forms.DataGridView();
            this.saveOutValBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSetDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputSetDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inValidationSetDataGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outValidationSetDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(487, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.menuCickHandler);
            // 
            // inputSetDataGridView
            // 
            this.inputSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputSetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputSetDataGridView.Location = new System.Drawing.Point(3, 33);
            this.inputSetDataGridView.Name = "inputSetDataGridView";
            this.inputSetDataGridView.Size = new System.Drawing.Size(467, 357);
            this.inputSetDataGridView.TabIndex = 1;
            this.inputSetDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.setChangedHandler);
            // 
            // outputSetDataGridView
            // 
            this.outputSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputSetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSetDataGridView.Location = new System.Drawing.Point(3, 33);
            this.outputSetDataGridView.Name = "outputSetDataGridView";
            this.outputSetDataGridView.Size = new System.Drawing.Size(715, 324);
            this.outputSetDataGridView.TabIndex = 2;
            this.outputSetDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.setChangedHandler);
            // 
            // inValidationSetDataGridView
            // 
            this.inValidationSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inValidationSetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inValidationSetDataGridView.Location = new System.Drawing.Point(3, 33);
            this.inValidationSetDataGridView.Name = "inValidationSetDataGridView";
            this.inValidationSetDataGridView.Size = new System.Drawing.Size(715, 324);
            this.inValidationSetDataGridView.TabIndex = 3;
            this.inValidationSetDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.setChangedHandler);
            // 
            // saveInputBtn
            // 
            this.saveInputBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveInputBtn.Location = new System.Drawing.Point(395, 3);
            this.saveInputBtn.Name = "saveInputBtn";
            this.saveInputBtn.Size = new System.Drawing.Size(75, 24);
            this.saveInputBtn.TabIndex = 4;
            this.saveInputBtn.Text = "Save";
            this.saveInputBtn.UseVisualStyleBackColor = true;
            this.saveInputBtn.Click += new System.EventHandler(this.buttonClickHandler);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(487, 425);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(479, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.inputSetDataGridView, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.saveInputBtn, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(473, 393);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(727, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.saveOutputBtn, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.outputSetDataGridView, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(721, 360);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // saveOutputBtn
            // 
            this.saveOutputBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveOutputBtn.Location = new System.Drawing.Point(643, 3);
            this.saveOutputBtn.Name = "saveOutputBtn";
            this.saveOutputBtn.Size = new System.Drawing.Size(75, 24);
            this.saveOutputBtn.TabIndex = 4;
            this.saveOutputBtn.Text = "Save";
            this.saveOutputBtn.UseVisualStyleBackColor = true;
            this.saveOutputBtn.Click += new System.EventHandler(this.buttonClickHandler);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(727, 366);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Validation input";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.inValidationSetDataGridView, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.saveInValBtn, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(721, 360);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // saveInValBtn
            // 
            this.saveInValBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveInValBtn.Location = new System.Drawing.Point(643, 3);
            this.saveInValBtn.Name = "saveInValBtn";
            this.saveInValBtn.Size = new System.Drawing.Size(75, 24);
            this.saveInValBtn.TabIndex = 4;
            this.saveInValBtn.Text = "Save";
            this.saveInValBtn.UseVisualStyleBackColor = true;
            this.saveInValBtn.Click += new System.EventHandler(this.buttonClickHandler);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(727, 366);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Validation output";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.outValidationSetDataGridView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.saveOutValBtn, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(721, 360);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // outValidationSetDataGridView
            // 
            this.outValidationSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outValidationSetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outValidationSetDataGridView.Location = new System.Drawing.Point(3, 33);
            this.outValidationSetDataGridView.Name = "outValidationSetDataGridView";
            this.outValidationSetDataGridView.Size = new System.Drawing.Size(715, 324);
            this.outValidationSetDataGridView.TabIndex = 3;
            this.outValidationSetDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.setChangedHandler);
            // 
            // saveOutValBtn
            // 
            this.saveOutValBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveOutValBtn.Location = new System.Drawing.Point(643, 3);
            this.saveOutValBtn.Name = "saveOutValBtn";
            this.saveOutValBtn.Size = new System.Drawing.Size(75, 24);
            this.saveOutValBtn.TabIndex = 4;
            this.saveOutValBtn.Text = "Save";
            this.saveOutValBtn.UseVisualStyleBackColor = true;
            this.saveOutValBtn.Click += new System.EventHandler(this.buttonClickHandler);
            // 
            // SetUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 449);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SetUi";
            this.Text = "Set viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClosingHandler);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSetDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputSetDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inValidationSetDataGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outValidationSetDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.DataGridView inputSetDataGridView;
        private System.Windows.Forms.DataGridView outputSetDataGridView;
        private System.Windows.Forms.DataGridView inValidationSetDataGridView;
        private System.Windows.Forms.Button saveInputBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button saveOutputBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button saveInValBtn;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView outValidationSetDataGridView;
        private System.Windows.Forms.Button saveOutValBtn;
    }
}