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
        private static int numberOfDecks = 5;
        private List<CardInfo> cardsInfo;
        private Sprite[] sprites;
        private List<Card> cards;
        private List<Enemy> enemies;
        private List<Card>[] partsOfCardsDeck = new List<Card>[numberOfDecks];
        private List<Enemy>[] partsOfEnemiesDeck = new List<Enemy>[numberOfDecks];
        private List<Card> generalCardsDeck = new List<Card>();
        private List<Enemy> generalEnemiesDeck = new List<Enemy>();
        private int[] numberCardInDeck = new int[numberOfDecks];

        //Всё, что связанно с загрузкой карт
        private CardsLoadSystem loadSystem = new CardsLoadSystem();
        private string jsonCardName = "InfoCards";
        private string jsonEnemyName = "InfoEnemies";




        
        private void Awake()
        {
            for (int i = 0; i < numberOfDecks; i++)
            {
                partsOfCardsDeck[i] = new List<Card>();
                partsOfEnemiesDeck[i] = new List<Enemy>();
            }
            //Загрузка всего что нужно
            loadSystem.Load(jsonCardName, jsonEnemyName, spriteSheet);
            cardsInfo = loadSystem.GetCardInfo();
            sprites = loadSystem.GetSprites();
            cards = loadSystem.GetCards();
            enemies = loadSystem.GetEnemies();

            //Создание колоды
            CreateDecks();
            //ShuffleDeck();
            CreateGeneralDecks();
        }

        private void CreateDecks()
        {
            foreach(Card card in cards)
            {
                if (card.NumberOfDeck == 0)
                    partsOfCardsDeck[card.NumberOfDeck].Add(card);
                if (card.NumberOfDeck == 1)
                    partsOfCardsDeck[card.NumberOfDeck].Add(card);
                if (card.NumberOfDeck == 2)
                    partsOfCardsDeck[card.NumberOfDeck].Add(card);
            }

            foreach(Enemy enemy in enemies)
            {
                if(enemy.NumberOfDeck == 0)
                    partsOfEnemiesDeck[enemy.NumberOfDeck].Add(enemy);
                if (enemy.NumberOfDeck == 1)
                    partsOfEnemiesDeck[enemy.NumberOfDeck].Add(enemy);
                if (enemy.NumberOfDeck == 2)
                    partsOfEnemiesDeck[enemy.NumberOfDeck].Add(enemy);
            }



            numberCardInDeck[0] = partsOfCardsDeck[0].Count + partsOfEnemiesDeck[0].Count;
            numberCardInDeck[1] = partsOfCardsDeck[1].Count + partsOfEnemiesDeck[1].Count;
            numberCardInDeck[2] = partsOfCardsDeck[2].Count + partsOfEnemiesDeck[2].Count;

        }

        private void ShuffleDeck(List<Card> Deck)
        {
                for(int j = Deck.Count - 1; j > 0; j--)
                {
                    int rand = Random.Range(0, Deck.Count - 1);
                    var tmp = Deck[rand];
                    Deck[rand] = Deck[j];
                    Deck[j] = tmp;   
                }
        }

        private void CreateGeneralDecks()
        {
            for (int i = 0; i < partsOfCardsDeck.Length; i++)
                for(int j = 0; j < partsOfCardsDeck[i].Count; j++)             
                    generalCardsDeck.Add(partsOfCardsDeck[i][j]);

            for (int i = 0; i < partsOfEnemiesDeck.Length; i++)
                for (int j = 0; j < partsOfEnemiesDeck[i].Count; j++)
                    generalEnemiesDeck.Add(partsOfEnemiesDeck[i][j]);
        }
        public void SpawnCard(int index)
        {
            GameSettings.canSpawnCard = false;
            cardView.DrawCard(generalCardsDeck[index]);
        }

        public void SpawnEnemy(int index)
        {
            GameSettings.canSpawnCard = false;
            cardView.DrawEnemy(generalEnemiesDeck[index]);
        }


        //проверку
        public Card GetCard(int index)
        {
            return generalCardsDeck[index];
        }

        public Enemy GetEnemy(int index)
        {
            return generalEnemiesDeck[index];
        }

        public int GetCardsCount()
        {
            return cards.Count;
        }
        public int GetEnemiesCount()
        {
            return enemies.Count;
        }

        public void PrintEnemy(Enemy enemy)
        {
            Debug.Log("ID = " + enemy.ID);
            Debug.Log("EnemyType = " + enemy.EnemyType);
            Debug.Log("BgCard.name = " + enemy.BgCard.name);
            Debug.Log("EdgingName.name = " + enemy.EdgingName.name);
            Debug.Log("ImageName.name = " + enemy.ImageName.name);
            Debug.Log("BgName.name = " + enemy.BgName.name);
            Debug.Log("Card name = " + enemy.CardName);
            Debug.Log("Change stats:");
            foreach (var stat in enemy.EnemyStats)
            {
                Debug.Log(stat);
            }
            Debug.Log("drop :");
            foreach (var cardName in enemy.Drop)
            {
                Debug.Log(cardName);
            }
        }
        public void Print(Card card)
        {
            /*foreach(Card card in cards)
            {*/
            Debug.Log("ID = " + card.ID);
            Debug.Log("CardType = " + card.CardType);
            Debug.Log("Bg.name = " + card.BgCard.name);
            Debug.Log("Edging.name = " + card.EdgingName.name);
            Debug.Log("Image.name = " + card.ImageName.name);
            Debug.Log("BgName.name = " + card.BgName.name);
            Debug.Log("Cards name = " + card.CardName);
            Debug.Log("Card info = " + card.Info);
            Debug.Log("Card Chance = " + card.Chance);
            Debug.Log("Card Can safe = " + card.CanSafe);
            Debug.Log("Change Stats:");
            foreach(var stat in card.ChangeStats)
            {
                Debug.Log(stat);
            }
            Debug.Log("Card NeedToTake:");
            foreach(var cardName in card.NeedToTake)
            {
                Debug.Log(cardName);
            }
            //}
        }
    }
}

