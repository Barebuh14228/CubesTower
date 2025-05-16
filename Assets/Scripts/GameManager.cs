using Cube;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [SerializeField] private TowerController _towerController;
    
    [Inject] private CubeCreator _cubeCreator;
    [Inject] private CubePresets _cubePresets;
    [Inject] private UIController _uiController;
    
    private void Start()
    {
        CreateSpawners();
    }

    private void CreateSpawners()
    {
        var presets = _cubePresets.Presets;

        foreach (var settings in presets)
        {
            _uiController.SpawnersContainer.CreateSpawner(settings);
        }
    }

    public bool TryDropCubeOnTower(CubeController cubeController)
    {
        return _towerController.TryDropCube(cubeController);
    }

    public void DropCubeInHole(CubeController cubeController)
    {
        //todo animate & destroy
    }
}