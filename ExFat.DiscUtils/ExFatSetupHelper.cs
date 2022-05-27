// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat


using System.Reflection;
using DiscUtils.Setup;

namespace ExFat.DiscUtils;
public static class ExFatSetupHelper
{
    public static void SetupFileSystems() => SetupHelper.RegisterAssembly(Assembly.GetExecutingAssembly());
}
