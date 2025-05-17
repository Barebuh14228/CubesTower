using UnityEngine;

namespace DragAndDrop
{
    public class DraggingController : MonoBehaviour //todo DraggingController<T>
    {
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private DropZone<DraggingCube>[] _dropZones;

        public void StartDragging(DraggingCube draggingCube)
        {
            draggingCube.transform.SetParent(_draggingParent);
        }
        
        public void TryDropItem(DraggingCube draggingCube)
        {
            foreach (var dropZone in _dropZones)
            {
                if (dropZone.CanDropItem(draggingCube))
                {
                    dropZone.DropItem(draggingCube);
                    return;
                }
            }
            
            draggingCube.Item.DestroyCube();
        }
    }
}