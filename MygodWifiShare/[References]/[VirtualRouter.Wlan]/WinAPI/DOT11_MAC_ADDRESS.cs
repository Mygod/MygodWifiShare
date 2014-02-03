using System.Runtime.InteropServices;

namespace VirtualRouter.Wlan.WinAPI
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DOT11_MAC_ADDRESS
    {
        public byte one;
        public byte two;
        public byte three;
        public byte four;
        public byte five;
        public byte six;

        public override string ToString()
        {
            return string.Format("{0:X2}:{1:X2}:{2:X2}:{3:X2}:{4:X2}:{5:X2}", one, two, three, four, five, six);
        }
    }
}