using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using Core;

namespace GameUI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField]
        private Image Health;
        [SerializeField]
        private Image Mana;
        [SerializeField]
        private TextMeshProUGUI HealthText;
        [SerializeField]
        private TextMeshProUGUI ManaText;

        private bool updateBar = false;

        public void UpdateBar()
        {
            updateBar = true;
        }

        private void Update()
        {
            CheckFillAmount();
            if(updateBar)
            {
                Health.fillAmount = Mathf.Lerp(Health.fillAmount, GameSettings.health, Time.deltaTime * 8);
                Mana.fillAmount = Mathf.Lerp(Mana.fillAmount, GameSettings.mana, Time.deltaTime * 8);
                
                HealthText.text = Convert.ToInt32(Mathf.Lerp(Health.fillAmount, GameSettings.health, Time.deltaTime * 8) * 100) + "";
                ManaText.text = Convert.ToInt32(Mathf.Lerp(Mana.fillAmount, GameSettings.mana, Time.deltaTime * 8) * 100) + "";
            }
        }

        private void CheckFillAmount()
        {
            if (Health.fillAmount == GameSettings.health && Mana.fillAmount == GameSettings.mana)
                updateBar = false;
            else
            {
                updateBar = true;
            }
        }
    }

}
