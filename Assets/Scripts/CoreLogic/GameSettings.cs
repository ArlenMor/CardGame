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
        public static float mana;
    }

}
