using System.Collections.Generic;
using System.Linq;
using Cube;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private Stack<CubeController> _cubesStack = new Stack<CubeController>();
        
        public void AddCube(CubeController cube)
        {
            _cubesStack.Push(cube);
        }

        public bool ContainCube(CubeController cube)
        {
            return _cubesStack.Contains(cube);
        }

        public bool HaveCubes()
        {
            return _cubesStack.Count > 0;
        }
        
        public Vector2[] GetCubesCorners()
        {
            return _cubesStack.SelectMany(cube => cube.RectTransform.GetWorldCornersArray()).ToArray();
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

        public void BlockDragging()
        {
            foreach (var cube in _cubesStack)
            {
                cube.DragEventsProvider.IgnoreEvents();
            }
        }

        public void UnblockDragging()
        {
            foreach (var cube in _cubesStack)
            {
                cube.DragEventsProvider.ListenEvents();
            }
        }

        public Rect GetTopCubeRect()
        {
            if (_cubesStack.TryPeek(out var cube))
            {
                return cube.RectTransform.GetWorldRect();
            }

            return default;
        }
    }
}