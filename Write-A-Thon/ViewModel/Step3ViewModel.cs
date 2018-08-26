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
    class Step3ViewModel: NotifyingViewModel
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
            uint wordsToWritePerDay = CalculateNumOfWordsToWritePerDay();
            GoalSummaryString = $"Based on your answers, you should write {wordsToWritePerDay} words per day.";
        }

        private uint CalculateNumOfWordsToWritePerDay()
        {
            uint numOfWordsPerDay = 0;
            if (GoalBeingCreated.TotalWordsPerDay > 0)
            {
                var currentDate = DateTimeOffset.UtcNow;
                TimeSpan timeBetweenNowAndDeadline = GoalBeingCreated.DueDate - currentDate;
                double totalDaysTillDeadline = timeBetweenNowAndDeadline.TotalDays;
                if (totalDaysTillDeadline > 0)
                {
                    uint roundedUpDaysTillDeadline = 1;
                    if (totalDaysTillDeadline > 1)
                    {
                        roundedUpDaysTillDeadline = (uint)Math.Ceiling(totalDaysTillDeadline);
                    }
                    numOfWordsPerDay = GoalBeingCreated.TotalWordsPerDay/ roundedUpDaysTillDeadline;
                }

            }
            return numOfWordsPerDay;
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
