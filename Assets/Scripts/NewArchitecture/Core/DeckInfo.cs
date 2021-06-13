using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Load;

namespace Core
{
    public class DeckInfo
    {
        private List<int> AllId = new List<int>();
        private List<int> AllPlayerdId = new List<int>();
        private List<Load.Enemy> Enemies = new List<Load.Enemy>();
        private List<Item> Items = new List<Item>();
        private List<Load.Event> Events = new List<Load.Event>();
        private List<Equipment> Equipments = new List<Equipment>();
        private List<Effect> Effects = new List<Effect>();

        public void Init(List<Item> items, List<Load.Event> events, List<Equipment> equipments, List<Load.Enemy> enemies, List<Effect> effects)
        {
            Enemies = enemies;      Items = items;
            Events = events;        Equipments = equipments;
            Effects = effects;

            foreach (var item in Enemies)
                AllId.Add(item.Id);
            foreach (var item in Items)
                AllId.Add(item.Id);
            foreach (var item in Events)
                AllId.Add(item.Id);
            foreach (var item in Equipments)
                AllId.Add(item.Id);
            foreach (var item in Effects)
                AllId.Add(item.Id);
        }

        public Tuple<Item, Load.Event, Equipment, Load.Enemy, Effect> FindCard(string TypeCard, int Id)
        {
            Item ItemCard = null;
            Load.Event EventCard = null;
            Equipment EquipmentCard = null;
            Load.Enemy EnemyCard = null;
            Effect EffectCard = null;

            if(TypeCard == "Item")
                ItemCard = FindItem(Id);
            if (TypeCard == "Event")
                EventCard = FindEvent(Id);
            if (TypeCard == "Equipment")
                EquipmentCard = FindEquipment(Id);
            if (TypeCard == "Enemy")
                EnemyCard = FindEnemy(Id);
            if (TypeCard == "Effect")
                EffectCard = FindEffect(Id);

            return Tuple.Create(ItemCard, EventCard, EquipmentCard, EnemyCard, EffectCard);
        }

        private Item FindItem(int Id)
        {
            foreach (var item in Items)
                if (item.Id == Id)
                    return item;
            return null;
        }

        private Load.Event FindEvent(int Id)
        {
            foreach (var item in Events)
                if (item.Id == Id)
                    return item;
            return null;
        }

        private Equipment FindEquipment(int Id)
        {
            foreach (var item in Equipments)
                if (item.Id == Id)
                    return item;
            return null;
        }

        private Load.Enemy FindEnemy(int Id)
        {
            foreach (var item in Enemies)
                if (item.Id == Id)
                    return item;
            return null;
        }

        private Effect FindEffect(int Id)
        {
            foreach (var item in Effects)
                if (item.Id == Id)
                    return item;
            return null;
        }

        public List<int> GetAllIDEnemies()
        {
            List<int> Id = new List<int>();
            foreach (var item in Enemies)
                Id.Add(item.Id);
            return Id;
        }
        public List<Item> GetAllItems()
        {
            return Items;
        }
        public List<Load.Event> GetAllEvents()
        {
            return Events;
        }
        public List<Equipment> GetAllEquipment()
        {
            return Equipments;
        }
    }

}