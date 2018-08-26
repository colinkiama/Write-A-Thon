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

        public InfoBarViewModel()
        {

        }
    }
}
