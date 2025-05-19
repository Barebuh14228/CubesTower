using Save;
using UnityEngine;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        public Color Color { get; private set; }
        

        public void Setup(Color color)
        {
            Color = color;
        }

        public CubeModelSave GetSave()
        {
            return new CubeModelSave()
            {
                Color = Color,
                Position = transform.position
            };
        }
    }
}