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
        public EventHandler LoadRequested;

        public void RequestToLoadFile() => LoadRequested?.Invoke(this, EventArgs.Empty);
        
    }
}
