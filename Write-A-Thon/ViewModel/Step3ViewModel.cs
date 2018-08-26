using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;
using Write_A_Thon.Model;

namespace Write_A_Thon.ViewModel
{
    class Step3ViewModel: NotifyingClass
    {
        public RelayCommand NavBackCommand{ get; set; }

        private Goal _goalBeingCreated;

        public Goal GoalBeingCreated
        {
            get { return _goalBeingCreated; }
            set
            {
                _goalBeingCreated = value;
                UpdateGoalSummary();
            }
        }

        private void UpdateGoalSummary()
        {
            uint wordsToWritePerDay = GoalBeingCreated.CalculateNumOfWordsToWritePerDay();
            GoalSummaryString = $"Based on your answers, you should write {wordsToWritePerDay} words per day.";
        }

        

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
