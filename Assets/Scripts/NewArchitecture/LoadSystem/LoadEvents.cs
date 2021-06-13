using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Load
{
    [System.Serializable]
    public struct EventsDeserialization
    {
        public int Id;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<int> NeedToTake;        //Id карт необходимых чтобы начать событие
        public List<float> ChangeStats;     //изменение статов игрока после использования
        public bool CanSkip;                //Можно ли свайпнуть влево
        public List<int> EffectRight;            //Всё что связано со свайпом вправо
        public List<int> ChanceEffectRight;      
        public List<int> DropRight;
        public List<int> ChanceDropRight;
        public List<int> EffectLeft;            //Всё что связано со свайпом влево
        public List<int> ChanceEffectLeft;      
        public List<int> DropLeft;
        public List<int> ChanceDropLeft;

        public List<string> Sprites; //BgCard, Edging, Image, BgName;
    }

    public struct EventsJson
    {
        public List<EventsDeserialization> AllEvents;
    }

    public class Event
    {
        public int Id;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<int> NeedToTake;        
        public List<float> ChangeStats;     
        public bool CanSkip;                
        public List<int> EffectRight;            
        public List<int> ChanceEffectRight;
        public List<int> DropRight;
        public List<int> ChanceDropRight;
        public List<int> EffectLeft;           
        public List<int> ChanceEffectLeft;
        public List<int> DropLeft;
        public List<int> ChanceDropLeft;

        public List<Sprite> Sprites; //BgCard, Edging, Image, BgName;

        public Event(    int _Id,
                        string _CardName,
                        string _InfoCard,
                        int _PartOfDeck,
                        List<int> _NeedToTake,
                        List<float> _ChangeStats,
                        bool _CanSkip,
                        List<int> _EffectRight,
                        List<int> _ChanceEffectRight,
                        List<int> _DropRight,
                        List<int> _ChanceDropRight,
                        List<int> _EffectLeft,
                        List<int> _ChanceEffectLeft,
                        List<int> _DropLeft,
                        List<int> _ChanceDropLeft,
                        List<Sprite> _Sprites)
        {
            Id = _Id;
            CardName = _CardName;
            InfoCard = _InfoCard;
            PartOfDeck = _PartOfDeck;
            NeedToTake = new List<int>();
            ChangeStats = new List<float>();
            CanSkip = _CanSkip;
            EffectRight = new List<int>();
            ChanceEffectRight = new List<int>();
            DropRight = new List<int>();
            ChanceDropRight = new List<int>();
            EffectLeft = new List<int>();
            ChanceEffectLeft = new List<int>();
            DropLeft = new List<int>();
            ChanceDropLeft = new List<int>();
            Sprites = new List<Sprite>();

            foreach (var item in _NeedToTake)
                NeedToTake.Add(item);
            foreach (var item in _ChangeStats)
                ChangeStats.Add(item);

            foreach (var item in _EffectRight)
                EffectRight.Add(item);
            foreach (var item in _ChanceEffectRight)
                ChanceEffectRight.Add(item);
            foreach (var item in _DropRight)
                DropRight.Add(item);
            foreach (var item in _ChanceDropRight)
                ChanceDropRight.Add(item);

            foreach (var item in _EffectLeft)
                EffectLeft.Add(item);
            foreach (var item in _ChanceEffectLeft)
                ChanceEffectLeft.Add(item);
            foreach (var item in _DropLeft)
                DropLeft.Add(item);
            foreach (var item in _ChanceDropLeft)
                ChanceDropLeft.Add(item);

            foreach (var item in _Sprites)
                Sprites.Add(item);
        }
    }

    public static class LoadEvents
    {
        public static List<Event> LoadEventsFromJson(string jsonName, Sprite[] allSprites)
        {
            List<Event> returnEvents = new List<Event>();

            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;

            if (file.name != "Events")
            {
                Debug.Log("{LoadLog} => [LoadEvents] => LoadEventsFromJson() => File not Found");
                return null;
            }

            string json = file.text;

            EventsJson eventsJson = JsonUtility.FromJson<EventsJson>(json);



            foreach (var _event in eventsJson.AllEvents)
            {
                List<Sprite> eventSprites = new List<Sprite>();

                Sprite BgCard = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;

                foreach (Sprite sprite in allSprites)
                {
                    if (_event.Sprites[0] == sprite.name)
                        BgCard = sprite;
                    if (_event.Sprites[1] == sprite.name)
                        Edging = sprite;
                    if (_event.Sprites[2] == sprite.name)
                        Image = sprite;
                    if (_event.Sprites[3] == sprite.name)
                        BgName = sprite;
                }

                eventSprites.Add(BgCard); eventSprites.Add(Edging); eventSprites.Add(Image); eventSprites.Add(BgName);


                returnEvents.Add(new Event(_event.Id, _event.CardName, _event.InfoCard, _event.PartOfDeck, _event.NeedToTake, _event.ChangeStats,
                    _event.CanSkip, _event.EffectRight, _event.ChanceEffectRight, _event.DropRight, _event.ChanceDropRight,
                    _event.EffectLeft, _event.ChanceDropLeft, _event.DropLeft, _event.ChanceDropLeft, eventSprites));
            }

            return returnEvents;
        }
    }
}