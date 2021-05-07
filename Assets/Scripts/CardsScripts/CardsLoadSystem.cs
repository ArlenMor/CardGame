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
        public string BGName;      
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

    //все карты из json
    public struct Cards
    {
        public List<CardInfo> AllCards;
    }

    //структура непосредственно с нужными спрайтами для карты 
    public struct Card
    {
        public Sprite Bg;           //задний фон
        public Sprite Edging;       //Окантовка
        public Sprite Image;        //Картинка сверху
        public Sprite BgName;       //фон под именем
        public string Name;         //Название карты
        public string Info;         //Информация о том, что делает карты
        public int NumberOfDeck;    //Номер колоды (сортируются по силе)
        public List<float> ChangeStats;
        public List<string> NeedToTake;
        public int Chance;
        public bool CanSafe;

        public Card(Sprite _Bg,
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
            Bg = _Bg;
            Edging = _Edging;
            Image = _Image;
            BgName = _BgName;
            Name = _Name;
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

    //Загрузка карт из json файла и спрайтов из spritesheeld'a 
    public class CardsLoadSystem
    {
        //все карты
        private List<CardInfo> cardsInfo = new List<CardInfo>();
        //все спрайты
        private Sprite[] sprites;
        //все карты со спрайтами
        private List<Card> cards = new List<Card>();

        //Загрузка из json файла
        private void LoadJson(string jsonName)
        {
            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;
            if (file.name != "InfoCards")
            {
                Debug.Log("{GameLog} => [CardsLoadSystem] - Load() => File not Found");
                return;
            }

            string json = file.text;
            //string json = Path.Combine(Application.persistentDataPath + "/Json/" + jsonName); /*File.ReadAllText(Path);*/
            Cards infoCard = JsonUtility.FromJson<Cards>(json);
            cardsInfo = infoCard.AllCards;
        }

        private void LoadSprite(Texture2D SpriteSheet)
        {
            sprites = Resources.LoadAll<Sprite>("CardSprites/" + SpriteSheet.name);
        }
        public List<CardInfo> GetCardInfo()
        {
            return cardsInfo;
        }

        public List<Card> GetCards()
        {
            return cards;
        }
        public Sprite[] GetSprites()
        {
            return sprites;
        }

        public void Load(string PathToJson, Texture2D spriteSheet)
        {
            LoadJson(PathToJson);
            LoadSprite(spriteSheet);
            CreateCards();
            GameSettings.numberCards = cardsInfo.Count;
        }

        private void CreateCards()
        {
            foreach(CardInfo card in cardsInfo)
            {
                Sprite Bg = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;

                foreach (Sprite sprite in sprites)
                {
                    if (sprite.name == card.BGName)
                        Bg = sprite;
                    
                    if (sprite.name == card.EdgingName)
                        Edging = sprite;
                    
                    if (sprite.name == card.ImageName)
                        Image = sprite;
                    
                    if (sprite.name == card.BgName)
                        BgName = sprite;
                    
                }

                cards.Add(new Card(Bg, Edging, Image, BgName, card.CardName, card.InfoCard, card.NumberOfDeck, card.ChangeStats, card.NeedToTake, card.Chance, card.CanSafe));
            }
            
        }
    }
}
