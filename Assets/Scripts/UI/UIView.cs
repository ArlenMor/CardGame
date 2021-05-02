using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Core;

namespace GameUI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private Image Health;
        [SerializeField]
        private Image Mana;

        public void UpdateBar()
        {
            Health.fillAmount = GameSettings.health;
            Mana.fillAmount = GameSettings.mana;
        }
    }

}
