using System;
using System.Runtime.InteropServices;

namespace Mygod.WifiShare
{
    public static class WlanNativeMethods
    {
        public delegate void WlanNotificationCallback(ref WlanNotificationData notificationData, IntPtr context);

        /// <summary>
        /// </summary>
        /// <param name="hClientHandle"></param>
        /// <param name="pReserved">Must pass in IntPtr.Zero</param>
        /// <returns></returns>
        [DllImport("Wlanapi.dll", EntryPoint = "WlanCloseHandle")]
        public static extern int WlanCloseHandle([In] IntPtr hClientHandle, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStart")]
        public static extern int WlanHostedNetworkForceStart(IntPtr hClientHandle,
            [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkForceStop")]
        public static extern int WlanHostedNetworkForceStop(IntPtr hClientHandle,
            [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkInitSettings")]
        public static extern int WlanHostedNetworkInitSettings(IntPtr hClientHandle,
            [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkQueryProperty")]
        public static extern int WlanHostedNetworkQueryProperty(IntPtr hClientHandle, WlanHostedNetworkOpcode opCode,
            [Out] out uint pDataSize, [Out] out IntPtr ppvData, [Out] out WlanOpcodeValueType pWlanOpcodeValueType,
            IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkQuerySecondaryKey")]
        public static extern int WlanHostedNetworkQuerySecondaryKey(IntPtr hClientHandle, [Out] out uint pKeyLength,
            [Out, MarshalAs(UnmanagedType.LPStr)] out string ppucKeyData, [Out] out bool pbIsPassPhrase,
            [Out] out bool pbPersistent, [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkQueryStatus")]
        public static extern int WlanHostedNetworkQueryStatus(IntPtr hClientHandle,
            [Out] out IntPtr ppWlanHostedNetworkStatus, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkRefreshSecuritySettings")]
        public static extern int WlanHostedNetworkRefreshSecuritySettings(IntPtr hClientHandle,
            [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkSetProperty")]
        public static extern int WlanHostedNetworkSetProperty(IntPtr hClientHandle, WlanHostedNetworkOpcode opCode,
            uint dwDataSize, IntPtr pvData, [Out] out WlanHostedNetworkReason pFailReason, IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanHostedNetworkSetSecondaryKey")]
        public static extern int WlanHostedNetworkSetSecondaryKey(IntPtr hClientHandle, uint dwKeyLength,
            byte[] pucKeyData, bool bIsPassPhrase, bool bPersistent, [Out] out WlanHostedNetworkReason pFailReason,
            IntPtr pReserved);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanOpenHandle")]
        public static extern int WlanOpenHandle(uint dwClientVersion, IntPtr pReserved,
            [Out] out uint pdwNegotiatedVersion, [Out] out IntPtr clientHandle);

        [DllImport("Wlanapi.dll", EntryPoint = "WlanRegisterNotification")]
        public static extern int WlanRegisterNotification(IntPtr hClientHandle, WlanNotificationSource dwNotifSource,
            bool bIgnoreDuplicate,
            WlanNotificationCallback funcCallback, IntPtr pCallbackContext, IntPtr pReserved,
            [Out] out WlanNotificationSource pdwPrevNotifSource);
    }

    public enum Dot11AuthAlgorithm
    {
        /// DOT11_AUTH_ALGO_80211_OPEN -> 1
        Open = 1,

        /// DOT11_AUTH_ALGO_80211_SHARED_KEY -> 2
        SharedKey = 2,

        /// DOT11_AUTH_ALGO_WPA -> 3
        Wpa = 3,

        /// DOT11_AUTH_ALGO_WPA_PSK -> 4
        WpaPsk = 4,

        /// DOT11_AUTH_ALGO_WPA_NONE -> 5
        WpaNone = 5,

        /// DOT11_AUTH_ALGO_RSNA -> 6
        Rsna = 6,

        /// DOT11_AUTH_ALGO_RSNA_PSK -> 7
        RsnaPsk = 7,
    }

    public enum Dot11CipherAlgorithm
    {
        /// DOT11_CIPHER_ALGO_NONE -> 0x00
        None = 0,

        /// DOT11_CIPHER_ALGO_WEP40 -> 0x01
        Wep40 = 1,

        /// DOT11_CIPHER_ALGO_TKIP -> 0x02
        Tkip = 2,

        /// DOT11_CIPHER_ALGO_CCMP -> 0x04
        Ccmp = 4,

        /// DOT11_CIPHER_ALGO_WEP104 -> 0x05
        Wep104 = 5,

        /// DOT11_CIPHER_ALGO_WPA_USE_GROUP -> 0x100
        WpaUseGroup = 256,

        /// DOT11_CIPHER_ALGO_WEP -> 0x101
        Wep = 257,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Dot11MacAddress
    {
        private readonly byte one;
        private readonly byte two;
        private readonly byte three;
        private readonly byte four;
        private readonly byte five;
        private readonly byte six;

        public override string ToString()
        {
            return string.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}", one, two, three, four, five, six);
        }
    }

    public enum Dot11PhyType : uint
    {
        Fhss = 1,
        Dsss = 2,
        Irbaseband = 3,
        Ofdm = 4,
        Hrdsss = 5,
        Erp = 6,
        Ht = 7,
        Vht = 8
    }

    //http://msdn.microsoft.com/en-us/library/ms706027%28VS.85%29.aspx
    public enum Dot11RadioState
    {
        Unknown,
        On,
        Off
    }

    [StructLayout(LayoutKind.Sequential)] //, CharSet = CharSet.Ansi)]
    public struct Ssid
    {
        /// ULONG->unsigned int
        public int Length; //uint

        /// UCHAR[]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Content;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        //public byte[] ucSSID;
    }

    [StructLayout(LayoutKind.Sequential)] //, CharSet =  CharSet.Unicode)]
    public struct WlanHostedNetworkConnectionSettings
    {
        public Ssid HostedNetworkSSID;
        public UInt32 MaxNumberOfPeers; // DWORD
    }

    //http://msdn.microsoft.com/en-us/library/dd439500%28VS.85%29.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanHostedNetworkDataPeerStateChange
    {
        public WlanHostedNetworkPeerState OldState;
        public WlanHostedNetworkPeerState NewState;
        public readonly WlanHostedNetworkReason Reason; //NewState;
    }

    //http://msdn.microsoft.com/en-us/library/dd439501%28VS.85%29.aspx
    public enum WlanHostedNetworkNotificationCode
    {
        /// <summary>
        ///     The Hosted Network state has changed.
        /// </summary>
        StateChange = 0x00001000,

        /// <summary>
        ///     The Hosted Network peer state has changed.
        /// </summary>
        PeerStateChange,

        /// <summary>
        ///     The Hosted Network radio state has changed.
        /// </summary>
        RadioStateChange
    }

    public enum WlanHostedNetworkOpcode
    {
        ConnectionSettings,
        SecuritySettings,
        StationProfile,
        Enable
    }

    //http://msdn.microsoft.com/en-us/library/dd439503%28VS.85%29.aspx
    public enum WlanHostedNetworkPeerAuthState
    {
        Invalid,
        Authenticated
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkPeerState
    {
        public Dot11MacAddress PeerMacAddress;
        public readonly WlanHostedNetworkPeerAuthState PeerAuthState;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanHostedNetworkRadioState
    {
        public readonly Dot11RadioState dot11SoftwareRadioState;
        public readonly Dot11RadioState dot11HardwareRadioState;
    }

    //http://msdn.microsoft.com/en-us/library/dd439506%28VS.85%29.aspx
    public enum WlanHostedNetworkReason
    {
        Success = 0,
        Unspecified,
        BadParameters,
        ServiceShuttingDown,
        InsufficientResources,
        ElevationRequired,
        ReadOnly,
        PersistenceFailed,
        CryptError,
        Impersonation,
        StopBeforeStart,
        InterfaceAvailable,
        InterfaceUnavailable,
        MiniportStopped,
        MiniportStarted,
        IncompatibleConnectionStarted,
        IncompatibleConnectionStopped,
        UserAction,
        ClientAbort,
        ApStartFailed,
        PeerArrived,
        PeerDeparted,
        PeerTimeout,
        GpDenied,
        ServiceUnavailable,
        DeviceChange,
        PropertiesChange,
        VirtualStationBlockingUse,
        ServiceAvailableOnVirtualStation
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanHostedNetworkSecuritySettings
    {
        public readonly Dot11AuthAlgorithm Dot11AuthAlgo;
        public readonly Dot11CipherAlgorithm Dot11CipherAlgo;
    }

    //http://msdn.microsoft.com/en-us/library/dd439508%28VS.85%29.aspx
    public enum WlanHostedNetworkState
    {
        Unavailable,
        Idle,
        Active
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanHostedNetworkStateChange
    {
        public readonly WlanHostedNetworkState OldState;
        public readonly WlanHostedNetworkState NewState;
        public readonly WlanHostedNetworkReason Reason; // NewState;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanHostedNetworkStatus
    {
        public WlanHostedNetworkState HostedNetworkState;
        public Guid IPDeviceID;
        public Dot11MacAddress wlanHostedNetworkBSSID;
        public Dot11PhyType dot11PhyType;
        public uint ulChannelFrequency; // ULONG
        public uint dwNumberOfPeers; // DWORD
        public WlanHostedNetworkPeerState[] PeerList;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct WlanHostedNetworkStatusTemp
    {
        public readonly WlanHostedNetworkState HostedNetworkState;
        public Guid IPDeviceID;
        public Dot11MacAddress wlanHostedNetworkBSSID;
        public readonly Dot11PhyType dot11PhyType;
        public readonly uint ChannelFrequency; // ULONG
        public readonly uint NumberOfPeers; // DWORD
    }

    /// <summary>
    ///     Contains information provided when registering for notifications.
    /// </summary>
    /// <remarks>
    ///     Corresponds to the native <c>WLAN_NOTIFICATION_DATA</c> type.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanNotificationData
    {
        /// <summary>
        ///     Specifies where the notification comes from.
        /// </summary>
        public readonly WlanNotificationSource notificationSource;

        /// <summary>
        ///     Indicates the type of notification. The value of this field indicates what type of associated data will
        ///     be present in <see cref="dataPtr" />.
        /// </summary>
        public readonly int notificationCode;

        /// <summary>
        ///     Indicates which interface the notification is for.
        /// </summary>
        private readonly Guid interfaceGuid;

        /// <summary>
        ///     Specifies the size of <see cref="dataPtr" />, in bytes.
        /// </summary>
        public readonly int dataSize;

        /// <summary>
        ///     Pointer to additional data needed for the notification, as indicated by
        /// <see cref="notificationCode" />.
        /// </summary>
        public IntPtr dataPtr;
    }

    /// <summary>
    ///     Specifies where the notification comes from.
    /// </summary>
    [Flags]
    public enum WlanNotificationSource : uint
    {
        /// <summary>
        ///     All notifications, including those generated by the 802.1X module.
        /// </summary>
        All = 0X0000FFFF,

        /// <summary>
        ///     Notifications generated by the wireless Hosted Network.
        /// </summary>
        Hnwk = 0X00000080,
    }

    public enum WlanOpcodeValueType
    {
        QueryOnly = 0,
        SetByGroupPolicy,
        SetByUser,
        Invalid
    }
}