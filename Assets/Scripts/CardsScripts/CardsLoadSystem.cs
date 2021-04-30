using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;


namespace Card
{
    [System.Serializable]
    //структура одной карты. —одержит в себе данные из json
    public struct CardInfo
    {
        public string BGName;      
        public string EdgingName;
        public string ImageName;
        public string BgName;
        public string CardName;
        public string InfoCard;
        public int NumberOfDeck;
    }

    //все карты из json
    public struct Cards
    {
        public List<CardInfo> AllCards;
    }

    //структура непосредственно с нужными спрайтами дл€ карты 
    public struct Card
    {
        public Sprite Bg;           //задний фон
        public Sprite Edging;       //ќкантовка
        public Sprite Image;        // артинка сверху
        public Sprite BgName;       //фон под именем
        public string Name;
        public string Info;
        public int NumberOfDeck;   

        public Card(Sprite _Bg, Sprite _Edging, Sprite _Image, Sprite _BgName, string _Name, string _Info, int _NumberOfDeck)
        {
            Bg = _Bg;
            Edging = _Edging;
            Image = _Image;
            BgName = _BgName;
            Name = _Name;
            Info = _Info;
            NumberOfDeck = _NumberOfDeck;
        }
    }

    //«агрузка карт из json файла и спрайтов из spritesheeld'a 
    public class CardsLoadSystem
    {
        //все карты
        private List<CardInfo> cardsInfo = new List<CardInfo>();
        //все спрайты
        private Sprite[] sprites;
        //все карты со спрайтами
        private List<Card> cards = new List<Card>();

        //«агрузка из json файла
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
        }

        private void CreateCards()
        {
            foreach(CardInfo card in cardsInfo)
            {
                //fix it!!!
                Sprite Bg = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;
                string Name;
                string Info;
                int Deck;

                Info = card.InfoCard;
                Name = card.CardName;
                Deck = card.NumberOfDeck;

                //проверить нет ли пустых полей, если есть, то выкинуть ex
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

                cards.Add(new Card(Bg, Edging, Image, BgName, Name, Info, Deck));
            }
            
        }
    }
}
