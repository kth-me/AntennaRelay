using AntennaRelay.ConsoleApp.Handlers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AntennaRelay.ConsoleApp
{
    internal class Client
    {
        private readonly DiscordSocketClient _client;
        private readonly Config _config;
        private readonly LogHandler _logger;
        private readonly IServiceProvider _services;

        public Client(CommandService commands = null, Config config = null, LogHandler logger = null)
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            _config = config ?? new ConfigHandler().GetConfig();
            _logger = logger ?? new LogHandler();
            _services = ConfigureServices();
        }

        public async Task InitializeAsync()
        {
            HookEvents();
            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();
            await _services.GetRequiredService<ClientEventHandler>().InitializeEvents();
            await Task.Delay(-1);
        }

        private void HookEvents()
        {
            _client.Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        private async Task OnReadyAsync()
        {
            await _client.SetGameAsync(name: _config.Status);
        }

        private Task LogAsync(LogMessage log)
        {
            _logger.Neutral(log.Message);
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<ConfigHandler>()
                .AddSingleton<ClientEventHandler>()
                .AddSingleton<LogHandler>()
                .BuildServiceProvider();
        }
    }
}