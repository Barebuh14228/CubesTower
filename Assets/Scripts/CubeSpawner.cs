using Cube;
using Settings;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
        
        [Inject] private CubeCreator _cubeCreator;
        
        private CubeSettings _settings;
        private CubeController _cubeController;

        public void Setup(CubeSettings settings)
        {
            _settings = settings;
            SpawnCube();
        }

        private void SpawnCube()
        {
            _cubeController = _cubeCreator.CreateCube(_settings);
            _cubeController.transform.SetParent(transform, false);
            _cubeController.CreateInSpawner();
            _layoutComponentsDisabler.RebuildAndDisable();
        }

        public void ReleaseCube()
        {
            _cubeController.ReleaseFromSpawner();
            _cubeController.OnDropEvent += RespawnCube;
        }

        private void RespawnCube()
        {
            _cubeController.OnDropEvent -= RespawnCube;
            SpawnCube();
            _cubeController.AppearInSpawner();
        }
    }
}