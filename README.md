# ExFat (deprecated)

**This repository is no longer maintained.** The LTRData exFAT implementation has moved to [DiscUtils.ExFat in LTRData/DiscUtils](https://github.com/LTRData/DiscUtils/tree/LTRData.DiscUtils-initial/Library/DiscUtils.ExFat).

Both projects were migrated in November 2025: [ExFat.DiscUtils](https://github.com/LTRData/DiscUtils/commit/5f1cc8ca7f25058b719ff94d013270551ab2d087) became `DiscUtils.ExFat`, and [ExFat.Core](https://github.com/LTRData/DiscUtils/commit/ce4c1f8dc9ab8437f0816ab79f678cfff1dc2bee) was incorporated into the same library under `DiscUtils.ExFat.Internal`. Subsequent development and fixes take place in the DiscUtils repository. Use that implementation for new development and migrate existing consumers away from the standalone packages here.

## Replacement package

Use [LTRData.DiscUtils.ExFat](https://www.nuget.org/packages/LTRData.DiscUtils.ExFat):

```sh
dotnet add package LTRData.DiscUtils.ExFat
```

See the [current package README](https://github.com/LTRData/DiscUtils/blob/LTRData.DiscUtils-initial/Library/DiscUtils.ExFat/README.md) for capabilities, examples and registration guidance, and the [DiscUtils repository](https://github.com/LTRData/DiscUtils) for framework targets and build instructions. Report issues with the maintained implementation in [LTRData/DiscUtils](https://github.com/LTRData/DiscUtils/issues).

## Migration notes

Replace references to `LTRData.ExFat.DiscUtils` and/or `LTRData.ExFat.Core` with `LTRData.DiscUtils.ExFat`. The replacement contains both the DiscUtils wrapper and the former core implementation.

| Previous API or namespace | Location in the replacement |
| --- | --- |
| `ExFat.DiscUtils.ExFatFileSystem` | `DiscUtils.ExFat.ExFatFileSystem` |
| `ExFat.DiscUtils.ExFatSetupHelper` | `DiscUtils.ExFat.ExFatSetupHelper` |
| `ExFat.ExFatFormatOptions`, `ExFat.ExFatOptions` | `DiscUtils.ExFat.Internal` |
| `ExFat.Filesystem` | `DiscUtils.ExFat.Internal.Filesystem` |
| `ExFat.Partition` | `DiscUtils.ExFat.Internal.Partition` |

For example, applications using the DiscUtils wrapper should change:

```csharp
using ExFat.DiscUtils;
```

to:

```csharp
using DiscUtils.ExFat;
```

The old standalone core APIs now reside under namespaces containing `Internal`. Review those usages against the current source and prefer the `ExFatFileSystem` API where it meets your needs. This is a source migration, not a drop-in assembly replacement; rebuild and test consumers against the current package.

For filesystem discovery, current DiscUtils supports explicit registration:

```csharp
DiscUtils.ExFat.Formats.Register();
```

Direct construction of `ExFatFileSystem` does not require registration. Register other providers as needed when using generic disk opening; see the current package README linked above.

## Historical source and attribution

This repository retains the standalone libraries, tests and internal generator as historical source. The [previous README](https://github.com/LTRData/ExFat/blob/55893929d507d3249b636bd972f3160b5e13b33c/README.md) is available for reference; its package, build-status and development-status guidance is obsolete.

The code originated in [picrap/ExFat](https://github.com/picrap/ExFat), written by Pascal Craponne, and was subsequently adapted in this LTRData fork. The [MIT license](https://github.com/LTRData/ExFat/blob/LTRData.ExFat-initial/LICENSE) and original attribution remain in place.
