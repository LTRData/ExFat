// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using DiscUtils.Vhdx;

namespace ExFat.DiscUtils.Environment;
internal class TestEnvironment : IDisposable
{
    protected string vhdxPath;
    protected Disk disk;

    protected TestEnvironment()
    {
    }

#if NETCOREAPP
    [SupportedOSPlatform("windows")]
#endif
    private static bool IsElevated
    {
        get
        {
            using var id = WindowsIdentity.GetCurrent();
            return id.Owner != id.User;
        }
    }

    public virtual void Dispose()
    {
        disk?.Dispose();
        // a check when required
        if (vhdxPath != null && File.Exists(vhdxPath))
        {
            try
            {
#if NET461_OR_GREATER || NETSTANDARD || NETCOREAPP
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return;
                }
#endif

                if (IsElevated)
                {
                    var t = CheckDisk();
                    if (!t.Item1)
                    {
                        throw new Exception($"VHDX filesystem is found corrupted by CHKDSK: {t.Item2}");
                    }
                }
                else
                {
                    throw new Exception("Not elevated");
                }
            }
            finally
            {
                File.Delete(vhdxPath);
            }
        }
    }

    private Tuple<bool, string> CheckDisk()
    {
        var previousDrives = DriveInfo.GetDrives();
        RunDiskPart("attach", vhdxPath);
        var newDrives = DriveInfo.GetDrives();
        var mountedDrive = newDrives.FirstOrDefault(d => previousDrives.All(p => p.Name != d.Name));
        var success = true;
        string checkResult = null;
        if (mountedDrive != null)
        {
            var result = ProcessUtility.Run("chkdsk", mountedDrive.Name.TrimEnd('\\'));
            success = result.Item1 == 0;
            checkResult = result.Item2;
        }

        RunDiskPart("detach", vhdxPath);
        return Tuple.Create(success, checkResult);
    }

    private static void RunDiskPart(string action, string vdiskPath)
    {
        var scriptPath = Path.GetTempFileName();
        using (var scriptStream = File.CreateText(scriptPath))
        {
            scriptStream.WriteLine($"select vdisk file=\"{vdiskPath}\"");
            scriptStream.WriteLine($"{action} vdisk");
        }

        ProcessUtility.Run("diskpart", $"/s {scriptPath}");
        File.Delete(scriptPath);
    }
}