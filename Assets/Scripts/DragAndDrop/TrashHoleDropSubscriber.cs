using DG.Tweening;
using UnityEngine;

namespace DragAndDrop
{
    public class TrashHoleDropSubscriber : DropSubscriber<DraggingCube>
    {
        [SerializeField] private Transform _dropPoint;
        [SerializeField] private Transform _dropMask;
        [SerializeField] private GameManager _gameManager;
        
        public override void NotifyOnDrop(DraggingCube item)
        {
            var cubeController = item.Value;
            
            var path = new []
            {
                cubeController.RectTransform.position,
                _dropPoint.transform.position + Vector3.up * 350,
            };
        
            var sequence = DOTween.Sequence();

            cubeController.transform
                .DORotate(new Vector3(0, 0, 720), 1.5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetLink(cubeController.gameObject);
            
            sequence
                .SetLink(cubeController.gameObject)
                .Append(cubeController.transform.DOPath(path, 0.5f, PathType.CatmullRom).SetEase(Ease.InQuad))
                .AppendCallback(() =>
                {
                    cubeController.transform.SetParent(_dropMask.transform, true);
                })
                .Append(cubeController.transform.DOMove(_dropPoint.transform.position, 0.2f).SetEase(Ease.Linear))
                .OnKill(() =>
                {
                    _gameManager.OnCubeDestroyed(cubeController);
                })
                .Play();
        }
    }
}