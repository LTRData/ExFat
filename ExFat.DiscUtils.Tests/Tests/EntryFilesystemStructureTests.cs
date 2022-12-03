// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

using ExFat.DiscUtils.Environment;
using ExFat.Filesystem;
using System.Linq;

namespace ExFat.DiscUtils.Tests;
[Trait("Category", "EntryFilesystem")]
public class EntryFilesystemStructureTests
{
    [Fact]
    [Trait("Category", "Read")]
    public void ReadFile()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatEntryFilesystem(testEnvironment.PartitionStream);
        var files = filesystem.EnumerateFileSystemEntries(filesystem.RootDirectory).ToArray();
        Assert.Contains(files, f => f.Name == DiskContent.LongContiguousFileName);
    }
}