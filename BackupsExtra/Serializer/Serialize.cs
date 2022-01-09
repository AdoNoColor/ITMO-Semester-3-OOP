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
            var serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            string json = JsonConvert.SerializeObject(backupJob, (Newtonsoft.Json.Formatting)Formatting.Indented, serializerSettings);

            string restorePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/сonfig";
            if (!Directory.Exists(restorePath))
            {
                Directory.CreateDirectory(restorePath);
            }

            if (File.Exists($"{restorePath}/.restore.json"))
                File.Delete($"{restorePath}/.restore.json");

            using StreamWriter streamWriter = File.CreateText($"{restorePath}/.config.json");
            streamWriter.WriteLine(json);

            return json;
        }

        public static BackupJobExtra DeserializeFromJson(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                throw new BackupsExtraException("No information about restore point found");
            if (!File.Exists(path))
                throw new BackupsExtraException("No information about restore point found");

            var serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
            };

            return JsonConvert.DeserializeObject<BackupJobExtra>(File.ReadAllText(path), serializerSettings);
        }
    }
}
