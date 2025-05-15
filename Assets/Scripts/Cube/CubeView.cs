using UnityEngine;
using UnityEngine.UI;

namespace Cube
{
    public class CubeView : MonoBehaviour
    {
        [SerializeField] private Image _image;
    
        public void Setup(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}