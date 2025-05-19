using DragEventsUtils;
using Settings;
using UnityEngine;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField] private CubeSpawnContainer _spawnContainerPrefab;
    [SerializeField] private DragEventsListener _defaultDragTarget;
    [SerializeField] private CubesPool _cubesPool;

    private CubeSpawnContainer[] _containers;
    
    public void CreateSpawners(CubeSettings[] settingsArray)
    {
        _containers = new CubeSpawnContainer[settingsArray.Length];

        for (int i = 0; i < _containers.Length; i++)
        {
            _containers[i] = CreateSpawner(settingsArray[i]);
        }
    }
    
    private CubeSpawnContainer CreateSpawner(CubeSettings settings)
    {
        var spawner = Instantiate(_spawnContainerPrefab, transform);
        
        spawner.SetSettings(settings);
        spawner.SetDragTarget(_defaultDragTarget);

        return spawner;
    }

    public void SpawnCubes()
    {
        foreach (var container in _containers)
        {
            if (!container.IsEmpty())
                continue;

            var cube = _cubesPool.Get(container.GetSettings());
            
            cube.DragEventsProvider.SetTarget(_defaultDragTarget);
            
            container.PlayAppearAnimation();
            container.SetCube(cube);
        }
    }
}