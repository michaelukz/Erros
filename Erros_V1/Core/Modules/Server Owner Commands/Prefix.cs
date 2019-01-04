using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Erros.Errors;
using System.Threading.Tasks;

namespace Erros
{
    [Summary("Change the default prefix for your server.")]
    public class Prefix : InteractiveBase<SocketCommandContext>
    {
        [Command("prefix")]
        public async Task Pref(string prefix=null)
        {
            var x = ServerList.getServer(Context.Guild);
            await Context.Message.DeleteAsync();
            if (prefix == null)
            {
                EmbedBuilder e = Error.avb05();
                await ReplyAsync("",false,e.Build());
            }
            else
            {
                if (Context.Message.Author.Id != x.ServerOwnerID)
                {
                    EmbedBuilder e = Error.avb03();
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    if (x.ServerLogChannel == "avb01")
                    {
                        EmbedBuilder e = Error.avb01(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        x.ServerPrefix = ($"{prefix}");
                        EmbedBuilder e = Logs.avb02(prefix);
                        await ReplyAsync("", false, e.Build());
                        ServerList.SaveServer();
                    }
                }
            }
            ServerList.SaveServer();
        }
    }
}
