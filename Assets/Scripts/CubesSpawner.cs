using DragAndDrop;
using DragEventsUtils;
using Settings;
using UnityEngine;

public class CubesSpawner : DropSubscriber<DraggingCube>
{
    [SerializeField] private CubeSpawnContainer _spawnContainerPrefab;
    [SerializeField] private DraggingController _draggingController;
    [SerializeField] private DragEventsListener _defaultDragTarget;
    [SerializeField] private CubesPool _cubesPool;

    private CubeSpawnContainer[] _containers;
    
    public void Setup(CubeSettings[] settingsArray)
    {
        _containers = new CubeSpawnContainer[settingsArray.Length];

        for (int i = 0; i < _containers.Length; i++)
        {
            _containers[i] = CreateSpawner(settingsArray[i]);
            _draggingController.AddDragSubscriber(_containers[i]);
        }

        SpawnCubes();
    }
    
    private CubeSpawnContainer CreateSpawner(CubeSettings settings)
    {
        var spawner = Instantiate(_spawnContainerPrefab, transform);
        
        spawner.SetSettings(settings);
        spawner.SetDragTarget(_defaultDragTarget);

        return spawner;
    }

    private void SpawnCubes()
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

    protected override void NotifyOnDrop(DraggingCube draggingItem)
    {
        SpawnCubes();
    }
}