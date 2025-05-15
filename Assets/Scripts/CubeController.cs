using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CubeController : MonoBehaviour //todo naming
{
    [SerializeField] private CubeView _cubeView;
    [SerializeField] private CubeDraggable _cubeDraggable;
    [SerializeField] private DragEventsProvider _dragEventsProvider;
    [SerializeField] private UnityEvent _onCreateEvent;
    [SerializeField] private UnityEvent _onDestroyEvent;
        
    [Inject] private GameManager _gameManager;
    [Inject] private UIController _uiController;
    [Inject] private CubeCreator _cubeCreator;
        
    private RectTransform _rectTransform;
    private Vector3[] _corners = new Vector3[4];
        
    private void Start()
    {
        _onCreateEvent?.Invoke();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(Sprite sprite)
    {
        _cubeView.Setup(sprite);
    }

    public void SetDraggableTarget(CustomDraggable draggableTarget)
    {
        _dragEventsProvider.SetTarget(draggableTarget);
    }
        
    public void SetCubeAsDraggableTarget()
    {
        SetDraggableTarget(_cubeDraggable);
    }

    public void DragCube()
    {
        transform.SetParent(_uiController.DraggingParent, true);
    }
        
    public void DropCube()
    {
        _gameManager.TryDropCube(this);
    }

    public void DestroyCube()
    {
        _onDestroyEvent?.Invoke();
    }
        
    public void ReturnToPool()
    {
        _cubeCreator.ReturnToPool(this);
    }

    public Vector3[] GetCorners()
    {
        _rectTransform.GetWorldCorners(_corners);
        return _corners;
    }
}