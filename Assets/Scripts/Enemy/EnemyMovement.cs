using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Card;
using Core;

public class EnemyMovement : CardMovement
{
    private float health;
    private float damage;
    private float armor;
    private float chanceToEscape;
    private float timeBetweenHits;


    private float proportionalFactor;
    private float timeStart;
    private Image timeImage;

    private void Start()
    {
        health = GameSettings.CurrentEnemiesHealth;
        damage = GameSettings.currentEnemiesDamage;
        armor = GameSettings.CurrentEnemiesArmor;
        chanceToEscape = GameSettings.CurrentEnemiesChanceToEscape;
        timeBetweenHits = GameSettings.CurrentEnemiesTimeBetweenHits;

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
            GameSettings.health -= damage/100;
            timeBetweenHits = GameSettings.CurrentEnemiesTimeBetweenHits;
        }
    }

    public void Hit()
    {
        if(armor > GameSettings.damage)
        {
            GameSettings.mana -= 0.01f;
            return;
        }
        health -= GameSettings.damage;
        GameSettings.mana -= 0.01f;

        CheckDeath();
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
