using System;
using System.Collections.ObjectModel;
using System.Data.EntityClient;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using Analects.SettingsService;
using NLog;
using SQLCELogViewer.Models;
using DataFormats = System.Windows.DataFormats;
using DragEventArgs = System.Windows.DragEventArgs;
using LogManager = NLog.LogManager;
using MessageBox = System.Windows.MessageBox;

namespace SQLCELogViewer
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Caliburn.Micro.Screen, IShell
    {
        private string providerConnectionString = string.Empty;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        readonly ISettingsService settingsService;
        public Settings Settings { get; set; }

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

        [ImportingConstructor]
        public ShellViewModel(ISettingsService settingsService1)
        {
            this.settingsService = settingsService1;
            Settings = settingsService.Get<Settings>("WindowSettings");
            if (Settings == null)
            {
                Settings = new Settings();
                SetDefaultSettings();
            }
        }

        public void LoadDataSource()
        {
            EntityConnectionStringBuilder entityBuilder;
            if (!CreateConnection(out entityBuilder))
                return;

            if (ItemsList != null)
                ItemsList.Clear();

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

        private bool CreateConnection(out EntityConnectionStringBuilder entityBuilder)
        {
            entityBuilder = new EntityConnectionStringBuilder();
            if (string.IsNullOrEmpty(providerConnectionString))
                return false;

            entityBuilder.Metadata = "res://*/LogDBModel.csdl|res://*/LogDBModel.ssdl|res://*/LogDBModel.msl";
            entityBuilder.Provider = "System.Data.SqlServerCe.4.0";
            entityBuilder.ProviderConnectionString = "Data Source=" + providerConnectionString;
            return true;
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

        public void SetDefaultSettings()
        {
            Settings.FontSize = 40;
            Settings.FontColor = "White";
            Settings.ItemBackgroundColor = "Black";
            Settings.ItemOpacity = 0.5;
            Settings.ItemHeight = 350;

            SaveSettings();
        }

        public void SaveSettings()
        {
            settingsService.Set("WindowSettings", Settings);
            settingsService.Save();
        }

        public void ClearLog()
        {
            EntityConnectionStringBuilder entityBuilder;
            if (!CreateConnection(out entityBuilder))
                return;

            using (LoggerEntities context = new LoggerEntities(entityBuilder.ConnectionString))
            {
                try
                {
                    var list = new ObservableCollection<LogEntry>(context.LogEntries.ToList());
                    foreach (var logEntry in list)
                    {
                        context.DeleteObject(logEntry);
                    }
                    context.SaveChanges();

                    LoadDataSource();
                }
                catch (Exception e)
                {
                    logger.ErrorException("LoadDataSource", e);
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
