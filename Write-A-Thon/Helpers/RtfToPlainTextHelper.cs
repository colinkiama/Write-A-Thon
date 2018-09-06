using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Write_A_Thon.Helpers
{
    public class RtfToPlainTextHelper
    {
        static RichEditBox richEditBox = new RichEditBox();
        public static string ConvertRtfToPlainText(string rtfContent)
        {
            richEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, rtfContent);
            richEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.None, out string plainText);
            return plainText;
        }
        
    }
}
