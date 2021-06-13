using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Load
{
    [System.Serializable]
    public struct EnemiesDeserialization
    {
        public int Id;
        public string CardName;
        public int PartOfDeck;
        public List<float> Stats; //Damage, Armor, Hp, escape, time
        public List<int> Drop;
        public List<int> ChanceToDrop;

        public List<string> Sprites; //BgCard, Edging, Image, BgName, ArmorImage, DamageImage;
    }

    public struct EnemiesJson
    {
        public List<EnemiesDeserialization> AllEnemies;
    }

    public class Enemy
    {
        public int Id;
        public string CardName;
        public int PartOfDeck;
        public List<float> Stats;
        public List<int> Drop;
        public List<int> ChanceToDrop;

        public List<Sprite> Sprites;

        public Enemy(   int _Id,
                        string _CardName,
                        int _PartOfDeck,
                        List<float> _Stats,
                        List<int> _Drop,
                        List<int> _ChanceToDrop,
                        List<Sprite> _Sprites)
        {
            Id = _Id;
            CardName = _CardName;
            PartOfDeck = _PartOfDeck;
            Stats = new List<float>();
            Drop = new List<int>();
            ChanceToDrop = new List<int>();
            Sprites = new List<Sprite>();

            foreach (var item in _Stats)
                Stats.Add(item);
            foreach (var item in _Drop)
                Drop.Add(item);
            foreach (var item in _ChanceToDrop)
                ChanceToDrop.Add(item);
            foreach (var item in _Sprites)
                Sprites.Add(item);
        }
    }

    public static class LoadEnemies
    {
        public static List<Enemy> LoadEnemiesFromJson(string jsonName, Sprite[] allSprites)
        {
            List<Enemy> returnEnemies = new List<Enemy>();

            TextAsset file = Resources.Load("Json/" + jsonName) as TextAsset;

            if(file.name != "Enemies")
            {
                Debug.Log("{LoadLog} => [LoadEnemies] => LoadEnemiesFromJson() => File not Found");
                return null;
            }

            string json = file.text;

            EnemiesJson enemiesJson = JsonUtility.FromJson<EnemiesJson>(json);

            

            foreach(var enemy in enemiesJson.AllEnemies)
            {
                List<Sprite> enemySprite = new List<Sprite>();

                Sprite BgCard = null;
                Sprite Edging = null;
                Sprite Image = null;
                Sprite BgName = null;
                Sprite ArmorImage = null;
                Sprite DamageImage = null;
                
                foreach(Sprite sprite in allSprites)
                {
                    if (enemy.Sprites[0] == sprite.name)
                        BgCard = sprite;
                    if (enemy.Sprites[1] == sprite.name)
                        Edging = sprite;
                    if (enemy.Sprites[2] == sprite.name)
                        Image = sprite;
                    if (enemy.Sprites[3] == sprite.name)
                        BgName = sprite;
                    if (enemy.Sprites[4] == sprite.name)
                        ArmorImage = sprite;
                    if (enemy.Sprites[5] == sprite.name)
                        DamageImage = sprite;
                }

                enemySprite.Add(BgCard); enemySprite.Add(Edging); enemySprite.Add(Image);
                enemySprite.Add(BgName); enemySprite.Add(ArmorImage); enemySprite.Add(DamageImage);


                returnEnemies.Add(new Enemy(enemy.Id, enemy.CardName, enemy.PartOfDeck, enemy.Stats, enemy.Drop, enemy.ChanceToDrop, enemySprite));
            }

            return returnEnemies;
        }
    }
}


