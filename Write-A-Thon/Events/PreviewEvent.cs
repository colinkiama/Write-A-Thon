using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Events
{
    public delegate void PreviewEventHandler(object sender, PreviewEventArgs previewEvent);
    
    public class PreviewEventArgs: EventArgs
    {
        public PreviewEventArgs(string previewText)
        {
            _previewText = previewText;
        }
        private string _previewText;

        public string PreviewText
        {
            get { return _previewText; }
            set { _previewText = value; }
        }
    }
}
