using System.Collections.Generic;
using System.Linq;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

//todo требуется написать систему определяющую пересечение rect-а с определенной зоной, причем как с квадратной так и с овальной
    
public class GameManager : MonoBehaviour
{
    [Inject] private CubeCreator _cubeCreator;
    [Inject] private CubePresets _cubePresets;
    [Inject] private UIController _uiController;
        
    private void Start()
    {
        SetupCubesPalette();
    }

    private void SetupCubesPalette() //todo rename
    {
        var presets = _cubePresets.Presets;

        foreach (var settings in presets)
        {
            var cube = _cubeCreator.CreateCube(settings);
            cube.SetDraggableTarget(_uiController.ScrollDraggable);
                
            _uiController.CubesPalette.AddCube(cube);
        }
    }
        
    public void TryDropCube(CubeController cubeController)
    {
        //todo выяснить попали ли мы в контейнеры HoleParent и DropParent
        //todo если не попали, тогда уничтожаем кубик
        //todo если кубик был из палитры то создаем новый на замену
            
        //как выяснилось, когда у канваса Screen Space выставлен в Overlay, свойство position у RectTranform
        //совпадает с его позицией на экране, хотя по идее должен возвращать мировую координату

        var corners = cubeController.GetCorners();
            
        foreach (var c in corners)
        {
            var contains = RectTransformUtility.RectangleContainsScreenPoint(_uiController.HoleParent, c);
        }
            
        var cube = _cubeCreator.CreateCube(cubeController);
        cube.SetDraggableTarget(_uiController.ScrollDraggable);
            
        _uiController.CubesPalette.ReplaceCube(cube);
            
        cubeController.DestroyCube();
    }
}