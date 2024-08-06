using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIManager : Singleton<PlayerUIManager>
{
    [SerializeField] private GameObject interactionKey; //수정필요
    public Inventory playerInventory;
    [SerializeField] private Inventory boxInventory;
    [SerializeField] private Slot slotPrefab;
    [SerializeField] private Item itemPrefab;

    private GameObj scanObject;
    private bool isAction; //인밴토리 열림 여부

    public Transform canvasTrans;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.Init(JsonDataManager.Instance.storageData.playerInven, InventoyType.player, slotPrefab, itemPrefab);
        playerInventory.Init(JsonDataManager.Instance.storageData.foodBoxInven, InventoyType.food, slotPrefab, itemPrefab);
        playerInventory.OnInventory(false);
        boxInventory.OnInventory(false);
        isAction = false;
        interactionKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //인밴토리 열기
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isAction == true)
            {
                playerInventory.SaveItemData();
                boxInventory.SaveItemData();
                playerInventory.OnInventory(false);
                boxInventory.OnInventory(false);
                isAction = false;
            }
            else if (!GameMgr.Instance.IsPause)
            {
                isAction = true;
                playerInventory.OnInventory(true);
            }
        }

        //스캔된 오브젝트 실행
        if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
        {
            playerInventory.SaveItemData();
            boxInventory.SaveItemData();
            interactionKey.SetActive(false);
            scanObject.Action();
        }
    }

    public void OnFoodBoxInven()
    {
        boxInventory.Init(JsonDataManager.Instance.storageData.foodBoxInven, InventoyType.food, slotPrefab, itemPrefab);
        isAction = true;
        boxInventory.OnInventory(true);
        playerInventory.OnInventory(true);
    }

    public void OnIngredientBoxInven()
    {
        boxInventory.Init(JsonDataManager.Instance.storageData.ingredientBoxInven, InventoyType.ingredient, slotPrefab, itemPrefab);
        isAction = true;
        boxInventory.OnInventory(true);
        playerInventory.OnInventory(true);
    }

    public void OnBoxInven(InventoryData _inventoryData)
    {
        boxInventory.Init(_inventoryData, InventoyType.box, slotPrefab, itemPrefab);
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

        if (scanObject != null && !TalkManager.Instance.isAction && Time.timeScale != 0)
        {
            interactionKey.SetActive(true);
        }
        else
        {
            interactionKey.SetActive(false);
        }
    }
}
