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
    //��������� ����� �����. �������� � ���� ������ �� json
    public struct CardInfo
    {
        public string BGName;      
        public string EdgingName;
        public string ImageName;
        public string BgName;
        public string CardName;
        public string InfoCard;
        public int NumberOfDeck;
        public float HealthPlus;
        public float ManaPlus;
        public float HealthMinus;
        public float ManaMinus;
    }

    //��� ����� �� json
    public struct Cards
    {
        public List<CardInfo> AllCards;
    }

    //��������� ��������������� � ������� ��������� ��� ����� 
    public struct Card
    {
        public Sprite Bg;           //������ ���
        public Sprite Edging;       //���������
        public Sprite Image;        //�������� ������
        public Sprite BgName;       //��� ��� ������
        public string Name;         //�������� �����
        public string Info;         //���������� � ���, ��� ������ �����
        public int NumberOfDeck;    //����� ������ (����������� �� ����)
        public float HealthPlus;          //��������� �������� ��� ������ ������
        public float ManaPlus;            //��������� ���� ��� ������ ������
        public float HealthMinus;
        public float ManaMinus;

        public Card(Sprite _Bg,
                    Sprite _Edging,
                    Sprite _Image,
                    Sprite _BgName,
                    string _Name,
                    string _Info,
                    int _NumberOfDeck,
                    float _Heaelth,
                    float _ManaPlus,
                    float _HealthMinus,
                    float _ManaMinus)
        {
            Bg = _Bg;
            Edging = _Edging;
            Image = _Image;
            BgName = _BgName;
            Name = _Name;
            Info = _Info;
            NumberOfDeck = _NumberOfDeck;
            HealthPlus = _Heaelth;
            ManaPlus = _ManaPlus;
            HealthMinus = _HealthMinus;
            ManaMinus = _ManaMinus;
        }
    }

    //�������� ���� �� json ����� � �������� �� spritesheeld'a 
    public class CardsLoadSystem
    {
        //��� �����
        private List<CardInfo> cardsInfo = new List<CardInfo>();
        //��� �������
        private Sprite[] sprites;
        //��� ����� �� ���������
        private List<Card> cards = new List<Card>();

        //�������� �� json �����
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
                //fix it!!!
                Sprite Bg = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;
                string Name;
                string Info;
                int Deck;
                float HealthPlus;
                float ManaPlus;
                float HealthMinus;
                float ManaMinus;

                Info = card.InfoCard;
                Name = card.CardName;
                Deck = card.NumberOfDeck;
                HealthPlus = card.HealthPlus;
                ManaPlus = card.ManaPlus;
                HealthMinus = card.HealthMinus;
                ManaMinus = card.ManaMinus;

                //��������� ��� �� ������ �����, ���� ����, �� �������� ex
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

                cards.Add(new Card(Bg, Edging, Image, BgName, Name, Info, Deck, HealthPlus, ManaPlus, HealthMinus, ManaMinus));
            }
            
        }
    }
}
