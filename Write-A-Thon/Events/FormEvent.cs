using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Events
{
    public delegate void FormEventHandler(object sender, FormEventArgs args);

    public class FormEventArgs : EventArgs
    {
        public int StepValue { get; set; }

        public FormEventArgs(int stepValue)
        {
            StepValue = stepValue;
        }
    }

}
