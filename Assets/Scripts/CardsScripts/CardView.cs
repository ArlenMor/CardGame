using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;

using Core;

namespace Card
{
    //—крипт прикреплен к канвасу
    //ќтрисовывет всЄ, что ему отправл€ет CardsManager
    public class CardView : MonoBehaviour
    {
        public GameObject prefCard;
        private GameObject instCard;
        private Animator animCard;

        public GameObject prefCardCover;
        private GameObject instCardCover;
        private Animator animCardCover;

        private void Update()
        {
        }

        //создание обертки карты
        private void CreateCardCover()
        {
            instCardCover = Instantiate(prefCardCover, new Vector3(0, 0, 0), Quaternion.identity, transform);
            animCardCover = instCardCover.GetComponent<Animator>();
            instCardCover.gameObject.name = "CardCover";
            instCardCover.gameObject.transform.SetSiblingIndex(2);
            animCardCover.SetTrigger("newCard");
        }

        private void CreateCard(Card card)
        {
            //создаю объект
            instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0), transform);
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

            animCard = instCard.GetComponent<Animator>();
            animCard.SetTrigger("openCard");

        }

        //добавить проверку на корректность переданной карты
        public void DrawCard(Card card)
        {
            StartCoroutine(DeleySpawnCard(card));
        }

        //ѕауза перед анимаци€ми
        IEnumerator DeleySpawnCard(Card card)
        {
            CreateCardCover();
            yield return new WaitForSeconds(animCardCover.runtimeAnimatorController.animationClips.Length - 0.25f);
            CreateCard(card);
            Destroy(instCardCover);
            yield return new WaitForSeconds(animCard.runtimeAnimatorController.animationClips.Length - 0.65f);
            Destroy(animCard);
        }
    }
}

