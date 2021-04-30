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

        private string jsonName = "InfoCards";

        private int[] numberCardInDeck = new int[3];

        private int index = 0;
        private static int numberOfDecks = 3;
        public CardView cardView;

        public Texture2D spriteSheet;

        private CardsLoadSystem loadSystem = new CardsLoadSystem();

        private List<CardInfo> cardsInfo;
        private Sprite[] sprites;
        private List<Card> cards;
        

        private List<Card>[] Decks = new List<Card>[numberOfDecks];
        private List<Card> generalDeck = new List<Card>();
        private void Awake()
        {
            for (int i = 0; i < numberOfDecks; i++)
            {
                Decks[i] = new List<Card>();
            }
            loadSystem.Load(jsonName, spriteSheet);
            cardsInfo = loadSystem.GetCardInfo();
            sprites = loadSystem.GetSprites();
            cards = loadSystem.GetCards();
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
        public void SpawnCard()
        {
            GameSettings.canSpawnCard = false;
            if(index < generalDeck.Count - 1)
            {
                cardView.DrawCard(generalDeck[index]);
                index++;
            }
            else
            {
                Debug.Log("{GameLog} => [CardsManager] - SpawnCard() => Deck is empty");
            }
        }
        public void Print()
        {
            foreach(Card card in cards)
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

