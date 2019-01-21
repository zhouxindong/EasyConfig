using System.Collections.Generic;

namespace EasyConfig.Internal
{
    public interface IConfigurationFileReader
    {
        List<ConfigurationSection> Read(string file_name);
    }
}