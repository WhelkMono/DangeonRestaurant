using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    private InventoyType inventoyType;
    public Item item;

    public void Init(Item _item, InventoyType _inventoyType)
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
                case InventoyType.food:
                    if (droppedItem.itemData.type != ItemDataType.foodData)
                    {
                        Debug.Log("���ĸ� ������ �� �ֽ��ϴ�.");
                        return;
                    }
                    break;
                case InventoyType.ingredient:
                    if (droppedItem.itemData.type != ItemDataType.ingredientData)
                    {
                        Debug.Log("��Ḹ ������ �� �ֽ��ϴ�.");
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
