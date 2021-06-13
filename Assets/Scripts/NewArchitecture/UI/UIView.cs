using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using Core;

namespace UI
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

        [SerializeField]
        private GameMaster gm;

        private bool updateBar = false;

        public void UpdateBar()
        {
            updateBar = true;
        }

        private void Update()
        { 
            CheckFillAmount();

            if (updateBar)
            {
                Health.fillAmount = Mathf.Lerp(Health.fillAmount, gm.playerStats.health / 100, Time.deltaTime * 8);
                Mana.fillAmount = Mathf.Lerp(Mana.fillAmount, gm.playerStats.energy / 100, Time.deltaTime * 8);

                HealthText.text = Convert.ToInt32(Mathf.Lerp(Health.fillAmount, gm.playerStats.health / 100, Time.deltaTime * 8) * 100) + "";
                ManaText.text = Convert.ToInt32(Mathf.Lerp(Mana.fillAmount, gm.playerStats.energy / 100, Time.deltaTime * 8) * 100) + "";
            }
        }

        private void CheckFillAmount()
        {
            if (Health.fillAmount == gm.playerStats.health / 100 && Mana.fillAmount == gm.playerStats.energy / 100)
                updateBar = false;
            else
                updateBar = true;
        }
    }
}

