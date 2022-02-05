// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils;

using System;
using System.Collections.Generic;
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
    public override IEnumerable<FileSystemInfo> Detect(Stream stream, VolumeInfo volume)
    {
        if (ExFatFileSystem.Detect(stream))
        {
            yield return new VfsFileSystemInfo("exFAT", ExFatFileSystem.Name, Open);
        }
    }

    private static DiscFileSystem Open(Stream stream, VolumeInfo volumeInfo, FileSystemParameters parameters) => new ExFatFileSystem(stream);
}
