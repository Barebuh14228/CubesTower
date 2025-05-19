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
        [SerializeField] private DraggingItem _draggingItem;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        
        public CubeModel Model => _cubeModel;
        public DraggingItem DraggingItem => _draggingItem;
        public DragEventsProvider DragEventsProvider => _dragEventsProvider;
        public CubeDragSubscriber DefaultDragTarget => _cubeDragSubscriber;

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _cubeView.SetSprite(_cubeModel.CubeSprite);
            _cubeModel.OnDestroyCalledEvent += DestroyCube;
            _cubeModel.BlockDraggingEvent += _dragEventsProvider.IgnoreEvents;
            _cubeModel.UnblockDraggingEvent += _dragEventsProvider.ListenEvents;
        }
        
        public void OnDestroyAnimationComplete()
        {
            _gameManager.OnCubeDestroyed(this);
        }

        public void Drag()
        {
            _gameManager.DragCube(this);
        }
        
        public void Drop()
        {
            _gameManager.DropCube(this);
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}

