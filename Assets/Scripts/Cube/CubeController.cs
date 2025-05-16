using System;
using DefaultNamespace;
using DragAndDrop;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        public event Action OnDropEvent;
        
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDraggable _cubeDraggable;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        
        [SerializeField] private UnityEvent _onAppearEvent;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        [Inject] private UIController _uiController;
        [Inject] private CubeCreator _cubeCreator;

        public CubeModel Model => _cubeModel;

        private void Start()
        {
            _cubeDraggable.OnEndDragEvent += Drop;
        }

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _cubeView.SetSprite(_cubeModel.CubeSprite);
        }

        public void CreateInSpawner()
        {
            _dragEventsProvider.SetTarget(_uiController.ScrollDraggable);
        }
        
        public void AppearInSpawner()
        {
            _onAppearEvent?.Invoke();
            CreateInSpawner();
        }
        
        public void ReleaseFromSpawner()
        {
            _dragEventsProvider.SetTarget(_cubeDraggable);
        }
        
        public void ReturnToPool()
        {
            _cubeCreator.ReturnToPool(this);
        }

        private void Drop()
        {
            OnDropEvent?.Invoke();
            
            if (_uiController.HoleParent.ContainRect(_cubeModel.RectTransform))
            {
                _gameManager.DropCubeInHole(this);
                
                return;
            }

            if (_uiController.TowerParent.ContainRect(_cubeModel.RectTransform))
            {
                if (!_gameManager.TryDropCubeOnTower(this))
                    DestroyCube();
                
                return;
            }
            
            DestroyCube();
        }
        
        private void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}

