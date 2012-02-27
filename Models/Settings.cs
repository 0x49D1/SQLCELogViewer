using System;
using Caliburn.Micro;

namespace SQLCELogViewer.Models
{
    [Serializable]
    public class Settings:PropertyChangedBase
    {
        public int ItemMaxWidth { get; set; }
        public int ItemHeight { get; set; }
        public double ItemOpacity { get; set; }
        public string ItemBackgroundColor { get; set; }

        public string FontColor { get; set; }
        public int FontSize { get; set; }

        public int Screen { get; set; }

    }
}
