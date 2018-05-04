using System;
using UWP.UnwantedToolkit.Controls;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWP.UnwantedToolkit.SampleApp.ControlPages
{
    public sealed partial class RemoteDevicesPicker : Page
    {
        public RemoteDevicesPicker()
        {
            this.InitializeComponent();
        }

        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RemoteDevicePicker remoteDevicePicker = new RemoteDevicePicker()
            {
                Title = "Pick Remote Device",
                DeviceListSelectionMode = ListViewSelectionMode.Extended
            };
            remoteDevicePicker.RemoteDevicePickerClosed += RemoteDevicePicker_RemoteDevicePickerClosed;
            await remoteDevicePicker.ShowAsync();
        }

        private async void RemoteDevicePicker_RemoteDevicePickerClosed(object sender, RemoteDevicePickerEventArgs e)
        {
            await new MessageDialog(e.Devices.Count.ToString()).ShowAsync();
        }
    }
}
