using System.Collections;
using System.IO;
using Cube;
using DefaultNamespace;
using DragAndDrop;
using Save;
using Settings;
using Tower;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [Header("Settings JSON Usage")]
    [SerializeField] private TextAsset _settingsJson;
    [SerializeField] private bool _useJSONPresets;
    [Space]
    [SerializeField] private TextAsset _textsJson;
    [SerializeField] private TowerController _towerController;
    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private SaveDataManager _saveDataManager;
    
    [Inject] private CubePresets _cubePresets;

    private void Start()
    {
        _saveDataManager.TryLoadSave();
        CreateSpawners();
        TextProvider.Init(_textsJson.text);
    }
    
    private void CreateSpawners()
    {
        var presets = _cubePresets.Presets;
        
        if (_useJSONPresets)
        {
            var arrayWrapper = JsonUtility.FromJson<CubeSettingsArrayWrapper>(_settingsJson.text);
            presets = arrayWrapper.Presets;
        }
        
        _cubesSpawner.Setup(presets);
    }

    public void SaveState()
    {
        var save = _towerController.TowerModel.GetSave();
        
        SaveUtils.Save(save);
    }
}