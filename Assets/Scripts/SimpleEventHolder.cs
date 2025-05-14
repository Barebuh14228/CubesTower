using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class SimpleEventHolder : MonoBehaviour
    { 
        [SerializeField] private UnityEvent _event;
        
        public void CallEvent()
        {
            _event?.Invoke();
        }
    }
}