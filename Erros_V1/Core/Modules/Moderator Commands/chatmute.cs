using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erros;
using Erros.Errors;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Erros
{
    [Summary("Mute a player from chatting in your server.")]
    public class Chatmute : InteractiveBase<SocketCommandContext>
    {
        [Command("ChatMute"),Alias("cmute")]
        public async Task Mute(SocketUser user = null,[Remainder]string reason = null)
        {
            var x = ServerList.getServer(Context.Guild);
            if (Context.Message.Author.Id != x.ServerOwnerID)
            {
                var e = Error.avb03();
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                if (x.ServerLogChannel == "nul")
                {
                    var e = Error.avb01(Context.Guild);
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    var f = ulong.TryParse(x.ServerLogChannel, out ulong res);
                    SocketTextChannel log = Context.Guild.GetTextChannel(res);
                    if (user == null)
                    {
                        var e = Error.avb02(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else if (reason == null)
                    {
                        var e = Error.avb04();
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        var e = Logs.avb11(user, reason, Context.Message.Author);
                        await log.SendMessageAsync("", false, e.Build());
                    }
                }
            }
        }
    }
}
