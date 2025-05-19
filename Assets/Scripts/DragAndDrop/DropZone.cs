using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone : MonoBehaviour
    {
        [SerializeField] private DropSubscriber _subscriber;

        public void Drop(DraggingItem draggingItem)
        {
            _subscriber.NotifyOnDrop(draggingItem);
        }
        public abstract bool CanDrop(DraggingItem draggingItem);
    }
}