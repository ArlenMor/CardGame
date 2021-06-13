using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Load;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private GameObject Inventory;

        [SerializeField]
        private GameObject Content;

        private bool isOpen = false;

        [Header("DefaultCellSprites")]
        public Sprite[] sprite = new Sprite[4];


        private void Start()
        {
            
        }

        private void Update()
        {
            if (isOpen)
                Inventory.SetActive(true);
            else
                Inventory.SetActive(false);

        }

        public void OpenInventory()
        {
            isOpen = !isOpen;
        }


        public void DrawItemInInventory(Item item, int numberOfCell)
        {
            GameObject cell = Content.transform.GetChild(numberOfCell - 1).gameObject;
            DrawItemInCell(cell, item.Sprites[0], item.Sprites[1], item.Sprites[2], item.Sprites[3], item.CardName);
            /*cell.GetComponent<Image>().sprite = item.Sprites[0];
            cell.transform.GetChild(0).GetComponent<Image>().sprite = item.Sprites[1];
            cell.transform.GetChild(1).GetComponent<Image>().sprite = item.Sprites[2];
            cell.transform.GetChild(2).GetComponent<Image>().sprite = item.Sprites[3];
            cell.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = item.CardName;*/
            cell.gameObject.AddComponent<CellItem>();
            cell.gameObject.GetComponent<CellItem>().Init(item);
        }

        public void DrawEmptyCell(int numberOfCell)
        {
            GameObject cell = Content.transform.GetChild(numberOfCell - 1).gameObject;
            DrawItemInCell(cell, sprite[0], sprite[1], sprite[2], sprite[3], "");
            Destroy(cell.GetComponent<CellItem>());
        }

        public void DrawItemInCell(GameObject cell, Sprite Bg, Sprite Edging, Sprite Image, Sprite BgName, string cardName)
        {
            cell.GetComponent<Image>().sprite = Bg;
            cell.transform.GetChild(0).GetComponent<Image>().sprite = Edging;
            cell.transform.GetChild(1).GetComponent<Image>().sprite = Image;
            cell.transform.GetChild(2).GetComponent<Image>().sprite = BgName;
            cell.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = cardName;
        }
    }

}
