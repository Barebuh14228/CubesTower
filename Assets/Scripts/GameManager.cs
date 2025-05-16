using Settings;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
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
}