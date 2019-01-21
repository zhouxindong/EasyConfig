using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyConfig.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyConfig.Internal.Tests
{
    [TestClass()]
    public class SectionMapperTests
    {
        [TestMethod()]
        public void MapSectionTest()
        {
            var builder = new StringBuilder();
            builder.AppendLine(" [Server] ");
            builder.AppendLine("\tName\t= WebRequestMethods.Http proxy host");
            builder.AppendLine("\tIp = 192.168.137.128");
            builder.AppendLine("\tPort\t= 8080");
            //builder.AppendLine("\tVersion\t= 1.1");
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            var config = new TextConfigurationFileReader().Read(file_name);
            Assert.AreEqual(config.Count, 1);
            Assert.AreEqual(config[0].Properties.Count, 3);

            var server_obj = new SectionMapper().MapSection<ServerConfig>(config[0]);
            Assert.AreEqual(server_obj.Name, "WebRequestMethods.Http proxy host");
            Assert.AreEqual(server_obj.IpAddress, "192.168.137.128");
            Assert.AreEqual(server_obj.Port, 8080);
            Assert.AreEqual(server_obj.Version, 2.3);

            ConfigurationManager.Load("application", file_name);
            var server = ConfigurationManager.GetClass<ServerConfig>("application");

            File.Delete(file_name);
        }
    }
}