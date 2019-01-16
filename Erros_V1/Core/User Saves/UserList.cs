using System;
using Discord.WebSocket;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Erros;

namespace Erros
{
    public static class UserList
    {
        public static List<UserFile> users;

        private static string AccountFile = @"D:\\Github\\Erros_V1\\Core\\User Saves\\UserJSON\\Users.json";

        static UserList()
        {
            if (UserStorage.IfExists(AccountFile))
            {
                users = UserStorage.LoadUserAccount(AccountFile).ToList();
            }
            else
            {
                users = new List<UserFile>();
                SaveUser();
            }
        }
        public static void SaveUser()
        {
            UserStorage.SaveUserAcc(users, AccountFile);
        }
        public static UserFile getUser(SocketGuildUser user)
        {
            return getOrCreateUser(user.Id, user.Username,user.Guild.Id,user.Guild.Name);
        }
        private static UserFile getOrCreateUser(ulong userid,string username,ulong serverid,string servername)
        {
            var result = from a in users
                         where a.UserID == userid
                         where a.ConnectedServerIDs == serverid
                         select a;

            var user = result.FirstOrDefault();
            if (user == null)
            {
                user = CreateUser(userid,username, serverid,servername);
                SaveUser();
            }
            return user;
        }
        private static UserFile CreateUser(ulong UserID,string Username,ulong serverid,string servername)
        {
            var newacc = new UserFile()
            {
                UserID = UserID,
                UserName = Username,
                ConnectedServerIDs = serverid,
                ConnectedServerNames = servername,
                Messages = 0,
                WarningAdmin = new List<ulong> { },
                WarningReasons = new List<string> { },
                WarningServer = new List<ulong> { }
            };
            users.Add(newacc);
            SaveUser();
            return newacc;
        }
    }
}
