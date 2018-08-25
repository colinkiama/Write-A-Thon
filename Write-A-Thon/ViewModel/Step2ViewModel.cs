using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;
using Write_A_Thon.View.GoalSettingViews;

namespace Write_A_Thon.ViewModel
{
    class Step2ViewModel:NotifyingViewModel
    {
        private uint _totalWordsToWrite;

        public uint TotalWordsToWrite
        {
            get { return _totalWordsToWrite; }
            set
            {
                _totalWordsToWrite = value;
                NotifyPropertyChanged();
                NavForwardCommand?.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand NavForwardCommand { get; set; }
        public RelayCommand NavBackCommand { get; set; }

        public Step2ViewModel()
        {
            NavBackCommand = new RelayCommand(NavigateBack);
            NavForwardCommand = new RelayCommand(NavigateForward, CheckIfValidNumberIsEntered);
        }

        private async void NavigateBack()
        {
            await FrameAnimationHelper.NavigateBack();
        }

        private void NavigateForward()
        {
            FrameAnimationHelper.Navigate(typeof(Step3View));
        }

        private bool CheckIfValidNumberIsEntered()
        {
            return TotalWordsToWrite > 0;
        }

        public void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = (TextBox)sender;
            if (txtBox.Text == string.Empty)
            {
                txtBox.Text = "0";
                txtBox.SelectAll();
            }
            bool parsed = uint.TryParse(txtBox.Text, out uint result);
            if (parsed)
            {
                TotalWordsToWrite = result;
            }
        }
    }
}
