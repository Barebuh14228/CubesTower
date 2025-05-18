using System;
using Cube;
using DG.Tweening;
using Settings;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DefaultNamespace
{
    public class CubeSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
        
        [Inject] private CubeCreator _cubeCreator;
        
        private CubeSettings _settings;
        private CubeController _cubeController;

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

        public void Setup(CubeSettings settings)
        {
            _settings = settings;
            SpawnCube();
        }

        private void SpawnCube()
        {
            _cubeController = _cubeCreator.CreateCube(_settings);
            _cubeController.transform.SetParent(transform, false);
            _cubeController.SetScrollAsDraggableTarget();
            _layoutComponentsDisabler.RebuildAndDisable();
        }

        private void ReleaseCube()
        {
            _cubeController.WarmDragging();
            _cubeController.OnDropEvent += RespawnCube;
        }
        
        // на случай когда мы нажали на кнопку до конца, но при этом не стали двигать кубик
        private void TryCancelDragging()
        {
            if (!_cubeController.PreDraggingState) 
                return;
            
            _cubeController.SetScrollAsDraggableTarget();
            _cubeController.OnDropEvent -= RespawnCube;
        }

        private void RespawnCube()
        {
            _cubeController.OnDropEvent -= RespawnCube;
            SpawnCube();
            _cubeController.AppearInSpawner();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _lazySequence.Value.PlayForward();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _lazySequence.Value.PlayBackwards();
            TryCancelDragging();
        }
    }
}