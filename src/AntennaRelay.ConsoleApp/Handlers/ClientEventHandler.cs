using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using AntennaRelay.ConsoleApp.Models;

namespace AntennaRelay.ConsoleApp.Handlers
{
    public class ClientEventHandler
    {
        private static DiscordSocketClient _client;
        private static ConfigModel _config;
        
        public SocketTextChannel FirstChannel
        {
            get
            {
                var channel = _client.GetChannel(Convert.ToUInt64(_config.FirstChannelId));
                return (SocketTextChannel) channel;
            }
        }
        public SocketTextChannel SecondChannel
        {
            get
            {
                var channel = _client.GetChannel(Convert.ToUInt64(_config.SecondChannelId));
                return (SocketTextChannel) channel;
            }
        }

        public ClientEventHandler(DiscordSocketClient client)
        {
            _client = client;
            _config ??= new ConfigHandler().GetConfig();
        }

        public async Task InitializeEvents()
        {
            _client.MessageReceived += MessageReceived;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;
            
            if (message.Channel == FirstChannel)
                await SecondChannel.SendMessageAsync(message.Content);
            
            if (message.Channel == SecondChannel)
                await FirstChannel.SendMessageAsync(message.Content);
        }
    }
}