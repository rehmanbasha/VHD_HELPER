using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices; 

namespace VHD_HELPER
{
    class vhdlib
    {

       
    }

    public class mount
    {
        [DllImport("DiskLibrary.dll", SetLastError = true)]
        public static extern int GetVolumeName(int disknumber, int partitionnumber, [In, Out] StringBuilder VolumeName);

        [DllImport("DiskLibrary.dll", SetLastError = true)]
        public static extern int GetDiskNumber([In, Out] StringBuilder VolumeName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVolumePathNamesForVolumeNameW([MarshalAs(UnmanagedType.LPWStr)] string lpszVolumeName,
           [MarshalAs(UnmanagedType.LPWStr)] string lpszVolumePathNames, uint cchBuferLength, ref UInt32 lpcchReturnLength);

        [DllImport("DiskLibrary.dll", SetLastError = true)]
        public static extern bool OpenAndAttachVHD([MarshalAs(UnmanagedType.LPWStr)] string vhdfile);

        [DllImport("DiskLibrary.dll", SetLastError = true)]
        public static extern bool OpenAndDetachVHD([MarshalAs(UnmanagedType.LPWStr)] string vhdfile);

        [DllImport("DiskLibrary.dll", SetLastError = true)]
        public static extern bool OpenAndGetPhysVHD([MarshalAs(UnmanagedType.LPWStr)] string vhdfile, [In, Out] StringBuilder physicaldrive);

    }
}
