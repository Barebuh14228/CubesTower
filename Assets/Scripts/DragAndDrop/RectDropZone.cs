using UnityEngine;

namespace DragAndDrop
{
    public class RectDropZone : DropZone
    {
        [SerializeField] private RectTransform _rectTransform;

        public override bool CanDrop(DraggingItem draggingItem)
        {
            return _rectTransform.ContainRect(draggingItem.RectTransform);
        }
    }
}