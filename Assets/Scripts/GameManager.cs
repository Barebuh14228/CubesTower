using Cube;
using DragAndDrop;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [SerializeField] private TowerController _towerController;
    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private DraggingController _draggingController;
    [SerializeField] private CubeCreator _cubeCreator;
    
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
        _draggingController.StartDragging(cubeController.DraggingItem);
    }
    
    public void DropCube(CubeController cubeController)
    {
        if (!_draggingController.TryDropItem(cubeController.DraggingItem))
        {
            cubeController.DestroyCube();
        }
        _cubesSpawner.SpawnCubes();
    }

    public void OnCubeDestroyed(CubeController cubeController)
    {
        _cubeCreator.ReturnToPool(cubeController);
    }
}