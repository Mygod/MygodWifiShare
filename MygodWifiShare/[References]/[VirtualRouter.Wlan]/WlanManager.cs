/*
* Virtual Router v1.0 - http://virtualrouter.codeplex.com
* Wifi Hot Spot for Windows 8, 7 and 2008 R2
* Copyright (c) 2013 Chris Pietschmann (http://pietschsoft.com)
* Licensed under the Microsoft Public License (Ms-PL)
* http://virtualrouter.codeplex.com/license
*/

using System;
using System.Runtime.InteropServices;
using VirtualRouter.Wlan.WinAPI;

namespace VirtualRouter.Wlan
{
    public sealed class WlanManager : IDisposable
    {
        private uint serverVersion;
        private IntPtr wlanHandle;

        public WlanManager()
        {
            Init();
        }

        private void Init()
        {
            try
            {
                WlanUtils.ThrowOnWin32Error(wlanapi.WlanOpenHandle(wlanapi.WLAN_CLIENT_VERSION_VISTA, IntPtr.Zero, out serverVersion,
                    out wlanHandle));

                WLAN_HOSTED_NETWORK_REASON failReason = InitSettings();
                if (failReason != WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success)
                    throw new Exception("Init Error WlanHostedNetworkInitSettings: " + failReason);
            }
            catch
            {
                wlanapi.WlanCloseHandle(wlanHandle, IntPtr.Zero);
                throw;
            }
        }

        #region "Public Methods"

        public WLAN_HOSTED_NETWORK_REASON ForceStart()
        {
            WLAN_HOSTED_NETWORK_REASON failReason;
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkForceStart(wlanHandle, out failReason, IntPtr.Zero));

            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON ForceStop()
        {
            WLAN_HOSTED_NETWORK_REASON failReason;
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkForceStop(wlanHandle, out failReason, IntPtr.Zero));

            return failReason;
        }

        private WLAN_HOSTED_NETWORK_REASON InitSettings()
        {
            WLAN_HOSTED_NETWORK_REASON failReason;
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkInitSettings(wlanHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON SetSecondaryKey(string passKey)
        {
            WLAN_HOSTED_NETWORK_REASON failReason;

            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkSetSecondaryKey(wlanHandle, (uint) (passKey.Length + 1), passKey, true,
                true, out failReason, IntPtr.Zero));

            return failReason;
        }

        public WLAN_HOSTED_NETWORK_STATUS QueryStatus()
        {
            IntPtr ptr;
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkQueryStatus(wlanHandle, out ptr, IntPtr.Zero));
            var netStat = (WLAN_HOSTED_NETWORK_STATUS_TEMP)Marshal.PtrToStructure(ptr, typeof(WLAN_HOSTED_NETWORK_STATUS_TEMP));
            var stat = new WLAN_HOSTED_NETWORK_STATUS();
            if ((stat.HostedNetworkState = netStat.HostedNetworkState) != WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable)
            {
                stat.IPDeviceID = netStat.IPDeviceID;
                stat.wlanHostedNetworkBSSID = netStat.wlanHostedNetworkBSSID;
                if (netStat.HostedNetworkState == WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active)
                {
                    stat.dot11PhyType = netStat.dot11PhyType;
                    stat.ulChannelFrequency = netStat.ulChannelFrequency;
                    stat.dwNumberOfPeers = netStat.dwNumberOfPeers;
                    stat.PeerList = new WLAN_HOSTED_NETWORK_PEER_STATE[stat.dwNumberOfPeers];
                    var offset = Marshal.SizeOf(typeof(WLAN_HOSTED_NETWORK_STATUS_TEMP));
                    for (var i = 0; i < netStat.dwNumberOfPeers; i++)
                        offset += Marshal.SizeOf(stat.PeerList[i] = (WLAN_HOSTED_NETWORK_PEER_STATE)Marshal.PtrToStructure(new IntPtr(ptr.ToInt64() + offset), typeof(WLAN_HOSTED_NETWORK_PEER_STATE)));
                }
            }
            return stat;
        }

        public WLAN_HOSTED_NETWORK_REASON SetConnectionSettings(string hostedNetworkSSID, int maxNumberOfPeers)
        {
            WLAN_HOSTED_NETWORK_REASON failReason;
            var settings = new WLAN_HOSTED_NETWORK_CONNECTION_SETTINGS
            {
                hostedNetworkSSID = WlanUtils.ConvertStringToDOT11_SSID(hostedNetworkSSID), dwMaxNumberOfPeers = (uint) maxNumberOfPeers
            };
            var settingsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(settings));
            Marshal.StructureToPtr(settings, settingsPtr, false);
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkSetProperty(wlanHandle,
                                        WLAN_HOSTED_NETWORK_OPCODE.wlan_hosted_network_opcode_connection_settings,
                                        (uint) Marshal.SizeOf(settings), settingsPtr, out failReason, IntPtr.Zero));
            return failReason;
        }

        public WLAN_HOSTED_NETWORK_REASON RefreshSecuritySettings()
        {
            WLAN_HOSTED_NETWORK_REASON failReason;
            WlanUtils.ThrowOnWin32Error(wlanapi.WlanHostedNetworkRefreshSecuritySettings(wlanHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (wlanHandle != IntPtr.Zero)
                wlanapi.WlanCloseHandle(wlanHandle, IntPtr.Zero);
        }

        #endregion
    }
}