using Config;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using Erros;
using System.Timers;
using System.Threading.Tasks;
using Erros.Errors;
using Timer = System.Timers.Timer;

namespace HandleCommands
{
    /// <summary> Detect whether a message is a command, then execute it. </summary>

    public class CommandHandler : InteractiveBase
    {
        public DiscordSocketClient _client;
        public static Timer v1;
        public static DateTime OldDay;
        public static uint CommandsToday;
        private CommandService _cmds;
        internal async Task InstallAsync(DiscordSocketClient c)
        {
            _client = c;                                                 // Save an instance of the discord client.
            _cmds = new CommandService();                                // Create a new instance of the commandservice. 
            _client.MessageReceived += HandleCommandAsync;
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly());    // Load all modules from the assembly.
            SocketSelfUser user = _client.CurrentUser;
            _client.Ready += ClientReady;
            _client.UserJoined += UserJoinedGuild;
        }

        private async Task UserJoinedGuild(SocketGuildUser user)
        {
            var g = user.Guild;
            var x = ServerList.getServer(g);
            var u = UserList.getUser(user);
            var output = ulong.TryParse(x.ServerLogChannel, out ulong LogID);
            var log = g.GetTextChannel(LogID);
            if (x.AutoRoleName == "nul")
            {
                Console.WriteLine($"[{g.Name}] A new user has joined. No Autorole is set.");
                return;
            }
            else
            {
                var role = g.GetRole(x.AutoRoleID);
                if (role == null)
                {
                    EmbedBuilder e = Error.avb09();
                    await log.SendMessageAsync("",false,e.Build());
                }
                else
                {
                    await user.AddRoleAsync(role);
                    Console.WriteLine($"[{g.Name}] A new user has joined. Assigned {user.Username} the {role.Name} role.");
                }
            }
            UserList.SaveUser();
            ServerList.SaveServer();
        }

        public static uint GetCommands()
        {
            return CommandsToday;
        }
        private async Task ClientReady()
        {
            await _client.SetStatusAsync(UserStatus.Idle);
            await _client.SetGameAsync("Listening for commands!",null, ActivityType.Listening);
        }

        public static Task InstantiateTimer()
        {
            //1500000 = 25Minutes
            v1 = new Timer() { Interval = 1500000, Enabled = true, AutoReset = true };
            v1.Elapsed += V1_Ticked;
            v1.Start();
            OldDay = DateTime.Now.Date;
            return Task.CompletedTask;
        }
        public async static void V1_Ticked(object sender, ElapsedEventArgs e)
        {
            if (OldDay != DateTime.Now.Date)
            {
                Console.WriteLine("A day has just passed. Commands Reset to 0.");
                CommandsToday = 0;
            }
        }

        public async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            var channel = (SocketTextChannel)s.Channel;
            var user = channel.Guild.GetUser(s.Author.Id);
            var x = ServerList.getServer(channel.Guild);
            var u = UserList.getUser(user);
            var services = new ServiceCollection()
                 .AddSingleton(_client)
                 .AddSingleton<InteractiveService>()
                 .BuildServiceProvider();
            var context = new SocketCommandContext(_client, msg);     // Create a new command context.
            var app = await context.Client.GetApplicationInfoAsync();
            if (s.Author.IsBot) return;
            Console.WriteLine($"[{s.Channel}][{s.Author}]: ({s}) @{DateTime.Now.Hour}:{DateTime.Now.Minute} ");
            if (x.MutedUserIDs.Contains(s.Author.Id)) { await s.DeleteAsync(); Console.WriteLine("User is Muted"); return; }
            if (x.BlackListedUsers.Contains(s.Author.Id)) { Console.WriteLine("User is Blacklisted"); return; }
            u.Messages += 1;
            int argPos = 0;                                           // Check if the message has either a string or mention prefix.
            if (msg.HasStringPrefix(x.ServerPrefix, ref argPos) ||
                msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {                                                         // Try and execute a command with the given context.
                var result = await _cmds.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)                                // If execution failed, reply with the error message.
                    await app.Owner.SendMessageAsync($"{result.Error} : {result.ErrorReason}");
                else
                {
                    CommandsToday += 1;
                }
            }
            UserList.SaveUser();
            ServerList.SaveServer();
        }
    }
}