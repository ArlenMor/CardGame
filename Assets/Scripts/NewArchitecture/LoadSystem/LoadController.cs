using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Core;

namespace Load
{
    public class LoadController : MonoBehaviour
    {
        [Header("Всё для загрузки")]
        [SerializeField]
        private string jsonFileEnemiesName;
        [SerializeField]
        private string jsonFileItemsName;
        [SerializeField]
        private string jsonFileEventsName;
        [SerializeField]
        private string jsonFileEquipmentsName;
        [SerializeField]
        private string jsonFileEffectsName;


        [SerializeField]
        private GameMaster gm;

        //Выгруженные карты
        private List<Enemy> AllEnemies;
        private List<Item> AllItems;
        private List<Load.Event> AllEvents;
        private List<Equipment> AllEquipments;
        private List<Effect> AllEffects;

        [SerializeField]
        private Texture2D spriteSheet;
        private Sprite[] sprites;

        private void Start()
        {
            loadSprites(spriteSheet);
            AllEnemies = LoadEnemies.LoadEnemiesFromJson(jsonFileEnemiesName, sprites);
            AllItems = LoadItems.LoadItemsFromJson(jsonFileItemsName, sprites);
            AllEvents = LoadEvents.LoadEventsFromJson(jsonFileEventsName, sprites);
            AllEquipments = LoadEquipments.LoadEquipmentsFromJson(jsonFileEquipmentsName, sprites);
            AllEffects = LoadEffects.LoadEffectsFromJson(jsonFileEffectsName);
            gm.LoadAllCardInDeckInfo(AllItems, AllEvents, AllEquipments, AllEnemies, AllEffects);
            
        }

        private void Print()
        {
            foreach (var item in AllEffects)
            {
                Debug.Log("Id: " + item.Id);
                Debug.Log("EffectName: " + item.EffectName);
                Debug.Log("EffectDuration: " + item.EffectDuration);
            }
        }

        private void loadSprites(Texture2D SpriteSheetCards)
        {
            sprites = Resources.LoadAll<Sprite>("CardSprites/" + SpriteSheetCards.name);
        }
    }

}
