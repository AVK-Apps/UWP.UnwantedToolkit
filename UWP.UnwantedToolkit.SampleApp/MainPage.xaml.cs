using System;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace UWP.UnwantedToolkit.SampleApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void nvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            Type type = Type.GetType("UWP.UnwantedToolkit.SampleApp.ControlPages." + item.Tag.ToString());
            contentFrame.Navigate(type);
        }
    }
}
