using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Load
{
    [System.Serializable]
    public struct EffectsDeserialization
    {
        public int Id;
        public string EffectName;
        public string EffectInfo;
        public List<float> ChangeStats;     //хп, эн, урон, броня
        public List<bool> Conditions;       //Каждый удар, каждую карту
        public int EffectDuration;
        public List<int> SpawnEnemies;

        //public List<string> Sprites; //BgCard, Edging, Image, BgName, Damage/aromr, strength/chanceBlocking;
    }

    public struct EffectsJson
    {
        public List<EffectsDeserialization> AllEffects;
    }

    public class Effect
    {
        public int Id;
        public string EffectName;
        public string EffectInfo;
        public List<float> ChangeStats;     //хп, эн, урон, броня
        public List<bool> Conditions;       //Каждый удар, каждую карту
        public int EffectDuration;
        public List<int> SpawnEnemies;

        public Effect(int _Id,
                        string _EffectName,
                        string _EffectInfo,
                        List<float> _ChangeStats,
                        List<bool> _Conditions,
                        int _EffectDuration,
                        List<int> _SpawnEnemies)
        {
            Id = _Id;
            EffectName = _EffectName;
            EffectInfo = _EffectInfo;
            ChangeStats = new List<float>();
            Conditions = new List<bool>();
            SpawnEnemies = new List<int>();
            EffectDuration = _EffectDuration;

            foreach (var item in _ChangeStats)
                ChangeStats.Add(item);
            foreach (var item in _Conditions)
                Conditions.Add(item);
            foreach (var item in _SpawnEnemies)
                SpawnEnemies.Add(item);
        }
    }

    public static class LoadEffects
    {
        public static List<Effect> LoadEffectsFromJson(string jsonName)
        {
            List<Effect> returnEffects = new List<Effect>();

            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;

            if (file.name != "Effects")
            {
                Debug.Log("{LoadLog} => [LoadEffects] => LoadEffectsFromJson() => File not Found");
                return null;
            }

            string json = file.text;

            EffectsJson effectsJson = JsonUtility.FromJson<EffectsJson>(json);



            foreach (var effect in effectsJson.AllEffects)
            {
                returnEffects.Add(new Effect(effect.Id, effect.EffectName, effect.EffectInfo, effect.ChangeStats, effect.Conditions, effect.EffectDuration, effect.SpawnEnemies));
            }

            return returnEffects;
        }
    }
}