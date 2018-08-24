using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.ViewModel
{
    class GoalSettingViewModel : NotifyingViewModel
    {
        private uint _totalWordsWritten;

        public uint TotalWordsWritten
        {
            get { return _totalWordsWritten; }
            set
            {
                _totalWordsWritten = value;
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


    }
}
