namespace libsannNETWorkbenchToolkit
{
    partial class MainConsole
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPageErrorProgress = new System.Windows.Forms.TabPage();
            this.mse_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageWeights = new System.Windows.Forms.TabPage();
            this.weights_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.output_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateExampleLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSetFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startWorkBenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tabPageErrorProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mse_chart)).BeginInit();
            this.tabPageWeights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weights_chart)).BeginInit();
            this.tabPageOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.output_chart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tabCtrl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 392);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabPageErrorProgress);
            this.tabCtrl.Controls.Add(this.tabPageWeights);
            this.tabCtrl.Controls.Add(this.tabPageOutput);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(3, 3);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(636, 346);
            this.tabCtrl.TabIndex = 0;
            // 
            // tabPageErrorProgress
            // 
            this.tabPageErrorProgress.Controls.Add(this.mse_chart);
            this.tabPageErrorProgress.Location = new System.Drawing.Point(4, 22);
            this.tabPageErrorProgress.Name = "tabPageErrorProgress";
            this.tabPageErrorProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrorProgress.Size = new System.Drawing.Size(628, 320);
            this.tabPageErrorProgress.TabIndex = 0;
            this.tabPageErrorProgress.Text = "Mse";
            this.tabPageErrorProgress.UseVisualStyleBackColor = true;
            // 
            // mse_chart
            // 
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea1.AxisX.LineColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            chartArea1.Name = "ChartArea1";
            this.mse_chart.ChartAreas.Add(chartArea1);
            this.mse_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.mse_chart.Legends.Add(legend1);
            this.mse_chart.Location = new System.Drawing.Point(3, 3);
            this.mse_chart.Name = "mse_chart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series1.Legend = "Legend1";
            series1.Name = "mse";
            this.mse_chart.Series.Add(series1);
            this.mse_chart.Size = new System.Drawing.Size(622, 314);
            this.mse_chart.TabIndex = 0;
            this.mse_chart.Text = "chart1";
            // 
            // tabPageWeights
            // 
            this.tabPageWeights.Controls.Add(this.weights_chart);
            this.tabPageWeights.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeights.Name = "tabPageWeights";
            this.tabPageWeights.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeights.Size = new System.Drawing.Size(628, 320);
            this.tabPageWeights.TabIndex = 1;
            this.tabPageWeights.Text = "Weights";
            this.tabPageWeights.UseVisualStyleBackColor = true;
            // 
            // weights_chart
            // 
            chartArea2.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea2.AxisX.LineColor = System.Drawing.Color.Blue;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            chartArea2.Name = "ChartArea1";
            this.weights_chart.ChartAreas.Add(chartArea2);
            this.weights_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.weights_chart.Legends.Add(legend2);
            this.weights_chart.Location = new System.Drawing.Point(3, 3);
            this.weights_chart.Name = "weights_chart";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Blue;
            series2.Legend = "Legend1";
            series2.Name = "weights";
            this.weights_chart.Series.Add(series2);
            this.weights_chart.Size = new System.Drawing.Size(622, 314);
            this.weights_chart.TabIndex = 1;
            this.weights_chart.Text = "chart1";
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.output_chart);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(628, 320);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // output_chart
            // 
            chartArea3.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea3.AxisX.LineColor = System.Drawing.Color.Blue;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            chartArea3.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            chartArea3.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            chartArea3.Name = "ChartArea1";
            this.output_chart.ChartAreas.Add(chartArea3);
            this.output_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.output_chart.Legends.Add(legend3);
            this.output_chart.Location = new System.Drawing.Point(3, 3);
            this.output_chart.Name = "output_chart";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Color = System.Drawing.Color.Green;
            series3.Legend = "Legend1";
            series3.Name = "Output";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "Target";
            this.output_chart.Series.Add(series3);
            this.output_chart.Series.Add(series4);
            this.output_chart.Size = new System.Drawing.Size(622, 314);
            this.output_chart.TabIndex = 1;
            this.output_chart.Text = "chart1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.startWorkBenchToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(642, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trainingSetToolStripMenuItem,
            this.configurationToolStripMenuItem});
            this.setupToolStripMenuItem.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.options;
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // trainingSetToolStripMenuItem
            // 
            this.trainingSetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setToolStripMenuItem,
            this.generateExampleLayoutToolStripMenuItem,
            this.loadSetFromFileToolStripMenuItem});
            this.trainingSetToolStripMenuItem.Name = "trainingSetToolStripMenuItem";
            this.trainingSetToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.trainingSetToolStripMenuItem.Text = "DataSet";
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.setToolStripMenuItem.Text = "Data Set";
            this.setToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // generateExampleLayoutToolStripMenuItem
            // 
            this.generateExampleLayoutToolStripMenuItem.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.document_file;
            this.generateExampleLayoutToolStripMenuItem.Name = "generateExampleLayoutToolStripMenuItem";
            this.generateExampleLayoutToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.generateExampleLayoutToolStripMenuItem.Text = "Generate example layout";
            this.generateExampleLayoutToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // loadSetFromFileToolStripMenuItem
            // 
            this.loadSetFromFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVToolStripMenuItem});
            this.loadSetFromFileToolStripMenuItem.Name = "loadSetFromFileToolStripMenuItem";
            this.loadSetFromFileToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadSetFromFileToolStripMenuItem.Text = "Load Set From File";
            // 
            // cSVToolStripMenuItem
            // 
            this.cSVToolStripMenuItem.Name = "cSVToolStripMenuItem";
            this.cSVToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.cSVToolStripMenuItem.Text = "CSV";
            this.cSVToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // startWorkBenchToolStripMenuItem
            // 
            this.startWorkBenchToolStripMenuItem.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.play;
            this.startWorkBenchToolStripMenuItem.Name = "startWorkBenchToolStripMenuItem";
            this.startWorkBenchToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.startWorkBenchToolStripMenuItem.Text = "Start";
            this.startWorkBenchToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItemClickHandler);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Image = global::libsannNETWorkbenchToolkit.Properties.Resources.paste;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // MainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainConsole";
            this.Text = "                                        ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tabPageErrorProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mse_chart)).EndInit();
            this.tabPageWeights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weights_chart)).EndInit();
            this.tabPageOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.output_chart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPageErrorProgress;
        private System.Windows.Forms.TabPage tabPageWeights;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingSetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startWorkBenchToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataVisualization.Charting.Chart mse_chart;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.ToolStripMenuItem generateExampleLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSetFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart weights_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart output_chart;
    }
}

