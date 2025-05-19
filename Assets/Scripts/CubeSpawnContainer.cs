using System;
using Cube;
using DG.Tweening;
using DragAndDrop;
using DragEventsUtils;
using Settings;
using UnityEngine;

public class CubeSpawnContainer : DragSubscriber<DraggingCube>
{
    [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
    
    private CubeSettings _settings;
    private CubeController _cubeController;
    private DragEventsListener _dragTarget;

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
    }

    public void SetSettings(CubeSettings settings)
    {
        _settings = settings;
    }

    public void SetDragTarget(DragEventsListener dragTarget)
    {
        _dragTarget = dragTarget;
    }

    public CubeSettings GetSettings()
    {
        return _settings;
    }

    public bool IsEmpty()
    {
        return _cubeController == null;
    }
    
    public void SetCube(CubeController cubeController, bool rebuildLayout = true)
    {
        _cubeController = cubeController;
        _cubeController.transform.SetParent(transform, false);
        _cubeController.DragEventsProvider.SetTarget(_dragTarget);
        
        if (rebuildLayout)
        {
            _layoutComponentsDisabler.RebuildAndDisable();
        }
    }
    
    private void ReleaseCube()
    {
        _cubeController.DragEventsProvider.SetTarget(_cubeController.DefaultDragTarget);
    }
    
    public void PlayAppearAnimation()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 3);
    }
    
    protected override void NotifyOnDrag(DraggingCube draggingItem)
    {
        if (draggingItem.Value.Id != _cubeController.Id)
            return;
        
        _cubeController.RectTransform.localScale = Vector3.one;
        _cubeController = null;
    }

    public void OnPointerPush()
    {
        _cubeController?.DragEventsProvider.SetTarget(_dragTarget);
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