using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DOT11_BSSID_LIST
    {
        private readonly NDIS_OBJECT_HEADER header;
        private readonly uint uNumOfEntries; // ULONG
        private readonly uint uTotalNumOfEntries; // ULONG
        private readonly DOT11_MAC_ADDRESS[] BSSIDs;
    }
}