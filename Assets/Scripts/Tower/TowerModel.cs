using System.Collections.Generic;
using System.Linq;
using Cube;
using Save;
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
            return _cubesStack.Any(c => cube.Id == c.Id);
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

        public TowerSave GetSave()
        {
            return new TowerSave()
            {
                Cubes = _cubesStack.Select(c => c.Model.GetSave()).Reverse().ToArray()
            };
        }
    }
}