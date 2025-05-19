using DG.Tweening;
using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;

public class TrashHole : DropSubscriber<DraggingCube>
{
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private Transform _dropMask;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UnityEvent _onDropStart;
        
    public override void NotifyOnDrop(DraggingCube item)
    {
        var cubeController = item.Value;
            
        var path = new []
        {
            cubeController.transform.position,
            _topPoint.transform.position
        };
        
        var sequence = DOTween.Sequence();

        cubeController.transform
            .DORotate(new Vector3(0, 0, 720), 1.5f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetLink(cubeController.gameObject);

        sequence
            .OnStart(() => _onDropStart?.Invoke())
            .Append(cubeController.transform.DOPath(path, 1f, PathType.CatmullRom).SetEase(Ease.InQuad))
            .AppendCallback(() =>
            {
                cubeController.transform.SetParent(_dropMask.transform, true);
            })
            .Append(cubeController.transform.DOMove(_bottomPoint.transform.position, 0.2f).SetEase(Ease.Linear))
            .OnKill(() =>
            {
                _gameManager.OnCubeDestroyed(cubeController);
            });
    }
}