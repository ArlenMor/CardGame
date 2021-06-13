using UnityEngine;
using UnityEngine.EventSystems;

using Core;
using Load;

namespace spaceItem
{
    public class ItemBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Item item;
        private Camera cam;

        private Vector3 offset;

        private bool dropCard = true;
        private bool goRightCard = false;
        private bool goLeftCard = false;

        [Header("GameMaster")]
        [SerializeField]
        private GameMaster gm;

        [Header("CarsParametrs")]
        [SerializeField]
        private float rotationCoef;
        [SerializeField]
        private float speedComebackCam;


        void Awake()
        {
            cam = Camera.allCameras[0];
            gm = transform.GetComponentInParent<GameMaster>();
        }
        void Update()
        {
            //Добавляем немного наклона
            transform.eulerAngles = new Vector3(0, 0, transform.position.x * rotationCoef);
            //если карту отпустили раньше времени, нужно вернуть её на изначальное место
            if (dropCard)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0,0, transform.position.z), Time.deltaTime * speedComebackCam);
            }
            //Если карта ушла влево или вправо, позволим ей уйти полностью с экрана перед уничтожением
            if (goRightCard)
            {
                transform.Translate(Vector3.right * speedComebackCam * 1.2f * Time.deltaTime);
                Invoke("DestroyObject", 0.5f);
            }
            if (goLeftCard)
            {
                transform.Translate(Vector3.left * speedComebackCam * 1.2f * Time.deltaTime);
                Invoke("DestroyObject", 0.5f);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            offset = transform.position - cam.ScreenToWorldPoint(eventData.position);
            offset.y = transform.position.y;
            dropCard = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newPos = cam.ScreenToWorldPoint(eventData.position);
            newPos.y = transform.position.y;
            transform.position = newPos + offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            dropCard = true;
        }

        private void DestroyObject()
        {
            Destroy(transform.gameObject);
            goLeftCard = goRightCard = false;
        }

        //нужно для уничтожения карты


        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Соприкосновение с Destroycardleft and right
            if (collision.transform.gameObject.tag == "DestroyCardRight")
            {
                if(gm.gameSettings.canAddInInventory == false)
                {
                }
                else
                {
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    gm.gameSettings.swipeRightItem = true;
                    gm.gameSettings.currentItem = gm.deckInfo.FindCard("Item", item.Id).Item1;
                    gm.gameSettings.canSpawn = true;
                    goRightCard = true;
                }
                
            }
            if (collision.transform.gameObject.tag == "DestroyCardLeft")
            {
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gm.gameSettings.swipeRightItem = false;
                gm.gameSettings.canSpawn = true;
                goLeftCard = true;
            }
        }
    }
}