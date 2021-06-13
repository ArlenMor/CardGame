using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class GameSettings
    {
        //флаг на спавн карты
        public static bool canSpawnCard;

        //количество карт в колоде
        public static int numberCards;

        //флаги для свайпа карты влево или вправо
        public static bool swipeLeft = false;
        public static bool swipeRight = false;

        //глобальные характеристики персонажа
        public static float health;
        public static float energy;
        public static float damage;

        //Глобальные переменные для врагов
        public static float currentEnemiesDamage;
        public static float CurrentEnemiesArmor;
        public static float CurrentEnemiesHealth;
        public static float CurrentEnemiesChanceToEscape;
        public static float CurrentEnemiesTimeBetweenHits;

        //Временно для отображения инфы
        public static string info = "";
    }

}
