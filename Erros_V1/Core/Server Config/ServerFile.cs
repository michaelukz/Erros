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
    public class ServerFile
    {
        public ulong ServerID { get; set; }
        public string ServerName { get; set; }
        public string ServerOwner { get; set; }
        public ulong ServerOwnerID { get; set; }
        public string ServerLogChannel { get; set; }
        public string ServerPrefix { get; set; }
        public string AutoRoleName { get; set; }
        public ulong AutoRoleID { get; set; }
        public List<ulong> MutedUserIDs { get; set; }
        public List<ulong> BanUserIDs { get; set; }
        public List<ulong> BanRoleIDs { get; set; }
        public List<ulong> KickUserIDs { get; set; }
        public List<ulong> KickRoleIDs { get; set; }
    }
}