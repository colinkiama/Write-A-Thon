using Flow_Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Services
{
    public class NavService
    {
        public static FlowFrame frame;

        public async static Task Navigate(Type pageType, object parameter)
        {
            await frame.Navigate(pageType, parameter);
        }

        public async static Task GoBack()
        {
            await frame.GoBack();
        }
    }
}
