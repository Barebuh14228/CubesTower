using Cube;
using DragAndDrop;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [Header("JSON Usage")]
    [SerializeField] private TextAsset _json;
    [SerializeField] private bool _useJSONPresets;
    [Space]
    [SerializeField] private TowerController _towerController;
    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private DraggingController _draggingController;
    [SerializeField] private CubesPool _cubesPool;
    
    [Inject] private CubePresets _cubePresets;

    private void Start()
    {
        CreateSpawners();
    }

    private void CreateSpawners()
    {
        var presets = _cubePresets.Presets;
        
        if (_useJSONPresets)
        {
            var arrayWrapper = JsonUtility.FromJson<CubeSettingsArrayWrapper>(_json.text);
            presets = arrayWrapper.Presets;
        }
        
        _cubesSpawner.CreateSpawners(presets);
        _cubesSpawner.SpawnCubes();
    }

    public void DragCube(CubeController cubeController)
    {
        _towerController.OnCubeDragged(cubeController);
        _draggingController.StartDragging(cubeController.DraggingCube);
    }
    
    public void DropCube(CubeController cubeController)
    {
        if (!_draggingController.TryDropItem(cubeController.DraggingCube))
        {
            cubeController.DestroyCube();
        }
        _cubesSpawner.SpawnCubes();
    }

    public void OnCubeDestroyed(CubeController cubeController)
    {
        _cubesPool.Release(cubeController);
    }
}