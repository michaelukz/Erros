using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;

namespace Erros
{
    public class TestCommand : ModuleBase<SocketCommandContext>
    {
        [Command("🛠️"),Alias("🛠")]
        public async Task TEST([Remainder] string x)
        {
            await Context.Client.SetStatusAsync(UserStatus.Online);
            await Context.Client.SetGameAsync("Activating Command.", null, ActivityType.Playing);
            var d = x.ToString();
            var r = d;
            await ReplyAsync("I respond to Emoji's also!");
            await Task.Delay(1000);
            await Context.Client.SetGameAsync("Listening for commands!", null, ActivityType.Listening);
            await Context.Client.SetStatusAsync(UserStatus.Idle);
        }
        [Command("commands")]
        public async Task CountComms()
        {
            await Context.Client.SetStatusAsync(UserStatus.Online);
            await Context.Client.SetGameAsync("Activating Command.", null, ActivityType.Playing);
            var x = HandleCommands.CommandHandler.GetCommands();
            await ReplyAsync($"So far today {x} commands have been used!");
            await Task.Delay(1000);
            await Context.Client.SetGameAsync("Listening for commands!", null, ActivityType.Listening);
            await Context.Client.SetStatusAsync(UserStatus.Idle);
        }
    }
}
