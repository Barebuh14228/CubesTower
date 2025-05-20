using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UITextLogger : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Sequence _sequence;
        
        public static UITextLogger Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        public void LogText(string text)
        {
            _canvasGroup.alpha = 1;
            _text.text = text;

            if (_sequence != null && _sequence.IsActive())
            {
                _sequence.Kill();
                _text.transform.localScale = Vector3.one;
            }

            _sequence = DOTween.Sequence();

            _sequence.Append(_text.transform.DOPunchScale(Vector3.one * 0.05f, 0.3f, 1));
            _sequence.AppendInterval(text.Length / 10f * 1f); //по секунде на 10 символов
            _sequence.Append(_canvasGroup.DOFade(0, 1f));
        }
    }
}