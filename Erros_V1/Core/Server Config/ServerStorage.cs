using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Erros;
using System.Runtime.Serialization;

namespace Erros
{
    public static class ServerStorage
    {
        //Save Users
        public static void SaveGuildAcc(IEnumerable<ServerFile> server, string filePath)
        {
            string json = JsonConvert.SerializeObject(server);
            File.WriteAllText(filePath, json);
        }
        //Load Users
        public static IEnumerable<ServerFile> LoadGuildAccount(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ServerFile>>(json);
        }
        //Check if path exists
        public static bool IfExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
