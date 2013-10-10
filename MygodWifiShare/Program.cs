using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using File = System.IO.File;

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
                             RegistrySSID = "sSSID", RegistryKey = "sKey";
        private static string ssid, key;

        private static DateTime CompilationTime
        {
            get
            {
                var b = new byte[2048];
                using (var s = new FileStream(Assembly.GetEntryAssembly().Location, FileMode.Open, FileAccess.Read)) s.Read(b, 0, 2048);
                var dt = new DateTime(1970, 1, 1).AddSeconds(BitConverter.ToInt32(b, BitConverter.ToInt32(b, 60) + 8));
                return dt + TimeZone.CurrentTimeZone.GetUtcOffset(dt);
            }
        }

        private static void Main(string[] args)
        {
            Console.Title = ProgramTitle;
            OutputRequirement();
            Console.WriteLine();
            if (CheckOS()) return;
            if (args.Length == 0)
                while (true) if (SwitchOperation(ReadOperation(), false)) Console.WriteLine(); else return;
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
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Resources.StartUpName + ".lnk";
            var auto = File.Exists(path);
            Console.WriteLine(Resources.IsAutoRun, auto ? string.Empty : Resources.Not);
            Console.Write(Resources.AskChange);
            if (Char.ToUpper(Console.ReadKey().KeyChar) != 'Y') return;
            Console.WriteLine();
            if (auto) File.Delete(path);
            else
            {
                var shortcut = (IWshShortcut) new WshShell().CreateShortcut(path);
                shortcut.Arguments = "B";
                shortcut.Description = Resources.StartUpDescription;
                shortcut.TargetPath = Application.ExecutablePath;
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
            Console.WriteLine(Resources.ChangeAutoRunFinish);
        }
        private static void ShowUpdateContent() 
        {
            Console.WriteLine(Resources.UpdateContent);
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
            Command(string.Format(Commands.Boot, ssid, key));
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
                Console.WriteLine("监视已连接设备中，按 Ctrl + C 键返回。");
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
            switch (Char.ToUpper(operation))
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
                    if (auto) Console.WriteLine(Resources.AutoNoShowingHelp); else ShowHelp();
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
                    if (auto) Console.WriteLine(Resources.AutoNoSettings); else Settings();
                    break;
                case 'T':
                    if (auto) Console.WriteLine(Resources.AutoNoSettingAutoRun); else SetAutoRun();
                    break;
                case 'U':
                    CheckForUpdates();
                    break;
                case 'W':
                    WatchCurrentDevices();
                    break;
                case 'X':
                    if (auto) Console.WriteLine(Resources.AutoNoCheckingSupport); else CheckSupport();
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
