using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace Mygod.WifiShare
{
    static class Commands
    {
        public const string Close = "netsh wlan set hostednetwork disallow";
        public const string Restart = "netsh wlan set hostednetwork disallow & " +
                                      "netsh wlan set hostednetwork mode=allow ssid=\"{0}\" key=\"{1}\" & " +
                                      "netsh wlan start hostednetwork";
        public const string CheckSupportArgs = "/c netsh wlan show all";
        public const string Boot = "netsh wlan set hostednetwork mode=allow ssid=\"{0}\" key=\"{1}\" & " +
                                   "netsh wlan start hostednetwork";
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
            OutputProgramNeeds();
            if (CheckOS()) return;
            if (args.Length == 0) while (true) { if (!SwitchOperation(ReadOperation(), false)) return; Console.WriteLine(); }
            UpdateSettings();
            foreach (var c in args.SelectMany(s => s)) SwitchOperation(c, true);
        }

        private static bool CheckOS()
        {
            if (Environment.OSVersion.Version.Major >= 6
                && (Environment.OSVersion.Version.Major != 6 || Environment.OSVersion.Version.Minor >= 1)) return false;
            Console.Write(Resources.OSError);
            Console.ReadKey();
            return true;
        }
        
        private static void SetAutoRun()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Resources.StartUpName + ".lnk";
            var auto = System.IO.File.Exists(path);
            Console.WriteLine(Resources.IsAutoRun, auto ? string.Empty : Resources.Not);
            Console.Write(Resources.AskChange);
            if (Char.ToUpper(Console.ReadKey().KeyChar) != 'Y') return;
            Console.WriteLine();
            if (auto) System.IO.File.Delete(path);
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
        private static void ShowUpdateContent() { Console.WriteLine(Resources.UpdateContent); }
        private static void Restart() { Command(string.Format(Commands.Restart, ssid, key)); }
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
            var cmd = new Process { StartInfo = { FileName = "cmd.exe", Arguments = Commands.CheckSupportArgs, UseShellExecute = false, 
                                                  CreateNoWindow = false, RedirectStandardOutput = true } };
            cmd.Start();
            var s = cmd.StandardOutput.ReadToEnd();
            Console.WriteLine(Resources.IsSupport,
                              s.Substring(s.IndexOf(Resources.SupportTheBearerNetwork, StringComparison.Ordinal) + 11, 1) == Resources.Yes
                                          ? string.Empty : Resources.Not); 
        }
        private static void Close() { Command(Commands.Close); }
        private static void Boot() { Command(string.Format(Commands.Boot, ssid, key)); }
        private static void Settings()
        {
            Console.WriteLine(Resources.SettingsStep1);
            Console.WriteLine(Resources.SettingsStep2 + ssid);
            Console.Write(Resources.SettingsStep3); ssid = Console.ReadLine();
            Console.WriteLine(Resources.SettingsStep4 + key);
            Console.Write(Resources.SettingsStep5); key = Console.ReadLine();
            var changed = false;
            if (!string.IsNullOrEmpty(ssid)) { Registry.SetValue(RegistryPosition, RegistrySSID, ssid); changed = true; }
            if (!string.IsNullOrEmpty(key)) { Registry.SetValue(RegistryPosition, RegistryKey, key); changed = true; }
            if (!changed) return;
            Console.WriteLine(Resources.SettingsStep6);
            if (Char.ToUpper(Console.ReadKey().KeyChar) != 'Y') return;
            UpdateSettings(); Restart();
        }
        private static void About()
        {
            Console.WriteLine(Resources.About, ProgramTitle, Resources.Producer, CompilationTime.ToLongDateString());
        }
        private static void ShowHelp()
        {
            Console.WriteLine(Resources.Help);
        }

        private static void OutputProgramNeeds()
        {
            Console.WriteLine(Resources.ProgramNeeds, ProgramTitle);
        }
        private static void UpdateSettings()
        {
            try { ssid = (string)Registry.GetValue(RegistryPosition, RegistrySSID, null); }
            catch (FormatException) { ssid = null; }
            if (ssid == null) { Registry.SetValue(RegistryPosition, RegistrySSID, Resources.DefaultSSID); ssid = Resources.DefaultSSID; }

            try { key = (string)Registry.GetValue(RegistryPosition, RegistryKey, null); }
            catch (FormatException) { key = null; }
            if (key != null) return;
            Registry.SetValue(RegistryPosition, RegistryKey, Resources.DefaultKey); key = Resources.DefaultKey;
        }
        private static char ReadOperation()
        {
            UpdateSettings();
            Console.Write(Resources.WelcomeToUse, ProgramTitle);
            var result = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            return result;
        }
        private static bool SwitchOperation(char operation, bool auto)
        {
            switch (Char.ToUpper(operation))
            {
                case 'A': About(); break;
                case 'B': Boot(); break;
                case 'C': Close(); break;
                case 'D': RestartInternetConnectionSharingService(); Restart(); break;
                case 'H': if (auto) Console.WriteLine(Resources.AutoNoShowingHelp); else ShowHelp(); break;
                case 'R': Restart(); break;
                case 'S': if (auto) Console.WriteLine(Resources.AutoNoSettings); else Settings(); break;
                case 'T': if (auto) Console.WriteLine(Resources.AutoNoSettingAutoRun); else SetAutoRun(); break;
                case 'U': ShowUpdateContent(); break;
                case 'X': if (auto) Console.WriteLine(Resources.AutoNoCheckingSupport); else CheckSupport(); break;
                default: return false;
            }
            Console.WriteLine();
            return true;
        }
        private static void Command(string command)
        {
            Console.WriteLine();
            var cmd = new Process { StartInfo = { FileName = "cmd.exe", Arguments = "/c " + command, 
                                                  UseShellExecute = false, CreateNoWindow = false, RedirectStandardOutput = true } };
            cmd.Start();
            Console.Write(cmd.StandardOutput.ReadToEnd());
        }
    }
}
