using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Write_A_Thon.Model;

namespace Write_A_Thon.ViewModel
{
    public class SettingsViewModel:NotifyingViewModel
    {
        private DateTimeOffset _selectedDate;

        public DateTimeOffset SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; }
        }

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

        public SettingsViewModel()
        {
            SelectedDate = App.InfoBarService.GoalProgress.DueDate;
            Target = App.InfoBarService.GoalProgress.TotalWordsToWrite;

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
                Target = result;
            }
        }
    }
}
