using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Load;
using Core;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private InventoryView inventoryView;
        [SerializeField]
        private GameMaster gm;
        private InventoryData inventoryData = new InventoryData();

        [SerializeField]
        private Transform cell;
        
        private CellItem Cell1;

        private void Update()
        {
            if (inventoryData.numberItemsInInventroy == 9)
                gm.gameSettings.canAddInInventory = false;
            else
                gm.gameSettings.canAddInInventory = true;

            
                
        }

        public void AddItemInInventory(Item item)
        {
            inventoryData.numberItemsInInventroy++;
            inventoryData.IDAllItemsInInventory.Add(item.Id);
            inventoryView.DrawItemInInventory(item, inventoryData.numberItemsInInventroy);
            Cell1 = cell.GetComponent<CellItem>();
            Cell1.deleteCell += _DeleteCell;
        }

        public void _DeleteCell(int Id)
        {
            Debug.Log("I here lol");
            foreach(var _item in inventoryData.IDAllItemsInInventory)
            {
                if(Id == _item)
                {
                    Item item = gm.deckInfo.FindCard("Item", Id).Item1;
                    inventoryView.DrawEmptyCell(1);
                }
            }
        }
    }
}

