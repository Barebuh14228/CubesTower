using System.Collections;
using System.IO;
using Cube;
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
    [SerializeField] private CubesPool _cubesPool;
    
    [Inject] private CubePresets _cubePresets;

    private void Start()
    {
        TryLoadSave();
        CreateSpawners();
        TextProvider.Init(_textsJson.text);
    }

    private void TryLoadSave()
    {
        if (!SaveUtils.TryLoad(out var saveData))
            return;

        StartCoroutine(WaitForRebuild(saveData));
    }

    private IEnumerator WaitForRebuild(TowerSave saveData)
    {   
        yield return new WaitForEndOfFrame();
        
        foreach (var cubeSave in saveData.Cubes)
        {
            var cube = _cubesPool.Get();
            cube.DragEventsRouter.SetTarget(cube.DefaultDragTarget);
            cube.Restore(cubeSave);
            cube.transform.position = cubeSave.Position;
            cube.transform.SetParent(_towerController.transform, true);
            cube.transform.localScale = Vector3.one;
            _towerController.TowerModel.AddCube(cube);
            _towerController.RecalculateBoundaries();
        }
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