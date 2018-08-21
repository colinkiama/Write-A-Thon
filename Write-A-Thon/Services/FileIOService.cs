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
        private string launchFileContent;

        public string MyProperty
        {
            get { return launchFileContent; }
            set { launchFileContent = value; }
        }

    }
}
