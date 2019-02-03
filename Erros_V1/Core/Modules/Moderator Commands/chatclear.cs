using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Commands;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using Discord.Net;
using Discord.Rest;
using Erros;
using Erros.Errors;
using System.Text;
using System.Threading.Tasks;

namespace Erros
{
    public class Chatclear : InteractiveBase<SocketCommandContext>
    {
        [Command("purge"),Alias("prune")]
        public async Task Prune(int messagecount = 0)
        {
            var x = ServerList.getServer(Context.Guild);
            var gchan = (SocketTextChannel)Context.Channel;
            var GUser = (SocketGuildUser)Context.Message.Author;
            if (x.ServerLogChannel == "avb01")
            {
                EmbedBuilder e = Error.avb01(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                var res = ulong.TryParse(x.ServerLogChannel, out ulong logid);
                var logs = Context.Guild.GetTextChannel(logid);
                if (messagecount==0)
                {
                    EmbedBuilder e = Error.avb12();
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    bool perms = false;
                    if (Context.Message.Author.Id == Context.Guild.Owner.Id)
                    {
                        perms = true;
                    } 
                    if (x.PruneUserIDs.Contains(Context.Message.Author.Id))
                    {
                        perms = true;
                    }
                    foreach (SocketRole rolex in GUser.Roles)
                    {
                        if (x.PruneRoleIDs.Contains(rolex.Id))
                        {
                            perms = true;
                        }
                    }
                    switch (perms)
                    {
                        case true:
                            var msgs = (await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, messagecount).FlattenAsync());
                            await (Context.Channel as ITextChannel).DeleteMessagesAsync(msgs as IEnumerable<IMessage>);
                            await Context.Message.DeleteAsync();
                            EmbedBuilder e = Logs.avb16(messagecount,GUser,gchan);
                            await logs.SendMessageAsync("", false, e.Build());
                            break;

                        case false:
                            EmbedBuilder er = Error.avb03();
                            await ReplyAsync("", false, er.Build());
                            break;
                    }
                }
            }
        }
    }
}
