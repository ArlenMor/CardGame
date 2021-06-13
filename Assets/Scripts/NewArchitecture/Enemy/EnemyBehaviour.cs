using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UI;

namespace Enemy
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private EnemyController enemyController;


        //всё для атаки
        private float timeBetweenHits;
        private float proportionalFactor;
        [SerializeField]
        private Image timeImage;
        private BarShake shake;
        

        private void Start()
        {
            shake = GameObject.Find("UIIcons").GetComponent<BarShake>();
            enemyController = transform.GetComponentInParent<EnemyController>();
            timeBetweenHits = enemyController.CurrentEnemies.TimeBetweenHits;

            //
            proportionalFactor = timeImage.fillAmount / timeBetweenHits;
        }

        private void Update()
        {
            //тряска камеры и урон
            timeBetweenHits -= Time.deltaTime;
            timeImage.fillAmount = timeBetweenHits * proportionalFactor;
            if (timeBetweenHits <= 0)
            {
                shake.Shake(0.1f, 0.2f);
                enemyController.gm.playerStats.health -= enemyController.CurrentEnemies.Damage;
                timeBetweenHits = enemyController.CurrentEnemies.TimeBetweenHits;
            }
        }

        public void HitEnemy()
        {
            enemyController.HitEnemy();
        }
    }

}
