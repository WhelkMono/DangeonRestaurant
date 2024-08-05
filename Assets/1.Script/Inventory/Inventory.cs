using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject invenContent;
    private Slot slotPrefab;
    private Item itemPrefab;
    public List<Slot> invenSlots;
    private int invenCount;

    public void Init(InventoryData inventoryData, Slot _slotPrefab, Item _itemPrefab)
    {
        invenCount = inventoryData.invenCount;
        slotPrefab = _slotPrefab;
        itemPrefab = _itemPrefab;

        int n = invenSlots.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(invenSlots[i].gameObject);
        }

        //½½·Ô »ý¼º
        invenSlots = new();
        for (int i = 0; i < inventoryData.invenCount; i++)
        {
            invenSlots.Add(Instantiate(slotPrefab, invenContent.transform));
            invenSlots[i].Init(null);
        }

        //LoadItemData
        List<ItemData> itemDatas = inventoryData.itemDatas;

        for (int i = 0; i < itemDatas.Count; i++)
        {
            Item item = Instantiate(itemPrefab, invenSlots[i].transform);
            item.Init(itemDatas[i]);
            invenSlots[i].Init(item);
        }

        InventoryPanel.SetActive(false);
    }

    public void OnInventory(bool isAction)
    {
        InventoryPanel.SetActive(isAction);
    }

    public bool GetIt(ItemData itemData)
    {
        for (int i = 0; i < invenCount; i++)
        {
            if(invenSlots[i].item == null)
            {
                Item item = Instantiate(itemPrefab, invenSlots[i].transform);
                item.Init(itemData);
                invenSlots[i].Init(item);
                return true;
            }
            else if (invenSlots[i].item.itemData.type == itemData.type &&
                invenSlots[i].item.itemData.id == itemData.id)
            {
                invenSlots[i].item.AddData(itemData.count);
                return true;
            }
        }

        Debug.Log("ÀÎº¥Åä¸®°¡ °¡µæ Ã¡½À´Ï´Ù.");
        return false;
    }

    public void SaveItemData(bool isPlayer)
    {
        InventoryData inventoryData = new();
        
        inventoryData.invenCount = invenCount;
        for (int i = 0; i < invenCount; i++)
        {
            if(invenSlots[i].item != null)
                inventoryData.itemDatas.Add(invenSlots[i].item.itemData);
        }

        if(isPlayer)
            JsonDataManager.Instance.storageData.playerInven = inventoryData;
        else
            JsonDataManager.Instance.storageData.restaurantBoxInven = inventoryData;
    }
}
