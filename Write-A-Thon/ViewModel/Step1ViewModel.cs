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
    class Step1ViewModel:NotifyingViewModel
    {
        public RelayCommand NavForwardCommand { get; set; }

        private DateTimeOffset _selectedDueDate;

        public DateTimeOffset SelectedDueDate
        {
            get { return _selectedDueDate; }
            set
            {
                _selectedDueDate = value;
                NotifyPropertyChanged();
                NavForwardCommand?.RaiseCanExecuteChanged();
            }
        }

        

        public Step1ViewModel()
        {
            SelectedDueDate = DateTimeOffset.UtcNow;
            NavForwardCommand = new RelayCommand(NavigateForward,CheckIfValidDateIsEntered);
        }

        private bool CheckIfValidDateIsEntered()
        {
            return SelectedDueDate > DateTimeOffset.UtcNow;
        }

        private void NavigateForward()
        {
            FrameAnimationHelper.Navigate(typeof(Step2View));
        }
    }
}
