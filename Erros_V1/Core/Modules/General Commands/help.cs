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
            EmbedBuilder e = HelpItem.avb01(Context.Guild);
            await ReplyAsync("", false, e.Build());
        }
        [Command("help")]
        public async Task HelpAsync(string helpType)
        {
            if (helpType.ToLower() == "owner")
            {
                EmbedBuilder e = HelpItem.avb02(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else if (helpType.ToLower() == "admin")
            {
                EmbedBuilder e = HelpItem.avb03(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                await ReplyAsync("I don't regognise that one, run `help` for a list of types.");
            }
        }
    }
}
