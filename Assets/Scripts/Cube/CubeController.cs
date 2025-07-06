using System;
using DG.Tweening;
using DragAndDrop;
using DragEventsUtils;
using Save;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private CubeModel _cubeModel;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _explosionAnimObject;
        [SerializeField] private DragEventsListener _dragListener;
        [SerializeField] private DragEventsRouter _dragEventsRouter;
        [SerializeField] private DraggingItem _draggingItem;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        [Inject] private CubesPool _cubesPool;
        
        public CubeModel Model => _cubeModel;
        public DragEventsRouter DragEventsRouter => _dragEventsRouter;
        public DragEventsListener DefaultDragTarget => _dragListener;
        public RectTransform RectTransform => _draggingItem.RectTransform;
        public string Id { get; private set; }

        public void Setup(Color color)
        {
            _cubeModel.Setup(color);
            _image.color = color;
        }
        
        public void OnDestroyAnimationComplete()
        {
            _cubesPool.Release(this);
        }
        
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
            DragEventsRouter.IgnoreEvents();
        }

        public void ResetState()
        {
            _image.color = Color.white;
            _explosionAnimObject.SetActive(false);
            transform.eulerAngles = Vector3.zero;
            transform.DOKill();
            _dragEventsRouter.ListenEvents();
            _canvasGroup.alpha = 1;
        }

        public void Restore(CubeModelSave save)
        {
            _cubeModel.Setup(save.Color);
            _image.color = save.Color;
        }

        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

