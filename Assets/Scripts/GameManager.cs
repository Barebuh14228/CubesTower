using DragAndDrop;
using DragEventsUtils;
using Save;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [Header("Settings JSON Usage")]
    [SerializeField] private TextAsset _settingsJson;
    [SerializeField] private bool _useJSONPresets;
    [Space]
    [SerializeField] private TextAsset _textsJson;
    [SerializeField] private CubeSpawnContainer _spawnContainerPrefab;
    [SerializeField] private TowerController _towerController;
    
    [SerializeField] private DraggingController _draggingController;
    [SerializeField] private DragEventsListener _defaultDragTarget;
    [SerializeField] private Transform _spawnersContainer;
    
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
        
        SetupCubesPalette(presets);
    }

    private void SetupCubesPalette(CubeSettings[] settingsArray)
    {
        var containers = new CubeSpawnContainer[settingsArray.Length];

        for (int i = 0; i < containers.Length; i++)
        {
            containers[i] = CreateSpawner(settingsArray[i]);
            _draggingController.AddDragSubscriber(containers[i].CubeDragSubscriber);
            _draggingController.AddDropSubscriber(containers[i].CubeDropSubscriber);

            containers[i].SpawnCube();
        }
    }
    
    private CubeSpawnContainer CreateSpawner(CubeSettings settings)
    {
        var spawner = Instantiate(_spawnContainerPrefab, _spawnersContainer);
        
        spawner.SetSettings(settings);
        spawner.SetDragTarget(_defaultDragTarget);

        return spawner;
    }

    public void SaveState()
    {
        var save = _towerController.TowerModel.GetSave();
        
        SaveUtils.Save(save);
    }
}