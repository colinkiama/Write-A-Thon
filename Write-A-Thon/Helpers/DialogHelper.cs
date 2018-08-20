using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Write_A_Thon.Helpers
{
    public static class DialogHelper
    {
        public async static Task<ContentDialogResult> ShowSaveUnsavedWorkDialog()
        {
            ContentDialog SaveUnsavedWorkDialog = new ContentDialog
            {
                Title = "Changes haven't been saved",
                Content = "Would you like to save the your work you've made. Unsaved work will be lost",
                PrimaryButtonText = "Save",
                SecondaryButtonText = "No",
                CloseButtonText = "Cancel",
            };

            ContentDialogResult result = await SaveUnsavedWorkDialog.ShowAsync();

            return result;
        }
       
    }
}
