using DG.Tweening;
using UnityEngine;

namespace DragAndDrop
{
    public class TrashHoleDropSubscriber : DropSubscriber<DraggingCube>
    {
        [SerializeField] private Transform _dropPoint;
        [SerializeField] private Transform _dropMask;
        
        public override void NotifyOnDrop(DraggingCube item)
        {
            var cubeController = item.Item;
            
            var path = new Vector3[]
            {
                cubeController.Model.RectTransform.GetWorldRect().center,
                _dropPoint.transform.position + Vector3.up * 350,
            };
        
            var seq = DOTween.Sequence();

            cubeController.transform.DORotate(new Vector3(0, 0, 720), 1.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
            seq
                .Append(cubeController.transform.DOPath(path, 0.5f, PathType.CatmullRom).SetEase(Ease.InQuad))
                .AppendCallback(() =>
                {
                    cubeController.transform.SetParent(_dropMask.transform, true);
                })
                .Append(cubeController.transform.DOMove(_dropPoint.transform.position, 0.2f).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    cubeController.ReturnToPool();
                })
                .Play();
        }
    }
}