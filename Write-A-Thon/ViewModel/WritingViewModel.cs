using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Write_A_Thon.Services;

namespace Write_A_Thon.ViewModel
{
    public class WritingViewModel : NotifyingViewModel
    {
        public StorageFile loadedFile;
        private string _editorText;

        public string EditorText
        {
            get { return _editorText; }
            set
            {
                _editorText = value;
                NotifyPropertyChanged();
            }
        }

        public WritingViewModel()
        {
            FileLaunchService fileLaunchService = new FileLaunchService("djsklad", null);
            (EditorText, loadedFile) = fileLaunchService.GetLaunchFileData();
            Debug.WriteLine(EditorText);
            Debug.WriteLine(loadedFile);
        }




    }
}
