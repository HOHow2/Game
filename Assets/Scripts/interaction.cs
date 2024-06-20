using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    public string ItemName;
    public bool Rangedetect;

    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider Item)
    {
        if (Item.CompareTag("Player"))
        {
            Rangedetect = true;
        }

    }

    private void OnTriggerExit(Collider Item)
    {
        if (Item.CompareTag("Player"))
        {
            Rangedetect = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Rangedetect && SelectionManager.Instance.Ontarget && SelectionManager.Instance.SelectedObject == gameObject)
        {

            // if the inventory is not full

            if (!InventorySystem.instance.CheckIfFull())
            {
                InventorySystem.instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }

            else
            {
                Debug.Log("Your inventory is full!");

            }
           
        }
    }
}
