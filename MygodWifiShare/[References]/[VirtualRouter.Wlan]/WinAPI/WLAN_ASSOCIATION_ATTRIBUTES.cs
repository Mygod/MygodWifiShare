using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_ASSOCIATION_ATTRIBUTES
    {
        private readonly DOT11_SSID dot11Ssid;
        private readonly DOT11_BSS_TYPE dot11BssType;
        private readonly DOT11_MAC_ADDRESS dot11Bssid;
        private readonly DOT11_PHY_TYPE dot11PhyType;
        private readonly uint uDot11PhyIndex; //ULONG
        private readonly uint wlanSignalQuality; //WLAN_SIGNAL_QUALITY -> ULONG
        private readonly uint ulRxRate; //ULONG
        private readonly uint ulTxRate; //ULONG
    }
}