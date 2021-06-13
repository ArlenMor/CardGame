using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryData
    {
        public List<int> IDAllItemsInInventory = new List<int>();
        public int IDCurrentWeapon;
        public int IDCurrentArmor;
        public int IDCurrentShield;

        public int numberItemsInInventroy = 0;
    }
}

