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

        public override bool Equals(object other)
        {
            if (other is CubeModel cubeModel)
            {
                return gameObject.GetInstanceID() == cubeModel.gameObject.GetInstanceID();
            }

            return false;
        }
        
        //todo hash code
    }
}