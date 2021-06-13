using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BarShake : MonoBehaviour
    {
        private Vector3 startPos;

        private float shakeAmount = 0;
        private void Start()
        {
            startPos = transform.position;
        }

        public void Shake(float amt, float lenght)
        {
            shakeAmount = amt;
            InvokeRepeating("BeginShake", 0, 0.01f);
            Invoke("StopShake", lenght);
        }

        private void BeginShake()
        {
            if (shakeAmount > 0)
            {
                Vector3 barPos = transform.position;

                float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
                //float offsety = Random.value * shakeAmount * 2 - shakeAmount;

                barPos.x += offsetX;
                //barPos.y += offsety;

                transform.position = barPos;
            }
        }

        private void StopShake()
        {
            CancelInvoke("BeginShake");
            transform.position = startPos;
        }
    }

}
