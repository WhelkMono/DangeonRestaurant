using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField] private GameObject playerWindow;
    [SerializeField] private ProfilePanel profilePanel;
    public Inventory playerInventory;
    [SerializeField] private Inventory boxInventory;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private Item itemPrefab;
    [SerializeField] private ItemDescWindow ItemDescWindowPrafap;
    [SerializeField] private ClockUI clockUI;

    private GameObj scanObject;
    private bool isAction; //인밴토리 열림 여부
    public bool isESCMenu; //ESCMenu 켜짐 여부

    public Transform canvasTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerWindow.SetActive(false);
        profilePanel.SetActivePanel(false);
        playerInventory.Init(JsonDataManager.Instance.storageData.playerInven, InventoryType.player, slotPrefab, itemPrefab);
        boxInventory.Init(JsonDataManager.Instance.storageData.foodBoxInven, InventoryType.food, slotPrefab, itemPrefab);
        playerInventory.OnInventory(false);
        boxInventory.OnInventory(false);
        isAction = false;

        TimeManager.Instance.clockUI = this.clockUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (isESCMenu) return;

        //인밴토리 열기
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isAction == true)
            {
                DisappearItemDesc();
                isAction = false;
                OnPlayerWindow(isAction);
            }
            else if (!GameMgr.Instance.IsPause)
            {
                isAction = true;
                OnPlayerWindow(isAction);
            }
        }

        if(GameMgr.Instance.IsPause)
            return;

        //스캔된 오브젝트 실행
        if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            scanObject.Action();
            playerInventory.SaveItemData();
            boxInventory.SaveItemData();
            GameMgr.Instance.Player.inventouryKey.SetActive(false);
        }
    }

    ItemDescWindow itemDescWindow;

    public void DisappearItemDesc()
    {
        if(itemDescWindow != null)
        {
            Destroy(itemDescWindow.gameObject);
        }
    }

    public void AppearItemDesc(ItemData itemData, Vector3 pos)
    {
        itemDescWindow = Instantiate(ItemDescWindowPrafap, canvasTrans);
        itemDescWindow.Init(itemData, pos);
    }

    public void OnPlayerWindowChange(Toggle toggle)
    {
        if (toggle.isOn)
            toggle.transform.localPosition = new Vector3(20, toggle.transform.localPosition.y, 0);
        else
            toggle.transform.localPosition = new Vector3(0, toggle.transform.localPosition.y, 0);
    }

    public void OnPlayerWindowChange(bool isInven)
    {
        OnPlayerInven(isInven);
        profilePanel.SetActivePanel(!isInven);
    }

    public void OnPlayerWindow(bool active)
    {
        playerWindow.SetActive(active);
        OnPlayerInven(active);
    }

    public void OnPlayerInven(bool active)
    {
        if (active)
        {
            playerInventory.OnInventory(true);
        }
        else
        {
            playerInventory.SaveItemData();
            boxInventory.SaveItemData();
            playerInventory.OnInventory(false);
            boxInventory.OnInventory(false);
        }
    }

    public void DeletedBoxItem(InventoryType inventoyType, ItemData itemData)
    {
        SetBox(inventoyType);
        boxInventory.DeletedItem(itemData);
    }

    public void SetBox(InventoryType inventoyType)
    {
        InventoryData inventoryData;
        switch (inventoyType)
        {
            case InventoryType.food:
                inventoryData = JsonDataManager.Instance.storageData.foodBoxInven;
                break;
            case InventoryType.ingredient:
                inventoryData = JsonDataManager.Instance.storageData.ingredientBoxInven;
                break;
            default:
                return;
        }

        boxInventory.Init(inventoryData, inventoyType, slotPrefab, itemPrefab);
    }

    public void OnBoxInven(InventoryType inventoyType)
    {
        SetBox(inventoyType);
        isAction = true;
        boxInventory.OnInventory(true);
        OnPlayerWindow(true);
    }

    public void OnBoxInven(InventoryData _inventoryData)
    {
        boxInventory.Init(_inventoryData, InventoryType.box, slotPrefab, itemPrefab);
        isAction = true;
        boxInventory.OnInventory(true);
        OnPlayerWindow(true);
    }

    public bool OnInteractionKey(GameObj _scanObject)
    {
        scanObject = _scanObject;
        
        //미완
        if (scanObject != null && !TalkManager.Instance.isAction && Time.timeScale != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
