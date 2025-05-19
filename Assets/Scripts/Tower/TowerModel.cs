using System.Collections.Generic;
using Cube;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private Stack<CubeController> _cubesStack = new Stack<CubeController>();
        
        public IReadOnlyCollection<CubeController> Cubes => _cubesStack.ToArray();
        
        public void AddCube(CubeController cube)
        {
            _cubesStack.Push(cube);
        }

        public bool ContainCube(CubeController cube)
        {
            return _cubesStack.Contains(cube);
        }

        public List<CubeController> RemoveCube(CubeController item)
        {
            var topCubes = new List<CubeController>();
            
            while (_cubesStack.Count > 0)
            {
                var removedItem = _cubesStack.Pop();

                if (removedItem.Equals(item))
                    break;
                
                topCubes.Add(removedItem);
            }
            
            return topCubes;
        }
    }
}