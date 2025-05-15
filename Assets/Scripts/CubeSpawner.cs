using Cube;
using DragAndDrop;
using Settings;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
        
        [Inject] private CubeCreator _cubeCreator;
        
        private CubeSettings _settings;
        private CubeModel _cubeModel;
        
        public void SetSettings(CubeSettings settings)
        {
            _settings = settings;
        }

        public void SpawnCube()
        {
            _cubeModel = _cubeCreator.CreateCube(_settings);
            _cubeModel.transform.SetParent(_container, false);
            _layoutComponentsDisabler.RebuildAndDisable();
        }

        public void ReleaseCube()
        {
            _cubeModel.ReleaseFromSpawner();
            _cubeModel = null;
        }

        public void RespawnIfEmpty()
        {
            if (_cubeModel == null)
            {
                SpawnCube();
            }
        }
    }
}