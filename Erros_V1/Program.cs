using Config;
using Discord;
using Discord.WebSocket;
using HandleCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Erros_V1_Core
{
    class Program
    {
        public static void Main(string[] args)
           => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _commands;

        public async Task StartAsync()
        {
            Configuration.EnsureExists();                    // Ensure the configuration file has been created.
            _client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,              // Specify console verbose information level.
                MessageCacheSize = 1000                      // Tell discord.net how long to store messages (per channel).
            });

            _client.Log += (l)                               // Register the console log event.
                => Console.Out.WriteLineAsync(l.ToString());

            await _client.LoginAsync(TokenType.Bot, Configuration.Load().Token);

            await _client.StartAsync();
            _commands = new CommandHandler();                // Initialize the command handler service
            await _commands.InstallAsync(_client);
            await Task.Delay(-1);
        }

    }
}
