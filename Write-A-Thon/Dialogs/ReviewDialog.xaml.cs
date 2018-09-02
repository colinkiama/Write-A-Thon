using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Write_A_Thon.Dialogs
{
    public sealed partial class ReviewDialog : ContentDialog
    {
        readonly RelayCommand reviewCommand;
        private string appLaunchSettingName;

        public ReviewDialog()
        {
            this.InitializeComponent();
            this.CloseButtonText = "Maybe Later";
            reviewCommand = new RelayCommand(GoToReviewApp);
        }

        public ReviewDialog(string appLaunchSettingName)
        {
            this.InitializeComponent();
            this.CloseButtonText = "Maybe Later";
            this.appLaunchSettingName = appLaunchSettingName;
            reviewCommand = new RelayCommand(GoToReviewApp);
        }

        private async void GoToReviewApp()
        {
            await ReviewHelper.TryRequestReviewAsync();
        }


    }
}
