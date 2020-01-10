using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using AntennaRelay.ConsoleApp.Models;

namespace AntennaRelay.ConsoleApp.Handlers
{
    internal class ConfigHandler
    {
        private const string _dataDirectory = "Data";
        private const string _configFile = "config.json";
        private const string _configLocation = _dataDirectory + "/" + _configFile;

        public ConfigModel GetConfig()
            => GetConfigData();

        private ConfigModel GetConfigData()
        {
            CheckConfigExists();
            var data = File.ReadAllText(_configLocation);
            var deserializedData = JsonConvert.DeserializeObject<ConfigModel>(data);
            return deserializedData;
        }

        private ConfigModel GenerateDefaultConfig()
            => new ConfigModel
            {
                Token = "",
                Playing = "Relay Ready",
                FirstChannelId = "663641901358121010",
                SecondChannelId = "663887969576419328"
            };

        private void CheckConfigExists()
        {
            if (!Directory.Exists(_dataDirectory))
                Directory.CreateDirectory(_dataDirectory);

            if (File.Exists(_configLocation))
                return;

            Console.WriteLine("No config file found.\n" +
                              $"A new one has been generated at {_configLocation}\n" +
                              "Fill in required values and restart the bot.");
            var data = JsonConvert.SerializeObject(GenerateDefaultConfig(), Formatting.Indented);
            File.WriteAllText(_configLocation, data, Encoding.UTF8);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}