using DragEventsUtils;
using UnityEngine;

public class UIController : MonoBehaviour //todo naming
{
    [SerializeField] private ScrollDragSubscriber _scrollDragable; 
    [SerializeField] private SpawnersContainer _spawnersContainer;
    [SerializeField] private RectTransform _draggingParent;
    [SerializeField] private RectTransform _holeParent;
    [SerializeField] private GameObject _holeMask;
    [SerializeField] private GameObject _holeBottomPoint;
    [SerializeField] private RectTransform _towerParent;
    
    
    public ScrollDragSubscriber ScrollDragSubscriber => _scrollDragable;
    public SpawnersContainer SpawnersContainer => _spawnersContainer;
}