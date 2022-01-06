// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils;

using System;
using System.IO;
using global::DiscUtils;
using global::DiscUtils.Vfs;
using FileSystemInfo = global::DiscUtils.FileSystemInfo;

/// <summary>
/// </summary>
[VfsFileSystemFactory]
// ReSharper disable once UnusedMember.Global
public class ExFatFilesystemFactory : VfsFileSystemFactory
{
    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="volume"></param>
    /// <returns></returns>
    public override FileSystemInfo[] Detect(Stream stream, VolumeInfo volume)
    {
        if (ExFatFileSystem.Detect(stream))
            return new FileSystemInfo[] { new VfsFileSystemInfo("exFAT", ExFatFileSystem.Name, Open) };

#if NET461_OR_GREATER || NETSTANDARD || NETCOREAPP
        return Array.Empty<FileSystemInfo>();
#else
        return new FileSystemInfo[0];
#endif
    }

    private static DiscFileSystem Open(Stream stream, VolumeInfo volumeInfo, FileSystemParameters parameters) => new ExFatFileSystem(stream);
}
