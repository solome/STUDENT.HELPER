using 学生助手.STUDENT.HELPER.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.Xaml.Media.Imaging;
using SQLite;

namespace 学生助手.STUDENT.HELPER
{
    public sealed partial class SplitPage : 学生助手.STUDENT.HELPER.Common.LayoutAwarePage
    {
        public SplitPage()
        {
            this.InitializeComponent();
        }

        #region Page state management

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            Random random = new Random();
            Uri uri = new Uri("ms-appx:///Assets/background_" + random.Next(1,7) +".jpg", UriKind.RelativeOrAbsolute);
            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(uri);
            this.SplitPage_Main_Grid.Background = background;
            var group = SampleDataSource.GetGroup((String)navigationParameter);
            this.DefaultViewModel["Group"] = group;
            this.DefaultViewModel["Items"] = group.Items;

            if (pageState == null)
            {
                this.itemListView.SelectedItem = null;
                if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                if (pageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    var selectedItem = SampleDataSource.GetItem((String)pageState["SelectedItem"]);
                    this.itemsViewSource.View.MoveCurrentTo(selectedItem);
                }
            }
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            if (this.itemsViewSource.View != null)
            {
                var selectedItem = (SampleDataItem)this.itemsViewSource.View.CurrentItem;
                if (selectedItem != null) pageState["SelectedItem"] = selectedItem.UniqueId;
            }
        }

        #endregion

        #region Logical page navigation

        private bool UsingLogicalPageNavigation(ApplicationViewState? viewState = null)
        {
            if (viewState == null) viewState = ApplicationView.Value;
            return viewState == ApplicationViewState.FullScreenPortrait ||
                viewState == ApplicationViewState.Snapped;
        }

        void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation()) this.InvalidateVisualState();
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation() && itemListView.SelectedItem != null)
            {
                this.itemListView.SelectedItem = null;
            }
            else
            {
                base.GoBack(sender, e);
            }
        }

        protected override string DetermineVisualState(ApplicationViewState viewState)
        {
            var logicalPageBack = this.UsingLogicalPageNavigation(viewState) && this.itemListView.SelectedItem != null;
            var physicalPageBack = this.Frame != null && this.Frame.CanGoBack;
            this.DefaultViewModel["CanGoBack"] = logicalPageBack || physicalPageBack;

            if (viewState == ApplicationViewState.Filled ||
                viewState == ApplicationViewState.FullScreenLandscape)
            {
                var windowWidth = Window.Current.Bounds.Width;
                if (windowWidth >= 1366) return "FullScreenLandscapeOrWide";
                return "FilledOrNarrow";
            }

            var defaultStateName = base.DetermineVisualState(viewState);
            return logicalPageBack ? defaultStateName + "_Detail" : defaultStateName;
        }

        #endregion

        private void AppBarAddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdateDataPage));
        }
        private void AppBarEditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UpdatePage));
        }

        private void AppBarHomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HelpBasicPage));
        }
    }
}
