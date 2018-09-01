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

        private uint _wordCount;

        public uint WordCount
        {
            get { return _wordCount; }
            set
            {
                _wordCount = value;
                NotifyPropertyChanged();
                UpdateReadingTime();
            }
        }

        private void UpdateReadingTime()
        {
            ReadingTime = WordCount / 250;
        }

        private uint _readingTime;

        public uint ReadingTime
        {
            get { return _readingTime; }
            private set
            {
                _readingTime = value;
                NotifyPropertyChanged();
            }
        }



        public InfoBarViewModel()
        {
            _wordCounterService = App.WordCounterService;
            _wordCounterService.WordCountChanged += _wordCounterService_WordCountChanged;
        }

        private void _wordCounterService_WordCountChanged(object sender, EventArgs e)
        {
            WordCount = _wordCounterService.WordCount;

        }
    }
}
