// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.DiscUtils.Tests;

using Environment;
using Xunit;


[TestCategory("Partition")]
public class IntegrityTests
{
    [Fact]
    [TestCategory("Detection")]
    public void ValidVolume()
    {
        using var testEnvironment = StreamTestEnvironment.FromExistingVhdx();
        Assert.True(ExFatFileSystem.Detect(testEnvironment.PartitionStream));
    }
}