using System.ComponentModel;
using EasyConfig.Attributes;

namespace EasyConfig.Internal.Tests
{
    [ConfigurationSection("Server")]
    public class ServerConfig : ConfigWithDefaultValue
    {
        [ConfigurationProperty]
        public string Name { get; set; }

        [ConfigurationProperty("Ip")]
        public string IpAddress { get; set; }

        [ConfigurationProperty]
        public int Port { get; set; }

        [ConfigurationProperty]
        [DefaultValue(2.3)]
        public double Version { get; set; }
    }
}