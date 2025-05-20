using System.Collections;
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
        
        StartCoroutine(WaitForRebuild(saveData));
    }
        
    private IEnumerator WaitForRebuild(TowerSave saveData)
    {   
        yield return new WaitForEndOfFrame();
        
        foreach (var cubeSave in saveData.Cubes)
        {
            var cube = _cubesPool.Get();
            cube.DragEventsRouter.SetTarget(cube.DefaultDragTarget);
            cube.Restore(cubeSave);
            cube.transform.SetParent(_towerController.transform);
            cube.transform.position = cubeSave.Position;
            cube.transform.localScale = Vector3.one;
            _towerController.TowerModel.AddCube(cube);
        }
        
        _towerController.RecalculateBoundaries();
    }
}