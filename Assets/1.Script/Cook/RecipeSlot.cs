using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private TMP_Text countTxt;

    public FoodData foodData;

    public void Init(FoodData _foodData)
    {
        foodData = _foodData;

        image.sprite = SpriteManager.Instance.FoodSprites[foodData.id];

        if(foodData.level == 10)
            levelTxt.text = "Lv.Max";
        else
            levelTxt.text = "Lv." + foodData.level.ToString();

        int count = 0;
        foreach (var itemdata in JsonDataManager.Instance.storageData.foodBoxInven.itemDatas)
        {
            if(itemdata.id == foodData.id)
            {
                count += itemdata.count;
            }
        }

        countTxt.text = count.ToString();
    }

    public void OnShowDesc()
    {
        if(foodData != null)
            CookingManager.Instance.ShowFoodData(foodData);
    }
}
