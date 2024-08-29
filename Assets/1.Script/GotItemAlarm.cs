using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotItemAlarm : Singleton<GotItemAlarm>
{
    public GameObject parent;
    public GotItem gotItemPrefap;

    public void PutOnItem(ItemData itemData)
    {
        GotItem gotItem = Instantiate(gotItemPrefap, parent.transform);

        switch (itemData.type)
        {
            case ItemDataType.foodData:
                gotItem.img.sprite = SpriteManager.Instance.FoodSprites[itemData.id];
                gotItem.text.text = JsonDataManager.Instance.itemData.foodDatas[itemData.id].name + " * " + itemData.count;
                break;
            case ItemDataType.ingredientData:
                gotItem.img.sprite = SpriteManager.Instance.IngredientSprites[itemData.id];
                gotItem.text.text = JsonDataManager.Instance.itemData.ingredientDatas[itemData.id].name + " * " + itemData.count;
                break;
        }
    }
}
