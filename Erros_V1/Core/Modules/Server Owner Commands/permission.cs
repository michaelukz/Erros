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
    [Summary("A permission system.")]
    public class Permission : InteractiveBase<SocketCommandContext>
    {
        [Command("addperm"),Summary("Add a permission to a role.")]
        public async Task Perms(string perm = null, SocketRole role =null)
        {
            await Context.Message.DeleteAsync();
            var x = ServerList.getServer(Context.Guild);
            if (Context.Message.Author.Id != x.ServerOwnerID)
            {
                EmbedBuilder e = Error.avb03();
                await ReplyAsync("",false,e.Build());
            }
            else
            {
                if (perm == null)
                {
                    EmbedBuilder e = Error.avb06();
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    if (role == null)
                    {
                        EmbedBuilder e = Error.avb07(Context.Guild);
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        string permissions = perm.ToLower();
                        if (x.ServerLogChannel == "avb01")
                        {
                            EmbedBuilder e = Error.avb01(Context.Guild);
                            await ReplyAsync("", false, e.Build());
                        }
                        else if (permissions == "-ban")
                        {
                            var logstring = x.ServerLogChannel;
                            var test = ulong.TryParse(logstring,out ulong LogID);
                            var logchannel = Context.Guild.GetTextChannel(LogID);
                            EmbedBuilder e = Logs.avb03(role,perm);
                            await logchannel.SendMessageAsync("",false,e.Build());
                            x.BanRoleIDs.Add(role.Id);
                            ServerList.SaveServer();
                        }
                    }
                }
            }
        }
        [Command("addperm"), Summary("Add a permission to a user.")]
        public async Task Perms(string perm = null, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            var x = ServerList.getServer(Context.Guild);
            if (Context.Message.Author.Id != x.ServerOwnerID)
            {
                EmbedBuilder e = Error.avb03();
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                if (perm == null)
                {
                    EmbedBuilder e = Error.avb06();
                    await ReplyAsync("", false, e.Build());
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
                        string permissions = perm.ToLower();
                        if (x.ServerLogChannel == "avb01")
                        {
                            EmbedBuilder e = Error.avb01(Context.Guild);
                            await ReplyAsync("", false, e.Build());
                        }
                        else if (permissions == "-ban")
                        {
                            var logstring = x.ServerLogChannel;
                            var test = ulong.TryParse(logstring, out ulong LogID);
                            var logchannel = Context.Guild.GetTextChannel(LogID);
                            EmbedBuilder e = Logs.avb04(user, perm);
                            await logchannel.SendMessageAsync("", false, e.Build());
                            x.BanUserIDs.Add(user.Id);
                            ServerList.SaveServer();
                        }
                    }
                }
            }
        }
        [Command("delperm")]
        public async Task DeletePermissions(string permission = null, SocketRole role = null)
        {
            await Context.Message.DeleteAsync();
            var x = ServerList.getServer(Context.Guild);
            if (x.ServerOwnerID == Context.Message.Author.Id)
            {
                if (x.ServerLogChannel == "avb01")
                {
                    EmbedBuilder e = Error.avb01(Context.Guild);
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    if (permission == null)
                    {
                        EmbedBuilder e = Error.avb06();
                        await ReplyAsync("", false, e.Build());
                    }
                    else
                    {
                        if (role == null)
                        {
                            EmbedBuilder e = Error.avb07(Context.Guild);
                            await ReplyAsync("", false, e.Build());
                        }
                        else
                        {
                            if (permission.ToLower() == "-ban")
                            {
                                x.BanRoleIDs.Remove(role.Id);
                                EmbedBuilder e = Logs.avb06(role, permission);
                                await ReplyAsync("", false, e.Build());
                                ServerList.SaveServer();
                            }
                        }
                    }
                }
            }
            else
            {
                EmbedBuilder e = Error.avb03();
                await ReplyAsync("", false, e.Build());
            }
        }
        [Command("delperm")]
        public async Task DeletePermissions(string permission = null, SocketGuildUser user = null)
        {
            await Context.Message.DeleteAsync();
            var x = ServerList.getServer(Context.Guild);
            if (x.ServerOwnerID == Context.Message.Author.Id)
            {
                if (x.ServerLogChannel == "avb01")
                {
                    EmbedBuilder e = Error.avb01(Context.Guild);
                    await ReplyAsync("", false, e.Build());
                }
                else
                {
                    if (permission == null)
                    {
                        EmbedBuilder e = Error.avb06();
                        await ReplyAsync("", false, e.Build());
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
                            if (permission.ToLower() == "-ban")
                            {
                                x.BanRoleIDs.Remove(user.Id);
                                EmbedBuilder e = Logs.avb05(user, permission);
                                await ReplyAsync("", false, e.Build());
                                ServerList.SaveServer();
                            }
                        }
                    }
                }
            }
            else
            {
                EmbedBuilder e = Error.avb03();
                await ReplyAsync("", false, e.Build());
            }
        }
    }
}
