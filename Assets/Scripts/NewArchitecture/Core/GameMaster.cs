using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Load;

namespace Core
{
    public class GameMaster : MonoBehaviour
    {
        public PlayerStats playerStats = new PlayerStats();
        public DeckInfo deckInfo = new DeckInfo();
        public NewGameSettings gameSettings = new NewGameSettings();


        private void Start()
        {
            
        }


        public void LoadAllCardInDeckInfo(List<Item> items, List<Load.Event> events, List<Equipment> equipments, List<Load.Enemy> enemies, List<Effect> effects)
        {
            deckInfo.Init(items, events, equipments, enemies, effects);
        }        
    }
}

