using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;
using NLog;

namespace DeltaLogViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            logger.Info(string.Format("{0} {1} started", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version));

            JumpList jList = new JumpList();
            JumpList.SetJumpList(Application.Current, jList);

            const string explorerPath = @"%windir%\explorer.exe";
            JumpTask appPathJL = new JumpTask
            {
                CustomCategory = "Paths",
                Title = "Open program directory",
                IconResourcePath = explorerPath,
                ApplicationPath = explorerPath,
                Arguments = @"/root," + AppDomain.CurrentDomain.BaseDirectory,
                Description = AppDomain.CurrentDomain.BaseDirectory
            };

            jList.JumpItems.Add(appPathJL);

            jList.Apply();
        }
    }
}
