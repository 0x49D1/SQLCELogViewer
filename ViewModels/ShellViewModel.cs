using System;
using System.Collections.ObjectModel;
using System.Data.EntityClient;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using NLog;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
using LogManager = NLog.LogManager;
using MessageBox = System.Windows.MessageBox;
using Screen = Caliburn.Micro.Screen;

namespace SQLCELogViewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        private string providerConnectionString = string.Empty;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private string _logFilePath = "SQLCELogViewer";

        public string LogFilePath
        {
            get { return _logFilePath; }
            set
            {
                _logFilePath = value;
                NotifyOfPropertyChange(() => LogFilePath);
            }
        }

        private ObservableCollection<LogEntry> _itemsList = null;

        public ObservableCollection<LogEntry> ItemsList
        {
            get { return _itemsList; }
            set
            {
                _itemsList = value;
                NotifyOfPropertyChange(() => ItemsList);
            }
        }

        public void LoadDataSource()
        {
            if (string.IsNullOrEmpty(providerConnectionString))
                return;
            if (ItemsList != null)
                ItemsList.Clear();

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Metadata = "res://*/LogDBModel.csdl|res://*/LogDBModel.ssdl|res://*/LogDBModel.msl";
            entityBuilder.Provider = "System.Data.SqlServerCe.4.0";
            entityBuilder.ProviderConnectionString = "Data Source=" + providerConnectionString;

            using (LoggerEntities context = new LoggerEntities(entityBuilder.ConnectionString))
            {
                try
                {
                    ItemsList = new ObservableCollection<LogEntry>(context.LogEntries.OrderByDescending(l => l.id).ToList());
                }
                catch (Exception e)
                {
                    logger.ErrorException("LoadDataSource", e);
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void ReloadDataGrid(string[] filePaths)
        {
            providerConnectionString = filePaths[0];
            LogFilePath = providerConnectionString.Length > 60
                              ? string.Format("{0}...{1}", providerConnectionString.Substring(0, 48),
                                              providerConnectionString.Substring(providerConnectionString.Length - 12, 12))
                              : providerConnectionString;
            LoadDataSource();
        }

        public void OpenLog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "SQLCE database file (*.sdf)|*.sdf|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = false;

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileNames.Length > 0)
            {
                ReloadDataGrid(ofd.FileNames);
            }
        }

        public void DropHandler(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null || droppedFilePaths.Length == 0)
                return;
            try
            {
                ReloadDataGrid(droppedFilePaths);
            }
            catch (Exception ex)
            {
                logger.ErrorException("DropHandler", ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
