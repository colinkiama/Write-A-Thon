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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Write_A_Thon.Events;
using Write_A_Thon.Helpers;
using Write_A_Thon.View.GoalSettingViews;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Write_A_Thon.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GoalSettingView : Page
    {
        public static event FormEventHandler formStepChanged;
        static event EventHandler formFinished;
        public GoalSettingView()
        {
            this.InitializeComponent();
            formStepChanged += GoalSettingView_formStepChanged;
            formFinished += GoalSettingView_formFinished;
            FrameAnimationHelper.frame = formFrame;
            formFrame.Navigate(typeof(Step1View));

        }

        private void GoalSettingView_formFinished(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(Shell), null, new SlideNavigationTransitionInfo());
        }

        private void GoalSettingView_formStepChanged(object sender, FormEventArgs args)
        {
            StepProgressTextBlock.Text = $"{args.StepValue}/3";
        }

        public static void RaiseFormStepChanged(int stepValue)
        {
            formStepChanged?.Invoke(null, new FormEventArgs(stepValue));
        }

        internal static void RaiseFormFinished()
        {
            formFinished?.Invoke(null, EventArgs.Empty);
        }
    }
}
