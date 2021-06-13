using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        public int Id;
        public string CardName;
        public int PartOfDeck;
        public float Damage;
        public float Armor;
        public float Health;
        public float ChanceToEscape;
        public float TimeBetweenHits;

        public List<int> Drop = new List<int>();
        public List<int> ChanceToDrop = new List<int>();

        public void Init(int id, string cardName, int partOfDeck, float damage, float armor, float health, float chanceToEscape, float timeBetweenHits, List<int> drop, List<int> chanceToDrop)
        {
            Id = id;
            CardName = cardName;
            PartOfDeck = partOfDeck;
            Damage = damage;
            Armor = armor;
            Health = health;
            ChanceToEscape = chanceToEscape;
            TimeBetweenHits = timeBetweenHits;
            Drop = drop;
            ChanceToDrop = chanceToDrop;
        }
    }

}
