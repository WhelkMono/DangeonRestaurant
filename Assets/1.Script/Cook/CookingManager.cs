using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookingManager : Singleton<CookingManager>
{
    [System.Serializable]
    public class FoodData_Panel
    {
        public GameObject foodData_Panel;
        public TMP_Text title;
        public Image image;
        public TMP_Text level;
        public TMP_Text price;
        public TMP_Text satiety;
        public TMP_Text taste;
        public TMP_Text description;
        public Transform recipe_Content;
    }

    [SerializeField] private GameObject Cooking_Panel;
    [SerializeField] private Transform recipeSlot_Content;
    [SerializeField] private RecipeSlot recipeSlotPrefab;

    [SerializeField] private FoodData_Panel foodData_Panel;
    [SerializeField] private IngredientSlot ingredientSlotPrefab;

    private List<IngredientSlot> ingredientSlots;
    private List<RecipeSlot> recipeSlots;
    public bool isCooking;

    void Start()
    {
        ingredientSlots = new();
        recipeSlots = new();
        Init();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)) OnCookPanel();
    }

    public void Init()
    {
        isCooking = false;
        Cooking_Panel.SetActive(false);
        foodData_Panel.foodData_Panel.SetActive(false);

        int n = recipeSlots.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(recipeSlots[i].gameObject);
        }

        recipeSlots = new();
        List<FoodData> foodDatas = JsonDataManager.Instance.storageData.foodDatas;

        foreach (var foodData in foodDatas)
        {
            RecipeSlot recipeSlot = Instantiate(recipeSlotPrefab, recipeSlot_Content);
            recipeSlot.Init(foodData);
            recipeSlots.Add(recipeSlot);
        }
    }

    public void OnCook()
    {

    }

    public void OnResearch()
    {

    }

    public void ShowFoodData(FoodData foodData)
    {
        int n = ingredientSlots.Count;
        for (int i = 0; i < n; i++)
        {
            Destroy(ingredientSlots[i].gameObject);
        }

        ingredientSlots = new();

        foodData_Panel.foodData_Panel.SetActive(true);

        FoodJsonData foodJsonData = JsonDataManager.Instance.itemData.foodDatas[foodData.id];
        foodData_Panel.title.text = foodJsonData.name;

        foodData_Panel.image.sprite = SpriteManager.Instance.FoodSprites[foodData.id];
        foodData_Panel.level.text = foodData.level.ToString();
        foodData_Panel.price.text = 
            Mathf.RoundToInt(foodJsonData.price + (foodJsonData.price * (foodData.level * 2 / 10f))).ToString();
        foodData_Panel.satiety.text = foodJsonData.satiety.ToString();
        foodData_Panel.taste.text = foodJsonData.taste[foodData.level].ToString();

        foodData_Panel.description.text = foodJsonData.description;

        List<ItemData> itemDatas = JsonDataManager.Instance.storageData.ingredientBoxInven.itemDatas;
        Sprite[] sprites = SpriteManager.Instance.IngredientSprites;
        List<IngredientJsonData> ingredientJsonDatas = JsonDataManager.Instance.itemData.ingredientDatas;

        for (int i = 0; i < foodJsonData.recipe.Length; i++)
        {
            int recipeId = foodJsonData.recipe[i].id;

            Sprite sprite = sprites[recipeId];
            string _name = ingredientJsonDatas[recipeId].name;
            int requCount = foodJsonData.recipe[i].count;
            int possCount = 0;

            foreach (var data in itemDatas)
            {
                if (recipeId == data.id)
                {
                    possCount = data.count;
                    break;
                }
            }

            IngredientSlot ingredientSlot = Instantiate(ingredientSlotPrefab, foodData_Panel.recipe_Content);
            ingredientSlot.Init(sprite, _name, requCount, possCount, "");
            ingredientSlots.Add(ingredientSlot);
        }
    }

    public void OnSort(TMP_Dropdown tMP_Dropdown)
    {
        //Debug.Log(tMP_Dropdown.value);

        switch (tMP_Dropdown.value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public void OnCookPanel()
    {
        isCooking = !isCooking;
        Cooking_Panel.SetActive(isCooking);
        GameMgr.Instance.Pause(isCooking);
    }
}
