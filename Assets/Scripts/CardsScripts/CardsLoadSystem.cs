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
    public struct Card
    {
        public string BGName;      
        public string EdgingName;
        public string ImageName;
        public string BgName;
        public string CardName;
        public string InfoCard;
    }

    public struct Cards
    {
        public List<Card> AllCards;
    }

    public struct CardInfoForInst
    {
        public Sprite Bg;           //задний фон
        public Sprite Edging;       //Окантовка
        public Sprite Image;        //Картинка сверху
        public Sprite BgName;         //фон под именем
        public string Name;
        public string Info;

        public CardInfoForInst(Sprite _Bg, Sprite _Edging, Sprite _Image, Sprite _BgName, string _Name, string _Info)
        {
            Bg = _Bg;
            Edging = _Edging;
            Image = _Image;
            BgName = _BgName;
            Name = _Name;
            Info = _Info;
        }
    }

    public class CardsLoadSystem
    {
        private List<Card> cards = new List<Card>();
        private Sprite[] sprites;
        private List<CardInfoForInst> cardsForInst = new List<CardInfoForInst>();

        /*public GameObject prefCard;
        private GameObject instCard;*/

        /*[SerializeField]
        private string loadPath;*/

        private void LoadJson(string Path)
        {
            if (!File.Exists(Path))
            {
                Debug.Log("{GameLog} => [CardsLoadSystem] - Load() => File not Found");
                return;
            }

            string json = File.ReadAllText(Path);
            Cards infoCard = JsonUtility.FromJson<Cards>(json);
            cards = infoCard.AllCards;


            Debug.Log("{GameLog} => [CardsLoadSystem] - Load() => Success");
        }

        private void LoadSprite(Texture2D SpriteSheet)
        {
            sprites = Resources.LoadAll<Sprite>("CardSprites/" + SpriteSheet.name);
        }
        public List<Card> GetCard()
        {
            return cards;
        }

        public List<CardInfoForInst> GetCardInfoForInsts()
        {
            return cardsForInst;
        }
        public Sprite[] GetSprites()
        {
            return sprites;
        }

        public void Load(string PathToJson, Texture2D spriteSheet)
        {
            LoadJson(PathToJson);
            LoadSprite(spriteSheet);
            CreateCardsForInst();
        }

        private void CreateCardsForInst()
        {
            foreach(Card card in cards)
            {
                //fix it!!!
                Sprite Bg = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;
                string Name;
                string Info;                
                /*instCard = Instantiate(prefCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
                instCard.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = card.CardName;

                instCard.transform.Find("Info").gameObject
                                  .GetComponent<TextMeshProUGUI>().text = card.InfoCard;*/

                Info = card.InfoCard;
                Name = card.CardName;

                //проверить нет ли пустых полей, если есть, то выкинуть ex
                foreach (Sprite sprite in sprites)
                {
                    if (sprite.name == card.BGName)
                        Bg = sprite;
                    //instCard.gameObject.GetComponent<Image>().sprite = sprite;
                    if (sprite.name == card.EdgingName)
                        Edging = sprite;
                    //instCard.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = sprite;
                    if (sprite.name == card.ImageName)
                        Image = sprite;
                    //instCard.transform.Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
                    if (sprite.name == card.BgName)
                        BgName = sprite;
                    //instCard.transform.Find("Name").gameObject.GetComponent<Image>().sprite = sprite;
                }

                cardsForInst.Add(new CardInfoForInst(Bg, Edging, Image, BgName, Name, Info));
            }
            
        }
    }
}
