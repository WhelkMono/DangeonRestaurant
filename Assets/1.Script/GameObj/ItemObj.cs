using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : MonoBehaviour, GameObj
{
    public ItemData itemData;

    public void Init(ItemData _itemData)
    {
        itemData = _itemData;
    }
    public void Action()
    {
        if (PlayerUIManager.Instance.playerInventory.GetIt(itemData))
        {
            Destroy(gameObject);
        }
    }
}
