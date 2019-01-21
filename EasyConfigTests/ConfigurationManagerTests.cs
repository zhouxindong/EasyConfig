using System.ComponentModel;
using System.IO;
using System.Text;
using EasyConfig;
using EasyConfig.Attributes;
using EasyConfig.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyConfigTests
{
    [TestClass()]
    public class ConfigurationManagerTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            var builder = new StringBuilder();
            builder.AppendLine("[Server]");
            builder.AppendLine("Name\t= WebRequestMethods.Http proxy host");
            builder.AppendLine("Ip = 192.168.137.128");
            builder.AppendLine("Port\t= 8080");
            //builder.AppendLine("Version\t= 1.1");
            builder.AppendLine("[Contact]");
            builder.AppendLine("FirstName = Antony");
            builder.AppendLine("LastName = Hopkins");
            builder.AppendLine("Telephone = +97254641234");
            builder.AppendLine("Address\t  = Medison st. 14-45");
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            ConfigurationManager.Load("application", file_name);
            ServerConfiguration server = ConfigurationManager.GetClass<ServerConfiguration>("application");
            Assert.IsNotNull(server);
            Assert.AreEqual("WebRequestMethods.Http proxy host", server.Name);
            Assert.AreEqual("192.168.137.128", server.Ip);
            Assert.AreEqual(8080, server.Port);
            Assert.AreEqual(2.3, server.Version, 0.0000001);
            Contact contact = ConfigurationManager.GetClass<Contact>("application");
            Assert.AreEqual("Antony", contact.FirstName);
            Assert.AreEqual("Hopkins", contact.LastName);
            Assert.AreEqual("+97254641234", contact.Telephone);
            Assert.AreEqual("Medison st. 14-45", contact.Address);

            Assert.AreEqual("192.168.137.128", ConfigurationManager.GetValue<string>("application",
                "Server", "Ip"));
            //Assert.AreEqual(2.3, ConfigurationManager.GetValue<double>("application",
            //    "Server", "Version"), 0.0000001);
            //Assert.AreEqual(1.1, ConfigurationManager.GetValue<double>("application",
            //    "Server", "ProtocolVersion"), 0.0000001);

            File.Delete(file_name);
        }
    }

    [ConfigurationSection("Server")]
    class ServerConfiguration : ConfigWithDefaultValue
    {
        [ConfigurationProperty]
        public string Name { get; set; }

        [ConfigurationProperty]
        public string Ip { get; set; }

        [ConfigurationProperty]
        public int Port { get; set; }

        [ConfigurationProperty("ProtocolVersion")]
        [DefaultValue(2.3)]
        public float Version { get; set; }
    }

    [ConfigurationSection()]
    class Contact : ConfigWithDefaultValue
    {
        [ConfigurationProperty]
        public string FirstName { get; set; }
        [ConfigurationProperty]
        public string LastName { get; set; }
        [ConfigurationProperty]
        public string Telephone { get; set; }
        [ConfigurationProperty]
        public string Address { get; set; }
    }

}