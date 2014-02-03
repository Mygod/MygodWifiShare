using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    //http://msdn.microsoft.com/en-us/library/ms706851%28VS.85%29.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_CONNECTION_PARAMETERS
    {
        private readonly WLAN_CONNECTION_MODE wlanConnectionMode;
        private readonly string strProfile; // LPCWSTR
        private readonly DOT11_SSID pDot11Ssid;
        private readonly DOT11_BSSID_LIST pDesiredBssidList;
        private readonly DOT11_BSS_TYPE dot11BssType;
        private readonly uint dwFlags; // DWORD
    }
}