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
        
        [Inject] private UIController _uiController;
        
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
    }
}

