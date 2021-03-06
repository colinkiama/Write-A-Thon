﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Write_A_Thon.View;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Threading.Tasks;
using Write_A_Thon.Services;
using Write_A_Thon.Dialogs;
using Windows.System;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Write_A_Thon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class Shell : Page
    {
        const string newFileString = "New File";
        const string saveString = "Save";
        const string saveAsString = "Save As...";
        const string loadFileString = "Load File";
        const string reviewString = "Rate/Review App";
        const string feedbackString = "Send Feedback";

        public static FileIOService fileIOService = new FileIOService();
        public Shell()
        {
            this.InitializeComponent();
            mainFrame.Navigate(typeof(WritingView));
            InfoBarFrame.Navigate(typeof(InfoBarView));
            SettingsFrame.Navigate(typeof(SettingsView));
        }


        private async void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton menuToggle)
            {
                if ((bool)menuToggle.IsChecked)
                {
                    await ShowMenuGrid();
                }
                else
                {
                    await HideMenuGrid();

                }
            }
        }

        private async Task ShowMenuGrid()
        {
            await MenuGrid.Offset(0).StartAsync();
        }


        private async Task HideMenuGrid()
        {
            float widthToMove = (float)MenuGrid.ActualWidth;
            await MenuGrid.Offset(-widthToMove).StartAsync();
        }

        private async Task HideMenuGrid(float duration = 500)
        {
            float widthToMove = (float)MenuGrid.ActualWidth;
            await MenuGrid.Offset(-widthToMove,0,duration).StartAsync();
        }

        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton settingsToggle)
            {
                if ((bool)settingsToggle.IsChecked)
                {
                    await ShowSettingsFrame();
                }
                else
                {
                    await HideSettingsFrame();
                }
            }
        }

        private async Task ShowSettingsFrame()
        {
            await SettingsFrame.Offset(0).StartAsync();
        }

        private async Task HideSettingsFrame(float duration = 500)
        {
            float widthToMove = (float)SettingsFrame.ActualWidth;
            await SettingsFrame.Offset(widthToMove,0,duration).StartAsync();
        }

        private async void MenuGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await HideMenuGrid(0);
        }

        private async void SettingsFrame_Loaded(object sender, RoutedEventArgs e)
        {
            await HideSettingsFrame(0);
        }

        private void MenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string clickedContent = (string)e.ClickedItem;
            switch (clickedContent)
            {
                case newFileString:
                    App.fileIOService.RequsetNewFile();
                    break;
                case saveString:
                    App.fileIOService.RequestToSaveFile();
                    break;
                case saveAsString:
                    App.fileIOService.RequestToSaveFile(true);
                    break;
                case loadFileString:
                    App.fileIOService.RequestToLoadFile();
                    break;
            }
        }

        private async void LowMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string clickedContent = (string)e.ClickedItem;
            switch (clickedContent)
            {
                case reviewString:
                    await new ReviewDialog().ShowAsync();
                    break;
                case feedbackString:
                    await Launcher.LaunchUriAsync(new Uri("mailto:colinkiama@hotmail.co.uk"));
                    break;
            }
        }
    }
}
