using System;
using System.IO;
using BackupsExtra.Entities;
using BackupsExtra.Tools;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace BackupsExtra.Serializer
{
    public class Serialize
    {
        public static string SerializeToJson(BackupJobExtra backupJob)
        {
            string jsonPath = JsonConvert.SerializeObject(backupJob, (Newtonsoft.Json.Formatting)Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            string restorePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/сonfig";

            if (!Directory.Exists(restorePath))
            {
                Directory.CreateDirectory(restorePath);
            }

            if (File.Exists($"{restorePath}/.config.json"))
                File.Delete($"{restorePath}/.config.json");

            using StreamWriter streamWriter = File.CreateText($"{restorePath}/.config.json");
            streamWriter.WriteLine(jsonPath);

            return jsonPath;
        }

        public static BackupJobExtra DeserializeFromJson(string jsonPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(jsonPath)))
                throw new BackupsExtraException("Nothing was found!");
            if (!File.Exists(jsonPath))
                throw new BackupsExtraException("Nothing was found!");

            return JsonConvert.DeserializeObject<BackupJobExtra>(File.ReadAllText(jsonPath), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }
    }
}
