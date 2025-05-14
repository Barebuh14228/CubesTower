using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    //todo
    public class UIBehaviourDisabler : MonoBehaviour
    { 
        [SerializeField] private UIBehaviour[] _uiBehaviours;
        
        private void Start()
        {
            StartCoroutine(DisableBehaviours());
        }

        private IEnumerator DisableBehaviours()
        {
            yield return new WaitForEndOfFrame();

            foreach (var uiBehaviour in _uiBehaviours)
            {
                uiBehaviour.enabled = false;
            }
        }
    }
}