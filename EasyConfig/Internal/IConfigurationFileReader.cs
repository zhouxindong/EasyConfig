namespace EasyConfig.Internal
{
    interface IConfigurationFileReader
    {
        ConfigurationSection[] Read(string file_name);
    }
}