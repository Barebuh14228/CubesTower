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
    }
    
    public void DropCube(CubeController cubeController)
    {
        _cubesSpawner.SpawnCubes();
    }
}