using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Write_A_Thon.Services
{
    public class FileLaunchService
    {
        public static bool wasFileLaunched = false;

        StorageFile LoadedFile;
        string FileContent;


        public FileLaunchService(string fileContent, StorageFile loadedFile)
        {
            LoadedFile = loadedFile;
            FileContent = fileContent;

            wasFileLaunched = true;
        }

        public (string fileContent, StorageFile loadedFile) GetLaunchFileData()
        {
            return (FileContent, LoadedFile);
            
        }


      
    }
}
