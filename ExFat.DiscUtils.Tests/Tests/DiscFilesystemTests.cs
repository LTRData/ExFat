// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils.Tests;

using System.IO;
using System.Linq;
using Environment;
using Xunit;


[TestCategory("DiscUtils")]
public class DiscFilesystemTests
{
    [Fact]
    [TestCategory("Read")]
    public void ReadAllFiles()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatFileSystem(testEnvironment.PartitionStream);
        var allFiles = filesystem.GetFiles("", "0*", SearchOption.AllDirectories);
        Assert.True(allFiles.All(p => Path.GetFileName(p).StartsWith("0")));
    }

    [Fact]
    [TestCategory("Read")]
    public void ReadRootFiles()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatFileSystem(testEnvironment.PartitionStream);
        var allFiles = filesystem.GetFiles("");
        Assert.True(allFiles.Contains(DiskContent.LongContiguousFileName));
        Assert.True(allFiles.Contains(DiskContent.LongSparseFile1Name));
        Assert.True(allFiles.Contains(DiskContent.LongSparseFile2Name));
        Assert.False(allFiles.Contains(DiskContent.EmptyRootFolderFileName));
        Assert.False(allFiles.Contains(DiskContent.LongFolderFileName));
    }

    [Fact]
    [TestCategory("Read")]
    public void ReadRootDirectories()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatFileSystem(testEnvironment.PartitionStream);
        var allDirectories = filesystem.GetDirectories("");
        Assert.False(allDirectories.Contains(DiskContent.LongContiguousFileName));
        Assert.False(allDirectories.Contains(DiskContent.LongSparseFile1Name));
        Assert.False(allDirectories.Contains(DiskContent.LongSparseFile2Name));
        Assert.True(allDirectories.Contains(DiskContent.EmptyRootFolderFileName));
        Assert.True(allDirectories.Contains(DiskContent.LongFolderFileName));
    }

    [Fact]
    [TestCategory("Write")]
    public void MoveFile()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatFileSystem(testEnvironment.PartitionStream);
        using (var a = filesystem.OpenFile("a", FileMode.Create))
        {
            a.WriteByte(1);
        }

        Assert.True(filesystem.FileExists("a"));
        filesystem.MoveFile("a", "b");
        Assert.False(filesystem.FileExists("a"));
        Assert.True(filesystem.FileExists("b"));
    }

    [Fact]
    [TestCategory("Write")]
    public void MoveFileToDirectory()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        using var filesystem = new ExFatFileSystem(testEnvironment.PartitionStream);
        using (var a = filesystem.OpenFile("a", FileMode.Create))
        {
            a.WriteByte(1);
        }

        filesystem.CreateDirectory("d");
        Assert.True(filesystem.FileExists("a"));
        filesystem.MoveFile("a", "d");
        Assert.False(filesystem.FileExists("a"));
        Assert.True(filesystem.FileExists("d\\a"));
    }
}