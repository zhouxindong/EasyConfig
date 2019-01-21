using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyConfig.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyConfig.Internal.Tests
{
    [TestClass()]
    public class TextConfigurationFileReaderTests
    {
        [TestMethod()]
        public void ReadConfigFileTest()
        {
            var builder = new StringBuilder();
            builder.AppendLine("// this is a config used for Server");
            builder.AppendLine(" [Server] ");
            builder.AppendLine("\tName\t= WebRequestMethods.Http proxy host");
            builder.AppendLine("\tIp = 192.168.137.128");
            builder.AppendLine("\tPort\t= 8080");
            builder.AppendLine("\tVersion\t= 1.1");
            builder.AppendLine("[Contact]");
            builder.AppendLine("\tFirstName = Antony");
            builder.AppendLine("\tLastName = Hopkins");
            builder.AppendLine("\tTelephone = +97254641234");
            builder.AppendLine("\tAddress\t  = Medison st. 14-45");
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            var config = new TextConfigurationFileReader().ReadConfigFile(file_name);
            foreach (var section in config)
            {
                Console.WriteLine($"[{section.Key}]");
                foreach (var property in section.Value)
                {
                    Console.WriteLine($"({property.Item1})=({property.Item2})");
                }
            }

            File.Delete(file_name);
        }

        [TestMethod]
        public void ReadConfigFileTest2()
        {
            var builder = new StringBuilder();
            builder.AppendLine("/* this is a config used for Server");
            builder.AppendLine(" another line */");
            builder.AppendLine("// [TestConfigSection] used for test");
            builder.AppendLine("[TestConfig]");
            builder.AppendLine();
            builder.AppendLine("DoubleProperty = 123.123456789");
            builder.AppendLine("StringProperty = TestConfigString");
            builder.AppendLine("BooleanProperty = true");
            builder.AppendLine();
            builder.AppendLine("IntegerProperty = 1000");
            builder.AppendLine();
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            var config = new TextConfigurationFileReader().ReadConfigFile(file_name);
            foreach (var section in config)
            {
                Console.WriteLine($"[{section.Key}]");
                foreach (var property in section.Value)
                {
                    Console.WriteLine($"({property.Item1})=({property.Item2})");
                }
            }

            File.Delete(file_name);
        }

        [TestMethod]
        public void ReadConfigFileTest3()
        {
            var builder = new StringBuilder();
            builder.AppendLine("[TestConfig]");
            builder.AppendLine("DoubleProperty = 123.123456789");
            builder.AppendLine("StringProperty = TestConfigString");
            builder.AppendLine("BooleanProperty = true");
            builder.AppendLine("sometext");
            builder.AppendLine("=no name");
            builder.AppendLine("IntegerProperty = 1000");
            builder.AppendLine("NullProperty=");
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            var config = new TextConfigurationFileReader().ReadConfigFile(file_name);
            foreach (var section in config)
            {
                Console.WriteLine($"[{section.Key}]");
                foreach (var property in section.Value)
                {
                    Console.WriteLine($"({property.Item1})=({property.Item2})");
                }
            }

            File.Delete(file_name);
        }

        [TestMethod()]
        public void ReadTest()
        {
            var builder = new StringBuilder();
            builder.AppendLine("// this is a config used for Server");
            builder.AppendLine(" [Server] ");
            builder.AppendLine("\tName\t= WebRequestMethods.Http proxy host");
            builder.AppendLine("\tIp = 192.168.137.128");
            builder.AppendLine("\tPort\t= 8080");
            builder.AppendLine("\tVersion\t= 1.1");
            builder.AppendLine("[Contact]");
            builder.AppendLine("\tFirstName = Antony");
            builder.AppendLine("\tLastName = Hopkins");
            builder.AppendLine("\tTelephone = +97254641234");
            builder.AppendLine("\tAddress\t  = Medison st. 14-45");
            var file_name = Path.GetTempFileName();
            var file_to_write = new StreamWriter(file_name);
            file_to_write.Write(builder.ToString());
            file_to_write.Close();

            var config = new TextConfigurationFileReader().Read(file_name);
            foreach (var item in config)
            {
                Console.WriteLine(item.ToString());
            }

            File.Delete(file_name);
        }
    }
}