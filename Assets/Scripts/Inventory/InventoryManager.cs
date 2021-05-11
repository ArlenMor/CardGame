using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

using Card;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]   
        private Transform container;

        public GameObject itemPref;
        private GameObject instItem;

        private List<Item> items = new List<Item>();

        public void AddCardInInventory(Card.Card card)
        {
            Item tmpItem = new Item(card.BgCard, card.EdgingName, card.BgName, card.ImageName, card.CardName, card.Info);
            items.Add(tmpItem);
            InstCardInInventory(items[items.Count - 1]);
        }

        public bool CheckCardInInventory(Card.Card card)
        {
            if (card.NeedToTake.Count == 0)
                return true;
            foreach(Item item in items)
            {
                foreach (string NeedToTakeCard in card.NeedToTake)
                    if (item.CardName == NeedToTakeCard)
                        return true;
            }
            return false;
        }

        //Создать View
       private void InstCardInInventory(Item item)
       {
            instItem = Instantiate(itemPref, container) as GameObject;
            instItem.name = item.CardName;

            instItem.transform.gameObject.GetComponent<Image>().sprite = item.Bg;
            instItem.transform.Find("Edging").GetComponent<Image>().sprite = item.Edging;
            instItem.transform.Find("Image").GetComponent<Image>().sprite = item.Image;
            instItem.transform.Find("BgName").GetComponent<Image>().sprite = item.BgName;
            instItem.transform.Find("BgName").transform.Find("CardName").GetComponent<TextMeshProUGUI>().text = item.CardName;
        }

        
    }
}

