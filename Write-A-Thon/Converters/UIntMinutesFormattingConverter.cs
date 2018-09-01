using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Write_A_Thon.Converters
{
    class UIntMinutesFormattingConverter: IValueConverter
    {
        const string minutesString = "Min Read";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string formattedMinutes = $"< 1 {minutesString}";
            if (value is uint minutes)
            {
                if (minutes > 0)
                {
                    formattedMinutes = $"{minutes} {minutesString}";
                }
            }
            return formattedMinutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0;
        }
    }
}
