using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory_item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isEquip;
    private GameObject Item_Equipping;
    public bool isEquipped;
    public bool isSelected;


    //// Start is called before the first frame update
    void Start()
    {
       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right) {
            if (isEquip && isEquipped == false && EquipSystem.instance.CheckFull() == false)
            {
                EquipSystem.instance.AddToQuickSlot(gameObject);
                isEquipped = true;
            }
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {

    }




    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }
    
}
