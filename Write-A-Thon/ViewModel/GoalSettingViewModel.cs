using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.ViewModel
{
    class GoalSettingViewModel : NotifyingViewModel
    {
        private uint _totalWordsToWrite;

        public uint TotalWordsToWrite
        {
            get { return _totalWordsToWrite; }
            set
            {
                _totalWordsToWrite = value;
                NotifyPropertyChanged();
                UpdateGoalSummaryString();
            }
        }

        private DateTimeOffset _selectedDueDate;

        public DateTimeOffset SelectedDueDate
        {
            get { return _selectedDueDate; }
            set
            {
                _selectedDueDate = value;
                NotifyPropertyChanged();
                UpdateGoalSummaryString();
            }
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


        public GoalSettingViewModel()
        {
            SelectedDueDate = DateTimeOffset.UtcNow;
        }


        private void UpdateGoalSummaryString()
        {
            uint wordsToWritePerDay = CalculateNumOfWordsToWritePerDay();
            GoalSummaryString = $"Based on your answers, you should write {wordsToWritePerDay} words per day.";
        }

        private uint CalculateNumOfWordsToWritePerDay()
        {
            uint numOfWordsPerDay = 0;
            if (TotalWordsToWrite > 0)
            {
                var currentDate = DateTimeOffset.UtcNow;
                TimeSpan timeBetweenNowAndDeadline = SelectedDueDate - currentDate;
                double totalDaysTillDeadline = timeBetweenNowAndDeadline.TotalDays;
                if (totalDaysTillDeadline > 0)
                {
                    uint roundedUpDaysTillDeadline = 1;
                    if (totalDaysTillDeadline > 1)
                    {
                        roundedUpDaysTillDeadline = (uint)Math.Ceiling(totalDaysTillDeadline);
                    }
                    numOfWordsPerDay = TotalWordsToWrite / roundedUpDaysTillDeadline;
                }

            }
            return numOfWordsPerDay;
        }


    }
}
