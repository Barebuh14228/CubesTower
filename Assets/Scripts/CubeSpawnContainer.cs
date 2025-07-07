using System;
using Cube;
using DefaultNamespace;
using DG.Tweening;
using DragAndDrop;
using DragEventsUtils;
using Settings;
using UnityEngine;
using Zenject;

public class CubeSpawnContainer : MonoBehaviour
{
    [SerializeField] private CubeDragSubscriber _cubeDragSubscriber;
    [SerializeField] private CubeDropSubscriber _cubeDropSubscriber;
    [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
    
    private CubeSettings _settings;
    private CubeController _cubeController;
    private DragEventsListener _dragTarget;
    
    [Inject] private CubesPool _cubesPool;
    [Inject] private DraggingController _draggingController;

    // используется ленивая инициализация чтобы анимация не проигрывалась на старте
    // в качестве альтернативы мог бы выставить флаг defaultAutoPlay у класса DOTween в false,
    // но решил не менять глобальное поведение твинов из-за одной анимации
    private Lazy<Sequence> _lazySequence;

    private void Start()
    {
        _lazySequence = new Lazy<Sequence>(() =>
            DOTween.Sequence()
                .SetLink(gameObject)
                .Append(transform.DOScale(Vector3.one * 0.9f, 0.15f))
                .OnComplete(ReleaseCube)
                .SetAutoKill(false)
        );
        
        _draggingController.AddDragSubscriber(_cubeDragSubscriber);
        _draggingController.AddDropSubscriber(_cubeDropSubscriber);
    }

    public void SetSettings(CubeSettings settings)
    {
        _settings = settings;
    }

    public void SetDragTarget(DragEventsListener dragTarget)
    {
        _dragTarget = dragTarget;
    }

    private bool IsEmpty()
    {
        return _cubeController == null;
    }

    private void SetCube(CubeController cubeController, bool rebuildLayout = true)
    {
        _cubeController = cubeController;
        _cubeController.transform.SetParent(transform, false);
        _cubeController.DragEventsRouter.SetTarget(_dragTarget);
        
        if (rebuildLayout)
        {
            _layoutComponentsDisabler.RebuildAndDisable();
        }
    }
    
    private void ReleaseCube()
    {
        UITextLogger.Instance.LogText(TextProvider.Get("container_press"));
        
        _cubeController.DragEventsRouter.SetTarget(_cubeController.DefaultDragTarget);
    }

    private void PlayAppearAnimation()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 3);
    }
    
    public void NotifyOnCubeDrag(DraggingCube draggingItem)
    {
        if (draggingItem.Value.Id != _cubeController.Id)
            return;
        
        _cubeController.RectTransform.localScale = Vector3.one;
        _cubeController = null;
        
        UITextLogger.Instance.LogText(TextProvider.Get("release"));
    }
    
    public void NotifyOnCubeDrop(DraggingCube draggingCube)
    {
        SpawnCube();
    }

    public void SpawnCube()
    {
        if (!IsEmpty())
            return;
        
        var cube = _cubesPool.Get(_settings);
            
        cube.DragEventsRouter.SetTarget(_dragTarget);
            
        PlayAppearAnimation();
        SetCube(cube);
    }

    public void OnPointerPush()
    {
        _cubeController?.DragEventsRouter.SetTarget(_dragTarget);
    }

    public void PlayPushAnimForward()
    {
        _lazySequence.Value.PlayForward();
    }

    public void PlayPushAnimBackward()
    {
        _lazySequence.Value.PlayBackwards();
    }

    
}