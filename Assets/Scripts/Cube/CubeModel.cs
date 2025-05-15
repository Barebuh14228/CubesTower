using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCreateEvent;
        [SerializeField] private UnityEvent _onSetupEvent;
        [SerializeField] private UnityEvent _onReleasedEvent;
        [SerializeField] private UnityEvent _onDestroyEvent;
        
        private RectTransform _rectTransform;
        private Vector3[] _corners = new Vector3[4];
        
        [Inject] private GameManager _gameManager;
        [Inject] private UIController _uiController;
        [Inject] private CubeCreator _cubeCreator;

        public Sprite CubeSprite { get; private set; }

        private void Start()
        {
            _onCreateEvent?.Invoke();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetupModel(Sprite sprite)
        {
            CubeSprite = sprite;
            _onSetupEvent?.Invoke();
        }

        public void ReleaseFromSpawner()
        {
            _onReleasedEvent?.Invoke();
        }
        
        public void DragCube()
        {
            transform.SetParent(_uiController.DraggingParent, true);
        }
            
        public void DropCube()
        {
            _gameManager.DropCube(this);
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
}