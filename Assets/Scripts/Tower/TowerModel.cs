using System.Collections.Generic;
using Cube;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private List<CubeModel> _items = new List<CubeModel>();

        public List<CubeModel> Items => _items;
        
        public void AddItem(CubeModel item)
        {
            _items.Add(item);
        }

        public bool ContainItem(CubeModel item)
        {
            return _items.Contains(item);
        }

        public void RemoveItem(CubeModel item)
        {
            _items.Remove(item);
        }
    }
}