using Cube;
using Settings;
using UnityEngine;
using Zenject;

//todo требуется написать систему определяющую пересечение rect-а с определенной зоной, причем как с квадратной так и с овальной
    
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
        
    public void DropCube(CubeModel cube)
    {
        //todo выяснить попали ли мы в контейнеры HoleParent и DropParent
        //todo если не попали, тогда уничтожаем кубик
        //todo если кубик был из палитры то создаем новый на замену
            
        //как выяснилось, когда у канваса Screen Space выставлен в Overlay, свойство position у RectTranform
        //совпадает с его позицией на экране, хотя по идее должен возвращать мировую координату

        /*var corners = cubeController.GetCorners();
            
        foreach (var c in corners)
        {
            var contains = RectTransformUtility.RectangleContainsScreenPoint(_uiController.HoleParent, c);
        }*/
        
        cube.DestroyCube();
        _uiController.SpawnersContainer.RespawnCubes();
    }
}