using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum InventoryType
{
    player,
    food,
    ingredient,
    box
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject invenContent;
    [SerializeField] private TMP_Text tiltleTxt;
    private Slot slotPrefab;
    private Item itemPrefab;
    public List<Slot> invenSlots;
    private int invenCount;
    public InventoryType inventoryType;

    public void Init(InventoryData inventoryData, InventoryType _inventoyType, Slot _slotPrefab, Item _itemPrefab)
    {
        string _tiltleTxt = "Box";
        switch (_inventoyType)
        {
            case InventoryType.player:
                _tiltleTxt = "Inventory";
                break;
            case InventoryType.food:
                _tiltleTxt = "FoodBox";
                break;
            case InventoryType.ingredient:
                _tiltleTxt = "IngredientBox";
                break;
            case InventoryType.box:
                _tiltleTxt = "Box";
                break;
        }

        tiltleTxt.text = _tiltleTxt;

        invenCount = inventoryData.invenCount;
        inventoryType = _inventoyType;
        slotPrefab = _slotPrefab;
        itemPrefab = _itemPrefab;

        int n = invenSlots.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(invenSlots[i].gameObject);
        }

        //슬롯 생성
        invenSlots = new();
        for (int i = 0; i < inventoryData.invenCount; i++)
        {
            invenSlots.Add(Instantiate(slotPrefab, invenContent.transform));
            invenSlots[i].Init(null, inventoryType);
        }

        //LoadItemData
        List<ItemData> itemDatas = inventoryData.itemDatas;

        for (int i = 0; i < itemDatas.Count; i++)
        {
            Item item = Instantiate(itemPrefab, invenSlots[i].transform);
            item.Init(itemDatas[i]);
            invenSlots[i].Init(item, inventoryType);
        }
    }

    public void OnInventory(bool isAction)
    {
        InventoryPanel.SetActive(isAction);
        GameMgr.Instance.Pause(isAction);
    }

    public bool GetIt(ItemData itemData)
    {
        for (int i = 0; i < invenCount; i++)
        {
            if (invenSlots[i].item != null && invenSlots[i].item.itemData.type == itemData.type &&
                invenSlots[i].item.itemData.id == itemData.id)
            {
                invenSlots[i].item.AddData(itemData.count);
                return true;
            }
        }

        for (int i = 0; i < invenCount; i++)
        {
            if (invenSlots[i].item == null)
            {
                Item item = Instantiate(itemPrefab, invenSlots[i].transform);

                ItemData _itemData = new ItemData();
                _itemData.id = itemData.id;
                _itemData.type = itemData.type;
                _itemData.count = itemData.count;
                item.Init(_itemData);
                invenSlots[i].Init(item, inventoryType);
                return true;
            }
        }

        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }

    public void DeletedItem(ItemData itemData)
    {
        for (int i = 0; i < invenCount; i++)
        {
            if (invenSlots[i].item != null && 
                invenSlots[i].item.itemData.type == itemData.type &&
                invenSlots[i].item.itemData.id == itemData.id)
            {
                invenSlots[i].item.SubData(itemData.count);
                return;
            }
        }

        Debug.Log("찾는 아이템이 인벤토리에 없습니다.");
    }

    public void SaveItemData()
    {
        InventoryData inventoryData = new();
        
        inventoryData.invenCount = invenCount;
        for (int i = 0; i < invenCount; i++)
        {
            if(invenSlots[i].item != null)
                inventoryData.itemDatas.Add(invenSlots[i].item.itemData);
        }

        switch (inventoryType)
        {
            case InventoryType.player:
                JsonDataManager.Instance.storageData.playerInven = inventoryData;
                break;
            case InventoryType.food:
                JsonDataManager.Instance.storageData.foodBoxInven = inventoryData;
                break;
            case InventoryType.ingredient:
                JsonDataManager.Instance.storageData.ingredientBoxInven = inventoryData;
                break;
            case InventoryType.box:
                Debug.Log("저장할 수 없는 데이터 입니다.");
                break;
        }
    }
}
