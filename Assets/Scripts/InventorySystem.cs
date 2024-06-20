using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class InventorySystem : MonoBehaviour
{


    public static InventorySystem instance {  get; set; }

    public GameObject inventoryScreen;

    public bool isOpen;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> ItemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    private GameObject Number_slot;

  




    private void Awake()
    {
        if(instance !=null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
       isOpen = false;
       inventoryScreen.SetActive(false);
       PopulateSlotList();
        Cursor.visible = false;

    }

        
    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreen.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);


            }
        }

   
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreen.SetActive(true);
            isOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen || Input.GetKeyDown(KeyCode.Escape) && isOpen) {
            inventoryScreen.SetActive(false);
            isOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;

        }


    }

    public void AddToInventory(string itemname)
    {

        
            whatSlotToEquip = FindNextEmptySlot();
            itemToAdd = Instantiate(Resources.Load<GameObject>(itemname), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);
            ItemList.Add(itemname);
       
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }

        }
        return new GameObject();

    }



    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter++;
            }

        }

        if (counter == 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(string nameToRemove, int AmountToRemove)

    {
        int counter = AmountToRemove;

        for (var i = slotList.Count -1; i >= 0; i--)

        {
            if (slotList[i].transform.childCount > 0 )
            {

                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter !=0)

                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);


                    counter -= 1;


                }

            }

        }


    }

    public void ReCaculateList()

    {
        ItemList.Clear();

        foreach (GameObject slot in slotList)

        {
            if (slot.transform.childCount >0)

            {
                string name = slot.transform.GetChild(0).name; // Stone (clone)
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");

                ItemList.Add(result);
            }
        }
    }



}
