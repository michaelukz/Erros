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
            if (type == null)
            {

            }
            else
            {
                if (Context.Message.Author.Id != x.ServerOwnerID)
                {
                    EmbedBuilder e = Error.avb03();
                    await ReplyAsync("",false,e.Build());
                }
                else
                {
                    if (type.ToLower() == "set")
                    {
                        x.ServerLogChannel = $"{channel.Id}";
                    }
                    else if (type.ToLower() == "view") { }
                    else if (type.ToLower() == "create") { }
                    else
                    {

                    }
                }
            }
            await ReplyAsync("done");
        }
    }
}
