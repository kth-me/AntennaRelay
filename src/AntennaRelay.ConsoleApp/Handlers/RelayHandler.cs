using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AntennaRelay.ConsoleApp.Models;
using Discord.WebSocket;

namespace AntennaRelay.ConsoleApp.Handlers
{
    internal class RelayHandler
    {
        private const string _dataDirectory = "Data";
        private const string _relaysFile = "relays.json";
        private const string _relaysLocation = _dataDirectory + "/" + _relaysFile;
        private static readonly Dictionary<string, string> _relays;

        public RelayModel GetRelays()
            => GetRelaysData();

        private RelayModel GetRelaysData()
        {
            CheckRelaysExists();
            var data = File.ReadAllText(_relaysLocation);
            var deserializedData = JsonConvert.DeserializeObject<RelayModel>(data);
            return deserializedData;
        }

        private RelayModel GenerateDefaultRelays()
            => new RelayModel{};

        private void CheckRelaysExists()
        {
            if (!Directory.Exists(_dataDirectory))
                Directory.CreateDirectory(_dataDirectory);

            if (File.Exists(_relaysLocation))
                return;

            Console.WriteLine("No relays file found.\n" +
                              $"A new one has been generated at {_relaysLocation}\n" +
                              "Fill in required values and restart the bot.");
            var data = JsonConvert.SerializeObject(GenerateDefaultRelays(), Formatting.Indented);
            File.WriteAllText(_relaysLocation, data, Encoding.UTF8);
            Console.ReadKey();
            Environment.Exit(0);
        }

        static RelayHandler()
        {
            var data = File.ReadAllText(_relaysLocation);
            var deserializedData = JsonConvert.DeserializeObject<dynamic>(data);
            _relays = deserializedData.ToObject<Dictionary<string, string>>();
        }
    }
}