using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, GameObj
{
    private ItemData itemData;

    public void Action()
    {
        itemData = new ItemData();
        itemData.type = ItemDataType.ingredientData;
        itemData.id = 2;
        itemData.count = 1;
        PlayerUIManager.Instance.playerInventory.GetIt(itemData);
    }
}
