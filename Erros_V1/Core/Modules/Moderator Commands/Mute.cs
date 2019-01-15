using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Erros.Errors;
using System.Text;
using System.Threading.Tasks;

namespace Erros
{
    public class Mute : InteractiveBase<SocketCommandContext>
    {
        [Command("mute")]
        public async Task MuteUser(SocketGuildUser User = null, ulong interval = 0,[Remainder] string reason = null)
        {
            var x = ServerList.getServer(Context.Guild);
            bool perms = false;
            var GUser = (SocketGuildUser)Context.Message.Author;
            if (User == null)
            {
                EmbedBuilder e = Error.avb02(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else if (interval == 0)
            {
                EmbedBuilder e = Error.avb11();
                await ReplyAsync("", false, e.Build());
            }
            else if (x.ServerLogChannel == "avb01")
            {
                EmbedBuilder e = Error.avb01(Context.Guild);
                await ReplyAsync("", false, e.Build());
            }
            else if (reason == null)
            {
                EmbedBuilder e = Error.avb04();
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                var f = ulong.TryParse(x.ServerLogChannel, out ulong res);
                SocketTextChannel log = Context.Guild.GetTextChannel(res);
                if (Context.Message.Author == Context.Guild.Owner) { perms = true; }
                if (x.MuteUserIDs.Contains(Context.Message.Author.Id)) { perms = true; }
                foreach (SocketRole rolex in GUser.Roles)
                {
                    if (x.MuteRoleIDs.Contains(rolex.Id))
                    {
                        perms = true;
                    }
                }
                switch (perms)
                {
                    case true:
                        await User.ModifyAsync(u => u.Mute = true);
                        EmbedBuilder e = Logs.avb13(User,reason,Context.Message.Author,interval);
                        await log.SendMessageAsync("", false, e.Build());
                        x.TimeoutLength.Add(interval);
                        x.TimeoutReason.Add($"{reason}");
                        x.TimeoutUsersID.Add(User.Id);
                        await Timeouts.InstantiateTimer(User,interval,reason,GUser,x.ServerLogChannel,x.ServerID);
                        break;
                    case false:
                        EmbedBuilder es = Error.avb03();
                        await ReplyAsync("", false, es.Build());
                        break;
                }
            }
        }
    }
}
