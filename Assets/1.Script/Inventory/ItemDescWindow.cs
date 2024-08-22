using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescWindow : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image img;
    [SerializeField] private TMP_Text nameTxt;
    [SerializeField] private TMP_Text satietyTxt;
    [SerializeField] private TMP_Text priceTxt;
    [SerializeField] private TMP_Text descTxt;

    public void Init(ItemData itemData, Vector3 pos)
    {
        transform.position = new Vector3(200, 0, 0) + pos;
        Debug.Log(itemData.id);

        /*switch (itemData.type)
        {
            case ItemDataType.foodData:
                img.sprite = SpriteManager.Instance.IngredientSprites[itemData.id];
                IngredientJsonData ingredientJsonData = JsonDataManager.Instance.itemData.ingredientDatas[itemData.id];
                nameTxt.text = ingredientJsonData.name;
                satietyTxt.text = ingredientJsonData.satiety.ToString();
                priceTxt.text = ingredientJsonData.price.ToString();
                descTxt.text = ingredientJsonData.description;
                break;
            case ItemDataType.ingredientData:
                img.sprite = SpriteManager.Instance.IngredientSprites[itemData.id];
                FoodJsonData foodJsonData = JsonDataManager.Instance.itemData.ingredientDatas[itemData.id];
                nameTxt.text = foodJsonData.name;
                satietyTxt.text = foodJsonData.satiety.ToString();
                priceTxt.text = foodJsonData.price.ToString();
                descTxt.text = foodJsonData.description;
                break;
        }*/
    }
}
