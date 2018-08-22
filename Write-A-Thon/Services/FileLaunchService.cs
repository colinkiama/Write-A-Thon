using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Write_A_Thon.Helpers;

namespace Write_A_Thon.Services
{
    public class FileLaunchService
    {
        public static bool wasFileLaunched = false;

        StorageFile LoadedFile;
        string FileContent;


        public FileLaunchService(StorageFile loadedFile)
        {
            LoadedFile = loadedFile;

            try
            {
                FileContent = Task.Run(async () => await new FileIOHelper().GetContentFromFileAsync(LoadedFile)).GetAwaiter().GetResult();
                wasFileLaunched = true;
            }
            catch (Exception)
            {

                
            }
        }

        public (string fileContent, StorageFile loadedFile) GetLaunchFileData()
        {
            return (FileContent, LoadedFile);

        }



    }
}
