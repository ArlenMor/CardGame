using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Card
{
    //—крипт прикреплен к канвасу
    //ќтрисовывет всЄ, что ему отправл€ет CardsManager
    public class CardView : MonoBehaviour
    {
        public GameObject prefCard;
        private GameObject instCard;

        //добавить проверку на корректность переданной карты
        public void DrawCard(Card card)
        {
            //создаю объект
            instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
            //мен€ю ему им€ на то, что соответствует карте
            instCard.gameObject.name = card.Name;
            instCard.gameObject.transform.SetSiblingIndex(1);

            //вписываю туда все параметры, которые пришли из card 
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

