using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Load
{
    [System.Serializable]
    public struct EquipmentsDeserialization
    {
        public int Id;
        public string EquipmentType;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<float> EquipmentStats;      //урон/брон€/блок, ѕрочность, поглощение урона, шансблока
        public int value;

        public List<string> Sprites; //BgCard, Edging, Image, BgName, Damage/aromr, strength/chanceBlocking;
    }

    public struct EquipmentsJson
    {
        public List<EquipmentsDeserialization> AllEquipments;
    }

    public class Equipment
    {
        public int Id;
        public string EquipmentType;
        public string CardName;
        public string InfoCard;
        public int PartOfDeck;
        public List<float> EquipmentStats;      
        public int value;

        public List<Sprite> Sprites; //BgCard, Edging, Image, BgName, Damage/aromr, strength/chanceBlocking;

        public Equipment(    int _Id,
                        string _EquipmentType,
                        string _CardName,
                        string _InfoCard,
                        int _PartOfDeck,
                        List<float> _EquipmentStats,
                        int _value,
                        List<Sprite> _Sprites)
        {
            Id = _Id;
            EquipmentType = _EquipmentType;
            CardName = _CardName;
            InfoCard = _InfoCard;
            PartOfDeck = _PartOfDeck;
            value = _value;

            EquipmentStats = new List<float>();
            Sprites = new List<Sprite>();
            foreach (var item in _EquipmentStats)
                EquipmentStats.Add(item);
            foreach (var item in _Sprites)
                Sprites.Add(item);

        }
    }

    public static class LoadEquipments
    {
        public static List<Equipment> LoadEquipmentsFromJson(string jsonName, Sprite[] allSprites)
        {
            List<Equipment> returnEquipments = new List<Equipment>();

            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;

            if (file.name != "Equipments")
            {
                Debug.Log("{LoadLog} => [LoadEquipments] => LoadEquipmentsFromJson() => File not Found");
                return null;
            }

            string json = file.text;

            EquipmentsJson equipmentsJson = JsonUtility.FromJson<EquipmentsJson>(json);



            foreach (var equip in equipmentsJson.AllEquipments)
            {
                List<Sprite> equipmentsSprites = new List<Sprite>();

                Sprite BgCard = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;
                Sprite FirstParam = null;   //”рон, брон€, блокирование
                Sprite SecondParam = null;  //прочность, поглащение урона, шанс блока

                foreach (Sprite sprite in allSprites)
                {
                    if (equip.Sprites[0] == sprite.name)
                        BgCard = sprite;
                    if (equip.Sprites[1] == sprite.name)
                        Edging = sprite;
                    if (equip.Sprites[2] == sprite.name)
                        Image = sprite;
                    if (equip.Sprites[3] == sprite.name)
                        BgName = sprite;
                    if (equip.Sprites[4] == sprite.name)
                        FirstParam = sprite;
                    if (equip.Sprites[5] == sprite.name)
                        SecondParam = sprite;
                }

                equipmentsSprites.Add(BgCard); equipmentsSprites.Add(Edging); equipmentsSprites.Add(Image); 
                equipmentsSprites.Add(BgName); equipmentsSprites.Add(FirstParam); equipmentsSprites.Add(SecondParam);


                returnEquipments.Add(new Equipment(equip.Id, equip.EquipmentType, equip.CardName, equip.InfoCard, equip.PartOfDeck, equip.EquipmentStats, equip.value, equipmentsSprites));
            }

            return returnEquipments;
        }
    }
}