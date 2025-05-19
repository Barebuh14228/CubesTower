using Cube;
using DragAndDrop;
using Settings;
using Tower;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
    
public class GameManager : MonoBehaviour
{
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
        _cubesSpawner.CreateSpawners(_cubePresets.Presets);
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