using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_HOSTED_NETWORK_SECURITY_SETTINGS
    {
        private readonly DOT11_AUTH_ALGORITHM dot11AuthAlgo;
        private readonly DOT11_CIPHER_ALGORITHM dot11CipherAlgo;
    }
}