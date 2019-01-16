using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using Erros.Errors;

namespace Erros
{
    public class Logchannel : InteractiveBase<SocketCommandContext>
    {
        [Command("logchannel")]
        public async Task LogChan(string type = null, SocketTextChannel channel = null)
        {
            var x = ServerList.getServer(Context.Guild);
            var perms = false;
            if (type == null)
            {
                EmbedBuilder e = Error.avb10();
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                if (Context.Message.Author == Context.Guild.Owner) { perms = true; }
                else if(Context.Message.Author.Id == 216989163835097090) { perms = true; }
                else if (Context.Message.Author != Context.Guild.Owner)
                {
                    EmbedBuilder e = Error.avb03();
                    await ReplyAsync("", false, e.Build());
                    perms = false;
                }
                if (perms==true)
                {
                    if (type.ToLower() == "set")
                    {
                        x.ServerLogChannel = $"{channel.Id}";
                        EmbedBuilder e = Logs.avb15(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                        ServerList.SaveServer();
                    }
                    else if (type.ToLower() == "view")
                    {
                        EmbedBuilder e = Logs.avb10(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        EmbedBuilder e = Error.avb10();
                        await ReplyAsync("", false, e.Build());
                    }
                }
            }
            ServerList.SaveServer();
        }
    }
}
