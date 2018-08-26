using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;
using Write_A_Thon.Model;
using Write_A_Thon.View.GoalSettingViews;

namespace Write_A_Thon.ViewModel
{
    class Step1ViewModel : NotifyingClass
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
            NavForwardCommand = new RelayCommand(NavigateForward, CheckIfValidDateIsEntered);
        }

        private bool CheckIfValidDateIsEntered()
        {
            return SelectedDueDate > DateTimeOffset.UtcNow;
        }

        private async void NavigateForward()
        {
            var goalBeingCreated = new Goal { DueDate = SelectedDueDate };
            await FrameAnimationHelper.Navigate(typeof(Step2View), goalBeingCreated);
        }
    }
}
