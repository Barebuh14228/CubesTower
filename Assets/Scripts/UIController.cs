using DragAndDrop;
using UnityEngine;

public class UIController : MonoBehaviour //todo naming
{
    [SerializeField] private ScrollDraggable _scrollDragable; 
    [SerializeField] private SpawnersContainer _spawnersContainer;
    [SerializeField] private RectTransform _draggingParent;
    [SerializeField] private RectTransform _holeParent;
    [SerializeField] private GameObject _holeMask;
    [SerializeField] private GameObject _holeBottomPoint;
    [SerializeField] private RectTransform _towerParent;
    
    
    public ScrollDraggable ScrollDraggable => _scrollDragable;
    public SpawnersContainer SpawnersContainer => _spawnersContainer;
    public RectTransform DraggingParent => _draggingParent;
    public RectTransform HoleParent => _holeParent;
    public GameObject HoleMask => _holeMask;
    public GameObject HoleBottomPoint => _holeBottomPoint;
    public RectTransform TowerParent => _towerParent;
}