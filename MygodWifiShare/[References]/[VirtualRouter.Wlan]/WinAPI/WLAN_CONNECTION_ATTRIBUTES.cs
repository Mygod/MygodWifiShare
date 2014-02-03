using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    //http://msdn.microsoft.com/en-us/library/ms706842%28VS.85%29.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WLAN_CONNECTION_ATTRIBUTES
    {
        private readonly WLAN_INTERFACE_STATE isState;
        private readonly WLAN_CONNECTION_MODE wlanCOnnectionMode;
        private readonly string strProfileMode; //WCHAR[256];
        private readonly WLAN_ASSOCIATION_ATTRIBUTES wlanAssociationAttributes;
        private readonly WLAN_SECURITY_ATTRIBUTES wlanSecurityAttributes;
    }
}