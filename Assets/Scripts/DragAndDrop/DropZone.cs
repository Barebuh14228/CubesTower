using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone<TDraggingItem> : MonoBehaviour 
        where TDraggingItem : DraggingItem
    {
        [SerializeField] private DropSubscriber<TDraggingItem> _subscriber;

        public void DropItem(TDraggingItem draggingItem)
        {
            NotifySubscriber(draggingItem);
        }
        
        public abstract bool CanDropItem(TDraggingItem draggingItem);

        private void NotifySubscriber(TDraggingItem draggingItem)
        {
            _subscriber.Drop(draggingItem);
        }
    }
}