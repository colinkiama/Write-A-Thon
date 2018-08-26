using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Commands;
using Write_A_Thon.Helpers;
using Write_A_Thon.Model;
using Write_A_Thon.View;

namespace Write_A_Thon.ViewModel
{
    class Step3ViewModel: NotifyingViewModel
    {
        public RelayCommand NavBackCommand{ get; set; }

        public RelayCommand FinishFormCommand { get; set; }

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
            FinishFormCommand = new RelayCommand(FinishForm);
        }

        private void FinishForm()
        {
            App.InfoBarService = new Services.InfoBarService(new GoalProgress(GoalBeingCreated));
            GoalSettingView.RaiseFormFinished();
        }

        private async void NavigateBack()
        {
            await FrameAnimationHelper.NavigateBack();
        }
    }
}
