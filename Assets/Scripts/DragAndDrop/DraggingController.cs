using UnityEngine;

namespace DragAndDrop
{
    public class DraggingController : MonoBehaviour
    {
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private DropZone[] _dropZones;

        public void StartDragging(DraggingCube draggingCube)
        {
            draggingCube.transform.SetParent(_draggingParent);
        }
        
        public void TryDropItem(DraggingCube draggingCube)
        {
            foreach (var dropZone in _dropZones)
            {
                if (dropZone.CanDrop(draggingCube))
                {
                    dropZone.Drop(draggingCube);
                    return;
                }
            }
            
            draggingCube.Item.DestroyCube();
        }
    }
}