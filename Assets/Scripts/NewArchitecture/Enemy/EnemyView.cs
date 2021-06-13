using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Load;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private Camera enemyCam;

        public GameObject prefEnemy;
        private GameObject instEnemy;
        private Animator enemyAnimator;

        [SerializeField] private ParticleSystem _particleDamage;
        private ParticleSystem instParticle;

        [SerializeField]
        private TextMeshProUGUI missText;

        public GameObject prefCardCover;
        private GameObject instCardCover;
        private Animator animCardCover;

        private void Start()
        {
            enemyCam = Camera.main;
        }

        public void DrawEnemy(Load.Enemy enemy)
        {
            StartCoroutine(DeleySpawnCard(enemy));
        }

        IEnumerator DeleySpawnCard(Load.Enemy enemy)
        {
            CreateCardCover();
            yield return new WaitForSeconds(animCardCover.runtimeAnimatorController.animationClips.Length - 0.25f);
            CreateEnemy(enemy);
            Destroy(instCardCover);
        }

        private void CreateCardCover()
        {
            instCardCover = Instantiate(prefCardCover, new Vector3(0, 0, 0), Quaternion.identity, transform);
            animCardCover = instCardCover.GetComponent<Animator>();
            instCardCover.gameObject.name = "CardCover";
            instCardCover.gameObject.transform.SetSiblingIndex(2);
            animCardCover.SetTrigger("newCard");
        }

        private void CreateEnemy(Load.Enemy enemy)
        {
            instEnemy = Instantiate(prefEnemy, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0), transform);
            //меняю ему имя на то, что соответствует карте

            enemyAnimator = instEnemy.GetComponent<Animator>();

            instEnemy.gameObject.name = enemy.CardName;
            instEnemy.gameObject.transform.SetSiblingIndex(1);

            //вписываю туда все параметры, которые пришли из card 
            instEnemy.transform.Find("Name").gameObject.transform
                    .GetChild(0).gameObject
                    .GetComponent<TextMeshProUGUI>().text = enemy.CardName;

            instEnemy.gameObject.GetComponent<Image>().sprite = enemy.Sprites[0];
            instEnemy.transform.Find("Edging").gameObject.GetComponent<Image>().sprite = enemy.Sprites[1];
            instEnemy.transform.Find("Image").gameObject.GetComponent<Image>().sprite = enemy.Sprites[2];
            instEnemy.transform.Find("Name").gameObject.GetComponent<Image>().sprite = enemy.Sprites[3];
            instEnemy.transform.Find("Armor").gameObject.GetComponent<Image>().sprite = enemy.Sprites[4];
            instEnemy.transform.Find("Damage").gameObject.GetComponent<Image>().sprite = enemy.Sprites[5];

            instEnemy.transform.Find("Armor").gameObject.transform
                .GetChild(0).gameObject
                .GetComponent<TextMeshProUGUI>().text = enemy.Stats[1] + "";

            instEnemy.transform.Find("Damage").gameObject.transform
                               .GetChild(0).gameObject
                               .GetComponent<TextMeshProUGUI>().text = enemy.Stats[0] + "";

        }

        public void DestroyEnemy()
        {
            Destroy(instEnemy);
        }

        public void HitEnemy()
        {
            
            Debug.Log("Hit");
        }

        public EnemyBehaviour GetEnemyBehaviour()
        {
            return instEnemy.GetComponent<EnemyBehaviour>();
        }

        public void DrawMiss()
        {
            bool rand = Random.Range(0, 100) > Random.Range(0, 100);
            if (rand)
                enemyAnimator.SetTrigger("missCard_1");
            else
                enemyAnimator.SetTrigger("missCard_2");

            StartCoroutine(DrawMissText());
        }

        public void DrawTakingDamage()
        {
            enemyAnimator.SetTrigger("hitCard");
            instParticle = Instantiate(_particleDamage, enemyCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)), Quaternion.identity, transform);
            instParticle.transform.SetSiblingIndex(0);
        }

        IEnumerator DrawMissText()
        {
            TextMeshProUGUI text = Instantiate(missText, enemyCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)), Quaternion.identity, transform);
            Animator textAnim = text.GetComponent<Animator>();
            textAnim.SetTrigger("miss");
            yield return new WaitForSeconds(textAnim.runtimeAnimatorController.animationClips.Length);
            Destroy(text);
        }
    }
}

