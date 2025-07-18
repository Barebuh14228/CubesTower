using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public class DraggingController : MonoBehaviour
    {
        [SerializeField] private Transform _draggingParent;
        
        [SerializeField] private List<DragSubscriber> _dragSubscribers;
        [SerializeField] private List<DropZone> _dropZones;
        [SerializeField] private List<DropSubscriber> _dropSubscribers;

        public void DragItem(DraggingItem draggingItem)
        {
            draggingItem.transform.SetParent(_draggingParent);
            foreach (var subscriber in _dragSubscribers)
            {
                subscriber.NotifyOnDrag(draggingItem);
            }
        }

        public void DropItem(DraggingItem draggingItem)
        {
            var dropped = false;
            
            foreach (var dropZone in _dropZones)
            {
                if (!dropZone.CanDrop(draggingItem)) 
                    continue;
                
                dropZone.Drop(draggingItem);
                dropped = true;
                break;
            }

            if (!dropped)
            {
                draggingItem.NotifyDropFailed();
            }
            
            foreach (var subscriber in _dropSubscribers)
            {
                subscriber.NotifyOnDrop(draggingItem);
            }
        }

        public void AddDragSubscriber(DragSubscriber dragSubscriber)
        {
            _dragSubscribers.Add(dragSubscriber);
        }
        
        public void AddDropSubscriber(DropSubscriber dropSubscriber)
        {
            _dropSubscribers.Add(dropSubscriber);
        }
    }
}