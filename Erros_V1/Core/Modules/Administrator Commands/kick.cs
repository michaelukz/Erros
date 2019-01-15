using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;

namespace Erros
{
    [Summary("Kick a user.")]
    public class Kick : InteractiveBase<SocketCommandContext>
    {
        [Command("kick")]
        public async Task KickAsync(SocketGuildUser user = null, [Remainder]string reason = null)
        {
            var server = ServerList.getServer(Context.Guild);
            if (server.ServerLogChannel == "avb01")
            {
                EmbedBuilder e = Errors.Error.avb01(Context.Guild);
                await ReplyAsync("", false, e.Build());
                return;
            }
            var logchan = ulong.TryParse(server.ServerLogChannel, out ulong logchanid);
            var logchannel = Context.Guild.GetTextChannel(logchanid);
            var GUser = (SocketGuildUser)Context.Message.Author;
            if (user == null)
            {
                EmbedBuilder e = Errors.Error.avb02(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                bool perms = false;
                if (Context.Message.Author == Context.Guild.Owner)
                {
                    perms = true;
                }
                if (!server.KickUserIDs.Contains(Context.Message.Author.Id))
                {
                    foreach (SocketRole rolex in GUser.Roles)
                    {
                        if (server.KickRoleIDs.Contains(rolex.Id))
                        {
                            perms = true;
                        }
                    }
                }
                if (perms == true)
                {
                    if (server.ServerLogChannel == "avb01")
                    {
                        EmbedBuilder e = Errors.Error.avb01(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        if (reason == null)
                        {
                            EmbedBuilder e = Errors.Error.avb04();
                            await ReplyAsync("", false, e.Build());
                        }
                        else
                        {
                            await user.KickAsync(reason);
                            EmbedBuilder e = Errors.Logs.avb07(user, reason, (SocketGuildUser)Context.Message.Author);
                            await logchannel.SendMessageAsync("", false, e.Build());
                        }
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
        [Command("kick")]
        public async Task KickAsync(SocketRole role = null, [Remainder]string reason = null)
        {
            var server = ServerList.getServer(Context.Guild);
            var GUser = (SocketGuildUser)Context.Message.Author;
            if (server.ServerLogChannel == "avb01")
            {
                EmbedBuilder e = Errors.Error.avb01(Context.Guild);
                await ReplyAsync("", false, e.Build());
                return;
            }
            var logchan = ulong.TryParse(server.ServerLogChannel, out ulong logchanid);
            var logchannel = Context.Guild.GetTextChannel(logchanid);
            if (role == null)
            {
                EmbedBuilder e = Errors.Error.avb07(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                bool perms = false;
                if (Context.Message.Author == Context.Guild.Owner)
                {
                    perms = true;
                }
                if (server.KickUserIDs.Contains(Context.Message.Author.Id))
                {
                    foreach (SocketRole rolex in GUser.Roles)
                    {
                        if (server.KickRoleIDs.Contains(rolex.Id))
                        {
                            perms = true;
                        }
                    }
                }
                if (perms == true)
                {
                    if (reason == null)
                    {
                        EmbedBuilder e = Errors.Error.avb04();
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        uint count = 0;
                        if (role.Members.Count() == 0)
                        {
                            EmbedBuilder e = Errors.Error.avb08();
                            await ReplyAsync("", false, e.Build());
                        }
                        else
                        {
                            foreach (SocketGuildUser userx in role.Members)
                            {
                                count += 1;
                                await userx.KickAsync(reason);
                            }
                            EmbedBuilder e = Errors.Logs.avb08(role, reason, (SocketGuildUser)Context.Message.Author, count);
                            await logchannel.SendMessageAsync("", false, e.Build());
                        }
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
