using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Load
{
    [System.Serializable]
    public struct ItemsDeserialization
    {
        public int Id;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<int> NeedToTake;        //Id карт необходимых чтобы поднять карту
        public List<float> ChangeStats;     //изменение статов игрока после использования
        public float Chance;                //шанс на успешное использование
        public List<int> Effect;            //id эффектов после использования
        public List<int> ChanceEffect;      //шанс на выпадение эффекта
        public List<int> Event;             //id события после выполнения
        public List<int> ChanceEvent;       //Шанс этого события
        public List<int> Enemy;             //id врага, который может попасться след картой после использования
        public List<int> ChanceEnemy;       //шанс на спавн врага
        public int Value;

        public List<string> Sprites; //BgCard, Edging, Image, BgName;
    }

    public struct ItemsJson
    {
        public List<ItemsDeserialization> AllItems;
    }

    public class Item
    {
        public int Id;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<int> NeedToTake;        
        public List<float> ChangeStats;     
        public float Chance;               
        public List<int> Effect;            
        public List<int> ChanceEffect;      
        public List<int> Event;             
        public List<int> ChanceEvent;      
        public List<int> Enemy;           
        public List<int> ChanceEnemy;
        public int value;

        public List<Sprite> Sprites;

        public Item(int _Id,
                        string _CardName,
                        string _InfoCard,
                        int _PartOfDeck,
                        List<int> _NeedToTake,
                        List<float> _ChangeStats,
                        float _Chance,
                        List<int> _Effect,
                        List<int> _ChanceEffect,
                        List<int> _Event,
                        List<int> _ChanceEvent,
                        List<int> _Enemy,
                        List<int> _ChanceEnemy,
                        int _value,
                        List<Sprite> _Sprites)
        {
            Id = _Id;
            CardName = _CardName;
            InfoCard = _InfoCard;
            PartOfDeck = _PartOfDeck;
            NeedToTake = new List<int>();
            ChangeStats = new List<float>();
            Chance = _Chance;
            Effect = new List<int>();
            ChanceEffect = new List<int>();
            Event = new List<int>();
            ChanceEvent = new List<int>();
            Enemy = new List<int>();
            ChanceEnemy = new List<int>();
            Sprites = new List<Sprite>();

            foreach (var item in _NeedToTake)
                NeedToTake.Add(item);
            foreach (var item in _ChangeStats)
                ChangeStats.Add(item);
            foreach (var item in _Effect)
                Effect.Add(item);
            foreach (var item in _ChanceEffect)
                ChanceEffect.Add(item);
            foreach (var item in _Event)
                Event.Add(item);
            foreach (var item in _ChanceEvent)
                ChanceEvent.Add(item);
            foreach (var item in _Enemy)
                Enemy.Add(item);
            foreach (var item in _ChanceEnemy)
                ChanceEnemy.Add(item);
            foreach (var item in _Sprites)
                Sprites.Add(item);

            value = _value;
        }
    }

    public static class LoadItems
    {
        public static List<Item> LoadItemsFromJson(string jsonName, Sprite[] allSprites)
        {
            List<Item> returnItems = new List<Item>();

            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;

            if (file.name != "Items")
            {
                Debug.Log("{LoadLog} => [LoadItems] => LoadItemsFromJson() => File not Found");
                return null;
            }

            string json = file.text;

            ItemsJson itemsJson = JsonUtility.FromJson<ItemsJson>(json);



            foreach (var item in itemsJson.AllItems)
            {
                List<Sprite> itemSprites = new List<Sprite>();

                Sprite BgCard = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;

                foreach (Sprite sprite in allSprites)
                {
                    if (item.Sprites[0] == sprite.name)
                        BgCard = sprite;
                    if (item.Sprites[1] == sprite.name)
                        Edging = sprite;
                    if (item.Sprites[2] == sprite.name)
                        Image = sprite;
                    if (item.Sprites[3] == sprite.name)
                        BgName = sprite;
                }

                itemSprites.Add(BgCard); itemSprites.Add(Edging); itemSprites.Add(Image); itemSprites.Add(BgName);


                returnItems.Add(new Item(   item.Id, item.CardName, item.InfoCard, item.PartOfDeck, item.NeedToTake, item.ChangeStats, item.Chance,
                                            item.Effect, item.ChanceEffect, item.Event, item.ChanceEvent, item.Enemy, item.ChanceEnemy, item.Value, itemSprites));
            }

            return returnItems;
        }
    }
}