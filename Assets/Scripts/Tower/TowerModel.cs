using System.Collections.Generic;
using System.Linq;
using Cube;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private Stack<CubeModel> _cubesStack = new Stack<CubeModel>();
        
        public Rect TopCubeRect { get; private set; }
        
        public void AddCube(CubeModel cube)
        {
            _cubesStack.Push(cube);
            TopCubeRect = cube.RectTransform.GetWorldRect();
        }

        public bool ContainCube(CubeModel cube)
        {
            return _cubesStack.Contains(cube);
        }

        public bool HaveCubes()
        {
            return _cubesStack.Count > 0;
        }
        
        public Vector3[] GetCubesCorners()
        {
            return _cubesStack.SelectMany(i => i.RectTransform.GetWorldCornersArray()).ToArray();
        }

        public List<CubeModel> RemoveCube(CubeModel item)
        {
            var topCubes = new List<CubeModel>();
            
            while (_cubesStack.Count > 0)
            {
                var removedItem = _cubesStack.Pop();

                if (removedItem.Equals(item))
                    break;
                
                topCubes.Add(removedItem);
            }

            TopCubeRect = _cubesStack.Any() ? _cubesStack.Peek().RectTransform.GetWorldRect() : default;

            return topCubes;
        }
    }
}