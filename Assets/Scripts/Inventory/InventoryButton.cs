using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject panel;

        private Vector3 startPosPanel;

        private bool openPanel = false;


        private void Start()
        {
            startPosPanel = panel.transform.position;
        }

        private void Update()
        {
            if (openPanel)
                panel.transform.position = Vector3.MoveTowards(panel.transform.position, new Vector3(0, panel.transform.position.y, 0), Time.deltaTime * 160f);
            else
                panel.transform.position = Vector3.MoveTowards(panel.transform.position, startPosPanel, Time.deltaTime * 160f);

        }
        public void OpenInventory()
        {
            openPanel = !openPanel;
            //panel.SetActive(true);
        }
    }

}
