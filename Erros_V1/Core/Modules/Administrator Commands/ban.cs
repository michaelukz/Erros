using System;
using System.Collections.Generic;
using System.Linq;
using Discord.Commands;
using Discord;
using Discord.Addons.Interactive;
using Discord.WebSocket;
using Erros;
using Erros.Errors;
using NReco;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NReco.ImageGenerator;

namespace Erros
{
    [Summary("Ban a user from a server.")]
    public class Ban : InteractiveBase<SocketCommandContext>
    {
        [Command("ban")]
        public async Task BanAsync(SocketUser user = null,[Remainder]string reason = null)
        {
            await Context.Message.DeleteAsync();
            var channel = (SocketGuildChannel)Context.Message.Channel;
            var guild = channel.Guild;
            var server = ServerList.getServer(guild);
            var GUser = (SocketGuildUser)Context.Message.Author;
            if (server.ServerLogChannel=="avb01")
            {
                EmbedBuilder e = Error.avb01(guild);
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                if (user == null)
                {
                    EmbedBuilder e = Error.avb02(guild);
                    await ReplyAsync("",false,e.Build());
                }
                else
                {
                    bool Perms = true;
                    if (Context.Message.Author == Context.Guild.Owner) { Perms = true; }
                    if (!server.BanUserIDs.Contains(Context.Message.Author.Id))
                    {
                        Perms = false;
                        foreach (SocketRole role in GUser.Roles)
                        {
                            if (server.BanRoleIDs.Contains(role.Id))
                            {
                                Perms = true;
                            }
                        }
                    }
                    else if (Perms == false)
                    {
                        EmbedBuilder e = Error.avb03();
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
                            var output = ulong.TryParse(server.ServerLogChannel, out ulong LogID);
                            var logchannel = guild.GetTextChannel(LogID);
                            EmbedBuilder e = Logs.avb01(user, reason, GUser);
                            await guild.AddBanAsync(user, 0, reason);
                            await logchannel.SendMessageAsync("", false, e.Build());
                        }
                    }

                }
            }
        }
    }
}
