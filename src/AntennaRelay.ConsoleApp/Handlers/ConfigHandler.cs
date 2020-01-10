using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace AntennaRelay.ConsoleApp.Handlers
{
    internal class ConfigHandler
    {
        private const string _dataDirectory = "Data";
        private const string _configFile = "config.json";
        private const string _configLocation = _dataDirectory + "/" + _configFile;

        public Config GetConfig()
            => GetConfigData();

        private Config GetConfigData()
        {
            CheckConfigExists();
            var data = File.ReadAllText(_configLocation);
            return JsonConvert.DeserializeObject<Config>(data);
        }

        private Config GenerateDefaultConfig()
            => new Config
            {
                Token = "",
                Status = "Relay Ready",
                FirstChannelId = "663641901358121010",
                SecondChannelId = "663887969576419328",
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
            var json = JsonConvert.SerializeObject(GenerateDefaultConfig(), Formatting.Indented);
            File.WriteAllText(_configLocation, json, Encoding.UTF8);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}