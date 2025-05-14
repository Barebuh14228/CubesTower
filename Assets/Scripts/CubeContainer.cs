using DefaultNamespace;
using UnityEngine;

public class CubeContainer : MonoBehaviour
{
    [SerializeField] private DragEventsProvider _dragEventsProvider;
    [SerializeField] private CubeDraggable _cubeDraggableComponent;

    public void Setup()
    {
        _cubeDraggableComponent.OnDragFinished += GetNewCube;
    }
    
    public void OnPressAnimationComplete()
    {
        _dragEventsProvider.SetTarget(_cubeDraggableComponent);
    }

    private void GetNewCube()
    {
        _cubeDraggableComponent.OnDragFinished -= GetNewCube;
        
        //todo что-то должно создать кубик, выставить его DragEventsProvider-у target и поместить его в контейнер
        //todo контейнер должен выставить себе _dragEventsProvider и _cubeDraggableComponent
    }
}
