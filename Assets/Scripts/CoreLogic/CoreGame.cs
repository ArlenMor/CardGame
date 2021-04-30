using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Card;

namespace Core
{
    //�����, ���������� �� ����������� ������� ������
    public class CoreGame : MonoBehaviour
    {

        [SerializeField]
        private CardsManager cardsManager;

        private void Start()
        {
            GameSettings.canSpawnCard = true;
        }
        void Update()
        {
            if (GameSettings.canSpawnCard)
                cardsManager.SpawnCard();
        }
    }
}

