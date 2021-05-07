using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Card;
using GameUI;

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
        private CardsManager cardsManager;

        [SerializeField]
        private UIManager uiManager;

        private int numberOfCards;
        private int indexCard = 0;

        private Card.Card currentCard;

        private void Start()
        {
            cam = Camera.allCameras[0];
            GameSettings.canSpawnCard = true;
            GameSettings.health = 1;
            GameSettings.mana = 1;
            numberOfCards = GameSettings.numberCards;
        }
        void Update()
        {
            if (GameSettings.swipeRight)
            {
                if(!(Random.Range(0,100) < currentCard.Chance))
                {
                    Debug.Log("Не фартануло");
                    GameSettings.swipeRight = GameSettings.swipeLeft = false;
                    return;
                }
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[0];
                GameSettings.mana = GameSettings.mana + currentCard.ChangeStats[1];
                CheckOverflowBar();

                uiManager.ChangeHealthBar();
                if(CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
            }
            else if (GameSettings.swipeLeft)
            {
                GameSettings.health = GameSettings.health + currentCard.ChangeStats[2];
                GameSettings.mana = GameSettings.mana + currentCard.ChangeStats[3];
                CheckOverflowBar();

                uiManager.ChangeHealthBar();

                if (CheckDie())
                {
                    StartCoroutine(ShowDieDialoge());
                    GameSettings.canSpawnCard = false;
                }

                GameSettings.swipeRight = GameSettings.swipeLeft = false;
            }

            if (GameSettings.canSpawnCard && indexCard < numberOfCards)
            {
                cardsManager.SpawnCard(indexCard);
                currentCard = cardsManager.GetCard(indexCard);
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
    }
}

