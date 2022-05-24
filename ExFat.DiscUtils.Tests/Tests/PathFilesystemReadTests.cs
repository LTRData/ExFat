// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils.Tests;

using System.Linq;
using Environment;
using Filesystem;
using Xunit;


[TestCategory("PathFilesystem")]
public class PathFilesystemReadTests
{
    [Fact]
    [TestCategory("Read")]
    public void ReadRootFolderEntriesTest()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatPathFilesystem(testEnvironment.PartitionStream);
        var entries = filesystem.EnumerateEntries(@"\").ToArray();
        Assert.Contains(entries, e => e.Path == DiskContent.LongContiguousFileName);
        Assert.Contains(entries, e => e.Path == DiskContent.LongSparseFile1Name);
        Assert.Contains(entries, e => e.Path == DiskContent.EmptyRootFolderFileName);
        Assert.Contains(entries, e => e.Path == DiskContent.LongFolderFileName);
    }

    [Fact]
    [TestCategory("Read")]
    public void ReadSubFolderFilesTest()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatPathFilesystem(testEnvironment.PartitionStream);
        var entries = filesystem.EnumerateEntries(DiskContent.LongFolderFileName).ToArray();
        Assert.Equal(DiskContent.LongFolderEntriesCount, entries.Length);
    }

    [Fact]
    [TestCategory("Read")]
    public void ReadDatesTest()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatPathFilesystem(testEnvironment.PartitionStream);
        var c = filesystem.GetCreationTime(DiskContent.LongContiguousFileName);
    }
}