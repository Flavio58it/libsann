﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ninject;
using log4net;

namespace libsannNETWorkbenchToolkit.Set
{
    using libsannNETWorkbenchToolkit.Utils;
    using libsannNETWorkbenchToolkit.ExceptionHandling;

    public partial class SetUi : FormLoggable
    {
        protected SetModel set;

        public SetUi()
        {
            InitializeComponent();
            Setup();
        }

        protected void Setup()
        {
            set = NinjectBinding.GetKernel.Get<SetModel>();

            if(set.inMatrix != null && set.outMatrix != null && set.inValMatrix != null && set.outValMatrix != null &&
                set.inMatrix.Any() && set.outMatrix.Any() && set.inValMatrix.Any() && set.outValMatrix.Any())
            {
                inputSetDataGridView.DataSource = BuildDataTable(set.inMatrix);
                outputSetDataGridView.DataSource = BuildDataTable(set.outMatrix);
                inValidationSetDataGridView.DataSource = BuildDataTable(set.inValMatrix);
                outValidationSetDataGridView.DataSource = BuildDataTable(set.outValMatrix);
            }

            saveInputBtn.Enabled = false;
            saveOutputBtn.Enabled = false;
            saveInValBtn.Enabled = false;
            saveOutValBtn.Enabled = false;
        }

        #region [ GUI Handler ]

        private void ClosingHandler(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void buttonClickHandler(object sender, EventArgs e)
        {
            if(string.Equals((sender as Button).Name, "saveInputBtn"))
            {
                if(Save(inputSetDataGridView))
                    saveInputBtn.Enabled = false;
            }
            if(string.Equals((sender as Button).Name, "saveOutputBtn"))
            {
                if(Save(outputSetDataGridView))
                    saveOutputBtn.Enabled = false;
            }
            if(string.Equals((sender as Button).Name, "saveInValBtn"))
            {
                if(Save(inValidationSetDataGridView))
                    saveInValBtn.Enabled = false;
            }
            if(string.Equals((sender as Button).Name, "saveOutValBtn"))
            {
                if(Save(outValidationSetDataGridView))
                    saveOutValBtn.Enabled = false;
            }
        }

        private void setChangedHandler(object sender, DataGridViewCellEventArgs e)
        {
            if(string.Equals((sender as DataGridView).Name, "inputSetDataGridView"))
                saveInputBtn.Enabled = true;
            if(string.Equals((sender as DataGridView).Name, "outputSetDataGridView"))
                saveOutputBtn.Enabled = true;
            if(string.Equals((sender as DataGridView).Name, "inValidationSetDataGridView"))
                saveInValBtn.Enabled = true;
            if(string.Equals((sender as DataGridView).Name, "outValidationSetDataGridView"))
                saveOutValBtn.Enabled = true;
        }

        private void menuCickHandler(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "csv files (*csv)|*.csv";
            dialog.RestoreDirectory = true;
            if(dialog.ShowDialog(this) == DialogResult.OK)
            {
                Stream stream = dialog.OpenFile();
                Export.SetToFile(stream);
            }            
        }

        #endregion

        private bool Save(DataGridView grid)
        {
            try
            {
                DataTable data = (DataTable)grid.DataSource;

                double[][] newData = DataTableToMatrix(data);

                switch(grid.Name)
                {
                    case "inputSetDataGridView":
                        {
                            set.inMatrix = newData;
                            break;
                        }
                    case "outputSetDataGridView":
                        {
                            set.outMatrix = newData;
                            break;
                        }
                    case "inValidationSetDataGridView":
                        {
                            set.inValMatrix = newData;
                            break;
                        }
                    case "outValidationSetDataGridView":
                        {
                            set.outValMatrix = newData;
                            break;
                        }
                }

                return true;
            }
            catch(Exception e)
            {
                logger.ErrorFormat(ExceptionManager.Parse(e) + Environment.NewLine + e.StackTrace);
            }

            return false;
        }

        private double[][] DataTableToMatrix(DataTable data)
        {
            List<double[]> inMatrix = new List<double[]>();
            for(int r = 0; r < data.Rows.Count; r++)
            {
                object[] row = data.Rows[r].ItemArray;
                List<double> rowd = new List<double>();

                foreach(object o in row)
                    rowd.Add(Convert.ToDouble(o));

                inMatrix.Add(rowd.ToArray());
            }
            return inMatrix.ToArray();
        }

        private DataTable BuildDataTable(double[][] data)
        {
            DataTable inData = new DataTable();

            int inCols = data[0].Count();
            int inRows = data.Count();

            for(int c = 0; c < inCols; c++)
            {
                inData.Columns.Add(c.ToString());
            }
            for(int r = 0; r < inRows; r++)
            {
                DataRow row = inData.NewRow();
                row.ItemArray = data[r].Cast<object>().ToArray();
                inData.Rows.Add(row);
            }

            return inData;
        }
    }
}