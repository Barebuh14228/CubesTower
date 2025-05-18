using DragEventsUtils;
using UnityEngine;

public class UIElementsProvider : MonoBehaviour
{
    [SerializeField] private ScrollDragSubscriber _scrollDragable; 
    
    public ScrollDragSubscriber ScrollDragSubscriber => _scrollDragable;
}