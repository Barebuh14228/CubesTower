using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public class RectDropZone : DropZone<DraggingCube>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent<DraggingCube> _onDrop;

        protected override bool CanDrop(DraggingCube draggingItem)
        {
            return _rectTransform.ContainRect(draggingItem.RectTransform);
        }

        protected override void Drop(DraggingCube item)
        {
            _onDrop?.Invoke(item);
        }
    }
}