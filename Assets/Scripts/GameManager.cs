using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
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
        
        public void OnCubeDropped(CubeController cubeController)
        {
            var cube = _cubeCreator.CreateCube(cubeController);
            cube.SetDraggableTarget(_uiController.ScrollDraggable);
            
            _uiController.CubesPalette.ReplaceCube(cube);
            
            Destroy(cubeController.gameObject);
        }
    }
}