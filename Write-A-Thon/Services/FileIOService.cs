using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Write_A_Thon.Helpers;

namespace Write_A_Thon.Services
{
    public class FileIOService
    {
        public event EventHandler LoadRequested;
        public event EventHandler SaveRequested;
        public event EventHandler SaveAsRequested;
        public event EventHandler NewFileRequested;

        public void RequestToSaveFile(bool forceSaveDialog = false)
        {
            if (forceSaveDialog)
            {
                SaveAsRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                SaveRequested?.Invoke(this, EventArgs.Empty);
            }
        }
        public void RequestToLoadFile() => LoadRequested?.Invoke(this, EventArgs.Empty);
        public void RequsetNewFile() => NewFileRequested?.Invoke(this, EventArgs.Empty);
    }
}
