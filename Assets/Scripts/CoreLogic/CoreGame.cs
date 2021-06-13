using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Card;
using GameUI;
using Inventory;

using SceneTransition;

namespace Core
{
    //класс, отвечающий за большинство игровой логики
    public class CoreGame : MonoBehaviour
    {
        private Camera cam;
        [SerializeField]
        private GameObject endGamePanel;

        [SerializeField]
        private TextMeshProUGUI textInfo;

        [SerializeField]
        private CardsManager cardsManager;

        private int numberOfCards;
        private int indexCard = 0;

        private int indexForEnemies = 0;
        private int indexForCards = 0;


        private Card.Card currentCard;
        private Card.Enemy currenEnemy;

        private void Start()
        {
            cam = Camera.allCameras[0];
            GameSettings.canSpawnCard = true;
            GameSettings.health = 1;
            GameSettings.energy = 1;
            GameSettings.damage = 3;
            numberOfCards = GameSettings.numberCards;
        }
        void Update()
        {
            textInfo.text = GameSettings.info;
            //если свайп вправо
            if (GameSettings.swipeRight)
            {
                
                //Проверяем шанс
                if(!(Random.Range(0,100) < currentCard.Chance))
                {
                    GameSettings.info += "\nНе повезло. Эффект карты не получен";
                    Debug.Log("Не фартануло");
                    GameSettings.swipeRight = GameSettings.swipeLeft = false;
                    return;
                }
                //меняем значения здоровья и маны
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[0];
                GameSettings.energy = GameSettings.energy + currentCard.ChangeStats[1];
                //Проверяем, чтобы значение не вышли за пределы
                CheckOverflowBar();

                //Проверка на смерть
                if (CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
                //Если нужно отправить карту в инвентарь
                
               
            }
            //если свайп влево
            else if (GameSettings.swipeLeft)
            {
                //Меняем значение здоровья и маны
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[2];
                GameSettings.energy = GameSettings.energy + currentCard.ChangeStats[3];
                //Проверяем переполнение
                CheckOverflowBar();

                //Проверка на смерть
                if (CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
            }

            //Достать следующую карту, если можно
            if (GameSettings.canSpawnCard && indexCard < numberOfCards)
            {

                //FIX IT!!!
                bool chance = 50 > Random.Range(0, 100);
                if (chance)
                {
                    if(indexForCards < cardsManager.GetCardsCount())
                    {
                        cardsManager.SpawnCard(indexForCards);
                        currentCard = cardsManager.GetCard(indexForCards);
                        indexForCards++;
                    }else if(indexForEnemies < cardsManager.GetEnemiesCount())
                    {
                        //fix it
                        cardsManager.SpawnEnemy(indexForEnemies);
                        currenEnemy = cardsManager.GetEnemy(indexForEnemies);
                        GameSettings.currentEnemiesDamage = currenEnemy.EnemyStats[0];
                        GameSettings.CurrentEnemiesArmor = currenEnemy.EnemyStats[1];
                        GameSettings.CurrentEnemiesHealth = currenEnemy.EnemyStats[2];
                        GameSettings.CurrentEnemiesChanceToEscape = currenEnemy.EnemyStats[3];
                        GameSettings.CurrentEnemiesTimeBetweenHits = currenEnemy.EnemyStats[4];
                        indexForEnemies++;
                    }
                    else
                    {
                        Debug.Log("Deck is empty");
                    }
                    
                }
                else if (indexForEnemies < cardsManager.GetEnemiesCount())
                {
                    cardsManager.SpawnEnemy(indexForEnemies);
                    currenEnemy = cardsManager.GetEnemy(indexForEnemies);
                    GameSettings.currentEnemiesDamage = currenEnemy.EnemyStats[0];
                    GameSettings.CurrentEnemiesArmor = currenEnemy.EnemyStats[1];
                    GameSettings.CurrentEnemiesHealth = currenEnemy.EnemyStats[2];
                    GameSettings.CurrentEnemiesChanceToEscape = currenEnemy.EnemyStats[3];
                    GameSettings.CurrentEnemiesTimeBetweenHits = currenEnemy.EnemyStats[4];
                    indexForEnemies++;
                }else if(indexForCards < cardsManager.GetCardsCount())
                {
                    cardsManager.SpawnCard(indexForCards);
                    currentCard = cardsManager.GetCard(indexForCards);
                    indexForCards++;
                }
                else
                {
                    Debug.Log("Deck is empty");
                }
                indexCard++;
            }
        }

        private void CheckOverflowBar()
        {
            if (GameSettings.health > 1)
                GameSettings.health = 1;
            if (GameSettings.energy > 1)
                GameSettings.energy = 1;
        }

        IEnumerator ShowDieDialoge()
        {
            GameObject obj = Instantiate(endGamePanel, Vector3.zero, Quaternion.identity, transform);
            yield return new WaitForSeconds(3.5f);
            SceneTransition.SceneTransition.SwitchToScene("MainMenu");
            Destroy(obj);
        }

        private bool CheckDie()
        {
            return GameSettings.health <= 0;
        }

/*        private bool CheckNeedToTake(Card.Card card)
        {
            //Debug.Log("{GameLog} -> CoreGame -> CheckNeedToTake(card) -> Нет необходимой карты в инвентаре");
            return inventoryManager.CheckCardInInventory(card);
        }*/
    }
}

