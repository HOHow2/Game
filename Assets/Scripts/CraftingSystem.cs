using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreen;
    public GameObject CategoryScreen;


    public List<string> inventoryItemList = new List<string>();


    public static CraftingSystem instance { get; set; }

    // Category Buttons

    Button CategorysButton;

    // Craft Button

    Button AxeCraftButton, PickaxeButton, SwordButton;


    // Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2, PickaxeReq1, PickaxeReq2, SwordReq1, SwordReq2;

    public bool isOpen;

    // ALL Blueprint
    private Check_Item Axe_Cr = new Check_Item("Axe", 2, "Stone", 3, "Stick", 3);
    private Check_Item Pickaxe_Cr = new Check_Item("Pickaxe", 2, "Stone", 3, "Stick", 3);
    private Check_Item Sword_Cr = new Check_Item("Sword", 2, "Stone", 4, "Stick", 2);





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


    // Start is called before the first frame update
    void Start()
    {

        isOpen = false;

        CategorysButton = craftingScreen.transform.Find("ToolsButton").GetComponent<Button>();
        CategorysButton.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Axe 
        AxeReq1 = CategoryScreen.transform.Find("Axe").transform.Find("AxeReq1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = CategoryScreen.transform.Find("Axe").transform.Find("AxeReq2").GetComponent<TextMeshProUGUI>();

        AxeCraftButton = CategoryScreen.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        AxeCraftButton.onClick.AddListener(delegate { CraftAnyItem(Axe_Cr); });


        //Pickaxe

        PickaxeReq1 = CategoryScreen.transform.Find("Pickaxe").transform.Find("PickaxeReq1").GetComponent<TextMeshProUGUI>();
        PickaxeReq2 = CategoryScreen.transform.Find("Pickaxe").transform.Find("PickaxeReq2").GetComponent<TextMeshProUGUI>();

        PickaxeButton = CategoryScreen.transform.Find("Pickaxe").transform.Find("Button").GetComponent<Button>();
        PickaxeButton.onClick.AddListener(delegate { CraftAnyItem(Pickaxe_Cr); });

        //// Sword

        SwordReq1 = CategoryScreen.transform.Find("Sword").transform.Find("SwordReq1").GetComponent<TextMeshProUGUI>();
        SwordReq2 = CategoryScreen.transform.Find("Sword").transform.Find("SwordReq2").GetComponent<TextMeshProUGUI>();


        SwordButton = CategoryScreen.transform.Find("Sword").transform.Find("Button").GetComponent<Button>();
        SwordButton.onClick.AddListener(delegate { CraftAnyItem(Sword_Cr); });






        // screen
        craftingScreen.SetActive(false);
        CategoryScreen.SetActive(false);


    }


    private void OpenToolsCategory()
    {
        craftingScreen.SetActive(false);
        CategoryScreen.SetActive(true);
    }

    private void CraftAnyItem(Check_Item check_Item)
    {
        // add Item into inventory
        InventorySystem.instance.AddToInventory(check_Item.itemname);


        if (check_Item.NumOfReq == 1)
        {
            InventorySystem.instance.RemoveItem(check_Item.Req1, check_Item.Req1amount);

        }

        else if (check_Item.NumOfReq == 2)

        {
            InventorySystem.instance.RemoveItem(check_Item.Req1, check_Item.Req1amount);
            InventorySystem.instance.RemoveItem(check_Item.Req2, check_Item.Req2amount);
        }


        StartCoroutine(calculate());

        RefreshNeededItems();

    }


    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);
        InventorySystem.instance.ReCaculateList();
    }


    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();



        if (Input.GetKeyDown(KeyCode.T) && !isOpen)
        {
            craftingScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.T) && isOpen || Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            craftingScreen.SetActive(false);
            CategoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            isOpen = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;

        }

    }

    private void RefreshNeededItems()

    {

        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.instance.ItemList;


        foreach (string ItemName in inventoryItemList)

        {
            switch (ItemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Stick":
                    stick_count += 1;
                    break;

            }

        }


        // --- Axe ----

        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "3 Stick [" + stick_count + "]";

        // --- Pickaxe ---

        PickaxeReq1.text = "3 Stone [" + stone_count + "]";
        PickaxeReq2.text = "3 Stick [" + stick_count + "]";

        //// --- Sword ---

        SwordReq1.text = "4 Stone [" + stone_count + "]";
        SwordReq2.text = "2 Stick [" + stick_count + "]";


        if (stone_count >= 3 && stick_count >= 3 || stone_count >= 4 && stick_count >= 2)
        {
            AxeCraftButton.gameObject.SetActive(true);
            PickaxeButton.gameObject.SetActive(true);
            SwordButton.gameObject.SetActive(true);
        }
        else if (stone_count < 3 && stick_count < 3 || stone_count < 4 && stick_count < 2)
        {
            AxeCraftButton.gameObject.SetActive(false);
            PickaxeButton.gameObject.SetActive(false);
            SwordButton.gameObject.SetActive(false);

        }

    }


    


}
