using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Core;


namespace Card
{
    public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Camera cam;


        Vector3 offset;
        private void Awake()
        {
            cam = Camera.allCameras[0];
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(eventData.position);
            newPos.z = 0f;
            newPos.y = transform.position.y;
            transform.position = newPos + offset;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = transform.position - cam.ScreenToWorldPoint(eventData.position);
            offset.y = transform.position.y;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.delta.x > 0)
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3, ForceMode2D.Impulse);
            if (eventData.delta.x < 0)
                transform.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 3, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.gameObject.tag == "DestroyCard")
            {
                Destroy(transform.gameObject);
                GameSettings.canSpawnCard = true;
            }
        }

        
    }
}
