using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

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
        //Drag and Drop
        public CardView cardView;
        public Texture2D spriteSheet;


        //Всё, что связаннос с картами
        private List<CardInfo> cardsInfo;
        private Sprite[] sprites;
        private List<Card> cards;
        private List<Card>[] Decks = new List<Card>[numberOfDecks];
        private List<Card> generalDeck = new List<Card>();
        private static int numberOfDecks = 3;
        private int[] numberCardInDeck = new int[3];

        //Всё, что связанно с загрузкой карт
        private CardsLoadSystem loadSystem = new CardsLoadSystem();
        private string jsonName = "InfoCards";




        
        private void Awake()
        {
            for (int i = 0; i < numberOfDecks; i++)
            {
                Decks[i] = new List<Card>();
            }
            //Загрузка всего что нужно
            loadSystem.Load(jsonName, spriteSheet);
            cardsInfo = loadSystem.GetCardInfo();
            sprites = loadSystem.GetSprites();
            cards = loadSystem.GetCards();
            //Создание колоды
            CreateDeck();
            ShuffleDeck();
            CreateGeneralDeck();
        }

        private void CreateDeck()
        {
            foreach(Card card in cards)
            {
                if (card.NumberOfDeck == 0)
                    Decks[card.NumberOfDeck].Add(card);
                if (card.NumberOfDeck == 1)
                    Decks[card.NumberOfDeck].Add(card);
                if (card.NumberOfDeck == 2)
                    Decks[card.NumberOfDeck].Add(card);
            }

            numberCardInDeck[0] = Decks[0].Count;
            numberCardInDeck[1] = Decks[1].Count;
            numberCardInDeck[2] = Decks[2].Count;

        }

        private void ShuffleDeck()
        {
            for(int i = 0; i < Decks.Length; i++)
            {
                for(int j = Decks[i].Count - 1; j > 0; j--)
                {
                    int rand = Random.Range(0, Decks[i].Count);
                    var tmp = Decks[i][rand];
                    Decks[i][rand] = Decks[i][j];
                    Decks[i][j] = tmp;   
                }
            }
        }

        private void CreateGeneralDeck()
        {
            for (int i = 0; i < Decks.Length; i++)
            {
                for(int j = 0; j < Decks[i].Count; j++)
                {
                    generalDeck.Add(Decks[i][j]);
                }
            }
        }
        public void SpawnCard(int index)
        {
            GameSettings.canSpawnCard = false;
            if(index < generalDeck.Count - 1)
            {
                cardView.DrawCard(generalDeck[index]);
            }
            else
            {
                Debug.Log("{GameLog} => [CardsManager] - SpawnCard() => Deck is empty");
            }
        }

        public Card GetCard(int index)
        {
            return generalDeck[index];
        }
        public void Print(Card card)
        {
            /*foreach(Card card in cards)
            {*/
                /*Debug.Log("Bg.name = " + card.Bg.name);
                Debug.Log("Edging.name = " + card.Edging.name);
                Debug.Log("Image.name = " + card.Image.name);
                Debug.Log("BgName.name = " + card.BgName.name);
                Debug.Log("Cards name = " + card.Name);
                Debug.Log("Card info = " + card.Info);*/
                Debug.Log("HP = " + card.HealthPlus);
                Debug.Log("HM = " + card.HealthMinus);
                Debug.Log("MP = " + card.ManaPlus);
                Debug.Log("MM = " + card.ManaMinus);
            //}
        }
    }
}

