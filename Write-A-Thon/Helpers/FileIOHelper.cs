using Write_A_Thon.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Write_A_Thon.Helpers
{
    public class FileIOHelper
    {
        public event FileIOEventHandler RaiseFileIOEvent;

        public StorageFile fileFromLastOperation = null;

        public async Task<string> GetContentFromFileAsync()
        {
            string contentToReturn = "";
            if (await LoadFileAsync() is StorageFile textFile)
            {
                var stream = await textFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                ulong size = stream.Size;
                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                        contentToReturn = dataReader.ReadString(numBytesLoaded);
                    }
                }

            }
            return contentToReturn;
        }

        public async Task<string> GetContentFromFileAsync(StorageFile fileToLoadFrom)
        {
            string contentToReturn = "";
            if (fileToLoadFrom != null)
            {
                var stream = await fileToLoadFrom.OpenAsync(Windows.Storage.FileAccessMode.Read);
                ulong size = stream.Size;
                using (var inputStream = stream.GetInputStreamAt(0))
                {
                    using (var dataReader = new Windows.Storage.Streams.DataReader(inputStream))
                    {
                        uint numBytesLoaded = await dataReader.LoadAsync((uint)size);
                        contentToReturn = dataReader.ReadString(numBytesLoaded);
                    }
                }

            }
            return contentToReturn;
        }

        public async Task<StorageFile> LoadFileAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".rtf");
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            fileFromLastOperation = file;
            return file;

        }


        public async Task<bool> SaveFileAsync(string fileContent)
        {
            bool isFileSaved = false;
            StorageFile saveFile = await CreateSaveFileFromPicker();
            string dataToSave = fileContent;

            if (saveFile != null)
            {
                if (saveFile.FileType == ".txt")
                {
                    dataToSave = RtfToPlainTextHelper.ConvertRtfToPlainText(dataToSave);
                }
            }
            try
            {
                var stream = await saveFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                    {
                        dataWriter.WriteString(dataToSave);
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                        Debug.WriteLine("File Saved");
                        isFileSaved = true;
                        fileFromLastOperation = saveFile;

                    }
                }
                stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
            }
            catch (Exception)
            {
                Debug.WriteLine("File not found");
            }
            OnRaiseFileIOEvent(isFileSaved);
            return isFileSaved;
        }

        private void OnRaiseFileIOEvent(bool isFileSaved)
        {
            RaiseFileIOEvent?.Invoke(this, new FileIOEventArgs(isFileSaved));
        }

        public async Task<bool> SaveFileAsync(string fileContent, StorageFile fileToSave, bool willChangeToNewFile = false)
        {
            bool isFileSaved = false;
            string dataToSave = fileContent;
            if (fileToSave != null)
            {
                if (fileToSave.FileType == ".txt")
                {
                    dataToSave = RtfToPlainTextHelper.ConvertRtfToPlainText(dataToSave);
                } 
            }
            try
            {
                var stream = await fileToSave.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
                using (var outputStream = stream.GetOutputStreamAt(0))
                {
                    using (var dataWriter = new Windows.Storage.Streams.DataWriter(outputStream))
                    {
                        dataWriter.WriteString(dataToSave);
                        await dataWriter.StoreAsync();
                        await outputStream.FlushAsync();
                        Debug.WriteLine("File Saved");
                        isFileSaved = true;
                        if (!willChangeToNewFile)
                        {
                            fileFromLastOperation = fileToSave;
                        }
                    }
                }
                stream.Dispose(); // Or use the stream variable (see previous code snippet) with a using statement as well.
            }
            catch (Exception)
            {
                Debug.WriteLine("File not found");
            }
            OnRaiseFileIOEvent(isFileSaved);
            return isFileSaved;
        }

        private async Task<StorageFile> CreateSaveFileFromPicker()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text Format", new List<string>() { ".rtf" });
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Untitled";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();

            return file;

        }
    }
}
