using System.Linq;
using DragAndDrop;
using UnityEngine;

public class UIController : MonoBehaviour //todo naming
{
    [SerializeField] private ScrollDraggable _scrollDragable; 
    [SerializeField] private SpawnersContainer _spawnersContainer;
    [SerializeField] private RectTransform _draggingParent;
    [SerializeField] private DropZone[] _dropZones;
    
    public ScrollDraggable ScrollDraggable => _scrollDragable;
    public SpawnersContainer SpawnersContainer => _spawnersContainer;
    public RectTransform DraggingParent => _draggingParent;
    
    public bool TryDropItem(DropItem item)
    {
        return _dropZones.Any(dz => dz.TryDropItem(item));
    }
}