using System;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace SQLCELogViewer.Utilities
{
    public static class Export
    {
        public static void ExportToCSV(System.Windows.Controls.DataGrid dg)
        {
            if (dg.Items.Count == 0)
                return;

            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(ofd.FileName))
                {
                    file.WriteLine(result);
                }
            }
        }
    }
}
