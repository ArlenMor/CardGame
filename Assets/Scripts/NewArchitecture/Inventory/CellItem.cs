using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using spaceItem;
using Load;
using Core;


namespace Inventory
{
    
    public class CellItem : MonoBehaviour, IPointerDownHandler
    {
        private ItemData data = new ItemData();
        private Transform itemDialog;
        private Camera cam;
        private GameMaster gm;

        private Button buttonUse;
        private Button buttonDrop;
        private Button buttonSell;

        private Vector3 startPosItemDialog;

        public delegate void DeleteCell(int id);

        public event DeleteCell deleteCell;

        
        void Start()
        {
            cam = Camera.allCameras[0];
            gm = transform.root.gameObject.GetComponent<GameMaster>();
        }

        
        void Update()
        {
            
        }

        

        public void Init(Item item)
        {
            data.Init(item.Id, item.CardName, item.InfoCard, item.PartOfDeck, item.NeedToTake, item.ChangeStats, item.Chance,
                item.Effect, item.ChanceEffect, item.Event, item.ChanceEvent, item.Enemy, item.ChanceEnemy, item.value);
            itemDialog = transform.parent.parent.parent.parent.GetChild(2);
            startPosItemDialog = itemDialog.transform.position;
            buttonUse = itemDialog.GetChild(0).GetComponent<Button>();
            buttonDrop = itemDialog.GetChild(1).GetComponent<Button>();
            buttonSell = itemDialog.GetChild(2).GetComponent<Button>();

            buttonUse.onClick.AddListener(Use);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(eventData.position);
            mousePos.z = transform.position.z - 1f;
            Debug.Log("I here");
            itemDialog.position = mousePos;
        }

        public void Use()
        {
            gm.playerStats.ChangePlayerStats(data.ChangeStats[0], data.ChangeStats[1]);
            itemDialog.position = startPosItemDialog;
            buttonUse.onClick.RemoveAllListeners();
            deleteCell(data.Id);
        }


    }

}
