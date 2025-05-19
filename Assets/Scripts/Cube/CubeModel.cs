using Settings;
using UnityEngine;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        public Sprite CubeSprite { get; private set; }
        

        public void Setup(CubeSettings settings)
        {
            CubeSprite = settings.Sprite;
        }
    }
}