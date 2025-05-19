using System;
using System.IO;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CubePresets", menuName = "Settings/CubePresets", order = 1)]
    public class CubePresets : ScriptableObject
    {
        [SerializeField] private CubeSettings[] _presets;
        
        public CubeSettings[] Presets => _presets;

        [ContextMenu("Create Settings JSON")]
        public void CreateSettingsJSON()
        {
            var wrapper = new CubeSettingsArrayWrapper() { Presets = _presets };
            var json = JsonUtility.ToJson(wrapper);
            
            File.WriteAllText(Application.persistentDataPath + "/SettingsJson.json", json);
            
            Debug.Log($"<color=green>SettingsJson saved to {Application.persistentDataPath}</color>");

            var xxx = new TextsDictionary();
            
            xxx.Texts = new []{ new TextKeyValuePair() {Key = "key", Value = "value"}};
            
            File.WriteAllText(Application.persistentDataPath + "/texts.json", JsonUtility.ToJson(xxx));

        }
    }

    [Serializable]
    public class CubeSettingsArrayWrapper
    {
        public CubeSettings[] Presets;
    }
}