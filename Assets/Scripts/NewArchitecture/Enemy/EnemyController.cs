using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;
using Load;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public GameMaster gm;
        [SerializeField]
        private EnemyView enemyView;
        
        private List<int> AllEnemies = new List<int>();
        public EnemyData CurrentEnemies = new EnemyData();

        private void Awake()
        {
        }

        private void Start()
        {
            AllEnemies = gm.deckInfo.GetAllIDEnemies();
        }

        public void SpawnEnemy(int id)
        {
            gm.gameSettings.canSpawn = false;
            Load.Enemy enemy = gm.deckInfo.FindCard("Enemy", id).Item4;
            enemyView.DrawEnemy(enemy);
            CurrentEnemies.Init(enemy.Id, enemy.CardName, enemy.PartOfDeck, enemy.Stats[0], enemy.Stats[1], enemy.Stats[2], enemy.Stats[3], enemy.Stats[4], enemy.Drop, enemy.ChanceToDrop);
        }

        public void HitEnemy()
        {
            //���������: ����� ��������� � ��� �����
            //������: ��������� ��������
            if(gm.playerStats.damage <= CurrentEnemies.Armor)
            {
                Debug.Log("������������ �����, ����� ������� �����");
                return;
            }

            if(CurrentEnemies.ChanceToEscape >= Random.Range(0, 100))
            {
                //���������� ������
                Debug.Log("Miss");
                enemyView.DrawMiss();
                return;
            }
            enemyView.DrawTakingDamage();
            CurrentEnemies.Health = CurrentEnemies.Health - gm.playerStats.damage + CurrentEnemies.Armor;
            Debug.Log("EnemyHealth: " + CurrentEnemies.Health);
            CheckDead();
                
        }

        private void CheckDead()
        {
            if(CurrentEnemies.Health <= 0)
            {
                //������� ������
                enemyView.DestroyEnemy();
                gm.gameSettings.canSpawn = true;
            }
        }

        public float getTimeBetweeHits()
        {
            return CurrentEnemies.TimeBetweenHits;
        }

        

        
    }

}
