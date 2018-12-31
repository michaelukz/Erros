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
            await ReplyAsync("");
        }
    }
}
