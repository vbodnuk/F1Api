using YamlDotNet.Serialization;

namespace Formula1ApiConnection;

public class SettingsModels
{
    [YamlMember(Alias = "DwhConnectionString",ApplyNamingConventions = false)]
    public string DwhConnectionString { get; set; }
}