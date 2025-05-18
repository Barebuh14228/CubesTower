using DragEventsUtils;
using UnityEngine;

public class UIElementsProvider : MonoBehaviour
{
    [SerializeField] private ScrollDragSubscriber _scrollDragable; 
    [SerializeField] private SpawnersContainer _spawnersContainer;
    
    public ScrollDragSubscriber ScrollDragSubscriber => _scrollDragable;
    public SpawnersContainer SpawnersContainer => _spawnersContainer;
}