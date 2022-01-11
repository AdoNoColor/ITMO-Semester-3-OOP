using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Reports.Tools;

namespace Reports.DataAccessLayer.Abstractions
{
    public abstract class AbstractStorage<T>
    {
        public List<T> Data { get; private set; }

        public void Add(T item) => Data.Add(item);

        public bool Remove(T item) => Data.Remove(item);

        public List<T> Find(Func<T, bool> predicate)
        {
            var ans = Data.Where(predicate).ToList();

            return ans.Count != 0 ? ans : null;
        }
        
        public string SerializeToJson()
        {
            var jsonPath = JsonConvert.SerializeObject(Data, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            var restorePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/сonfig";

            if (!Directory.Exists(restorePath))
            {
                Directory.CreateDirectory(restorePath);
            }

            if (File.Exists($"{restorePath}/.config.json"))
                File.Delete($"{restorePath}/.config.json");

            using var streamWriter = File.CreateText($"{restorePath}/.config.json");
            streamWriter.WriteLine(jsonPath);

            return jsonPath;
        }
        
        public List<T> DeserializeFromJson(string jsonPath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(jsonPath)))
                throw new ReportsException("Nothing was found!");
            if (!File.Exists(jsonPath))
                throw new ReportsException("Nothing was found!");
            

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonPath), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
        }
    }
}