using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject invenContent;
    [SerializeField] private GameObject boxInventory;
    [SerializeField] private GameObject boxInvenContent;

    private List<Slot> invenSlots;
    private List<Slot> boxInvenSlots;

    public bool isAction;

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAction = !isAction;
            inventory.SetActive(isAction);
            Time.timeScale = inventory.activeSelf ? 0 : 1;
        }
    }

    public void Init()
    {
        isAction = false;
        inventory.SetActive(false);
        invenSlots = new List<Slot>();

        for (int i = 0; i < invenContent.transform.childCount; i++)
        {
            invenSlots.Add(invenContent.transform.GetChild(i).GetComponent<Slot>());
            invenSlots[i].DeleteData();
        }

        LoadItemData();
    }

    public void LoadItemData()
    {
        for (int i = 0; i < JsonDataManager.Instance.playerData.inventoryData.Count; i++)
        {
            invenSlots[i].SetData(JsonDataManager.Instance.playerData.inventoryData[i]);
        }
    }

    public void SaveItemData()
    {
        JsonDataManager.Instance.playerData.inventoryData = new();
        for (int i = 0; i < invenSlots.Count; i++)
        {
            if (invenSlots[i].slotData.itemAddressData != null)
            {
                JsonDataManager.Instance.playerData.inventoryData.Add(invenSlots[i].slotData.itemAddressData);
            }
        }
    }

    public void GetIt(ItemAddressData itemAddressData)
    {
        for (int i = 0; i < invenSlots.Count; i++)
        {
            if (invenSlots[i].slotData.itemAddressData == null)
            {
                invenSlots[i].SetData(itemAddressData);
                SaveItemData();
                return;
            }
            else if (invenSlots[i].slotData.itemAddressData.type == itemAddressData.type &&
                invenSlots[i].slotData.itemAddressData.id == itemAddressData.id)
            {
                invenSlots[i].AddData(itemAddressData.count);
                SaveItemData();
                return;
            }
        }
    }
}
