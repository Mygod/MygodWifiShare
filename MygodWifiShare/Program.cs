using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using ROOT.CIMV2.Win32;
using VirtualRouter.Wlan;
using VirtualRouter.Wlan.WinAPI;
using Action = System.Action;

namespace Mygod.WifiShare
{
    // ReSharper disable MemberCanBePrivate.Global
    static class Arp
    {
        [DllImport("iphlpapi.dll")]
        private static extern int GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);
        [DllImport("iphlpapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int FreeMibTable(IntPtr pIpNetTable);

        [StructLayout(LayoutKind.Sequential)]
        public struct MibIpNetRow
        {
            /// <summary>
            /// The index of the adapter.
            /// </summary>
            public readonly int Index;
            /// <summary>
            /// The length, in bytes, of the physical address.
            /// </summary>
            public readonly int PhysAddrLen;
            /// <summary>
            /// The physical address.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public readonly byte[] PhysAddr;
            /// <summary>
            /// The IPv4 address.
            /// </summary>
            public readonly uint Addr;
            /// <summary>
            /// The type of ARP entry. This type can be one of the following values.
            /// </summary>
            public readonly IPNetRowType Type;

            public string MacAddress
            {
                get { return string.Join(":", PhysAddr.Take(PhysAddrLen).Select(b => b.ToString("X2"))); }
            }
            public IPAddress IPAddress
            {
                get { return new IPAddress(Addr); }
            }

            public override string ToString()
            {
                var type = string.Empty;
                switch (Type)
                {
                    case IPNetRowType.Other:
                        type = "其他";
                        break;
                    case IPNetRowType.Invalid:
                        type = "无效";
                        break;
                    case IPNetRowType.Dynamic:
                        type = "动态";
                        break;
                    case IPNetRowType.Static:
                        type = "静态";
                        break;
                }
                return string.IsNullOrWhiteSpace(type) ? IPAddress.ToString() : string.Format("{0} ({1})", IPAddress, type);
            }
        }

        public enum IPNetRowType
        {
            Other = 1, Invalid, Dynamic, Static
        }

        public static IEnumerable<MibIpNetRow> GetIpNetTable()
        {
            int bytesNeeded = 0, result = GetIpNetTable(IntPtr.Zero, ref bytesNeeded, false);
            if (result != 122 && result != 0) throw new Win32Exception(result);
            var buffer = IntPtr.Zero;
            try
            {
                buffer = Marshal.AllocCoTaskMem(bytesNeeded);
                result = GetIpNetTable(buffer, ref bytesNeeded, false);
                if (result != 0) throw new Win32Exception(result);
                int entries = Marshal.ReadInt32(buffer), offset = Marshal.SizeOf(typeof(MibIpNetRow));
                var ptr = new IntPtr(buffer.ToInt64() + 4 - offset);
                for (var index = 0; index < entries; index++)
                    yield return (MibIpNetRow)Marshal.PtrToStructure(ptr += offset, typeof(MibIpNetRow));
            }
            finally
            {
                FreeMibTable(buffer);
            }
        }
    }
    // ReSharper restore MemberCanBePrivate.Global

    static class R
    {
        public static readonly string
            Requirement = @"{0} 需求配置：
 ※ 安装 .NET Framework 4.5.1 或更高（Windows 8.1 自带）
 ※ Windows 7 或更高版本的 Windows
 ※ 连接到 Internet
 ※ 有支持无线网络共享（按 A 查看）的 Wi-Fi 无线网络适配器",
            WelcomeToUse = @"欢迎使用 {0}！
可用操作：（输入其他退出）
    A 查看当前共享的设置与状态            B 启动/重启共享
    C 关闭共享                            D 深度重启共享
    H 更多帮助                            I 初始化设置
    K 刷新安全设置 (用后需要再次启动共享) Q 查看当前已连接的设备
    S 设置无线网络名、密码与最大客户端数  T 设置开机时自动运行
    U 检查更新                            W 监视客户端
请输入操作：",
            QuickHelp = @"一、第一次使用：输入 S 对无线网络名和密码进行设置，输入 B 启动无线网络共享，如果跳出网络选择，请选择家庭网络或工作网络，输入 I 并按照提示进行第一次配置。

二、再次使用：只需输入 B 启动无线网络共享即可。",
            Help = @"三、开机自动启动无线网络共享
0. 启动本软件。
1. 输入 T，如看到“当前设置自动启动无线网络共享。”的字样，关闭本程序，否则执行第2步。
2. 输入 Y 修改成不自动启动无线网络共享。

四、开机不自动启动无线网络共享
0. 启动本软件。
1. 输入 T，如看到“当前设置不自动启动无线网络共享。”的字样，关闭本程序，否则执行第2步。
2. 输入 Y 修改成自动启动无线网络共享。

五、可能碰到的问题：
①执行 一. 2 时出现“无法启动承载网络。组或资源的状态不是执行请求操作的正确状态。”字样：
　这可能是由于您的 Wi-Fi 没有打开，或者硬件开关被关闭导致，要解决只需打开即可。也有可能是无线网卡正在被使用导致的，这种情况下可以尝试以下步骤解决问题。
　　1. 点击右下角的“网络与共享中心”，如果当前连接到某无线网络，则选中当前连接到的无线网络，点击“断开”并执行第4步，否则执行第3步。
　　2. 打开“控制面板 - 网络和 Internet - 网络和共享中心 - 更改适配器设置”，禁用除了你当前的上网连接（如“本地连接”“宽带连接”）以外的所有连接并重新启用再试。
　　3. 再次执行 一. 2，若问题继续，请执行第3步。

②原来可以使用的网络共享时间长后无法继续使用：
　　1. 输入 B 重启共享。
　　2. 若问题持续，输入 D 深度重启共享。
    3. 若问题持续，输入 C 关闭后再次输入 D 深度重启。
    4. 若问题持续，重启电脑。

③Android 设备不断地显示“正在获取 IP 地址……”
    1. 按照②的方法重启共享。
    2. 若无效，在您的 Android 设备上按住你的热点名。
    3. 点击“修改网络”。
    4. 点击“显示高级选项”。
    5. 点击“IP 设置”，选“静态”。
    6. “IP 地址”中输入“192.168.137.xxx”，其中要将 xxx 替换成 2 至 255 中一个你喜欢的数（但愿你没有碰上冲突）。如果你在 一、7 中使用了别的静态 IP 地址，这里前缀也要对应修改。
    7. 在“域名 1”中输入“8.8.8.8”，“域名 2”中输入“8.8.4.4”。
    8. 点击“保存”，稍等片刻即可连上网络。若可以正常上网则说明设置没有问题，若不能请返回检查设置。

六、进阶使用：
此工具支持命令行参数，用法：
    Mygod无线网络共享.exe [命令序列]

若命令序列不为空，则程序运行完后将退出，不等待用户继续输入。

命令序列    相当于启动此工具后按的一系列键盘，支持除以下指令外的全部指令：H、I、S、T、X。";
    }

    static class Program
    {
        private static AssemblyName NowAssemblyName { get { return Assembly.GetCallingAssembly().GetName(); } }
        private static string ProgramTitle { get { return NowAssemblyName.Name + @" V" + NowAssemblyName.Version; } }
        private const string RegistryPosition = @"HKEY_CURRENT_USER\Software\Mygod\ShareWifi\",
                             RegistrySSID = "SSID", RegistryKey = "Key", RegistryPeersCount = "PeersCount", TaskName = "MygodWifiShare";
        private static string ssid, key;
        private static int peersCount;
        private static readonly WlanManager Manager = new WlanManager();

        private static void Main(string[] args)
        {
            Console.Title = ProgramTitle;
            OutputRequirement();
            Console.WriteLine();
            if (CheckOS()) return;
            if (args.Length == 0)
                while (true)
                    if (SwitchOperation(ReadOperation(), false)) Console.WriteLine();
                    else return;
            UpdateSettings();
            foreach (var c in args.SelectMany(s => s)) SwitchOperation(c, true);
        }

        private static bool CheckOS()
        {
            if (Environment.OSVersion.Version.Major > 6
                || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1)) return false;
            Console.Write("本程序必须在 Windows 7 或更高版本的 Windows 下运行！\n按任意键退出此程序。");
            Console.ReadKey();
            return true;
        }

        /// <summary>
        /// 用于将错误转化为可读的字符串。
        /// </summary>
        /// <param name="e">错误。</param>
        /// <returns>错误字符串。</returns>
        private static string GetMessage(this Exception e)
        {
            var result = new StringBuilder();
            GetMessage(e, result);
            return result.ToString();
        }
        private static void GetMessage(Exception e, StringBuilder result)
        {
            while (e != null && !(e is AggregateException))
            {
                result.AppendFormat("({0}) {1}{2}{3}{2}", e.GetType(), e.Message, Environment.NewLine, e.StackTrace);
                e = e.InnerException;
            }
            var ae = e as AggregateException;
            if (ae != null) foreach (var ex in ae.InnerExceptions) GetMessage(ex, result);
        }
        private static void Try(Action tryAction, Action failAction = null)
        {
            try
            {
                if (tryAction != null) tryAction();
            }
            catch (Exception exc)
            {
                Console.WriteLine("未知错误：" + exc.GetMessage());
                if (failAction != null) failAction();
            }
        }

        private static string ReasonToString(WLAN_HOSTED_NETWORK_REASON reason)
        {
            switch (reason)
            {
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success: return "操作成功。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_unspecified: return "未知错误。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_bad_parameters: return "参数错误。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_shutting_down: return "服务正在关闭。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_insufficient_resources: return "服务资源不足。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_elevation_required: return "当前操作需要提升权限。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_read_only: return "尝试修改只读数据。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_persistence_failed: return "数据持久化失败。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_crypt_error: return "加密时出现错误。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_impersonation: return "用户模拟失败。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_stop_before_start: return "函数调用顺序错误。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_interface_available: return "无线接口可用。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_interface_unavailable: return "无线接口不可用。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_miniport_stopped: return "无线微型端口驱动程序终止了托管网络。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_miniport_started: return "无线微型端口驱动程序状态已改变。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_incompatible_connection_started:
                    return "开始了一个不兼容的连接。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_incompatible_connection_stopped:
                    return "一个不兼容的连接已停止。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_user_action: return "由于用户操作，状态已改变。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_client_abort: return "由于客户端终止，状态已改变。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_ap_start_failed: return "无线托管网络驱动启动失败。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_arrived: return "一个客户端已连接到无线托管网络。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_departed: return "一个客户端已从无线托管网络断开连接。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_peer_timeout: return "一个客户端连接超时。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_gp_denied: return "操作被组策略禁止。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_unavailable: return "无线局域网服务未运行。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_device_change: return "无线托管网络所使用的无线适配器已改变。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_properties_change: return "无线托管网络所使用的属性已改变。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_virtual_station_blocking_use:
                    return "一个活动的虚拟站阻止了操作。";
                case WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_service_available_on_virtual_station:
                    return "在虚拟站上一个相同的服务已可用。";
                default: return "未知的 WLAN_HOSTED_NETWORK_REASON。";
            }
        }
        private static void WriteReason(WLAN_HOSTED_NETWORK_REASON reason)
        {
            Console.WriteLine("{0} ({1})", ReasonToString(reason), reason);
        }
        private static string StateToString(WLAN_HOSTED_NETWORK_STATE state)
        {
            switch (state)
            {
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable: return "不可用";
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_idle: return "未启用";
                case WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active: return "已启用";
                default: return "未知";
            }
        }
        private static string PhyTypeToString(DOT11_PHY_TYPE type)
        {
            switch (type)
            {
                case DOT11_PHY_TYPE.dot11_phy_type_fhss: return "FHSS";
                case DOT11_PHY_TYPE.dot11_phy_type_dsss: return "DSSS";
                case DOT11_PHY_TYPE.dot11_phy_type_irbaseband: return "红外基带";
                case DOT11_PHY_TYPE.dot11_phy_type_ofdm: return "OFDM";
                case DOT11_PHY_TYPE.dot11_phy_type_hrdsss: return "HRDSSS";
                case DOT11_PHY_TYPE.dot11_phy_type_erp: return "ERP";
                case DOT11_PHY_TYPE.dot11_phy_type_ht: return "802.11n";
                case DOT11_PHY_TYPE.dot11_phy_type_vht: return "802.11ac";
                default: return (int) type < 0 ? "IHV" : "未知";
            }
        }
        private static string AuthStateToString(WLAN_HOSTED_NETWORK_PEER_AUTH_STATE state)
        {
            switch (state)
            {
                case WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.wlan_hosted_network_peer_state_invalid: return "无效";
                case WLAN_HOSTED_NETWORK_PEER_AUTH_STATE.wlan_hosted_network_peer_state_authenticated: return "已认证";
                default: return "未知";
            }
        }

        private static void SetAutoRun()
        {
            using (var service = new TaskService())
            {
                var task = service.FindTask(TaskName);
                Console.WriteLine("当前设置{0}自动启动无线网络共享。", task == null ? "不" : string.Empty);
                Console.Write("修改吗？(Y 确定，其他取消)");
                if (char.ToUpper(Console.ReadKey().KeyChar) != 'Y') return;
                Console.WriteLine();
                if (task == null)
                {
                    var def = service.NewTask();
                    def.Triggers.Add(new LogonTrigger { UserId = WindowsIdentity.GetCurrent().Name });
                    def.Actions.Add(new ExecAction(Assembly.GetEntryAssembly().Location, "B"));
                    def.Principal.RunLevel = TaskRunLevel.Highest;  // MOST IMPORTANT FIX
                    service.RootFolder.RegisterTaskDefinition(TaskName, def);
                }
                else service.RootFolder.DeleteTask(TaskName);
            }
            Console.WriteLine("修改自动运行完成。");
        }
        private static void RestartInternetConnectionSharingService()
        {
            try
            {
                var service = new ServiceController("SharedAccess");
                Console.WriteLine("正在停止服务 {0}……", service.DisplayName);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 1, 0));
                Console.WriteLine("正在启动服务 {0}……", service.DisplayName);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 1, 0));
            }
            catch (Exception exc)
            {
                Console.WriteLine("重启服务时出现错误：{0}", exc.Message);
            }
        }
        private static void Close()
        {
            Console.Write("正在关闭共享……");
            WriteReason(Manager.ForceStop());
        }
        private static void Boot()
        {
            Close();
            Try(() =>
            {
                Console.Write("正在应用设置……");
                var reason = Manager.SetConnectionSettings(ssid, peersCount);
                if (reason != WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success)
                {
                    WriteReason(reason);
                    Close();
                    return;
                }
                WriteReason(reason = Manager.SetSecondaryKey(key));
                if (reason != WLAN_HOSTED_NETWORK_REASON.wlan_hosted_network_reason_success)
                {
                    Close();
                    return;
                }
                Console.Write("启动共享中……");
                WriteReason(Manager.ForceStart());
            }, Close);
        }
        private static void Init()
        {
            Console.WriteLine("配置 Microsoft 托管网络虚拟适配器中……");
            var virtualAdapter = new ManagementObjectSearcher(
                    new SelectQuery("Win32_NetworkAdapter", "PhysicalAdapter=1 AND ServiceName='vwifimp'"))
                .Get().OfType<ManagementObject>().Select(result => new NetworkAdapter(result)).SingleOrDefault();
            if (virtualAdapter == null)
            {
                Console.WriteLine("查询 Microsoft 托管网络虚拟适配器失败！请先启动无线网络共享后再试。");
                return;
            }
            virtualAdapter.NetConnectionID = "无线网络共享";
            var mo = new ManagementObjectSearcher(
                new SelectQuery("Win32_NetworkAdapterConfiguration", string.Format("SettingID='{0}'", virtualAdapter.GUID)))
                .Get().OfType<ManagementObject>().SingleOrDefault();
            if (mo == null)
            {
                Console.WriteLine("查询 Microsoft 托管网络虚拟适配器具体配置失败！请先启动无线网络共享后再试。");
                return;
            }
            mo.InvokeMethod("EnableStatic", new object[] { new[] { "192.168.137.1" }, new[] { "255.255.255.0" } });
            mo.InvokeMethod("SetGateways", new object[] { new string[0], new ushort[0] });
            mo.InvokeMethod("SetDNSServerSearchOrder", new object[] { new[] { "8.8.8.8", "8.8.4.4" } });
            Console.WriteLine("搜索可用网络连接中……");
            dynamic manager = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5C63C1AD-3956-4FF8-8486-40034758315B")));
            dynamic virtualConnection = null;
            var query = new List<Tuple<dynamic, dynamic>>();
            foreach (var connection in manager.EnumEveryConnection)
            {
                var props = manager.NetConnectionProps[connection];
                if (props.Guid == virtualAdapter.GUID)
                    if (virtualConnection == null) virtualConnection = connection;
                    else return;
                else if (props.Status == 2) // NCS_CONNECTED
                {
                    Console.WriteLine("{0}. {1} ({2})", query.Count, props.Name, props.DeviceName);
                    query.Add(new Tuple<dynamic, dynamic>(connection, props.Guid));
                }
            }
            if (query.Count > 0)
            {
                var picked = 0;
                if (query.Count > 1)
                {
                    Console.Write("请选择要共享的网络连接序号：（即前面的序号，输个错误的取消操作）");
                    if (!int.TryParse(Console.ReadLine(), out picked) || picked < 0 || picked >= query.Count) return;
                }
                else Console.WriteLine("共享唯一可用的网络连接中……");
                foreach (var connection in manager.EnumEveryConnection)
                {
                    var conf = manager.INetSharingConfigurationForINetConnection[connection];
                    var props = manager.NetConnectionProps[connection];
                    if (conf.SharingEnabled && (props.Guid != query[picked].Item2 || conf.SharingConnectionType != 0)
                        && (props.Guid != virtualAdapter.GUID && conf.SharingConnectionType != 1)) conf.DisableSharing();
                }
                var tempConf = manager.INetSharingConfigurationForINetConnection[query[picked].Item1];
                if (!tempConf.SharingEnabled) tempConf.EnableSharing(0);    // ICSSHARINGTYPE_PRIVATE
                tempConf = manager.INetSharingConfigurationForINetConnection[virtualConnection];
                if (!tempConf.SharingEnabled) tempConf.EnableSharing(1);    // ICSSHARINGTYPE_PUBLIC
                Console.WriteLine("初始化设置完成！");
            }
            else Console.WriteLine("没有可用的网络连接！");
        }
        private static void RefreshKey()
        {
            Console.Write("正在刷新安全设置……");
            Try(() => WriteReason(Manager.RefreshSecuritySettings()));
        }
        private static void Settings()
        {
            Console.WriteLine("请输入您的新的设置，为空则不修改。");
            Console.WriteLine("旧的无线网络名：" + ssid);
            Console.Write("新的无线网络名：");
            ssid = Console.ReadLine();
            Console.WriteLine("旧的无线密码：" + key);
            Console.Write("新的无线密码：");
            key = Console.ReadLine();
            var changed = false;
            if (!string.IsNullOrEmpty(ssid)) 
            { 
                Registry.SetValue(RegistryPosition, RegistrySSID, ssid);
                changed = true;
            }
            if (key != null && key.Length >= 8 && key.Length < 64)
            {
                Registry.SetValue(RegistryPosition, RegistryKey, key);
                changed = true;
            }
            Console.WriteLine("旧的最大客户端数：" + peersCount);
            Console.Write("新的最大客户端数：");
            if (int.TryParse(Console.ReadLine(), out peersCount) && peersCount >= 0)
            {
                Registry.SetValue(RegistryPosition, RegistryPeersCount, peersCount);
                changed = true;
            }
            if (!changed) return;
            Console.Write("修改完毕，是否要立即生效？(Y 生效，其他键不生效)");
            var ch = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            if (ch != 'Y') return;
            UpdateSettings();
            Boot();
        }
        private static void CheckForUpdates()
        {
            Process.Start("http://studio.mygod.tk/product/mygod-wifi-share/");
        }
        private static void ShowHelp()
        {
            Console.WriteLine(R.QuickHelp);
            Console.WriteLine();
            Console.WriteLine(R.Help);
        }
        
        private static void ShowStatus()
        {
            Try(() =>
            {
                var status = Manager.QueryStatus();
                Console.WriteLine("状态：\t\t\t{0} ({1})", StateToString(status.HostedNetworkState), status.HostedNetworkState);
                if (status.HostedNetworkState == WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_unavailable) return;
                Console.WriteLine("实际网络 ID：\t\t{0}\nBSSID：\t\t\t{1}", status.IPDeviceID, status.wlanHostedNetworkBSSID);
                if (status.HostedNetworkState == WLAN_HOSTED_NETWORK_STATE.wlan_hosted_network_active)
                    Console.WriteLine("802.11 物理层类型：\t{0}\n网络接口信道频率：\t{1}\n已认证客户端数量：\t{2}",
                                      PhyTypeToString(status.dot11PhyType), status.ulChannelFrequency, status.dwNumberOfPeers);
            });
        }
        private static string QueryCurrentDevices()
        {
            try
            {
                var result = new StringBuilder();
                var lookup = Arp.GetIpNetTable().ToLookup(row => row.MacAddress, row => row);
                var i = 0;
                foreach (var address in Manager.QueryStatus().PeerList)
                {
                    result.AppendFormat("设备 #{0} 物理地址：{1} ({2})\n", ++i, address.PeerMacAddress,
                                        AuthStateToString(address.PeerAuthState));
                    var padding = string.Empty.PadLeft(8 + (int) Math.Floor(Math.Log10(i)));
                    var ips = lookup[address.PeerMacAddress.ToString()].ToArray();
                    if (ips.Length > 0) result.AppendFormat("{1}IP  地址：{0}\n{1}设备名称：{2}\n", string.Join("; ", ips), padding,
                        string.Join("; ", ips.Select(ip =>
                        {
                            try
                            {
                                var entry = Dns.GetHostEntry(ip.IPAddress);
                                return string.Join(", ", new[] { entry.HostName }.Union(entry.Aliases));
                            }
                            catch
                            {
                                return "(未知)";
                            }
                        })));
                }
                return result.ToString();
            }
            catch (Exception exc)
            {
                return "查询客户端失败，详细信息：" + exc.GetMessage();
            }
        }
        private static bool keepGoing;
        private static void WatchCurrentDevices()
        {
            keepGoing = true;
            Console.CancelKeyPress += StopWatching;
            while (keepGoing)
            {
                var result = QueryCurrentDevices();
                Console.Clear();
                Console.WriteLine("监视已连接设备中，按 Ctrl + C 键返回。");
                Console.WriteLine(result);      // prevent flashing
                Thread.Sleep(500);
            }
            Console.CancelKeyPress -= StopWatching;
            Console.Clear();
        }
        private static void StopWatching(object sender, ConsoleCancelEventArgs e)
        {
            keepGoing = false;
            e.Cancel = true;
        }
        
        private static void OutputRequirement()
        {
            Console.WriteLine(R.Requirement, ProgramTitle);
            Console.WriteLine();
            Console.WriteLine(R.QuickHelp);
        }
        private static void UpdateSettings()
        {
            try
            {
                ssid = (string)Registry.GetValue(RegistryPosition, RegistrySSID, null);
            }
            catch (FormatException)
            {
                ssid = null;
            }
            if (ssid == null) Registry.SetValue(RegistryPosition, RegistrySSID, ssid = "Mygod Hotspot");
            try
            {
                key = (string)Registry.GetValue(RegistryPosition, RegistryKey, null);
            }
            catch (FormatException)
            {
                key = null;
            }
            if (key == null) Registry.SetValue(RegistryPosition, RegistryKey, key = "AwesomePassword");
            try
            {
                peersCount = (int) Registry.GetValue(RegistryPosition, RegistryPeersCount, null);
            }
            catch
            {
                peersCount = -1;
            }
            if (peersCount < 0) Registry.SetValue(RegistryPosition, RegistryPeersCount, peersCount = 100);
        }
        private static char ReadOperation()
        {
            UpdateSettings();
            Console.Write(R.WelcomeToUse, ProgramTitle);
            var result = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            return result;
        }
        private static bool SwitchOperation(char operation, bool auto)
        {
            switch (char.ToUpper(operation))
            {
                case 'A':
                    ShowStatus();
                    break;
                case 'B':
                    Boot();
                    break;
                case 'C':
                    Close();
                    break;
                case 'D':
                    RestartInternetConnectionSharingService();
                    Boot();
                    break;
                case 'H':
                    if (auto) Console.WriteLine("自动状态下拒绝显示帮助！");
                    else ShowHelp();
                    break;
                case 'I':
                    if (auto) Console.WriteLine("自动状态下拒绝初始化设置！");
                    else Init();
                    break;
                case 'K':
                    RefreshKey();
                    break;
                case 'Q':
                    Console.WriteLine(QueryCurrentDevices());
                    break;
                case 'S':
                    if (auto) Console.WriteLine("自动状态下拒绝设置！");
                    else Settings();
                    break;
                case 'T':
                    if (auto) Console.WriteLine("自动状态下拒绝设置自动启动！");
                    else SetAutoRun();
                    break;
                case 'U':
                    CheckForUpdates();
                    break;
                case 'W':
                    WatchCurrentDevices();
                    break;
                default:
                    return false;
            }
            Console.WriteLine();
            return true;
        }
    }
}
