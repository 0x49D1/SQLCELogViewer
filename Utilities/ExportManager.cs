using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace SQLCELogViewer.Utilities
{
    public static class ExportManager
    {
        public static void ExportToCSV(System.Windows.Controls.DataGrid dg)
        {
            if (dg.Items.Count == 0)
                return;

            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            var stream = (System.IO.Stream)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            var encoding = System.Text.Encoding.GetEncoding("UTF-8");
            var reader = new System.IO.StreamReader(stream, encoding);
            string result = reader.ReadToEnd();
            Clipboard.Clear();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = false;

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
