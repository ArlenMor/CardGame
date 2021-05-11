using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

using Core;


namespace Card
{
    [System.Serializable]
    //структура одной карты. Содержит в себе данные из json
    public struct CardInfo
    {
        public int ID;
        public string CardType;
        public string BgCard;      
        public string EdgingName;
        public string ImageName;
        public string BgName;
        public string CardName;
        public string InfoCard;
        public int NumberOfDeck;
        public List<float> ChangeStats; //4 параметра. Два первых - свайп вправо, два последних - свайп влево
        public List<string> NeedToTake; //Необходимые карты, для того чтобы свайпнуть вправа эту
        public int Chance;            //Шанс успеха
        public bool CanSafe;            //Можно ли положить эту карту в инвентарь
        
    }

    [System.Serializable]

    public struct EnemyInfo
    {
        public int ID;                      //ID карты
        public string EnemyType;            //Тип врага
        public string BgCard;               //имя спрайта заднего фона
        public string EdgingName;
        public string ImageName;
        public string BgName;
        public string CardName;             //название карты
        public string ArmorName;
        public string DamageName;
        public int NumberOfDeck;            //Номер колоды
        public List<float> EnemyStats;      //Статы. урон, защита, здоровье, шанс уклонения, время между ударами
        public List<string> Drop;           //Список предметов, которые могут выпастьь при убийстве этой карты
        public List<int> ChanceToDrop;      //Шанс на дроп той или иной вещи. Индекс шанса соответствует индексу вещи
        public int MaxNumberOfDropItems;    //Максимальное количество вещей, которые могут дропнуться
    }

    //все карты из json
    public struct Cards
    {
        public List<CardInfo> AllCards;
        public List<EnemyInfo> AllEnemies;
    }

    //структура непосредственно с нужными спрайтами для карты 
    public struct Card
    {
        public int ID;
        public string CardType;
        public Sprite BgCard;           //задний фон
        public Sprite EdgingName;       //Окантовка
        public Sprite ImageName;        //Картинка сверху
        public Sprite BgName;       //фон под именем
        public string CardName;         //Название карты
        public string Info;         //Информация о том, что делает карты
        public int NumberOfDeck;    //Номер колоды (сортируются по силе)
        public List<float> ChangeStats;
        public List<string> NeedToTake;
        public int Chance;
        public bool CanSafe;

        public Card(int _ID,
                    string _CardType,
                    Sprite _Bg,
                    Sprite _Edging,
                    Sprite _Image,
                    Sprite _BgName,
                    string _Name,
                    string _Info,
                    int _NumberOfDeck,
                    List<float> _ChangeStats,
                    List<string> _NeedToTake,
                    int _Chance,
                    bool _CanSafe)
        {
            ID = _ID;
            CardType = _CardType;
            BgCard = _Bg;
            EdgingName = _Edging;
            ImageName = _Image;
            BgName = _BgName;
            CardName = _Name;
            Info = _Info;
            NumberOfDeck = _NumberOfDeck;
            ChangeStats = new List<float>();
            NeedToTake = new List<string>();
            foreach (var stat in _ChangeStats)
                ChangeStats.Add(stat);
            foreach (var nameCard in _NeedToTake)
                NeedToTake.Add(nameCard);

            Chance = _Chance;
            CanSafe = _CanSafe;
        }
    }

    public struct Enemy
    {
        public int ID;                      
        public string EnemyType;            
        public Sprite BgCard;               
        public Sprite EdgingName;
        public Sprite ImageName;
        public Sprite BgName;
        public string CardName;             
        public Sprite ArmorName;
        public Sprite DamageName;
        public int NumberOfDeck;            
        public List<float> EnemyStats;      
        public List<string> Drop;           
        public List<int> ChanceToDrop;      
        public int MaxNumberOfDropItems;  
        
        public Enemy(   int _ID,
                        string _EnemyType,
                        Sprite _BgCard,
                        Sprite _EdgingName,
                        Sprite _ImageName,
                        Sprite _BgName,
                        string _CardName,
                        Sprite _ArmorName,
                        Sprite _DamageName,
                        int _NumberOfDeck,
                        List<float> _EnemyStats,
                        List<string> _Drop,
                        List<int> _ChanceToDrop,
                        int _MaxNumberOfDropItem)
        {
            ID = _ID;
            EnemyType = _EnemyType;
            BgCard = _BgCard;
            EdgingName = _EdgingName;
            ImageName = _ImageName;
            BgName = _BgName;
            CardName = _CardName;
            ArmorName = _ArmorName;
            DamageName = _DamageName;
            NumberOfDeck = _NumberOfDeck;
            EnemyStats = new List<float>();
            foreach (var item in _EnemyStats)
                EnemyStats.Add(item);
            Drop = new List<string>();
            foreach (var item in _Drop)
                Drop.Add(item);
            ChanceToDrop = new List<int>();
            foreach (var item in _ChanceToDrop)
                ChanceToDrop.Add(item);
            MaxNumberOfDropItems = _MaxNumberOfDropItem;
        }
    }

    //Загрузка карт из json файла и спрайтов из spritesheeld'a 
    public class CardsLoadSystem
    {
        //все карты
        private List<CardInfo> cardsInfo = new List<CardInfo>();
        //Все карты врагов
        private List<EnemyInfo> enemiesInfo = new List<EnemyInfo>();
        //все спрайты
        private Sprite[] cardSprites;
        //все карты со спрайтами
        private List<Card> cards = new List<Card>();
        private List<Enemy> enemies = new List<Enemy>();

        //Загрузка из json файла
        private void LoadJson(string jsonName)
        {
            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;
            if (file.name != "InfoCards")
            {
                Debug.Log("{GameLog} => [CardsLoadSystem] => LoadJsonCards() => File not Found");
                return;
            }

            string json = file.text;
            //string json = Path.Combine(Application.persistentDataPath + "/Json/" + jsonName); /*File.ReadAllText(Path);*/
            Cards infoCard = JsonUtility.FromJson<Cards>(json);

            cardsInfo = infoCard.AllCards;
            enemiesInfo = infoCard.AllEnemies;
        }

        private void LoadSprite(Texture2D SpriteSheetCards)
        {
            cardSprites = Resources.LoadAll<Sprite>("CardSprites/" + SpriteSheetCards.name);
        }
        public List<CardInfo> GetCardInfo()
        {
            return cardsInfo;
        }

        public List<Card> GetCards()
        {
            return cards;
        }

        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
        public Sprite[] GetSprites()
        {
            return cardSprites;
        }

        public void Load(string JsonCardName, string JsonEnemyName, Texture2D spriteSheetCards)
        {
            LoadJson(JsonCardName);
            LoadSprite(spriteSheetCards);
            CreateCards();
            GameSettings.numberCards = cardsInfo.Count + enemiesInfo.Count;
        }

        private void CreateCards()
        {
            foreach(CardInfo card in cardsInfo)
            {
                Sprite BgCard = null;
                Sprite EdgingName = null;
                Sprite ImageName = null;
                Sprite BgName = null;

                foreach (Sprite sprite in cardSprites)
                {
                    if (sprite.name == card.BgCard)
                        BgCard = sprite;
                    
                    if (sprite.name == card.EdgingName)
                        EdgingName = sprite;
                    
                    if (sprite.name == card.ImageName)
                        ImageName = sprite;
                    
                    if (sprite.name == card.BgName)
                        BgName = sprite;
                }

                cards.Add(new Card( card.ID, card.CardType, BgCard, 
                                    EdgingName, ImageName, BgName, 
                                    card.CardName, card.InfoCard, 
                                    card.NumberOfDeck, card.ChangeStats, 
                                    card.NeedToTake, card.Chance, card.CanSafe));
            }
            
            foreach(var enemy in enemiesInfo)
            {

                Sprite BgCard = null;
                Sprite EdgingName = null;
                Sprite ImageName = null;
                Sprite BgName = null;
                Sprite ArmorName = null;
                Sprite DamageName = null;

                foreach (Sprite sprite in cardSprites)
                {
                    if (sprite.name == enemy.BgCard)
                        BgCard = sprite;
                    if (sprite.name == enemy.EdgingName)
                        EdgingName = sprite;
                    if (sprite.name == enemy.ImageName)
                        ImageName = sprite;
                    if (sprite.name == enemy.BgName)
                        BgName = sprite;
                    if (sprite.name == enemy.ArmorName)
                        ArmorName = sprite;
                    if (sprite.name == enemy.DamageName)
                        DamageName = sprite;
                }
                enemies.Add(new Enemy(enemy.ID, enemy.EnemyType, BgCard,
                                            EdgingName, ImageName, BgName, enemy.CardName,
                                            ArmorName, DamageName, enemy.NumberOfDeck,
                                            enemy.EnemyStats, enemy.Drop, enemy.ChanceToDrop,
                                            enemy.MaxNumberOfDropItems));


            }
        }

    }
}
