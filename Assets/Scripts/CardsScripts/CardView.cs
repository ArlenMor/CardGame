using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Card
{
    //Скрипт прикреплен к канвасу
    //Отрисовывет всё, что ему отправляет CardsManager
    public class CardView : MonoBehaviour
    {
        public GameObject prefCard;
        private GameObject instCard;

        public void DrawCard(CardInfoForInst card)
        {
            instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
            instCard.gameObject.name = card.Name;
            instCard.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = card.Name;
            instCard.transform.Find("Info").gameObject
                                  .GetComponent<TextMeshProUGUI>().text = card.Info;

            instCard.gameObject.GetComponent<Image>().sprite = card.Bg;
            instCard.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = card.Edging;
            instCard.transform.Find("Image").gameObject.GetComponent<Image>().sprite = card.Image;
            instCard.transform.Find("Name").gameObject.GetComponent<Image>().sprite = card.BgName;
        }
    }
}

