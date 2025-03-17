using System;

namespace PhotoViewer.Core
{
    public class ImageItem
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
