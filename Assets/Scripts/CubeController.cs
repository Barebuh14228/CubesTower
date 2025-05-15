using UnityEngine;

namespace DefaultNamespace
{
    public class CubeController : MonoBehaviour //todo naming
    {
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDraggable _cubeDraggable;
        [SerializeField] private DragEventsProvider _dragEventsProvider;

        public void Setup(Sprite sprite)
        {
            _cubeView.Setup(sprite);
        }

        public void SetDraggableTarget(CustomDraggable draggableTarget)
        {
            _dragEventsProvider.SetTarget(draggableTarget);
        }
        
        public void SetCubeAsDraggableTarget()
        {
            SetDraggableTarget(_cubeDraggable);
        }
    }
}