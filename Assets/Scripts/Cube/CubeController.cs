using System;
using DefaultNamespace;
using DragAndDrop;
using DragEventsUtils;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        public event Action OnDropEvent;
        
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDragSubscriber _cubeDragSubscriber;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        [SerializeField] private DraggingCube _draggingCube;
        
        [SerializeField] private UnityEvent _onAppearEvent;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        [Inject] private UIController _uiController;
        [Inject] private CubeCreator _cubeCreator;
        [Inject] private DraggingController _draggingController;

        public CubeModel Model => _cubeModel;

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _cubeView.SetSprite(_cubeModel.CubeSprite);
        }

        public void CreateInSpawner()
        {
            _dragEventsProvider.SetTarget(_uiController.ScrollDragSubscriber);
        }
        
        public void AppearInSpawner()
        {
            _onAppearEvent?.Invoke();
            CreateInSpawner();
        }
        
        public void ReleaseFromSpawner()
        {
            _dragEventsProvider.SetTarget(_cubeDragSubscriber);
        }
        
        public void ReturnToPool()
        {
            _cubeCreator.ReturnToPool(this);
        }

        public void Drag()
        {
            _draggingController.StartDragging(_draggingCube);
            _gameManager.NotifyCubeDragged(this);
        }
        
        public void Drop()
        {
            OnDropEvent?.Invoke();
            
            _draggingController.TryDropItem(_draggingCube);
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}

