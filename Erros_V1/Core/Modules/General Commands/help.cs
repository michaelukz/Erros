using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using Discord.Commands;
using Erros.Errors;

namespace Erros
{
    public class Help : InteractiveBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            EmbedBuilder e = Logs.avb10(Context.Guild);
            await ReplyAsync("", false, e.Build());
        }
        [Command("help")]
        public async Task HelpAsync(string helpType)
        {
            if (helpType.ToLower() == "owner")
            {
                EmbedBuilder e = Logs.avb11(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
        }
    }
}
