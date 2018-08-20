using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Write_A_Thon.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WritingView : Page
    {

        
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }


        // Using a DependencyProperty as the backing store for RichText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RichEditBox), new PropertyMetadata(string.Empty, RichTextCallBack));

        private static void RichTextCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var reb = (RichEditBox)d;
            if (e.NewValue != e.OldValue)
            {
                reb.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, (string)e.NewValue);
            }

        }




        public WritingView()
        {
            this.InitializeComponent();

        }

        private void WritingBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var reb = sender as RichEditBox;
            string value = string.Empty;
            reb.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out value);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }
            SetRichText(reb, value);
        }
    }
}
