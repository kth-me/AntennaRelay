﻿using Discord.WebSocket;
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
        private readonly ConfigModel _config;
        private static List<Dictionary<string, Dictionary<string, string>>> _relays;

        public ClientEventHandler(DiscordSocketClient client)
        {
            _client = client;
            _config ??= new ConfigHandler().GetConfig();
            _relays = RelayHandler.GetRelays();
        }

        public async Task InitializeEvents()
        {
            _client.MessageReceived += MessageReceived;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id || 
                !message.Content.Contains(_config.RelayPrefix))
                return;

            var filteredMessage = FilterMessage(message);

            foreach (var relay in _relays)
            {
                foreach (var link in relay)
                {
                    if (message.Channel == GetChannelFromIdString(link.Value["SourceChannelId"]))
                    {
                        await GetChannelFromIdString(link.Value["DestinationChannelId"]).SendMessageAsync(text: filteredMessage);
                    }
                }
            }
        }

        private string FilterMessage(SocketMessage message)
        {
            var filteredMessage = message.Content.Remove(message.Content.IndexOf(_config.RelayPrefix), _config.RelayPrefix.Length).Trim();

            if (!message.Author.IsBot)
                filteredMessage = $"{message.Author.Username}: {filteredMessage}";
            
            return filteredMessage;
        }

        private SocketTextChannel GetChannelFromIdString(string channelId)
        {
            var channel = _client.GetChannel(Convert.ToUInt64(channelId));
            return (SocketTextChannel)channel;
        }

    }
}