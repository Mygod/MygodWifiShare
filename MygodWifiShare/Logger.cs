using System.IO;

namespace Mygod.WifiShare
{
    internal static class Logger
    {
        public static readonly string LogPath = Path.Combine(Path.GetTempPath(), "MygodWifiShare.log");
        public static StreamWriter Instance;
        public static bool Initialized;

        public static bool Initialize()
        {
            try
            {
                Instance = new StreamWriter(LogPath, true);
                return Initialized = true;
            }
            catch
            {
                return false;   // another instance is already running
            }
        }
    }
}
