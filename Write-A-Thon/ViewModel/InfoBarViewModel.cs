using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.ViewModel
{
    public class InfoBarViewModel:NotifyingViewModel
    {
        private string _ediorText;

        public string EditorText
        {
            get { return _ediorText; }
            set { _ediorText = value; }
        }

    }
}
