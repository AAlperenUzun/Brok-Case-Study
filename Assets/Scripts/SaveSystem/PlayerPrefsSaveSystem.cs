using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string SaveKey = "GameSaveData";
    
        public void SaveGame(SaveData saveData)
        {
            string jsonData = JsonConvert.SerializeObject(saveData);
            PlayerPrefs.SetString(SaveKey, jsonData);
            PlayerPrefs.Save();
        }
    
        public SaveData LoadGame()
        {
            if (!SaveExists())
                return new SaveData();
            
            string jsonData = PlayerPrefs.GetString(SaveKey);
            return JsonConvert.DeserializeObject<SaveData>(jsonData);
        }
    
        public bool SaveExists()
        {
            return PlayerPrefs.HasKey(SaveKey);
        }
    }
    public class JsonFileSaveSystem : ISaveSystem
    {
        private readonly string savePath;
    
        public JsonFileSaveSystem()
        {
            savePath = Path.Combine(Application.persistentDataPath, "SaveData.json");
        }
    
        public void SaveGame(SaveData saveData)
        {
            string jsonData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(savePath, jsonData);
        }
    
        public SaveData LoadGame()
        {
            if (!SaveExists())
                return new SaveData();
            
            string jsonData = File.ReadAllText(savePath);
            return JsonConvert.DeserializeObject<SaveData>(jsonData);
        }
    
        public bool SaveExists()
        {
            return File.Exists(savePath);
        }
    }
}