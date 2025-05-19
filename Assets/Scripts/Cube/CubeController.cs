using DG.Tweening;
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
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDragSubscriber _cubeDragSubscriber;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        [SerializeField] private DraggingCube _draggingCube;
        
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        [Inject] private CubeCreator _cubeCreator;
        [Inject] private DraggingController _draggingController;
        
        public CubeModel Model => _cubeModel;
        public bool IsDragging { get; private set; }

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _cubeView.SetSprite(_cubeModel.CubeSprite);
            _cubeModel.OnDestroyCalledEvent += DestroyCube;
            _cubeModel.BlockDraggingEvent += _dragEventsProvider.IgnoreEvents;
            _cubeModel.UnblockDraggingEvent += _dragEventsProvider.ListenEvents;
        }

        public void OverrideDragTarget(DragEventsSubscriber dragSubscriber)
        {
            _dragEventsProvider.SetTarget(dragSubscriber);
        }

        public void ResetDragTarget()
        {
            _dragEventsProvider.SetTarget(_cubeDragSubscriber);
        }
        
        public void ReturnToPool()
        {
            _cubeCreator.ReturnToPool(this);
        }

        public void Drag()
        {
            IsDragging = true;
            _draggingController.StartDragging(_draggingCube);
            _gameManager.DragCube(this);
        }
        
        public void Drop()
        {
            IsDragging = false;
            if (!_draggingController.TryDropItem(_draggingCube))
            {
                DestroyCube();
            }
            
            _gameManager.DropCube(this);
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}

