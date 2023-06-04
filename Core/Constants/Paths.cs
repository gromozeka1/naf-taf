using Core.Support;
using System.Reflection;

namespace Core.Constants;

public static class Paths
{
    public static string AssemblyPath { get; } = Path.GetDirectoryName(
        Assembly.GetExecutingAssembly().Location)!;
    
    public static string Screenshots { get; } = Path.GetFullPath("Screenshots", DirectoryHelper.TryGetProjectDirectoryPath());

    public static string Reports { get; } = Path.GetFullPath("Report", DirectoryHelper.TryGetProjectDirectoryPath());
}
