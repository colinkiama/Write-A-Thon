using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Events
{
    public delegate void FileIOEventHandler(object sender, FileIOEventArgs args);


    public class FileIOEventArgs : EventArgs
    {
        public FileIOEventArgs(bool operationSuccessful)
        {
            _operationSuccessful = operationSuccessful;
        }

        private bool _operationSuccessful;

        public bool OperationSuccessful
        {
            get { return _operationSuccessful; }
        }

        


    }
}
