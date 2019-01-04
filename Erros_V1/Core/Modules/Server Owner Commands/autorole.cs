using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Discord.Addons.Interactive;

namespace Erros
{
    public class Autorole : InteractiveBase<SocketCommandContext>
    {
        [Command("autorole")]
        public async Task Autoroles(SocketRole role = null)
        {
            var x = ServerList.getServer(Context.Guild);
            if (x.ServerLogChannel == "avb01")
            {
                EmbedBuilder e = Errors.Error.avb01(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                if (Context.Message.Author.Id == x.ServerOwnerID)
                {
                    if (role == null)
                    {
                        EmbedBuilder e = Errors.Error.avb07(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        x.AutoRoleID = role.Id;
                        x.AutoRoleName = role.Name;
                        EmbedBuilder e = Errors.Logs.avb09(role);
                        await ReplyAsync("", false, e.Build());
                    }
                }
                else
                {
                    EmbedBuilder e = Errors.Error.avb03();
                    await ReplyAsync("", false, e.Build());
                }
            }
            ServerList.SaveServer();
        }
    }
}
