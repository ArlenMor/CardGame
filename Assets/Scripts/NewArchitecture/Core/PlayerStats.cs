using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Load;

namespace Core
{
    public class PlayerStats
    {
        public float health;
        public float energy;
        public float damage;
        public float armor;

        public List<Effect> effects = new List<Effect>();

        public int CurrentWeapon;
        public int CurrentArmor;
        public int CurrentShield;
        public List<int> CurrentItems = new List<int>();

        public PlayerStats()
        {
            health = 50;
            energy = 100;
            armor = 0;
            damage = 3;
        }

        public void ChangePlayerStats(float hp, float en)
        {
            if(health + hp <= 100)
                health += hp;
            else
                health = 100;

            if (energy + en <= 100)
                energy += en;
            else
                energy = 100;

            if (health < 0)
                health = 0;
            if (energy < 0)
                energy = 0;
        }
    }

}

