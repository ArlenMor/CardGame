using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Inventory
{
    public class Item
    {
        public Sprite Bg;
        public Sprite Edging;
        public Sprite BgName;
        public Sprite Image;
        public string CardName;
        public string CardInfo;

        public Item(Sprite _Bg, Sprite _Edging, Sprite _BgName, Sprite _Image, string _cardName, string _CardInfo)
        {
            Bg = _Bg;
            Edging = _Edging;
            BgName = _BgName;
            this.Image = _Image;
            CardName = _cardName;
            CardInfo = _CardInfo;
        }
    }

}
