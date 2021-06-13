using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Load;
using Core;

namespace spaceItem
{
    public class ItemController : MonoBehaviour
    {
        public GameMaster gm;
        [SerializeField]
        private ItemView itemView;

        private ItemData currentItem = new ItemData();

        private List<ItemData> AllItems = new List<ItemData>();

        public void SpawnItem(int id)
        {
            gm.gameSettings.canSpawn = false;
            Item item = gm.deckInfo.FindCard("Item", id).Item1;
            itemView.DrawItem(item);
            currentItem.Init(item.Id, item.CardName, item.InfoCard, item.PartOfDeck, item.NeedToTake, item.ChangeStats,
                                item.Chance, item.Effect, item.ChanceEffect, item.Event, item.ChanceEvent, item.Enemy, item.ChanceEnemy, item.value);
            AllItems.Add(currentItem);
        }
    }

}

