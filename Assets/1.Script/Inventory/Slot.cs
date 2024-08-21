using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    private InventoryType inventoyType;
    public Item item;

    public void Init(Item _item, InventoryType _inventoyType)
    {
        item = _item;
        inventoyType = _inventoyType;

        if (item != null)
            item.slot = this;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = eventData.pointerDrag.GetComponent<Item>();
        //Debug.Log(droppedItem);
        if (droppedItem != null && droppedItem != item)
        {
            switch (inventoyType)
            {
                case InventoryType.food:
                    if (droppedItem.itemData.type != ItemDataType.foodData)
                    {
                        Debug.Log("음식만 저장할 수 있습니다.");
                        return;
                    }
                    break;
                case InventoryType.ingredient:
                    if (droppedItem.itemData.type != ItemDataType.ingredientData)
                    {
                        Debug.Log("재료만 저장할 수 있습니다.");
                        return;
                    }
                    break;
                default:
                    break;
            }


            //Debug.Log("OnDrop");
            if (item == null)
            {
                droppedItem.slot.item = null;
                droppedItem.slot = this;
                item = droppedItem;
            }
            else if (droppedItem.itemData.type ==  item.itemData.type &&
                droppedItem.itemData.id == item.itemData.id)
            {
                item.AddData(droppedItem.itemData.count);
                Destroy(droppedItem.gameObject);
            }
            else
            {
                droppedItem.slot.item = item;
                item.slot = droppedItem.slot;

                droppedItem.slot.item.transform.SetParent(item.slot.transform);
                droppedItem.slot.item.transform.localPosition = Vector2.zero;

                item = droppedItem;
                droppedItem.slot = this;
            }
        }
    }
}
