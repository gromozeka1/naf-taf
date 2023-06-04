namespace Core.Config;

public class TestSettings
{
    public string? DriverType { get; set; }

    public string? BaseUiUrl { get; set; }

    public string? BaseApiUrl { get; set; }

    public string[]? Args { get; set; }

    public float? TimeoutInSeconds;

    public bool Headless { get; set; } = true;

    public bool IsMaximized { get; set; }

    public float? SlowMo { get; set; }

    public bool DevTools { get; set; }

    public bool IgnoreHTTPSErrors { get; set; }
}
