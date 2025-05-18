using UnityEngine;

namespace DragAndDrop
{
    public class DraggingController : MonoBehaviour
    {
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private DropZone[] _dropZones;

        public void StartDragging(DraggingItem draggingItem)
        {
            draggingItem.transform.SetParent(_draggingParent);
        }
        
        public bool TryDropItem(DraggingCube draggingCube)
        {
            foreach (var dropZone in _dropZones)
            {
                if (dropZone.CanDrop(draggingCube))
                {
                    dropZone.Drop(draggingCube);
                    return true;
                }
            }

            return false;
        }
    }
}