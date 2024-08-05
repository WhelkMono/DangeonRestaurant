using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Item item;

    public void Init(Item _item)
    {
        item = _item;

        if(item != null)
            item.slot = this;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item droppedItem = eventData.pointerDrag.GetComponent<Item>();
        //Debug.Log(droppedItem);
        if (droppedItem != null && droppedItem != item)
        {
            //Debug.Log("OnDrop");
            if(item == null)
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
