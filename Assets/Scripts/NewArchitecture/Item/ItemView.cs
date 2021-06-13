using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Load;

namespace spaceItem
{
    public class ItemView : MonoBehaviour
    {
        private Camera itemCam;

        public GameObject prefItem;
        private GameObject instItem;
        private Animator itemAnimator;

        public GameObject prefCardCover;
        private GameObject instCardCover;
        private Animator animCardCover;

        private void Start()
        {
            itemCam = Camera.main;
        }

        public void DrawItem(Item item)
        {
            StartCoroutine(DeleySpawnItem(item));
        }

        IEnumerator DeleySpawnItem(Item item)
        {
            CreateCardCover();
            yield return new WaitForSeconds(animCardCover.runtimeAnimatorController.animationClips.Length - 0.25f);
            CreateItem(item);
            Destroy(instCardCover);
            Destroy(itemAnimator);
        }

        private void CreateCardCover()
        {
            instCardCover = Instantiate(prefCardCover, new Vector3(0, 0, 0), Quaternion.identity, transform);
            animCardCover = instCardCover.GetComponent<Animator>();
            instCardCover.gameObject.name = "CardCover";
            instCardCover.gameObject.transform.SetSiblingIndex(2);
            animCardCover.SetTrigger("newCard");
        }

        private void CreateItem(Item item)
        {
            instItem = Instantiate(prefItem, /*new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0),*/ transform);
            //меняю ему имя на то, что соответствует карте

            itemAnimator = instItem.GetComponent<Animator>();

            instItem.gameObject.name = item.CardName;
            instItem.gameObject.transform.SetSiblingIndex(1);

            //вписываю туда все параметры, которые пришли из Item 
            instItem.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = item.CardName;

            instItem.gameObject.GetComponent<Image>().sprite = item.Sprites[0];
            instItem.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = item.Sprites[1];
            instItem.transform.Find("Image").gameObject.GetComponent<Image>().sprite = item.Sprites[2];
            instItem.transform.Find("Name").gameObject.GetComponent<Image>().sprite = item.Sprites[3];


            instItem.transform.Find("Info").gameObject.GetComponent<TextMeshProUGUI>().text = item.InfoCard;

            itemAnimator.SetTrigger("openCard");

            instItem.GetComponent<ItemBehaviour>().item = item;
        }

        public void DestroyEnemy()
        {
            Destroy(instItem);
        }
    }

}

