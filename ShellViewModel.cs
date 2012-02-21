using System;
using System.Collections.ObjectModel;
using System.Data.EntityClient;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using NLog;
using LogManager = NLog.LogManager;

namespace SQLCELogViewer
{


    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        private string providerConnectionString = string.Empty;
        private static Logger logger = LogManager.GetCurrentClassLogger();

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

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.Metadata = "res://*/LogDBModel.csdl|res://*/LogDBModel.ssdl|res://*/LogDBModel.msl";
            entityBuilder.Provider = "System.Data.SqlServerCe.4.0";
            entityBuilder.ProviderConnectionString = "Data Source=" + providerConnectionString;

            LoggerEntities context = new LoggerEntities(entityBuilder.ConnectionString);
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

        public void DropHandler(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null || droppedFilePaths.Length == 0)
                return;
            try
            {
                providerConnectionString = droppedFilePaths[0];
                LoadDataSource();
            }
            catch (Exception ex)
            {
                logger.ErrorException("DropHandler", ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
