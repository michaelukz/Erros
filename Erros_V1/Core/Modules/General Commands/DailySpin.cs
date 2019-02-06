using System;
using System.Collections.Generic;
using System.Linq;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Erros;
using Erros.Errors;
using System.Text;
using System.Threading.Tasks;

namespace Erros
{
    public class DailySpin : InteractiveBase<SocketCommandContext>
    {
        [Command("spin")]
        public async Task Help()
        {
            var guser = Context.Guild.GetUser(Context.Message.Author.Id);
            var guild = Context.Guild;
            var chan = Context.Guild.GetTextChannel(Context.Channel.Id);
            var u = UserList.getUser(guser);
            if (u.LastSpin == DateTime.Today)
            {
                EmbedBuilder e = Error.avb16();
                await ReplyAsync("",false,e.Build());
            }
            else
            {
                u.LastSpin = DateTime.Today;
                Random random = new Random();
                int index = random.Next(0,99);
                if (index >= 0 && index <= 69)
                {
                    int[] array = new int[] { 100, 200, 300, 400, 500 };
                    Random xp = new Random();
                    int guess = xp.Next(array.Length);
                    int result = array[guess];
                    await Levels.Calculator(guild, guser, result);
                    Console.WriteLine("XP obtained {0}",result);
                    await ReplyAsync($"Congratulations {guser.Mention}\nYou won {result} xp!");
                }
                else if (index >= 70 && index <= 94)
                {
                    int[] array = new int[] { 100, 200, 300, 400, 500 };
                    Random xp = new Random();
                    int guess = xp.Next(array.Length);
                    int result = array[guess];
                    await Levels.InstantLevel(guild, guser, chan);
                    Console.WriteLine("Level obtained");
                }
                else
                {
                    Console.WriteLine("Nothing obtained");
                    await ReplyAsync($"Oh no!\nYou didn't win anything this time, Better luck tomorrow!");
                }
            }
            UserList.SaveUser();
        }
    }
}
