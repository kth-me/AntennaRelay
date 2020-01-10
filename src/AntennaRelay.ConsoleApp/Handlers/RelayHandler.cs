using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AntennaRelay.ConsoleApp.Models;
using Discord.WebSocket;

namespace AntennaRelay.ConsoleApp.Handlers
{
    public static class RelayHandler
    {
        private const string _dataDirectory = "Data";
        private const string _relaysFile = "relays.json";
        private const string _relaysLocation = _dataDirectory + "/" + _relaysFile;

        public static List<Dictionary<string, Dictionary<string, string>>> GetRelays()
            => GetRelayData();

        private static List<Dictionary<string, Dictionary<string, string>>> GetRelayData()
        {
            CheckRelaysExists();
            var data = File.ReadAllText(_relaysLocation);
            var deserializedData = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, string>>>>(data);
            return deserializedData;
        }

        private static void CheckRelaysExists()
        {
            if (!Directory.Exists(_dataDirectory))
                Directory.CreateDirectory(_dataDirectory);

            if (File.Exists(_relaysLocation))
                return;

            var data = JsonConvert.SerializeObject(value: RelayModel.Relay, Formatting.Indented);
            File.WriteAllText(_relaysLocation, data, Encoding.UTF8);
        }
    }
}