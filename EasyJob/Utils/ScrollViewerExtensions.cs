using System;
using System.Windows;
using System.Windows.Controls;

namespace EasyJob.Utils
{
    public static class ScrollViewerExtensions
    {
        public static readonly DependencyProperty AlwaysScrollToEndProperty = DependencyProperty.RegisterAttached("AlwaysScrollToEnd",
            typeof(bool), typeof(ScrollViewerExtensions), new PropertyMetadata(false, AlwaysScrollToEndChanged));

        private static bool _autoScroll;

        public static bool GetAlwaysScrollToEnd(this ScrollViewer scroll) => (bool)scroll.GetValue(AlwaysScrollToEndProperty);

        public static void SetAlwaysScrollToEnd(this ScrollViewer scroll, bool alwaysScrollToEnd) =>
            scroll.SetValue(AlwaysScrollToEndProperty, alwaysScrollToEnd);

        private static void AlwaysScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ScrollViewer scroll)
            {
                var alwaysScrollToEnd = e.NewValue != null && (bool)e.NewValue;
                if (alwaysScrollToEnd)
                {
                    scroll.ScrollToEnd();
                    scroll.ScrollChanged += ScrollChanged;
                }
                else
                {
                    scroll.ScrollChanged -= ScrollChanged;
                }
            }
            else
            {
                throw new InvalidOperationException(
                    "The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }
        }

        private static void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender is not ScrollViewer scroll)
            {
                throw new InvalidOperationException(
                    "The attached AlwaysScrollToEnd property can only be applied to ScrollViewer instances.");
            }

            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {
                _autoScroll = scroll.VerticalOffset == scroll.ScrollableHeight;
            }

            // Content scroll event : autoscroll eventually
            if (_autoScroll && e.ExtentHeightChange != 0)
            {
                scroll.ScrollToVerticalOffset(scroll.ExtentHeight);
            }
        }
    }
}
