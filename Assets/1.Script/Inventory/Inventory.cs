using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum InventoyType
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
    private Slot slotPrefab;
    private Item itemPrefab;
    public List<Slot> invenSlots;
    private int invenCount;
    public InventoyType inventoyType;

    public void Init(InventoryData inventoryData, InventoyType _inventoyType, Slot _slotPrefab, Item _itemPrefab)
    {
        invenCount = inventoryData.invenCount;
        inventoyType = _inventoyType;
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
            invenSlots[i].Init(null, inventoyType);
        }

        //LoadItemData
        List<ItemData> itemDatas = inventoryData.itemDatas;

        for (int i = 0; i < itemDatas.Count; i++)
        {
            Item item = Instantiate(itemPrefab, invenSlots[i].transform);
            item.Init(itemDatas[i]);
            invenSlots[i].Init(item, inventoyType);
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
            if(invenSlots[i].item == null)
            {
                Item item = Instantiate(itemPrefab, invenSlots[i].transform);
                item.Init(itemData);
                invenSlots[i].Init(item, inventoyType);
                return true;
            }
            else if (invenSlots[i].item.itemData.type == itemData.type &&
                invenSlots[i].item.itemData.id == itemData.id)
            {
                invenSlots[i].item.AddData(itemData.count);
                return true;
            }
        }

        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
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

        switch (inventoyType)
        {
            case InventoyType.player:
                JsonDataManager.Instance.storageData.playerInven = inventoryData;
                break;
            case InventoyType.food:
                JsonDataManager.Instance.storageData.foodBoxInven = inventoryData;
                break;
            case InventoyType.ingredient:
                JsonDataManager.Instance.storageData.ingredientBoxInven = inventoryData;
                break;
            case InventoyType.box:
                Debug.Log("저장할 수 없는 데이터 입니다.");
                break;
        }
    }
}
