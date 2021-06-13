using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class GameSettings
    {
        //���� �� ����� �����
        public static bool canSpawnCard;

        //���������� ���� � ������
        public static int numberCards;

        //����� ��� ������ ����� ����� ��� ������
        public static bool swipeLeft = false;
        public static bool swipeRight = false;

        //���������� �������������� ���������
        public static float health;
        public static float energy;
        public static float damage;

        //���������� ���������� ��� ������
        public static float currentEnemiesDamage;
        public static float CurrentEnemiesArmor;
        public static float CurrentEnemiesHealth;
        public static float CurrentEnemiesChanceToEscape;
        public static float CurrentEnemiesTimeBetweenHits;

        //�������� ��� ����������� ����
        public static string info = "";
    }

}
