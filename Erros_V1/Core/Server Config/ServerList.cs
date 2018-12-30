using System;
using Discord.WebSocket;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Erros;

namespace Erros
{
    public static class ServerList
    {
        public static List<ServerFile> server;

        private static string AccountFile = @"C:\\Users\\micha\\source\\repos\\Erros_V1\\Erros_V1\\Core\\Server Config\\UserJSON\\Server.json";

        static ServerList()
        {
            if (ServerStorage.IfExists(AccountFile))
            {
                server = ServerStorage.LoadGuildAccount(AccountFile).ToList();
            }
            else
            {
                server = new List<ServerFile>();
                SaveServer();
            }
        }
        public static void SaveServer()
        {
            ServerStorage.SaveGuildAcc(server, AccountFile);
        }
        public static ServerFile getServer(SocketGuild guild)
        {
            return getOrCreateServer(guild.Id, guild.Name, guild.Owner.Id, guild.Owner.Username);
        }

        private static ServerFile getOrCreateServer(ulong Serverid,string ServerName,ulong owner,string OwnerName)
        {
            var result = from a in server
                         where a.ServerID == Serverid
                         select a;

            var guild = result.FirstOrDefault();
            if (guild == null)
            {
                guild = CreateServer(Serverid,ServerName,owner,OwnerName);
                SaveServer();
            }
            return guild;
        }
        private static ServerFile CreateServer(ulong ServerID,string ServerName,ulong owner,string OwnerName)
        {
            var newacc = new ServerFile()
            {
                ServerID = ServerID,
                ServerName = ServerName,
                ServerOwner = OwnerName,
                ServerOwnerID = owner,
                ServerLogChannel = "avb01",
                ServerPrefix = ")",
                AutoRoleID = 0,
                AutoRoleName = "nul",
                BanUserIDs = new List<ulong> { 216989163835097090 },
                BanRoleIDs = new List<ulong> { },
                KickRoleIDs = new List<ulong> { 216989163835097090 },
                KickUserIDs = new List<ulong> { }
            };
            server.Add(newacc);
            SaveServer();
            return newacc;
        }
    }
}
