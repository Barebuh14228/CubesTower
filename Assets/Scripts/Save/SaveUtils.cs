using System.IO;
using UnityEditor;
using UnityEngine;

namespace Save
{
    public static class SaveUtils
    {
        private static string _saveFileName = "save.json";
        private static string _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
        
        public static void Save(TowerSave saveData)
        {
            var json = JsonUtility.ToJson(saveData);
            
            File.WriteAllText(_savePath, json);
        }

        public static bool TryLoad(out TowerSave result)
        {
            result = null;
            
            if (!File.Exists(_savePath))
                return false;
            
            var json = File.ReadAllText(_savePath);
            
            result = JsonUtility.FromJson<TowerSave>(json);
            
            return true;
        }

        [MenuItem("Save Data/Clear")]
        public static void DeleteSave()
        {
            if (!File.Exists(_savePath))
                return;
            
            File.Delete(_savePath);
        }
    }
}