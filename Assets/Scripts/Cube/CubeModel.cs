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
        
        public Sprite CubeSprite { get; private set; }

        private void Start()
        {
            _onCreateEvent?.Invoke();
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
    
        public void DestroyCube()
        {
            _onDestroyEvent?.Invoke();
        }
    }
}