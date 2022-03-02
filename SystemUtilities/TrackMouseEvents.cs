using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SystemUtilities
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;

        public uint dwTime;
        public LASTINPUTINFO(uint init)
        {
            cbSize = (uint)Marshal.SizeOf<LASTINPUTINFO>();
            dwTime = init;
        }
    }

    public class TrackMouseEvents
    {
        public IEnumerable<Process> getRunningProcesses()
        {
            var processes = Process.GetProcesses().Where(pr => pr.MainWindowHandle != IntPtr.Zero); ;
            return processes;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO(0);
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }
        /// <summary>
        /// Get the Last input time in milliseconds
        /// </summary>
        /// <returns></returns>
        public long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }

    }
}
