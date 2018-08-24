using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Write_A_Thon.Converters
{
    class IndexToStepsConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string stepsToReturn = "null";
            if (value is int selectedIndex)
            {
                stepsToReturn = $"{selectedIndex + 1}/3";
            }
            return stepsToReturn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0;
        }
    }
}
