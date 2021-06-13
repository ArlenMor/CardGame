using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UI;
using Load;
using Inventory;

namespace Core
{
    public class CoreLogic : MonoBehaviour
    {
        [SerializeField]
        private GameMaster gm;
        [Header("Контроллеры:")]
        [SerializeField]
        private DeckController deckController;
        [SerializeField]
        private InventoryController inventory;
        [SerializeField]
        private UIController uiController;


        private void Update()
        {
            if(gm.gameSettings.canSpawn)
            {
                deckController.ItemController.SpawnItem(0);
            }

            if (gm.gameSettings.swipeRightItem == true)
            {
                Load.Item item = gm.gameSettings.currentItem;
                inventory.AddItemInInventory(item);
                gm.gameSettings.swipeRightItem = false;
                
            }


         
        }

    }

}
