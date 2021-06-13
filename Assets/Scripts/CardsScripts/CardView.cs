using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;

using Core;

namespace Card
{
    //Скрипт прикреплен к канвасу
    //Отрисовывет всё, что ему отправляет CardsManager
    public class CardView : MonoBehaviour
    {
        public GameObject prefCard;
        private GameObject instCard;
        private Animator animCard;

        public GameObject prefEnemy;
        private GameObject instEnemy;

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
            GameSettings.info += "\nПодумай хорошенько...";
            //создаю объект
            instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0), transform);
            //меняю ему имя на то, что соответствует карте
            
            instCard.gameObject.name = card.CardName;
            instCard.gameObject.transform.SetSiblingIndex(1);

            //вписываю туда все параметры, которые пришли из card 
            instCard.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = card.CardName;
            instCard.transform.Find("Info").gameObject
                                  .GetComponent<TextMeshProUGUI>().text = card.Info;

            instCard.gameObject.GetComponent<Image>().sprite = card.BgCard;
            instCard.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = card.EdgingName;
            instCard.transform.Find("Image").gameObject.GetComponent<Image>().sprite = card.ImageName;
            instCard.transform.Find("Name").gameObject.GetComponent<Image>().sprite = card.BgName;

            animCard = instCard.GetComponent<Animator>();
            animCard.SetTrigger("openCard");

        }

        //добавить проверку на корректность переданной карты
        public void DrawCard(Card card)
        {
            StartCoroutine(DeleySpawnCard(card));
        }

        //Пауза перед анимациями
        IEnumerator DeleySpawnCard(Card card)
        {
            CreateCardCover();
            yield return new WaitForSeconds(animCardCover.runtimeAnimatorController.animationClips.Length - 0.25f);
            CreateCard(card);
            Destroy(instCardCover);
            yield return new WaitForSeconds(animCard.runtimeAnimatorController.animationClips.Length - 0.65f);
            Destroy(animCard);
        }

        IEnumerator DeleySpawnEnemy(Enemy enemy)
        {
            CreateCardCover();
            yield return new WaitForSeconds(animCardCover.runtimeAnimatorController.animationClips.Length - 0.25f);
            CreateEnemy(enemy);
            Destroy(instCardCover);
            yield return new WaitForSeconds(1f);
            Destroy(animCard);
        }

        public void DrawEnemy(Enemy enemy)
        {
            StartCoroutine(DeleySpawnEnemy(enemy));
        }

        private void CreateEnemy(Enemy enemy)
        {
            GameSettings.info += "\nВраг! Бей его!!!";
            //создаю объект
            instEnemy = Instantiate(prefEnemy, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform);
            //меняю ему имя на то, что соответствует карте

            instEnemy.gameObject.name = enemy.CardName;
            instEnemy.gameObject.transform.SetSiblingIndex(1);

            //вписываю туда все параметры, которые пришли из card 
            instEnemy.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = enemy.CardName;

            instEnemy.gameObject.GetComponent<Image>().sprite = enemy.BgCard;
            instEnemy.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = enemy.EdgingName;
            instEnemy.transform.Find("Image").gameObject.GetComponent<Image>().sprite = enemy.ImageName;
            instEnemy.transform.Find("Name").gameObject.GetComponent<Image>().sprite = enemy.BgName;
            instEnemy.transform.Find("Armor").gameObject.GetComponent<Image>().sprite = enemy.ArmorName;
            instEnemy.transform.Find("Damage").gameObject.GetComponent<Image>().sprite = enemy.DamageName;

            instEnemy.transform.Find("Armor").gameObject.transform
                .GetChild(0).gameObject
                .GetComponent<TextMeshProUGUI>().text = enemy.EnemyStats[1] + "";

            instEnemy.transform.Find("Damage").gameObject.transform
                               .GetChild(0).gameObject
                               .GetComponent<TextMeshProUGUI>().text = enemy.EnemyStats[0] + "";

        }
    }
}

