using Caliburn.Micro;

namespace SQLCELogViewer.MDI
{
    internal class MDIViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public void Open(IScreen screen)
        {
            this.ActivateItem(screen);
        }
    }
}
