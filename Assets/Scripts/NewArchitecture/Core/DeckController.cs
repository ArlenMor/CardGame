using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy;
using spaceItem;

namespace Core
{
    public class DeckController : MonoBehaviour
    {
        [Header("Контроллеры")]
        [SerializeField]
        private EnemyController enemyController;
        [SerializeField]
        private ItemController itemController;

        public EnemyController EnemyController { get => enemyController; set => enemyController = value; }
        public ItemController ItemController { get => itemController; set => itemController = value; }
    }

}
