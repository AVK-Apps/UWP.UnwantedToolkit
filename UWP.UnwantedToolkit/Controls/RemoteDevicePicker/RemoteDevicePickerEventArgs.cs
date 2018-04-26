using System;
using System.Collections.ObjectModel;
using Windows.System.RemoteSystems;

namespace UWP.UnwantedToolkit.Controls
{
    /// <summary>
    /// Arguments for the OnClose event which is fired after user closes the picker.
    /// </summary>
    public class RemoteDevicePickerEventArgs : EventArgs
    {
        /// <summary>
        /// List of Devices that are selected.
        /// </summary>
        public ObservableCollection<RemoteSystem> Devices { get; }

        internal RemoteDevicePickerEventArgs(ObservableCollection<RemoteSystem> devices)
        {
            Devices = devices;
        }
    }
}