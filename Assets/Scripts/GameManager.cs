using Cube;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [SerializeField] private TowerController _towerController;
    
    [Inject] private CubePresets _cubePresets;
    [Inject] private UIElementsProvider _uiController;

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

    public void NotifyCubeDragged(CubeController cubeController)
    {
        _towerController.OnCubeDragged(cubeController);
    }
}