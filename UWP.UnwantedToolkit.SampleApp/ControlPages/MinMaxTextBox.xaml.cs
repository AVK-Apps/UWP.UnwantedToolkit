using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWP.UnwantedToolkit.SampleApp.ControlPages
{
    public sealed partial class MinMaxTextBox : Page
    {
        public MinMaxTextBox()
        {
            InitializeComponent();
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await new MessageDialog(minMaxTextBox.Text).ShowAsync();
        }
    }
}