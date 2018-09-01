using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Write_A_Thon.Helpers;
using Write_A_Thon.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Write_A_Thon.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WritingView : Page
    {

        public StorageFile loadedFile;
        public bool fileSaved;
        public bool unsavedWork;
        string lastSavedContent;
        FileIOHelper fileIOHelper = new FileIOHelper();

        public WritingView()
        {
            this.InitializeComponent();
            App.fileIOService.NewFileRequested += FileIOService_NewFileRequested;
            App.fileIOService.SaveRequested += FileIOService_SaveRequested;
            App.fileIOService.SaveAsRequested += FileIOService_SaveAsRequested;
            App.fileIOService.LoadRequested += FileIOService_LoadRequested;
            string EditorText = "";
            if (FileLaunchService.wasFileLaunched)
            {
                (EditorText, loadedFile) = App.fileLaunchService.GetLaunchFileData();
            }
            else
            {
                EditorText = String.Empty;
            }

            SetRichEditBoxContent(EditorText);
        }

        private async void FileIOService_NewFileRequested(object sender, EventArgs e)
        {
            await CreateNewFile();
        }

        private async Task<bool> CreateNewFile()
        {
            bool newFileCreated = false;
            if (loadedFile == null || unsavedWork)
            {
                bool userProceeded = await AskIfUserWantsToSave();
                if (userProceeded)
                {
                    ClearWritingView();
                    newFileCreated = true;
                }
            }
            else
            {
                ClearWritingView();
                newFileCreated = true;
            }
            return newFileCreated;
        }

        private async Task<bool> AskIfUserWantsToSave()
        {
            bool willProceed = false;

            var result = await DialogHelper.ShowSaveUnsavedWorkDialog();

            if (result != ContentDialogResult.None)
            {
                willProceed = true;

                if (result == ContentDialogResult.Primary)
                {
                    await SaveFile();
                }

            }

            return willProceed;

        }

        private void ClearWritingView()
        {
            SetRichEditBoxContent(string.Empty);
            loadedFile = null;
            fileSaved = false;
            unsavedWork = false;
        }

        private async void FileIOService_SaveRequested(object sender, EventArgs e)
        {
            await SaveFile();

        }

        private async Task SaveFile()
        {
            if (loadedFile == null)
            {
                await fileIOHelper.SaveFileAsync(GetRichEditBoxContent());
            }
            else
            {
                await fileIOHelper.SaveFileAsync(GetRichEditBoxContent(), loadedFile);
            }
        }

        private async void FileIOService_SaveAsRequested(object sender, EventArgs e)
        {
            await fileIOHelper.SaveFileAsync(GetRichEditBoxContent());
        }

        private async void FileIOService_LoadRequested(object sender, EventArgs e)
        {
            bool newFileCreated = await CreateNewFile();
            if (newFileCreated)
            {
                await LoadFile();
            }
        }

        private async Task LoadFile()
        {
            loadedFile = await fileIOHelper.LoadFileAsync();
            string fileContent = await fileIOHelper.GetContentFromFileAsync();
            if (fileContent != null)
            {
                SetRichEditBoxContent(fileContent);

            }
        }

        private bool CheckIfSavedContentHasChanged()
        {
            bool contentHasChanged = false;
            if (lastSavedContent != null)
            {
                contentHasChanged = lastSavedContent.Equals(GetRichEditBoxContent());
            }
            return contentHasChanged;
        }

        private void SetRichEditBoxContent(string editorText)
        {
            WritingRichEditBox.Document.SetText(TextSetOptions.FormatRtf, editorText);
        }


        private string GetRichEditBoxContent()
        {
            WritingRichEditBox.Document.GetText(TextGetOptions.FormatRtf, out string contentToReturn);
            return contentToReturn;
        }

        private void WritingRichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var reb = (RichEditBox)sender;
            reb.Document.GetText(TextGetOptions.NoHidden, out string rebContent);
            WordCounterService.CalculateWordCount(rebContent);
        }
    }
}
