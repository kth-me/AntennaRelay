using Discord.WebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using AntennaRelay.ConsoleApp.Models;

namespace AntennaRelay.ConsoleApp.Handlers
{
    public class ClientEventHandler
    {
        private static DiscordSocketClient _client;
        private RelayModel _relays;

        //private List<Dictionary<string, Dictionary<string, string>>> _relays = new List<Dictionary<string, Dictionary<string, string>>>()
        //{
        //    new Dictionary<string, Dictionary<string, string>>()
        //    {
        //        {
        //            "FirstRelay", new Dictionary<string, string>()
        //            {
        //                {"Source", "457063331518349315"},
        //                {"Destination", "650599729348083722"}
        //            }
        //        }
        //    },
        //    new Dictionary<string, Dictionary<string, string>>()
        //    {
        //        {
        //            "SecondRelay", new Dictionary<string, string>()
        //            {
        //                {"Source", "665034602448158731"},
        //                {"Destination", "665034625185349632"}
        //            }
        //        }
        //    },
        //    new Dictionary<string, Dictionary<string, string>>()
        //    {
        //        {
        //            "ThirdRelay", new Dictionary<string, string>()
        //            {
        //                {"Source", "457063331518349315"},
        //                {"Destination", "665034625185349632"}
        //            }
        //        }
        //    }
        //};

        public ClientEventHandler(DiscordSocketClient client)
        {
            _client = client;
            _relays = new RelayHandler().GetRelays();
        }

        public async Task InitializeEvents()
        {
            _client.MessageReceived += MessageReceived;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id || !message.Content.StartsWith("/a"))
                return;

            foreach (var relay in _relays)
            {
                foreach (var link in relay)
                {
                    if (message.Channel == GetChannelFromId(link.Value["Source"]))
                        await GetChannelFromId(link.Value["Destination"]).SendMessageAsync(message.Content);
                }
            }
        }

        private SocketTextChannel GetChannelFromId(string channelId)
        {
            var channel = _client.GetChannel(Convert.ToUInt64(channelId));
            return (SocketTextChannel)channel;
        }
    }
}