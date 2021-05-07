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
        [SerializeField]
        private float rotationCoef;
        [SerializeField]
        private float speedComebackCam;


        //� ����� ������� ��������� �����
        private bool goLeftCard = false;
        private bool goRightCard = false;

        //���������� ������������ ��������� ����� ��� ���
        private bool dropCard = false;

        Vector3 offset;
        private void Awake()
        {
            cam = Camera.allCameras[0];
        }

        private void Update()
        {
            transform.eulerAngles = new Vector3(0, 0, transform.position.x * rotationCoef);
            //���� ����� ��������� ������ �������, ����� ������� � �� ����������� �����
            if(dropCard)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), Time.deltaTime * speedComebackCam);
            }
            //���� ����� ���� ����� ��� ������, �������� �� ���� ��������� � ������ ����� ������������
            if(goRightCard)
            {
                transform.Translate(Vector3.right * speedComebackCam * 1.2f * Time.deltaTime);
                Invoke("DestroyObject", 0.5f);
            }
            if(goLeftCard)
            {
                transform.Translate(Vector3.left * speedComebackCam * 1.2f * Time.deltaTime);
                Invoke("DestroyObject", 0.5f);
            }

            
        }

        private void DestroyObject()
        {
            Destroy(transform.gameObject);
            goLeftCard = goRightCard = false;
        }

        //����� ������������� �����
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(eventData.position);
            newPos.z = 0f;
            newPos.y = transform.position.y;
            transform.position = newPos + offset;
        }

        //����� ������� �����
        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = transform.position - cam.ScreenToWorldPoint(eventData.position);
            offset.y = transform.position.y;
            dropCard = false;
        }

        //����� �������� �����
        public void OnEndDrag(PointerEventData eventData)
        {
            dropCard = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //��������������� � Destroycardleft and right
            if(collision.transform.gameObject.tag == "DestroyCardRight")
            {
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                GameSettings.canSpawnCard = true;
                GameSettings.swipeRight = true;
                goRightCard = true;
            }
            if (collision.transform.gameObject.tag == "DestroyCardLeft")
            {
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                GameSettings.canSpawnCard = true;
                GameSettings.swipeLeft = true;
                goLeftCard = true;
            }
        }

    }
}
