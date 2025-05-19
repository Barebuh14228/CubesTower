using System;
using DG.Tweening;
using DragAndDrop;
using DragEventsUtils;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _explosionAnimObject;
        [SerializeField] private CubeDragSubscriber _cubeDragSubscriber;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        [SerializeField] private DraggingCube _draggingCube;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private GameManager _gameManager;
        
        public CubeModel Model => _cubeModel;
        public DraggingItem DraggingCube => _draggingCube;
        public DragEventsProvider DragEventsProvider => _dragEventsProvider;
        public CubeDragSubscriber DefaultDragTarget => _cubeDragSubscriber;
        public RectTransform RectTransform => _draggingCube.RectTransform;
        public string Id { get; private set; }

        public void Setup(CubeSettings cubeSettings)
        {
            _cubeModel.Setup(cubeSettings);
            _image.sprite = _cubeModel.CubeSprite;
        }
        
        public void OnDestroyAnimationComplete()
        {
            _gameManager.OnCubeDestroyed(this);
        }

        public void Drag()
        {
            _gameManager.DragCube(this);
            RectTransform.localScale = Vector3.one;
        }
        
        public void Drop()
        {
            _gameManager.DropCube(this);
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
            DragEventsProvider.IgnoreEvents();
        }

        public void ResetState()
        {
            _image.color = Color.white;
            _explosionAnimObject.SetActive(false);
            transform.eulerAngles = Vector3.zero;
            transform.DOKill();
            _dragEventsProvider.ListenEvents();
        }

        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

