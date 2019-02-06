using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Erros
{
    public class UserFile
    {
        public ulong UserID { get; set; }
        public string UserName { get; set; }
        public ulong Messages { get; set; }
        public ulong ConnectedServerIDs { get; set; }
        public string ConnectedServerNames { get; set; }
        public List<ulong> WarningServer { get; set; }
        public List<ulong> WarningAdmin { get; set; }
        public List<string> WarningReasons { get; set; }
        public ulong GifSent { get; set; }
        public DateTime LastSpin { get; set; }
        public int XP { get; set; }
        public uint Level { get; set; }
        public uint Prestige { get; set; }
    }
}