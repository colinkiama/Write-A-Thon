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

        StorageFile loadedFile;
        bool textLoadedIn;
        FileIOHelper fileIOHelper = new FileIOHelper();

        private bool _unsavedWork;

        public bool UnsavedWork
        {
            get { return _unsavedWork; }
            set
            {
                _unsavedWork = value;
                UpdateFileTitleUnsavedStatus();
            }
        }

        private void UpdateFileTitleUnsavedStatus()
        {
            if (UnsavedWork)
            {
                if (!CurrentFileTitleTextBlock.Text.Last().Equals('*'))
                {
                    CurrentFileTitleTextBlock.Text += "*";

                }
            }
            else
            {
                if (CurrentFileTitleTextBlock.Text.Last().Equals('*'))
                {
                    string currentTitle = CurrentFileTitleTextBlock.Text;
                    CurrentFileTitleTextBlock.Text = currentTitle.Remove(currentTitle.Length - 1);
                }
                
            }
        }

        public WritingView()
        {
            this.InitializeComponent();
            UnsavedWork = true;

            App.fileIOService.NewFileRequested += FileIOService_NewFileRequested;
            App.fileIOService.SaveRequested += FileIOService_SaveRequested;
            App.fileIOService.SaveAsRequested += FileIOService_SaveAsRequested;
            App.fileIOService.LoadRequested += FileIOService_LoadRequested;
            fileIOHelper.RaiseFileIOEvent += FileIOHelper_RaiseFileIOEvent;

            string EditorText = "";
            if (FileLaunchService.wasFileLaunched)
            {
                (EditorText, loadedFile) = App.fileLaunchService.GetLaunchFileData();
                SetCurrentFileTitle(loadedFile.DisplayName);
                UnsavedWork = false;
            }
            else
            {
                EditorText = String.Empty;
            }

            SetRichEditBoxContent(EditorText);
        }

        private void FileIOHelper_RaiseFileIOEvent(object sender, Events.FileIOEventArgs args)
        {
            if (args.OperationSuccessful)
            {
                loadedFile = fileIOHelper.fileFromLastOperation;
                SetCurrentFileTitle(loadedFile.DisplayName);
            }
        }

        private async void FileIOService_NewFileRequested(object sender, EventArgs e)
        {
            await CreateNewFile();
        }

        private async Task<bool> CreateNewFile()
        {
            bool newFileCreated = false;
            if (loadedFile == null || UnsavedWork)
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
            UnsavedWork = false;
            SetCurrentFileTitle(string.Empty);
        }

        private void SetCurrentFileTitle(string title)
        {
            if (title.Equals(string.Empty))
            {
                CurrentFileTitleTextBlock.Text = "Untitled";
            }
            else
            {
                CurrentFileTitleTextBlock.Text = title;
            }
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
            SetCurrentFileTitle(loadedFile.DisplayName);
            string fileContent = await fileIOHelper.GetContentFromFileAsync(loadedFile);
            if (fileContent != null)
            {
                SetRichEditBoxContent(fileContent);
            }
        }



        private void SetRichEditBoxContent(string editorText)
        {
            textLoadedIn = true;
            WritingRichEditBox.Document.SetText(TextSetOptions.FormatRtf, editorText);
        }


        private string GetRichEditBoxContent()
        {
            WritingRichEditBox.Document.GetText(TextGetOptions.FormatRtf, out string contentToReturn);
            return contentToReturn;
        }

        private void WritingRichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!textLoadedIn)
            {
                UnsavedWork = true;
            }
            textLoadedIn = false;
            var reb = (RichEditBox)sender;
            reb.Document.GetText(TextGetOptions.NoHidden, out string rebContent);
            WordCounterService.CalculateWordCount(rebContent);
        }
    }
}
