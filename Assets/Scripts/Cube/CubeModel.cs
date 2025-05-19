using Settings;
using UnityEngine;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        public Color Color{ get; private set; }
        

        public void Setup(CubeSettings settings)
        {
            Color = settings.Color;
        }
    }
}