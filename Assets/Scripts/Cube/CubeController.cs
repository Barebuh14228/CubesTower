using System;
using DragAndDrop;
using DragEventsUtils;
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
        [SerializeField] private CubeDragSubscriber _cubeDragSubscriber;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        [SerializeField] private DraggingCube _draggingCube;
        
        [SerializeField] private UnityEvent _onAppearEvent;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        [Inject] private UIElementsProvider _uiController;
        [Inject] private CubeCreator _cubeCreator;
        [Inject] private DraggingController _draggingController;

        public CubeModel Model => _cubeModel;

        public bool PreDraggingState { get; private set; }

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _cubeView.SetSprite(_cubeModel.CubeSprite);
            _cubeModel.OnDestroyCalledEvent += DestroyCube;
            _cubeModel.BlockDraggingEvent += _dragEventsProvider.IgnoreEvents;
            _cubeModel.UnblockDraggingEvent += _dragEventsProvider.ListenEvents;
        }

        public void SetScrollAsDraggableTarget()
        {
            _dragEventsProvider.SetTarget(_uiController.ScrollDragSubscriber);
        }
        
        public void AppearInSpawner()
        {
            _onAppearEvent?.Invoke();
            SetScrollAsDraggableTarget();
        }

        public void WarmDragging()
        {
            _dragEventsProvider.SetTarget(_cubeDragSubscriber);
            PreDraggingState = true;
        }
        
        public void ReturnToPool()
        {
            _cubeCreator.ReturnToPool(this);
        }

        public void Drag()
        {
            _draggingController.StartDragging(_draggingCube);
            _gameManager.NotifyCubeDragged(this);
            PreDraggingState = false;
        }
        
        public void Drop()
        {
            OnDropEvent?.Invoke();

            if (!_draggingController.TryDropItem(_draggingCube))
            {
                DestroyCube();
            }
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}

