using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipSystem : MonoBehaviour
{

    public static EquipSystem instance {  get; set; }
    public GameObject QuickSlotScreen;


    public List<GameObject> quick_slot = new List<GameObject>();

    public GameObject SlotHolder;

    public int selectedNumber = -1;


    /// <summary>
    
    public GameObject selectedItem;

    public GameObject ToolHolder;

    public GameObject selectedItemModel;


    


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        PopulateSlotList();


    }

    private void PopulateSlotList()
    {
        foreach(Transform child in  QuickSlotScreen.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quick_slot.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlot(GameObject EquipItem)
    {
        GameObject EmptySlot = FindNextEmptySlot();
        EquipItem.transform.SetParent(EmptySlot.transform, false);
        InventorySystem.instance.ReCaculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quick_slot )
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }

        }
        return new GameObject();
    }

    
    public bool CheckFull()
    {
        int counter = 0;

        foreach (GameObject slot in quick_slot)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
        }
        if (counter == 5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkIfSlotIsFull(int slotnumber)
    {
        if (quick_slot[slotnumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    GameObject getSelectedItem(int slotnumber)
    {
        return quick_slot[slotnumber - 1].transform.GetChild(0).gameObject;
    }



    private void SelectQuickSlot(int number)
    {
        if (checkIfSlotIsFull(number) == true)
        {

            if (selectedNumber != number)
            {
                selectedNumber = number;

                // unselect previous item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<Inventory_item>().isSelected = false;
                }

                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<Inventory_item>().isSelected = true;

                SetEquippedModel(selectedItem);

                // changing the color
                foreach (Transform child in SlotHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;        
                }

                Text toBeChanged = SlotHolder.transform.Find("Circle" + number).transform.Find("Text").GetComponent<Text>();
                toBeChanged.color = Color.red;

            }


            else // we are trying to select the same slot
            {
                selectedNumber = -1; // null


                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<Inventory_item>().isSelected = false;
                    selectedItem = null;
                }

                if (selectedItemModel !=null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;


                }

                // changing the color
                foreach (Transform child in SlotHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;
                }


            }
        }

    }


    private void SetEquippedModel(GameObject selectedItem)
    {

        if (selectedItemModel != null)
        {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;

        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>("Viking_" + selectedItemName), new Vector3(-0.024f, 0.013f, 0.268f), Quaternion.Euler(90f, 0, -90f));
        selectedItemModel.transform.SetParent(ToolHolder.transform, false);

    }
    //// 








    // update 
    void Update()
    {

        // 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }

        // 2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);

        }

        // 3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }

        // 4
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }

        // 5
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
    }

   

    


}
