using DefaultNamespace;
using Settings;
using UnityEngine;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        public Sprite CubeSprite { get; private set; }
        public RectTransform RectTransform => _rectTransform;

        public void Setup(CubeSettings settings)
        {
            CubeSprite = settings.Sprite;
        }
    }
}