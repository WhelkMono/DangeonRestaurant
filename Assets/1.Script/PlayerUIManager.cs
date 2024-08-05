using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField] private GameObject interactionKey;
    public Inventory playerInventory;
    [SerializeField] private Inventory boxInventory;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private Item itemPrefab;

    private GameObj scanObject;
    private bool isAction;
    private bool isRBox;

    public Transform canvasTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.Init(JsonDataManager.Instance.storageData.playerInven, slotPrefab, itemPrefab);
        boxInventory.Init(JsonDataManager.Instance.storageData.restaurantBoxInven, slotPrefab, itemPrefab);

        isRBox = false;
        isAction = false;
        interactionKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnBoxInven(new InventoryData());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isAction = !isAction;
            playerInventory.OnInventory(isAction);
            if(isAction == false)
            {
                if (isRBox)
                {
                    isRBox = false;
                    playerInventory.SaveItemData(true);
                    boxInventory.SaveItemData(false);
                }
                boxInventory.OnInventory(false);
            }
            Time.timeScale = isAction ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.F) && scanObject != null && !isAction)
        {
            interactionKey.SetActive(false);
            scanObject.Action();
        }
    }

    public void OnRestaurantBoxInven()
    {
        boxInventory.Init(JsonDataManager.Instance.storageData.restaurantBoxInven, slotPrefab, itemPrefab);
        isRBox = true;
        isAction = true;
        boxInventory.OnInventory(true);
        playerInventory.OnInventory(true);
    }

    public void OnBoxInven(InventoryData _inventoryData)
    {
        boxInventory.Init(_inventoryData, slotPrefab, itemPrefab);
        isAction = true;
        boxInventory.OnInventory(true);
        playerInventory.OnInventory(true);
    }

    public void OnInteractionKey(GameObject _scanObject)
    {
        if(_scanObject != null && _scanObject.GetComponent<GameObj>() != null)
            scanObject = _scanObject.GetComponent<GameObj>();
        else
            scanObject = null;

        if (scanObject != null && !TalkManager.Instance.isAction)
        {
            interactionKey.SetActive(true);
        }
        else
        {
            interactionKey.SetActive(false);
        }
    }
}
