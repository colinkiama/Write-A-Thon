using Write_A_Thon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Write_A_Thon.Events
{
    public delegate void ShortcutEventHandler(object sender, ShortcutEventArgs args);

    public class ShortcutEventArgs : EventArgs
    {

        public ShortcutEventArgs(ShortcutType shortcutType)
        {
            ShortcutType = shortcutType;
        }

        public ShortcutType ShortcutType { get; set; }

    }

}
