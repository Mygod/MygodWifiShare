using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using ROOT.CIMV2.Win32;

namespace Mygod.WifiShare
{
    static class Commands
    {
        public const string Close = "wlan set hostednetwork disallow",
                            CheckSupportArgs = "wlan show all",
                            UpdateSettings = "wlan set hostednetwork mode=allow ssid=\"{0}\" key=\"{1}\"",
                            Boot = "wlan start hostednetwork", 
                            GetStatus = "wlan show hostednetwork", 
                            Refresh = "wlan refresh hostednetwork key";
    }

    static class Program
    {
        private static AssemblyName NowAssemblyName { get { return Assembly.GetCallingAssembly().GetName(); } }
        private static string ProgramTitle { get { return NowAssemblyName.Name + @" V" + NowAssemblyName.Version; } }
        private const string RegistryPosition = @"HKEY_CURRENT_USER\Software\Mygod\ShareWifi\",
                             RegistrySSID = "sSSID", RegistryKey = "sKey", TaskName = "MygodWifiShare";
        private static string ssid, key;

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
            Console.Write(Resources.OSError);
            Console.ReadKey();
            return true;
        }
        
        private static void SetAutoRun()
        {
            using (var service = new TaskService())
            {
                var task = service.FindTask(TaskName);
                Console.WriteLine(Resources.IsAutoRun, task == null ? Resources.Not : string.Empty);
                Console.Write(Resources.AskChange);
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
            Console.WriteLine(Resources.ChangeAutoRunFinish);
        }
        private static void Restart()
        {
            Close();
            Boot();
        }
        private static void RestartInternetConnectionSharingService()
        {
            try
            {
                var service = new ServiceController("SharedAccess");
                Console.WriteLine(Resources.StoppingService, service.DisplayName);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 1, 0));
                Console.WriteLine(Resources.StartingService, service.DisplayName);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 1, 0));
            }
            catch (Exception exc)
            {
                Console.WriteLine(Resources.ServiceError, exc.Message);
            }
        }
        private static void CheckSupport()
        {
            Console.WriteLine();
            var s = FetchCommand(Commands.CheckSupportArgs);
            Console.WriteLine(Resources.IsSupport,
                              s.Substring(s.IndexOf(Resources.SupportTheBearerNetwork, StringComparison.Ordinal) + 11, 1) == Resources.Yes
                                          ? string.Empty : Resources.Not); 
        }
        private static void Close()
        {
            Command(Commands.Close);
        }
        private static void Boot()
        {
            Command(string.Format(Commands.UpdateSettings, ssid, key));
            Command(string.Format(Commands.Boot));
        }
        private static void Init()
        {
            var virtualAdapter = new ManagementObjectSearcher(
                    new SelectQuery("Win32_NetworkAdapter", "PhysicalAdapter=1 AND ServiceName='vwifimp'"))
                .Get().OfType<ManagementObject>().Select(result => new NetworkAdapter(result)).SingleOrDefault();
            if (virtualAdapter == null)
            {
                Console.WriteLine(Resources.QueryVirtualAdapterFailed);
                return;
            }
            virtualAdapter.NetConnectionID = "无线网络共享";
            var mo = new ManagementObjectSearcher(
                new SelectQuery("Win32_NetworkAdapterConfiguration", string.Format("SettingID='{0}'", virtualAdapter.GUID)))
                .Get().OfType<ManagementObject>().SingleOrDefault();
            if (mo == null)
            {
                Console.WriteLine(Resources.QueryVirtualAdapterConfigurationFailed);
                return;
            }
            mo.InvokeMethod("EnableStatic", new object[] { new[] { "192.168.137.1" }, new[] { "255.255.255.0" } });
            mo.InvokeMethod("SetGateways", new object[] { new string[0], new ushort[0] });
            mo.InvokeMethod("SetDNSServerSearchOrder", new object[] { new[] { "8.8.8.8", "8.8.4.4" } });
            dynamic manager = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("5C63C1AD-3956-4FF8-8486-40034758315B")));
            dynamic virtualConnection = null;
            var query = new List<Tuple<dynamic, dynamic, dynamic>>();   // NCS_CONNECTED
            foreach (var connection in manager.EnumEveryConnection)
            {
                var props = manager.NetConnectionProps[connection];
                if (props.Guid == virtualAdapter.GUID)
                    if (virtualConnection == null) virtualConnection = connection;
                    else return;
                else if (props.Status == 2) query.Add(new Tuple<dynamic, dynamic, dynamic>(connection, props.Name, props.DeviceName));
            }
            if (virtualConnection == null || query.Count <= 0) return;
            for (var i = 0; i < query.Count; i++) Console.WriteLine(Resources.ConnectionFormat, i, query[i].Item2, query[i].Item3);
            Console.Write(Resources.PickConnectionPrompt);
            int picked;
            if (!int.TryParse(Console.ReadLine(), out picked) || picked < 0 || picked >= query.Count) return;
            foreach (var connection in manager.EnumEveryConnection)
            {
                var conf = manager.INetSharingConfigurationForINetConnection[connection];
                if (conf.SharingEnabled) conf.DisableSharing();
            }
            manager.INetSharingConfigurationForINetConnection[query[picked].Item1].EnableSharing(0);    // ICSSHARINGTYPE_PUBLIC
            manager.INetSharingConfigurationForINetConnection[virtualConnection].EnableSharing(1);      // ICSSHARINGTYPE_PRIVATE
            Console.WriteLine(Resources.InitFinished);
        }
        private static void RefreshKey()
        {
            Command(Commands.Refresh);
        }
        private static void Settings()
        {
            Console.WriteLine(Resources.SettingsStep1);
            Console.WriteLine(Resources.SettingsStep2 + ssid);
            Console.Write(Resources.SettingsStep3);
            ssid = Console.ReadLine();
            Console.WriteLine(Resources.SettingsStep4 + key);
            Console.Write(Resources.SettingsStep5);
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
            if (!changed) return;
            Console.WriteLine(Resources.SettingsStep6);
            if (char.ToUpper(Console.ReadKey().KeyChar) != 'Y') return;
            UpdateSettings();
            Restart();
        }
        private static void CheckForUpdates()
        {
            Process.Start("http://mygodstudio.tk/product/mygod-wifi-share/");
        }
        private static void ShowHelp()
        {
            Console.WriteLine(Resources.Help);
        }
        
        private static readonly Regex MacAddressFetcher
            = new Regex(@"^\s*(([0-9a-fA-F]{2}\:){5}[0-9a-fA-F]{2}).*$", RegexOptions.Compiled | RegexOptions.Multiline);
        private static void ShowStatus()
        {
            Console.WriteLine();
            Console.WriteLine(MacAddressFetcher.Replace(FetchCommand(Commands.GetStatus), string.Empty).TrimEnd('\r', '\n'));
        }
        private static string QueryCurrentDevices()
        {
            var result = new StringBuilder();
            var lookup = Run("arp", "-a").Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                         .Select(line => line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                                         .Where(pieces => pieces.Length == 3)
                                         .ToLookup(pieces => pieces[1].ToLowerInvariant(), pieces => pieces[0].ToLowerInvariant());
            var i = 0;
            foreach (var address in MacAddressFetcher.Matches(FetchCommand(Commands.GetStatus)).Cast<Match>()
                                                     .Select(match => match.Groups[1].Value.ToLowerInvariant().Replace(':', '-')))
            {
                result.AppendFormat("设备 #{0} 物理地址：{1}\n", ++i, address);
                var ips = lookup[address].ToArray();
                if (ips.Length > 0)
                {
                    result.AppendFormat("{1}IP  地址：{0}\n", string.Join("; ", ips),
                                        string.Empty.PadLeft(8 + (int) Math.Floor(Math.Log10(i))));
                    try
                    {
                        result.AppendFormat("{1}设备名称：{0}\n", string.Join("; ", ips.Select(ip => 
                        {
                            var entry = Dns.GetHostEntry(ip);
                            return string.Join(", ", new[] { entry.HostName }.Union(entry.Aliases));
                        })), string.Empty.PadLeft(8 + (int)Math.Floor(Math.Log10(i))));
                    }
                    catch { }
                }
            }
            return result.ToString();
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
                Console.WriteLine(Resources.WatchCurrentDevices);
                Console.WriteLine(result);      // prevent flashing
                Thread.Sleep(500);
            }
            Console.CancelKeyPress -= StopWatching;
            Console.Clear();
        }

        static void StopWatching(object sender, ConsoleCancelEventArgs e)
        {
            keepGoing = false;
            e.Cancel = true;
        }
        
        private static void OutputRequirement()
        {
            Console.WriteLine(Resources.Requirement, ProgramTitle);
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
            if (ssid == null)
            {
                Registry.SetValue(RegistryPosition, RegistrySSID, Resources.DefaultSSID);
                ssid = Resources.DefaultSSID; 
            }

            try
            {
                key = (string)Registry.GetValue(RegistryPosition, RegistryKey, null);
            }
            catch (FormatException)
            {
                key = null;
            }
            if (key != null) return;
            Registry.SetValue(RegistryPosition, RegistryKey, Resources.DefaultKey);
            key = Resources.DefaultKey;
        }
        private static char ReadOperation()
        {
            UpdateSettings();
            Console.Write(Resources.WelcomeToUse, ProgramTitle);
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
                    Restart();
                    break;
                case 'H':
                    if (auto) Console.WriteLine(Resources.AutoNoShowingHelp);
                    else ShowHelp();
                    break;
                case 'I':
                    if (auto) Console.WriteLine(Resources.AutoNoInit);
                    else Init();
                    break;
                case 'K':
                    RefreshKey();
                    break;
                case 'Q':
                    Console.WriteLine();
                    Console.WriteLine(QueryCurrentDevices());
                    break;
                case 'R':
                    Restart();
                    break;
                case 'S':
                    if (auto) Console.WriteLine(Resources.AutoNoSettings);
                    else Settings();
                    break;
                case 'T':
                    if (auto) Console.WriteLine(Resources.AutoNoSettingAutoRun);
                    else SetAutoRun();
                    break;
                case 'U':
                    CheckForUpdates();
                    break;
                case 'W':
                    WatchCurrentDevices();
                    break;
                case 'X':
                    if (auto) Console.WriteLine(Resources.AutoNoCheckingSupport);
                    else CheckSupport();
                    break;
                default:
                    return false;
            }
            Console.WriteLine();
            return true;
        }
        private static string Run(string path, string arguments)
        {
            using (var process = Process.Start(new ProcessStartInfo { FileName = path, Arguments = arguments, UseShellExecute = false,
                CreateNoWindow = false, RedirectStandardOutput = true })) return process.StandardOutput.ReadToEnd();
        }
        private static string FetchCommand(string command)
        {
            return Run("netsh.exe", command);
        }
        private static void Command(string command)
        {
            Console.WriteLine();
            Console.Write(FetchCommand(command));
        }
    }
}
