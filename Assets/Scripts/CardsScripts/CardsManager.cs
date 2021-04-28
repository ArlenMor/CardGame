using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Card
{
    //Имеет в себе список всех карт
    //Инициализирует все карты для мгновенной выгрузки в префаб по имени
    //Имеет в себе последовательность спавна карт в игре
    //Хранит все карты с активными эффектами
    //Имеет доступ на отрисовку информации при помощи класса CardView
    //
    public class CardsManager : MonoBehaviour
    {
        [Header("Path To Json")]
        public string pathToJson;

        [SerializeField]
        private CardView cardView;

        public Texture2D spriteSheet;

        private CardsLoadSystem loadSystem = new CardsLoadSystem();

        private List<Card> cards;
        private Sprite[] sprites;
        private List<CardInfoForInst> cardsForInst;
        private void Awake()
        {
            loadSystem.Load(pathToJson, spriteSheet);
            cards = loadSystem.GetCard();
            sprites = loadSystem.GetSprites();
            cardsForInst = loadSystem.GetCardInfoForInsts();
            Print();
            cardView.DrawCard(cardsForInst[0]);
        }

        public void Print()
        {
            foreach(CardInfoForInst card in cardsForInst)
            {
                Debug.Log("Bg.name = " + card.Bg.name);
                Debug.Log("Edging.name = " + card.Edging.name);
                Debug.Log("Image.name = " + card.Image.name);
                Debug.Log("BgName.name = " + card.BgName.name);
                Debug.Log("Cards name = " + card.Name);
                Debug.Log("Card info = " + card.Info);
            }
        }

       
    }
}

