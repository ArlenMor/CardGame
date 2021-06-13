using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Load;


namespace spaceItem
{
    public class ItemData
    {
        public int Id;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<int> NeedToTake = new List<int>();
        public List<float> ChangeStats = new List<float>();
        public float Chance;
        public List<int> Effect = new List<int>();
        public List<int> ChanceEffect = new List<int>();
        public List<int> Event = new List<int>();
        public List<int> ChanceEvent = new List<int>();
        public List<int> Enemy = new List<int>();
        public List<int> ChanceEnemy = new List<int>();
        public int Value;

        public void Init(int id, string cardName, string infoCard, int partOfDeck, List<int> needToTake, List<float> changeStats, float chance, List<int> effect, List<int> chanceEffect, List<int> @event, List<int> chanceEvent, List<int> enemy, List<int> chanceEnemy, int value)
        {
            Id = id;
            CardName = cardName;
            InfoCard = infoCard;
            PartOfDeck = partOfDeck;
            NeedToTake = needToTake;
            ChangeStats = changeStats;
            Chance = chance;
            Effect = effect;
            ChanceEffect = chanceEffect;
            Event = @event;
            ChanceEvent = chanceEvent;
            Enemy = enemy;
            ChanceEnemy = chanceEnemy;
            Value = value;
        }
    }

}
