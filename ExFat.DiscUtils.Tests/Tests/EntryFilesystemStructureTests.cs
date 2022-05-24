// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils.Tests;

using System.Linq;
using Environment;
using Filesystem;
using Xunit;


[TestCategory("EntryFilesystem")]
public class EntryFilesystemStructureTests
{
    [Fact]
    [TestCategory("Read")]
    public void ReadFile()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatEntryFilesystem(testEnvironment.PartitionStream);
        var files = filesystem.EnumerateFileSystemEntries(filesystem.RootDirectory).ToArray();
        Assert.Contains(files, f => f.Name == DiskContent.LongContiguousFileName);
    }
}