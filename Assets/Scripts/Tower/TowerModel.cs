using System.Collections.Generic;
using DragAndDrop;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private List<DropItem> _items = new List<DropItem>();

        public List<DropItem> Items => _items;
        
        public void AddItem(DropItem item)
        {
            _items.Add(item);
            
            //todo закидываем анимацией наверх
            //todo или дропаем вниз
        }
    }
}