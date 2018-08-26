using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Write_A_Thon.Commands;
using Write_A_Thon.Model;

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
                FlipViewStep2NavForwardCommand?.RaiseCanExecuteChanged();
                UpdateGoalSummaryString();
            }
        }


        public RelayCommand<FlipView> FlipViewStep1NavForwardCommand { get; set; }

        public RelayCommand<FlipView> FlipViewStep2NavForwardCommand { get; set; }

        public RelayCommand<FlipView> FlipViewNavBackCommand { get; set; }


        private DateTimeOffset _selectedDueDate;

        public DateTimeOffset SelectedDueDate
        {
            get { return _selectedDueDate; }
            set
            {
                _selectedDueDate = value;
                NotifyPropertyChanged();
                FlipViewStep1NavForwardCommand?.RaiseCanExecuteChanged();
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
            FlipViewStep1NavForwardCommand = new RelayCommand<FlipView>(StepForward, CheckIfValidDateIsEntered);
            FlipViewStep2NavForwardCommand = new RelayCommand<FlipView>(StepForward, CheckIfValidNumberIsEntered);
            FlipViewNavBackCommand = new RelayCommand<FlipView>(StepBack);
        }

        private bool CheckIfValidNumberIsEntered()
        {
            return TotalWordsToWrite > 0;
        }

        private bool CheckIfValidDateIsEntered()
        {
            return SelectedDueDate > DateTimeOffset.UtcNow;
        }

        private void StepBack(FlipView flipView)
        {
            if (flipView.SelectedIndex > 0)
            {
                flipView.SelectedIndex -= 1;
            }
        }

        private void StepForward(FlipView flipView)
        {
            flipView.SelectedIndex += 1;
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
