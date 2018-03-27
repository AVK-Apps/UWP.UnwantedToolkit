using System;
using System.Collections.ObjectModel;
using Windows.System.RemoteSystems;

namespace UWP.UnwantedToolkit.Controls
{
    public class RemoteDevicePickerEventArgs : EventArgs
    {
        public ObservableCollection<RemoteSystem> Devices { get; }
        internal RemoteDevicePickerEventArgs(ObservableCollection<RemoteSystem> devices)
        {
            Devices = devices;
        }
    }
}