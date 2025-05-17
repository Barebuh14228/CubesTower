using UnityEngine;

namespace DragAndDrop
{
    public class RectDropZone : DropZone<DraggingCube>
    {
        [SerializeField] private RectTransform _rectTransform;

        protected override bool CanDropItem(DraggingCube draggingItem)
        {
            return _rectTransform.ContainRect(draggingItem.GetWorldRect());
        }
    }
}