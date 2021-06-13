using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

using Card;
using Core;
using GameUI;

public class EnemyMovement : CardMovement
{
    private float health;
    private float damage;
    private float armor;
    private float chanceToEscape;
    private float timeBetweenHits;

    private Animator anim;

    private Camera enemyCam;
    [SerializeField] private ParticleSystem _particleDamage;
    private ParticleSystem instParticle;

    [SerializeField]
    private TextMeshProUGUI missText;

    public BarShake shake;


    private float proportionalFactor;
    private float timeStart;
    private Image timeImage;

    private void Start()
    {

        shake = GameObject.Find("UIIcons").gameObject.GetComponent<BarShake>();

        anim = GetComponent<Animator>();
        health = GameSettings.CurrentEnemiesHealth;
        damage = GameSettings.currentEnemiesDamage;
        armor = GameSettings.CurrentEnemiesArmor;
        chanceToEscape = GameSettings.CurrentEnemiesChanceToEscape;
        timeBetweenHits = GameSettings.CurrentEnemiesTimeBetweenHits;

        enemyCam = Camera.main;
        timeImage = transform.Find("Time").gameObject.GetComponent<Image>();
        proportionalFactor = timeImage.fillAmount / timeBetweenHits;
    }
    private void Update()
    {
        transform.position = new Vector3(0, 0, 0);

        timeBetweenHits -= Time.deltaTime;
        timeImage.fillAmount = timeBetweenHits * proportionalFactor;
        if (timeBetweenHits <=  0)
        {
            shake.Shake(0.1f, 0.2f);
            GameSettings.health -= damage/100;
            timeBetweenHits = GameSettings.CurrentEnemiesTimeBetweenHits;
        }
    }


    public void Hit()
    {
        if(armor > GameSettings.damage)
        {
            GameSettings.energy -= 0.01f;
            
            return;
        }
        if(chanceToEscape < Random.Range(0, 100))
        {
            anim.SetTrigger("hitCard");
            instParticle = Instantiate(_particleDamage, enemyCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)), Quaternion.identity, transform);
            instParticle.transform.SetSiblingIndex(0);
            health -= GameSettings.damage;
            GameSettings.energy -= 0.002f;
        }
        else
        {
            bool rand = Random.Range(0, 100) > Random.Range(0, 100);
            if (rand)
                anim.SetTrigger("missCard_1");
            else
                anim.SetTrigger("missCard_2");
            StartCoroutine(CreateMissText());
        }

        CheckDeath();
    }

    IEnumerator CreateMissText()
    {
        TextMeshProUGUI text = Instantiate(missText, enemyCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5)), Quaternion.identity, transform);
        Animator textAnim = text.GetComponent<Animator>();
        textAnim.SetTrigger("miss");
        yield return new WaitForSeconds(textAnim.runtimeAnimatorController.animationClips.Length);
        Destroy(text);
    }

    

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(transform.gameObject);
            GameSettings.canSpawnCard = true;
        }
    }
}
