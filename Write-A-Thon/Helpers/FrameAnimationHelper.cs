using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;

namespace Write_A_Thon.Helpers
{
    public static class FrameAnimationHelper
    {
        private static Compositor _compositor;
        public static Frame frame;

        public async static Task Navigate(Type pageType, object parameter = null)
        {
            await AnimatePageOut();
            if (CheckIfFirstForwardStackItemHasPageType(pageType))
            {
                frame.GoForward();
            }
            else
            {
                frame.Navigate(pageType, parameter, new SuppressNavigationTransitionInfo());
            }

           await AnimatePageIn();
        }

        public async static Task NavigateBack()
        {
            await AnimatePageOutReverse();
            frame.GoBack(new SuppressNavigationTransitionInfo());
            await AnimatePageInReverse();
        }

        private async static Task AnimatePageInReverse()
        {
            var newPage = frame.Content;
            if (newPage != null)
            {
                var page = newPage as FrameworkElement;

                if (_compositor == null)
                    _compositor = ElementCompositionPreview.GetElementVisual(page).Compositor;

                var visual = ElementCompositionPreview.GetElementVisual(page);

                visual.Offset = new Vector3(-140, 0, 0);
                visual.Opacity = 0f;
                visual.Scale = new Vector3(1, 1, 0);

                KeyFrameAnimation offsetInAnimation = _compositor.CreateScalarKeyFrameAnimation();
                offsetInAnimation.InsertExpressionKeyFrame(1f, "0");
                offsetInAnimation.Duration = TimeSpan.FromMilliseconds(250);


                KeyFrameAnimation fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(1f, "1");
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(250);


                visual.StartAnimation("Offset.X", offsetInAnimation);
                visual.StartAnimation("Opacity", fadeAnimation);

                await Task.Delay(fadeAnimation.Duration);
            }
        }

        private async static Task AnimatePageOutReverse()
        {
            var oldPage = frame.Content;
            if (oldPage != null)
            {
                var page = oldPage as FrameworkElement;

                if (_compositor == null)
                    _compositor = ElementCompositionPreview.GetElementVisual(page).Compositor;

                var visual = ElementCompositionPreview.GetElementVisual(page);

                string offsetToUse = $"{frame.ActualWidth}";
                string defaultOffset = "-140";

                KeyFrameAnimation offsetInAnimation = _compositor.CreateScalarKeyFrameAnimation();
                offsetInAnimation.InsertExpressionKeyFrame(1f, offsetToUse);
                offsetInAnimation.Duration = TimeSpan.FromMilliseconds(250);

                KeyFrameAnimation fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(1f, "0");
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(200);

                visual.StartAnimation("Offset.X", offsetInAnimation);
                //visual.StartAnimation("Opacity", fadeAnimation);
                await Task.Delay(fadeAnimation.Duration);
            }
        }

        private async static Task AnimatePageIn()
        {

            var newPage = frame.Content;
            if (newPage != null)
            {
                var page = newPage as FrameworkElement;

                if (_compositor == null)
                    _compositor = ElementCompositionPreview.GetElementVisual(page).Compositor;

                var visual = ElementCompositionPreview.GetElementVisual(page);

                visual.Offset = new Vector3(140, 0, 0);
                visual.Opacity = 0f;
                visual.Scale = new Vector3(1, 1, 0);

                KeyFrameAnimation offsetInAnimation = _compositor.CreateScalarKeyFrameAnimation();
                offsetInAnimation.InsertExpressionKeyFrame(1f, "0");
                offsetInAnimation.Duration = TimeSpan.FromMilliseconds(250);


                KeyFrameAnimation fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(1f, "1");
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(250);


                visual.StartAnimation("Offset.X", offsetInAnimation);
                visual.StartAnimation("Opacity", fadeAnimation);
                await Task.Delay(fadeAnimation.Duration);
            }


        }

        private async static Task AnimatePageOut()
        {
            var oldPage = frame.Content;
            if (oldPage != null)
            {
                var page = oldPage as FrameworkElement;

                if (_compositor == null)
                    _compositor = ElementCompositionPreview.GetElementVisual(page).Compositor;

                var visual = ElementCompositionPreview.GetElementVisual(page);


                KeyFrameAnimation offsetInAnimation = _compositor.CreateScalarKeyFrameAnimation();
                string offsetToUse = $"-{frame.ActualWidth}";
                offsetInAnimation.InsertExpressionKeyFrame(1f, offsetToUse);
                offsetInAnimation.Duration = TimeSpan.FromMilliseconds(250);

                KeyFrameAnimation fadeAnimation = _compositor.CreateScalarKeyFrameAnimation();
                fadeAnimation.InsertExpressionKeyFrame(1f, "0");
                fadeAnimation.Duration = TimeSpan.FromMilliseconds(200);

                visual.StartAnimation("Offset.X", offsetInAnimation);
                visual.StartAnimation("Opacity", fadeAnimation);
                await Task.Delay(offsetInAnimation.Duration);
            }
        }

        private static bool CheckIfFirstForwardStackItemHasPageType(Type pageType)
        {
            bool frameHistoryHasPageType = false;
            var pageList = frame.ForwardStack;
            if (pageList.Count > 1)
            {
                frameHistoryHasPageType = (pageList.First().SourcePageType == pageType);
            }

            return frameHistoryHasPageType;
        }
    }
}
