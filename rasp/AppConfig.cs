namespace rasp;

public sealed class AppConfig
{
    public AppConfig()
    {
        ConnectionStringId = string.Empty;
        ConnectionStrings = [];
    }

    public AppConfig(string connectionStringId, List<ConnectionString> connectionStrings)
    {
        ConnectionStringId = connectionStringId;
        ConnectionStrings = connectionStrings;
    }

    public string ConnectionStringId { get; set; }

    public List<ConnectionString> ConnectionStrings { get; set; }
}

public record ConnectionString(string Name, string Value);
