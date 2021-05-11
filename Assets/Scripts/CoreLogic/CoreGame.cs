using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Card;
using GameUI;
using Inventory;

using SceneTransition;

namespace Core
{
    //�����, ���������� �� ����������� ������� ������
    public class CoreGame : MonoBehaviour
    {
        private Camera cam;
        [SerializeField]
        private GameObject endGamePanel;

        [SerializeField]
        private CardsManager cardsManager;

        [SerializeField]
        private UIManager uiManager;

        [SerializeField]
        private InventoryManager inventoryManager;

        private int numberOfCards;
        private int indexCard = 0;

        private int indexForEnemies = 0;
        private int indexForCards = 0;


        private Card.Card currentCard;
        private Enemy currenEnemy;

        private void Start()
        {
            cam = Camera.allCameras[0];
            GameSettings.canSpawnCard = true;
            GameSettings.health = 1;
            GameSettings.mana = 1;
            GameSettings.damage = 6;
            numberOfCards = GameSettings.numberCards;
        }
        void Update()
        {
            //���� ����� ������
            if (GameSettings.swipeRight)
            {
                if(!inventoryManager.CheckCardInInventory(currentCard))
                {
                    Debug.Log("{GameLog} -> CoreGame -> Update -> ��� ������ �����, ����� ������������");
                    GameSettings.swipeRight = GameSettings.swipeLeft = false;
                    return;
                }
                //��������� ����
                if(!(Random.Range(0,100) < currentCard.Chance))
                {
                    Debug.Log("�� ���������");
                    GameSettings.swipeRight = GameSettings.swipeLeft = false;
                    return;
                }
                //������ �������� �������� � ����
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[0];
                GameSettings.mana = GameSettings.mana + currentCard.ChangeStats[1];
                //���������, ����� �������� �� ����� �� �������
                CheckOverflowBar();

                //�������� bar
                uiManager.ChangeHealthBar();
                //�������� �� ������
                if (CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
                //���� ����� ��������� ����� � ���������
                
                if(currentCard.CanSafe)
                    inventoryManager.AddCardInInventory(currentCard);
            }
            //���� ����� �����
            else if (GameSettings.swipeLeft)
            {
                //������ �������� �������� � ����
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[2];
                GameSettings.mana = GameSettings.mana + currentCard.ChangeStats[3];
                //��������� ������������
                CheckOverflowBar();

                //������ bar
                uiManager.ChangeHealthBar();

                //�������� �� ������
                if (CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
            }

            //������� ��������� �����, ���� �����
            if (GameSettings.canSpawnCard && indexCard < numberOfCards)
            {

                //FIX IT!!!
                bool chance = 50 > Random.Range(0, 100);
                Debug.Log("chanse is " + chance);
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
            if (GameSettings.mana > 1)
                GameSettings.mana = 1;
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
            //Debug.Log("{GameLog} -> CoreGame -> CheckNeedToTake(card) -> ��� ����������� ����� � ���������");
            return inventoryManager.CheckCardInInventory(card);
        }*/
    }
}

