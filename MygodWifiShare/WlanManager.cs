using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Mygod.WifiShare
{
    public static class WlanManager
    {
        private static readonly IntPtr WlanHandle;
        public static readonly StringBuilder InternalLog = new StringBuilder();

        static WlanManager()
        {
            try
            {
                uint serverVersion;
                Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanOpenHandle(2, IntPtr.Zero, out serverVersion, out WlanHandle));
                // WLAN_CLIENT_VERSION_VISTA: Client version for Windows Vista and Windows Server 2008
                WlanNotificationSource notifSource;
                Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanRegisterNotification(WlanHandle, WlanNotificationSource.All, true, OnNotification, IntPtr.Zero, IntPtr.Zero, out notifSource));
                var failReason = InitSettings();
                if (failReason != WlanHostedNetworkReason.Success)
                    throw new Exception("Init Error WlanHostedNetworkInitSettings: " + failReason);
                AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
                {
                    if (WlanHandle != IntPtr.Zero) Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanCloseHandle(WlanHandle, IntPtr.Zero));
                };
            }
            catch
            {
                WlanNativeMethods.WlanCloseHandle(WlanHandle, IntPtr.Zero);
                throw;
            }
        }

        private static Ssid ToDOT11_SSID(string ssid)
        {
            return new Ssid
            {
                Content = ssid,
                Length = ssid.Length
            };
        }
        public static string ToString(WlanHostedNetworkReason reason)
        {
            switch (reason)
            {
                case WlanHostedNetworkReason.Success: return "操作成功。";
                case WlanHostedNetworkReason.Unspecified: return "未知错误。";
                case WlanHostedNetworkReason.BadParameters: return "参数错误。";
                case WlanHostedNetworkReason.ServiceShuttingDown: return "服务正在关闭。";
                case WlanHostedNetworkReason.InsufficientResources: return "服务资源不足。";
                case WlanHostedNetworkReason.ElevationRequired: return "当前操作需要提升权限。";
                case WlanHostedNetworkReason.ReadOnly: return "尝试修改只读数据。";
                case WlanHostedNetworkReason.PersistenceFailed: return "数据持久化失败。";
                case WlanHostedNetworkReason.CryptError: return "加密时出现错误。";
                case WlanHostedNetworkReason.Impersonation: return "用户模拟失败。";
                case WlanHostedNetworkReason.StopBeforeStart: return "函数调用顺序错误。";
                case WlanHostedNetworkReason.InterfaceAvailable: return "无线接口可用。";
                case WlanHostedNetworkReason.InterfaceUnavailable: return "无线接口不可用。";
                case WlanHostedNetworkReason.MiniportStopped: return "无线微型端口驱动程序终止了托管网络。";
                case WlanHostedNetworkReason.MiniportStarted: return "无线微型端口驱动程序状态已改变。";
                case WlanHostedNetworkReason.IncompatibleConnectionStarted:
                    return "开始了一个不兼容的连接。";
                case WlanHostedNetworkReason.IncompatibleConnectionStopped:
                    return "一个不兼容的连接已停止。";
                case WlanHostedNetworkReason.UserAction: return "由于用户操作，状态已改变。";
                case WlanHostedNetworkReason.ClientAbort: return "由于客户端终止，状态已改变。";
                case WlanHostedNetworkReason.ApStartFailed: return "无线托管网络驱动启动失败。";
                case WlanHostedNetworkReason.PeerArrived: return "一个客户端已连接到无线托管网络。";
                case WlanHostedNetworkReason.PeerDeparted: return "一个客户端已从无线托管网络断开连接。";
                case WlanHostedNetworkReason.PeerTimeout: return "一个客户端连接超时。";
                case WlanHostedNetworkReason.GpDenied: return "操作被组策略禁止。";
                case WlanHostedNetworkReason.ServiceUnavailable: return "无线局域网服务未运行。";
                case WlanHostedNetworkReason.DeviceChange: return "无线托管网络所使用的无线适配器已改变。";
                case WlanHostedNetworkReason.PropertiesChange: return "无线托管网络所使用的属性已改变。";
                case WlanHostedNetworkReason.VirtualStationBlockingUse:
                    return "一个活动的虚拟站阻止了操作。";
                case WlanHostedNetworkReason.ServiceAvailableOnVirtualStation:
                    return "在虚拟站上一个相同的服务已可用。";
                default: return "未知的 WLAN_HOSTED_NETWORK_REASON。";
            }
        }
        public static string ToString(WlanHostedNetworkState state)
        {
            switch (state)
            {
                case WlanHostedNetworkState.Unavailable: return "不可用";
                case WlanHostedNetworkState.Idle: return "未启用";
                case WlanHostedNetworkState.Active: return "已启用";
                default: return "未知的 WLAN_HOSTED_NETWORK_STATE";
            }
        }
        public static string ToString(Dot11PhyType type)
        {
            switch (type)
            {
                case Dot11PhyType.Fhss: return "FHSS";
                case Dot11PhyType.Dsss: return "DSSS";
                case Dot11PhyType.Irbaseband: return "红外基带";
                case Dot11PhyType.Ofdm: return "OFDM";
                case Dot11PhyType.Hrdsss: return "HRDSSS";
                case Dot11PhyType.Erp: return "ERP";
                case Dot11PhyType.Ht: return "802.11n";
                case Dot11PhyType.Vht: return "802.11ac";
                default: return (int)type < 0 ? "IHV" : "未知的 DOT11_PHY_TYPE";
            }
        }
        public static string ToString(WlanHostedNetworkPeerAuthState state)
        {
            switch (state)
            {
                case WlanHostedNetworkPeerAuthState.Invalid: return "无效";
                case WlanHostedNetworkPeerAuthState.Authenticated: return "已认证";
                default: return "未知的 WLAN_HOSTED_NETWORK_PEER_AUTH_STATE";
            }
        }
        private static string ToString(Dot11RadioState state)
        {
            switch (state)
            {
                case Dot11RadioState.On: return "开";
                case Dot11RadioState.Off: return "关";
                case Dot11RadioState.Unknown: return "未知";
                default: return "未知的 DOT11_RADIO_STATE";
            }
        }
        public static string ToString(WlanOpcodeValueType opCode)
        {
            switch (opCode)
            {
                case WlanOpcodeValueType.QueryOnly: return "未知";
                case WlanOpcodeValueType.SetByGroupPolicy: return "组策略设置";
                case WlanOpcodeValueType.SetByUser: return "用户自定义";
                case WlanOpcodeValueType.Invalid: return "无效";
                default: return "未知的 WLAN_OPCODE_VALUE_TYPE";
            }
        }
        public static string ToString(Dot11AuthAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case Dot11AuthAlgorithm.Open: return "IEEE 802.11 开放式系统";
                case Dot11AuthAlgorithm.SharedKey: return "802.11 共享密钥认证算法";
                case Dot11AuthAlgorithm.Wpa: return "WPA";
                case Dot11AuthAlgorithm.WpaPsk: return "使用 PSK 的 WPA";
                case Dot11AuthAlgorithm.WpaNone: return "无 WPA";
                case Dot11AuthAlgorithm.Rsna: return "802.11i RSNA";
                case Dot11AuthAlgorithm.RsnaPsk: return "使用 PSK 的 802.11i RSNA";
                default: return algorithm < 0 ? "IHV" : "未知的 WLAN_OPCODE_VALUE_TYPE";
            }
        }
        public static string ToString(Dot11CipherAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case Dot11CipherAlgorithm.None: return "无";
                case Dot11CipherAlgorithm.Wep40: return "WEP (40 位密钥)";
                case Dot11CipherAlgorithm.Tkip: return "TKIP";
                case Dot11CipherAlgorithm.Ccmp: return "AES-CCMP";
                case Dot11CipherAlgorithm.Wep104: return "WEP (104 位密钥)";
                case Dot11CipherAlgorithm.WpaUseGroup: return "WPA/RSN (使用组密钥)";
                case Dot11CipherAlgorithm.Wep: return "WEP";
                default: return algorithm < 0 ? "IHV" : "未知的 DOT11_CIPHER_ALGORITHM";
            }
        }

        private static void OnNotification(ref WlanNotificationData notifData, IntPtr context)
        {
            if (notifData.dataSize <= 0 || notifData.dataPtr == IntPtr.Zero
                || notifData.notificationSource != WlanNotificationSource.Hnwk) return;
            lock (InternalLog)
            {
                InternalLog.AppendFormat("[{0}]\t", DateTime.Now.ToString("yyyy.M.d H:mm:ss"));
                switch ((WlanHostedNetworkNotificationCode)notifData.notificationCode)
                {
                    case WlanHostedNetworkNotificationCode.StateChange:
                        var pStateChange = (WlanHostedNetworkStateChange)
                            Marshal.PtrToStructure(notifData.dataPtr, typeof(WlanHostedNetworkStateChange));
                        InternalLog.Append("托管网络状态已改变：" + ToString(pStateChange.OldState));
                        if (pStateChange.OldState != pStateChange.NewState)
                            InternalLog.Append(" => " + ToString(pStateChange.NewState));
                        InternalLog.AppendLine("；原因：" + ToString(pStateChange.Reason));
                        break;
                    case WlanHostedNetworkNotificationCode.PeerStateChange:
                        var pPeerStateChange = (WlanHostedNetworkDataPeerStateChange)
                            Marshal.PtrToStructure(notifData.dataPtr, typeof(WlanHostedNetworkDataPeerStateChange));
                        var lookup = Program.Lookup;
                        InternalLog.AppendLine(string.Format("客户端已改变。原因：{0}\n{1} ====>\n{2}", ToString(pPeerStateChange.Reason),
                                               Program.GetDeviceDetails(pPeerStateChange.OldState, true, lookup),
                                               Program.GetDeviceDetails(pPeerStateChange.NewState, true, lookup)));
                        break;
                    case WlanHostedNetworkNotificationCode.RadioStateChange:
                        var pRadioState = (WlanHostedNetworkRadioState)
                            Marshal.PtrToStructure(notifData.dataPtr, typeof(WlanHostedNetworkRadioState));
                        InternalLog.AppendLine(string.Format("无线状态已改变。软件开关：{0}；硬件开关：{1}。",
                            ToString(pRadioState.dot11SoftwareRadioState), ToString(pRadioState.dot11HardwareRadioState)));
                        break;
                    default:
                        InternalLog.AppendLine("具体事件未知。");
                        break;
                }
                InternalLog.AppendLine();
            }
        }

        public static WlanHostedNetworkReason ForceStart()
        {
            WlanHostedNetworkReason failReason;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkForceStart(WlanHandle, out failReason, IntPtr.Zero));

            return failReason;
        }

        public static WlanHostedNetworkReason ForceStop()
        {
            WlanHostedNetworkReason failReason;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkForceStop(WlanHandle, out failReason, IntPtr.Zero));

            return failReason;
        }

        private static WlanHostedNetworkReason InitSettings()
        {
            WlanHostedNetworkReason failReason;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkInitSettings(WlanHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanHostedNetworkReason QuerySecondaryKey(out string passKey, out bool isPassPhrase, out bool isPersistent)
        {
            WlanHostedNetworkReason failReason;
            uint keyLen;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkQuerySecondaryKey(WlanHandle, out keyLen, out passKey,
                                            out isPassPhrase, out isPersistent, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanHostedNetworkReason SetSecondaryKey(string passKey, bool isPassPhrase = true, bool isPersistent = true)
        {
            WlanHostedNetworkReason failReason;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkSetSecondaryKey(WlanHandle, (uint)(passKey.Length + 1), passKey,
                                            isPassPhrase, isPersistent, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanHostedNetworkStatus QueryStatus()
        {
            IntPtr ptr;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkQueryStatus(WlanHandle, out ptr, IntPtr.Zero));
            var netStat = (WlanHostedNetworkStatusTemp)Marshal.PtrToStructure(ptr, typeof(WlanHostedNetworkStatusTemp));
            var stat = new WlanHostedNetworkStatus();
            if ((stat.HostedNetworkState = netStat.HostedNetworkState) != WlanHostedNetworkState.Unavailable)
            {
                stat.IPDeviceID = netStat.IPDeviceID;
                stat.wlanHostedNetworkBSSID = netStat.wlanHostedNetworkBSSID;
                if (netStat.HostedNetworkState == WlanHostedNetworkState.Active)
                {
                    stat.dot11PhyType = netStat.dot11PhyType;
                    stat.ulChannelFrequency = netStat.ChannelFrequency;
                    stat.dwNumberOfPeers = netStat.NumberOfPeers;
                    stat.PeerList = new WlanHostedNetworkPeerState[stat.dwNumberOfPeers];
                    var offset = Marshal.SizeOf(typeof(WlanHostedNetworkStatusTemp));
                    for (var i = 0; i < netStat.NumberOfPeers; i++)
                        offset += Marshal.SizeOf(stat.PeerList[i] = (WlanHostedNetworkPeerState)Marshal.PtrToStructure(new IntPtr(ptr.ToInt64() + offset), typeof(WlanHostedNetworkPeerState)));
                }
            }
            return stat;
        }

        public static WlanHostedNetworkReason SetConnectionSettings(string hostedNetworkSSID, int maxNumberOfPeers)
        {
            WlanHostedNetworkReason failReason;
            var settings = new WlanHostedNetworkConnectionSettings
            {
                HostedNetworkSSID = ToDOT11_SSID(hostedNetworkSSID), MaxNumberOfPeers = (uint) maxNumberOfPeers
            };
            var settingsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(settings));
            Marshal.StructureToPtr(settings, settingsPtr, false);
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkSetProperty(WlanHandle,
                                        WlanHostedNetworkOpcode.ConnectionSettings,
                                        (uint) Marshal.SizeOf(settings), settingsPtr, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanHostedNetworkReason SetEnabled(bool enabled)
        {
            WlanHostedNetworkReason failReason;
            var settingsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(enabled));
            Marshal.StructureToPtr(enabled, settingsPtr, false);
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkSetProperty(WlanHandle,
                                        WlanHostedNetworkOpcode.Enable,
                                        (uint)Marshal.SizeOf(enabled), settingsPtr, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanHostedNetworkReason RefreshSecuritySettings()
        {
            WlanHostedNetworkReason failReason;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkRefreshSecuritySettings(WlanHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        public static WlanOpcodeValueType QueryConnectionSettings(out WlanHostedNetworkConnectionSettings settings)
        {
            uint dataSize;
            IntPtr dataPtr;
            WlanOpcodeValueType opcode;
            var hr = WlanNativeMethods.WlanHostedNetworkQueryProperty(WlanHandle,
                WlanHostedNetworkOpcode.ConnectionSettings,
                out dataSize, out dataPtr, out opcode, IntPtr.Zero);
            if (hr == 1610) throw new BadConfigurationException();
            Marshal.ThrowExceptionForHR(hr);
            settings = (WlanHostedNetworkConnectionSettings)
                Marshal.PtrToStructure(dataPtr, typeof(WlanHostedNetworkConnectionSettings));
            return opcode;
        }

        public static WlanOpcodeValueType QuerySecuritySettings(out WlanHostedNetworkSecuritySettings settings)
        {
            uint dataSize;
            IntPtr dataPtr;
            WlanOpcodeValueType opcode;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkQueryProperty(WlanHandle,
                                        WlanHostedNetworkOpcode.SecuritySettings,
                                        out dataSize, out dataPtr, out opcode, IntPtr.Zero));
            settings = (WlanHostedNetworkSecuritySettings)
                Marshal.PtrToStructure(dataPtr, typeof(WlanHostedNetworkSecuritySettings));
            return opcode;
        }

        public static WlanOpcodeValueType QueryStationProfile(out string profile)
        {
            uint dataSize;
            IntPtr dataPtr;
            WlanOpcodeValueType opcode;
            var hr = WlanNativeMethods.WlanHostedNetworkQueryProperty(WlanHandle,
                WlanHostedNetworkOpcode.StationProfile,
                out dataSize, out dataPtr, out opcode, IntPtr.Zero);
            if (hr == 1610) throw new BadConfigurationException();
            Marshal.ThrowExceptionForHR(hr);
            profile = Marshal.PtrToStringUni(dataPtr, (int) (dataSize >> 1));
            return opcode;
        }

        public static WlanOpcodeValueType QueryEnabled(out bool enabled)
        {
            uint dataSize;
            IntPtr dataPtr;
            WlanOpcodeValueType opcode;
            Marshal.ThrowExceptionForHR(WlanNativeMethods.WlanHostedNetworkQueryProperty(WlanHandle,
                                        WlanHostedNetworkOpcode.Enable,
                                        out dataSize, out dataPtr, out opcode, IntPtr.Zero));
            enabled = (bool)Marshal.PtrToStructure(dataPtr, typeof(bool));
            return opcode;
        }
    }

    public sealed class BadConfigurationException : Win32Exception
    {
        public BadConfigurationException() : base(1610)
        {
        }
    }
}