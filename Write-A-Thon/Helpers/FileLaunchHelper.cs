using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Write_A_Thon.Helpers
{
    public class FileLaunchHelper
    {
        public bool WasFileLaunched { get; set; } = false;
        public StorageFile LaunchFile { get; set; }
    }
}
