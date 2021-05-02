using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;

using Core;

namespace Card
{
    //������ ���������� � �������
    //����������� ��, ��� ��� ���������� CardsManager
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

        //�������� ������� �����
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
            //������ ������
            instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.Euler(0, 90, 0), transform);
            //����� ��� ��� �� ��, ��� ������������� �����
            
            instCard.gameObject.name = card.Name;
            instCard.gameObject.transform.SetSiblingIndex(1);

            //�������� ���� ��� ���������, ������� ������ �� card 
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

        //�������� �������� �� ������������ ���������� �����
        public void DrawCard(Card card)
        {
            StartCoroutine(DeleySpawnCard(card));
        }

        //����� ����� ����������
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

