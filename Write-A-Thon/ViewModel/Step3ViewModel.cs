using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;

namespace Write_A_Thon.ViewModel
{
    class Step3ViewModel: NotifyingViewModel
    {
        public RelayCommand NavBackCommand{ get; set; }

        private string _goalSummaryString;

        public string GoalSummaryString
        {
            get { return _goalSummaryString; }
            set
            {
                _goalSummaryString = value;
                NotifyPropertyChanged();
            }
        }

        public Step3ViewModel()
        {
            NavBackCommand = new RelayCommand(NavigateBack);
        }

        private async void NavigateBack()
        {
            await FrameAnimationHelper.NavigateBack();
        }
    }
}
