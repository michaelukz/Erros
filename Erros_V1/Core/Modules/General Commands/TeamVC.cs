using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Erros;
using Erros.Errors;

namespace Erros
{
    public class TeamVC : InteractiveBase<SocketCommandContext>
    {
        [Command("teamvc"), Alias("tvc")]
        public async Task TeamVoiceChannel([Remainder]string stuff = null)
        {
            var msg = Context.Message;
            var gxr = (SocketGuildChannel)Context.Message.Channel;
            var guild = gxr.Guild;
            var x = ServerList.getServer(guild);
            var GU = (SocketGuildUser)Context.Message.Author;
            if (GU.VoiceChannel == null)
            {
                EmbedBuilder e = Error.avb13();
                await ReplyAsync("", false, e.Build());
            }
            else if (stuff == null)
            {
                EmbedBuilder e = Error.avb14();
                await ReplyAsync("", false, e.Build());
            }
            else
            {
                var users = msg.MentionedUsers;
                int count = 0;
                foreach (SocketUser user in users)
                {
                    if (user.Id == GU.Id)
                    {
                        continue;
                    }
                    else
                    {
                        count += 1;
                    }
                }
                if (count == 0)
                {
                    EmbedBuilder e = Error.avb15();
                    await ReplyAsync("", false, e.Build());
                    return;
                }
                char[] letters = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
                string Name = "";
                Random r = new Random();
                for (int i = 0; i < 9; i += 1)
                {
                    Name += letters[r.Next(0, 25)];
                }
                var chan = await guild.CreateVoiceChannelAsync($"{Name}");
                var everyone = guild.EveryoneRole;
                ulong cid = chan.Id;
                foreach (SocketUser user in users)
                {
                    if (user.Id == GU.Id)
                    {
                        continue;
                    }
                    else
                    {
                        var userOverwrite = new OverwritePermissions(connect: PermValue.Allow, speak: PermValue.Allow, useVoiceActivation: PermValue.Allow);
                        await chan.AddPermissionOverwriteAsync(user, userOverwrite);
                    }
                }
                var ownerOverwrite = new OverwritePermissions(connect: PermValue.Allow, speak: PermValue.Allow, useVoiceActivation: PermValue.Allow);
                var EveryoneOverwrite = new OverwritePermissions(connect: PermValue.Deny, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                await chan.AddPermissionOverwriteAsync(GU, ownerOverwrite);
                await chan.AddPermissionOverwriteAsync(everyone, EveryoneOverwrite);
                await GU.ModifyAsync(f => f.ChannelId = cid);
                x.TeamVCs.Add(cid);
                x.TeamVCOwnerID.Add(GU.Id);
                ServerList.SaveServer();

            }
        }
    }
}
