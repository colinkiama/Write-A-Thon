using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Write_A_Thon.Model;
using Write_A_Thon.Services;

namespace Write_A_Thon.ViewModel
{
    public class InfoBarViewModel : NotifyingViewModel
    {
        private WordCounterService _wordCounterService;
        private InfoBarService _infoBarService;

        private uint _target;

        public uint Target
        {
            get { return _target; }
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }



        private uint _wordCount;

        public uint WordCount
        {
            get { return _wordCount; }
            set
            {
                _wordCount = value;
                NotifyPropertyChanged();
                UpdateProgress();
            }
        }

        private void UpdateProgress()
        {
            Progress = WordCount / Target * 100;
        }

        private uint _progress;

        public uint Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                NotifyPropertyChanged();
            }
        }

        public InfoBarViewModel()
        {
            _infoBarService = App.InfoBarService;
            _wordCounterService = App.WordCounterService;
            _wordCounterService.WordCountChanged += _wordCounterService_WordCountChanged;
            Target = _infoBarService.GetWordsRequiredPerDay();
        }

        private void _wordCounterService_WordCountChanged(object sender, EventArgs e)
        {
            WordCount = _wordCounterService.WordCount;

        }
    }
}
