using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public enum ItemDataType
{
    foodData,
    ingredientDat
}

[System.Serializable]
public class SlotData
{
    public Image image;
    public TMP_Text countTxt;

    [HideInInspector] public ItemAddressData itemAddressData;
}

public class Slot : MonoBehaviour
{
    public SlotData slotData;

    public void SetData(ItemAddressData itemAddressData)
    {
        slotData.image.color = Color.white;
        slotData.image.sprite = SpriteManager.Instance.ItemSprites[itemAddressData.id];
        slotData.countTxt.text = itemAddressData.count.ToString();
        slotData.itemAddressData = itemAddressData;
    }

    public void DeleteData()
    {
        slotData.image.color = new Color(1, 1, 1, 0);
        slotData.image.sprite = null;
        slotData.countTxt.text = "";

        slotData.itemAddressData = null;
    }

    public void AddData(int addCount)
    {
        slotData.itemAddressData.count += addCount;
        slotData.countTxt.text = slotData.itemAddressData.count.ToString();
    }
}
