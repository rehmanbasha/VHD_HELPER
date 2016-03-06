using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel; 

namespace VHD_HELPER
{
    class vhdlib
    {

        #region Nativecall
        [DllImport("kernel32.dll")]
        public static extern bool DeleteVolumeMountPoint(string lpszVolumeMountPoint);

        [DllImport("kernel32.dll")]
        static extern bool GetVolumePathNamesForVolumeName(string lpszVolumeMountPoint, string volpath, int ilength, int olength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetVolumeNameForVolumeMountPoint(string lpszVolumeMountPoint, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);

        [DllImport("kernel32.dll")]
        static extern bool SetVolumeMountPoint(string lpszVolumeMountPoint, string lpszVolumeName);


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
        #endregion


        private static List<string> GetMountPointsForVolume(string volumeDeviceName)
        {
            List<string> result = new List<string>();
            // GetVolumePathNamesForVolumeName is only available on Windows XP/2003 and above
            int osVersionMajor = Environment.OSVersion.Version.Major;
            int osVersionMinor = Environment.OSVersion.Version.Minor;
            if (osVersionMajor < 5 || (osVersionMajor == 5 && osVersionMinor < 1))
            {
                return result;
            }

            try
            {
                uint lpcchReturnLength = 0;
                string buffer = "";

                GetVolumePathNamesForVolumeNameW(volumeDeviceName, buffer, (uint)buffer.Length, ref lpcchReturnLength);
                if (lpcchReturnLength == 0)
                {
                    return result;
                }

                buffer = new string(new char[lpcchReturnLength]);

                if (!GetVolumePathNamesForVolumeNameW(volumeDeviceName, buffer, lpcchReturnLength, ref lpcchReturnLength))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                string[] mounts = buffer.Split('\0');
                foreach (string mount in mounts)
                {
                    if (mount.Length > 0)
                    {
                        result.Add(mount);
                    }
                }
            }
            catch (Exception ex)
            {
              
            }

            return result;
        }


        public static List<String> GetMountPoints(int disknumber, int partitionnum)
        {
            List<string> mountedvolume = null;
            StringBuilder strb = new StringBuilder(512);
            if (GetVolumeName(disknumber, partitionnum, strb) == 1)
            {
                mountedvolume = GetMountPointsForVolume(strb.ToString());
            }

            return mountedvolume;
        }

        public static void MountPartition(int diskNum, int partition, string mountpoint)
        {
            StringBuilder strb = new StringBuilder(512);
            if (GetVolumeName(diskNum, partition, strb) == 1)
            {
                List<string> mountedvolume = GetMountPointsForVolume(strb.ToString());
                foreach (string mountvol in mountedvolume)
                    DeleteVolumeMountPoint(mountvol);
                SetVolumeMountPoint(mountpoint + "\\", strb.ToString());
            }
        }


        public static string GetPhyVhd(string vhdfile)
        {
            StringBuilder PhysicalDrive = new StringBuilder(1024 * 256);
            OpenAndGetPhysVHD(vhdfile, PhysicalDrive);
            return PhysicalDrive.ToString();
        }

        public static string GetMountPoints(string vhdfile)
        {
            List<string> mountPoints = new List<string>();
            int diskno = int.Parse(vhdlib.GetPhyVhd(vhdfile.ToString()));
            List<string> mountpts = new List<string>();
            mountPoints= vhdlib.GetMountPoints(diskno, 1);

            string drive_letters="";

            foreach (string letter in mountPoints)
            {
                if (drive_letters == "")
                    drive_letters = letter;
                else
                    drive_letters += ", " + letter;
            }

            return drive_letters;
        }
       
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
