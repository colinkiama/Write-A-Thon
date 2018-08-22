﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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


        public WritingView()
        {
            this.InitializeComponent();
            string EditorText = "";
            if (FileLaunchService.wasFileLaunched)
            {

                (EditorText, loadedFile) = App.fileLaunchService.GetLaunchFileData();

            }
            else
            {
                EditorText = "Yo mama";
            }

            SetRichEditBoxContent(EditorText);
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
    }
}
