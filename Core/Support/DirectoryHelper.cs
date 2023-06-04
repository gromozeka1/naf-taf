namespace Core.Support;

public static class DirectoryHelper
{
    public static string TryGetProjectDirectoryPath()
    {
        DirectoryInfo directory = new DirectoryInfo(Paths.AssemblyPath) 
            ?? throw new ArgumentException($"Assembly path '{Paths.AssemblyPath}' is invalid");

        while (!directory.GetFiles("*.csproj").Any() && directory.Parent is not null)
        {
            directory = directory.Parent;
        }

        return directory.FullName;
    }

    public static void SetUpTempFolders()
    {
        if (!Directory.Exists(Paths.Screenshots))
        {
            Logger.Log.Information("--------------- Create folder for Screenshots ----------------");
            Directory.CreateDirectory(Paths.Screenshots);
        }

        if (!Directory.Exists(Paths.Reports))
        {
            Logger.Log.Information("--------------- Create folder for Reports ----------------");
            Directory.CreateDirectory(Paths.Reports);
        }
    }

    public static void ClearUpTempFolder()
    {
    }
}
