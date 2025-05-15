using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    [RequireComponent(typeof(RectTransform))]
    public class LayoutComponentsDisabler : MonoBehaviour
    { 
        [SerializeField] private UIBehaviour[] _uiBehaviours;

        private RectTransform _rectTransform;
        private bool _waitForRebuild = true;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            StartCoroutine(DisableBehaviours());
        }

        private IEnumerator DisableBehaviours()
        {
            var framesCount = 0;
            var rect = _rectTransform.rect;
            
            while (_waitForRebuild)
            {
                if (!rect.Equals(_rectTransform.rect))
                {
                    _waitForRebuild = false;
                    rect = _rectTransform.rect;
                }
                framesCount++;
                yield return new WaitForEndOfFrame();
            }
            
            //ждем когда изменения прекратятся (в случае с LayoutGroup могут происходить рекурсивные изменения)
            while (!rect.Equals(_rectTransform.rect))
            {
                rect = _rectTransform.rect;
                framesCount++;
                yield return new WaitForEndOfFrame();
            }

            foreach (var uiBehaviour in _uiBehaviours)
            {
                uiBehaviour.enabled = false;
            }
            
            Debug.Log($"UIBehaviours desabled for {gameObject.name} in {framesCount} frames");
        }
    }
}