using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_HOSTED_NETWORK_RADIO_STATE
    {
        private readonly DOT11_RADIO_STATE dot11SoftwareRadioState;
        private readonly DOT11_RADIO_STATE dot11HardwareRadioState;
    }
}