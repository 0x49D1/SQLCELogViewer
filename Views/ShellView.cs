using System.Windows.Input;
using MahApps.Metro.Controls;
using SQLCELogViewer.Utilities;

namespace SQLCELogViewer
{
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) != 0) && e.Key == Key.E)
            {
                ExportManager.ExportToCSV(dgLog);
            }
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
