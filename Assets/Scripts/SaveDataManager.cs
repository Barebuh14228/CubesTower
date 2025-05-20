using System.Collections;
using System.Linq;
using Cube;
using Save;
using Tower;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField] private TowerController _towerController;
    [SerializeField] private CubesPool _cubesPool;
        
    public void TryLoadSave()
    {
        if (!SaveUtils.TryLoad(out var saveData))
            return;

        var restoredCubes = saveData.Cubes.Select(RestoreCube).ToList();
        
        _towerController.RestoreCubes(restoredCubes);
    }

    private CubeController RestoreCube(CubeModelSave save)
    {
        var cube = _cubesPool.Get();
        cube.Restore(save);
        cube.DragEventsRouter.SetTarget(cube.DefaultDragTarget);
        cube.transform.position = save.Position;
        return cube;
    }
    
}