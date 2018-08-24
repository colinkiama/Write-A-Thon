﻿using System;
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
            }
        }

        private string _goalSummaryString;

        public string GoalSummaryString
        {
            get { return _goalSummaryString; }
            set { _goalSummaryString = value;
                NotifyPropertyChanged();
            }
        }


        public GoalSettingViewModel()
        {
            SelectedDueDate = DateTimeOffset.UtcNow;
        }

        public override void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.NotifyPropertyChanged(propertyName);
            UpdateGoalSummaryString();
        }

        private void UpdateGoalSummaryString()
        {
            int wordsToWritePerDay = CalculateNumOfWordsToWritePerDay();
            GoalSummaryString = $"Based on your answers, you should do {wordsToWritePerDay} per day.";
        }

        private int CalculateNumOfWordsToWritePerDay()
        {
            throw new NotImplementedException();
        }

        private int CalculateNumOfDaysToCompleteWork()
        {
            throw new NotImplementedException();
        }
    }
}