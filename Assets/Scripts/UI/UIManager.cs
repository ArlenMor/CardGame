using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private UIView View;

        public void ChangeHealthBar()
        {
            View.UpdateBar();
        }
    }
}