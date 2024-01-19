namespace Demo1.Common
{
    public sealed class AppSettingsConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; init; } = new();
        public BogusGenerateData BogusGenerateData { get; init; } = new();
    }

    public sealed class ConnectionStrings
    {
        public readonly string Config = "ConnectionStrings";
        public string DefaultConnection { get; set; }
    }
    public class BogusGenerateData
    {
        public readonly string Config = "BogusGenerateData";
        public int NumberOfEmployees { get; set; }
        public int NumberOfOpenRequests { get; set; }
    }
}
