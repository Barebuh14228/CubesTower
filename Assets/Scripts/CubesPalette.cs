using UnityEngine;

namespace DefaultNamespace
{
    public class CubesPalette : MonoBehaviour //todo rename
    {
        [SerializeField] private CubeContainer _cubeContainerPrefab;

        public void AddCube(CubeController cubeController)
        {
            //создание кубика отделено от присваивания парента, потому что если присваивать парент при создании у кубика
            //будет меняться scale
            
            var cubeContainer = Instantiate(_cubeContainerPrefab);
            
            cubeContainer.transform.SetParent(transform);
            cubeContainer.Setup(cubeController);
        }
    }
}