using DefaultNamespace.Save;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    //todo objectpool
    //todo abstract factory ?
    
    public class CubeCreator : MonoBehaviour
    {
        [SerializeField] private CubeController _cubeViewPrefab;
        
        public CubeController CreateCube(CubeSettings cubeSettings)
        {
            var cube = Instantiate(_cubeViewPrefab);
            
            cube.Setup(cubeSettings.Sprite);

            return cube;
        }
    }
}