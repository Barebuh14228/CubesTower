using DragAndDrop;
using UnityEngine;
using Zenject;

namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDraggable _cubeDraggable;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        [SerializeField] private DropItem _dropItem;
        [SerializeField] private RectTransform _rectTransform;
        
        [Inject] private UIController _uiController;
        [Inject] private CubeCreator _cubeCreator;
        
        public void SetupView()
        {
            _cubeView.Setup(_cubeModel.CubeSprite);
        }
    
        public void SetDraggableTarget(CustomDraggable draggableTarget)
        {
            _dragEventsProvider.SetTarget(draggableTarget);
        }

        public void SetDefaultDraggableTarget()
        {
            SetDraggableTarget(_uiController.ScrollDraggable);
        }

        public void SetDefaultDraggingParent()
        {
            _dropItem.SetDraggingParent(_uiController.DraggingParent);
        }
        
        public void OnStartDragging()
        {
            _dropItem.MoveToDraggingParent();
            
            //todo если кубик вляется частью башни то нужно оповестить башню
        }

        public void OnFinishDragging()
        {
            var dropped = _uiController.TryDropItem(_dropItem);
            
            if (!dropped)
            {
                _cubeModel.DestroyCube();
            }
            
            _uiController.SpawnersContainer.RespawnCubes();
        }
        
        public void ReturnToPool()
        {
            _cubeCreator.ReturnToPool(_cubeModel);
        }
    }
}

