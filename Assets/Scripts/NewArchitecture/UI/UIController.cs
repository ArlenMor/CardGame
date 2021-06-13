using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private UIView View;

        public void ChangeHealthBar()
        {
            View.UpdateBar();
        }
    }

}
