using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Erros;
using Erros.Errors;

namespace Erros
{
    public class Blocklist : InteractiveBase<SocketCommandContext>
    {
        [Command("blacklist")]
        public async Task blacklist(SocketGuildUser user = null,[Remainder]string reason = null)
        {
            var x = ServerList.getServer(Context.Guild); // Fetch the server from the server file and save for x (yay math joke.)
            if (x.ServerLogChannel == "nul")
            {
                EmbedBuilder e = Error.avb01(Context.Guild);
                await ReplyAsync("",false,e.Build());
            }
            else
            {
                if (user == null)
                {
                    EmbedBuilder e = Error.avb02(Context.Guild);
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    if (reason == null)
                    {
                        EmbedBuilder e = Error.avb04();
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        bool perms = false;
                        if (Context.Message.Author == Context.Guild.Owner)
                        {
                            perms = true;
                        }
                        if (x.blackListUserIDs.Contains(Context.Message.Author.Id))
                        {
                            perms = true;
                        }
                        if  (!x.blackListUserIDs.Contains(Context.Message.Author.Id)) // If the user doesn't have permission, check if their roles do.
                        {
                            foreach (SocketRole rolex in user.Roles)
                            {
                                if (x.blackListRoleIDs.Contains(rolex.Id))
                                {
                                    perms = true;
                                }
                            }
                        }
                        if (perms == true)
                        {
                            x.BlackListedUsers.Add(user.Id);
                            EmbedBuilder e = Logs.avb12(user,reason,Context.Message.Author);
                            await ReplyAsync("", false, e.Build());
                        }
                        else
                        {
                            EmbedBuilder e = Error.avb03();
                            await ReplyAsync("", false, e.Build());
                        }
                    }
                }
            }
            //var u = UserList.getUser(user);
        }
    }
}
