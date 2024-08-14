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

    [System.Serializable]
    public class Cook_Panel
    {
        public GameObject cook_Panel;
        public Image image;
        public TMP_Text NameTxt;
        public Slider slider;
        public TMP_Text CountTxt;
    }

    [System.Serializable]
    public class CookCheck_Panel
    {
        public GameObject cookCheck_Panel;
        public Image image;
        public TMP_Text NameTxt;
        public TMP_Text CountTxt;
    }

    [System.Serializable]
    public class Research_Panel
    {
        public GameObject research_Panel;
        public TMP_Text levelTxt;
        public TMP_Text description;
        public Transform recipe_Content;
    }

    [SerializeField] private GameObject Cooking_Panel;
    [SerializeField] private Transform recipeSlot_Content;
    [SerializeField] private RecipeSlot recipeSlotPrefab;
    [SerializeField] private FoodData_Panel foodData_Panel;
    [SerializeField] private IngredientSlot ingredientSlotPrefab;
    [SerializeField] private Research_Panel research_Panel;
    [SerializeField] private ResearchSlot researchSlotPrepab;
    [SerializeField] private Cook_Panel cook_Panel;
    [SerializeField] private CookCheck_Panel cookCheck_Panel;

    private List<IngredientSlot> ingredientSlots;
    private List<RecipeSlot> recipeSlots;
    private List<ResearchSlot> researchSlots;
    public bool isCooking;

    void Start()
    {
        ingredientSlots = new();
        recipeSlots = new();
        researchSlots = new();
        isCooking = false;
        Cooking_Panel.SetActive(false);
        cookCheck_Panel.cookCheck_Panel.SetActive(false);
        research_Panel.research_Panel.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T)) OnCookPanel();
    }

    public void Init()
    {
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

    private int _id;
    private int _level;
    private int _price1;
    private int _price2;
    private int _taste1;
    private int _taste2;

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
        _id = foodJsonData.id;
        _level = foodData.level;
        _price1 = Mathf.RoundToInt(foodJsonData.price + (foodJsonData.price * (foodData.level * 2 / 10f)));
        _price2 = Mathf.RoundToInt(foodJsonData.price + (foodJsonData.price * ((foodData.level + 1) * 2 / 10f)));
        _taste1 = foodJsonData.taste[foodData.level];
        if(foodData.level < 10) _taste2 = foodJsonData.taste[foodData.level + 1];
        foodData_Panel.title.text = foodJsonData.name;

        foodData_Panel.image.sprite = SpriteManager.Instance.FoodSprites[foodData.id];
        foodData_Panel.level.text = "Lv." + _level.ToString();
        foodData_Panel.price.text = "가격:" + _price1.ToString();
        foodData_Panel.satiety.text = "포만감:" + foodJsonData.satiety.ToString();
        foodData_Panel.taste.text = "맛:" + _taste1.ToString();

        foodData_Panel.description.text = foodJsonData.description;

        List<ItemData> itemDatas = JsonDataManager.Instance.storageData.ingredientBoxInven.itemDatas;
        Sprite[] sprites = SpriteManager.Instance.IngredientSprites;
        List<IngredientJsonData> ingredientJsonDatas = JsonDataManager.Instance.itemData.ingredientDatas;

        int cookCount = -1;

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
                    possCount += data.count;
                }
            }

            IngredientSlot ingredientSlot = Instantiate(ingredientSlotPrefab, foodData_Panel.recipe_Content);
            ingredientSlot.Init(sprite, _name, requCount, possCount, "");
            ingredientSlots.Add(ingredientSlot);

            if (Mathf.FloorToInt((float)possCount / requCount) < cookCount || cookCount == -1)
                cookCount = Mathf.FloorToInt((float)possCount / requCount);
        }

        cook_Panel.slider.maxValue = cookCount;
        cook_Panel.slider.value = 1;
        cook_Panel.image.sprite = foodData_Panel.image.sprite;
        cook_Panel.NameTxt.text = foodData_Panel.title.text;
        cook_Panel.CountTxt.text = "1/" + cook_Panel.slider.maxValue.ToString();

        if(cookCount > 0)
            cook_Panel.cook_Panel.SetActive(true);
        else
            cook_Panel.cook_Panel.SetActive(false);
    }

    public void OnSetCookCountTxt()
    {
        if(cook_Panel.slider.value == 0)
            cook_Panel.slider.value = 1;
        cook_Panel.CountTxt.text = cook_Panel.slider.value.ToString() + "/" + cook_Panel.slider.maxValue.ToString();
    }

    public void OnCook(bool active)
    {
        cookCheck_Panel.cookCheck_Panel.SetActive(active);
        if (active)
        {
            cookCheck_Panel.image.sprite = foodData_Panel.image.sprite;
            cookCheck_Panel.NameTxt.text = foodData_Panel.title.text;
            cookCheck_Panel.CountTxt.text = cook_Panel.slider.value.ToString();

            ItemData itemData = new();
            itemData.type = ItemDataType.foodData;
            itemData.id = _id;
            itemData.count = (int)cook_Panel.slider.value;
            PlayerUIManager.Instance.playerInventory.GetIt(itemData);

            foreach (var recipe in JsonDataManager.Instance.itemData.foodDatas[_id].recipe)
            {
                itemData = new();
                itemData.type = ItemDataType.ingredientData;
                itemData.id = recipe.id;
                itemData.count = recipe.count * (int)cook_Panel.slider.value;

                PlayerUIManager.Instance.DeletedBoxItem(InventoyType.ingredient, itemData);
            }
        }
        else
        {
            foodData_Panel.foodData_Panel.SetActive(false);
        }
    }

    private bool isResearch = false;
    public void OnResearch()
    {
        if (isResearch)
        {
            List<FoodData> foodDatas = JsonDataManager.Instance.storageData.foodDatas;
            for (int i = 0; i < foodDatas.Count; i++)
            {
                if(foodDatas[i].id == _id)
                {
                    foodDatas[i].level++;
                    break;
                }
            }

            foreach (var recipe in JsonDataManager.Instance.itemData.foodDatas[_id].recipe)
            {
                ItemData itemData = new();
                itemData.type = ItemDataType.ingredientData;
                itemData.id = recipe.id;
                itemData.count = recipe.count * _level * 2;

                PlayerUIManager.Instance.DeletedBoxItem(InventoyType.ingredient, itemData);
            }

            Init();
            OnResearchPanel(false);
        }
        else
        {
            Debug.Log("재료가 부족합니다.");
        }
    }

    public void OnResearchPanel(bool active)
    {
        if (active)
        {
            if(_level == 10)
            {
                Debug.Log("최대 레벨입니다.");
                return;
            }

            research_Panel.research_Panel.SetActive(true);

            research_Panel.levelTxt.text = $"LV.{_level} -> LV.{_level + 1}";
            research_Panel.description.text = $"가격:{_price1} -> {_price2}<br>맛:{_taste1} -> {_taste2}";

            int n = researchSlots.Count;
            for (int i = 0; i < n; i++)
            {
                Destroy(researchSlots[i].gameObject);
            }

            researchSlots = new();

            FoodJsonData foodJsonData = JsonDataManager.Instance.itemData.foodDatas[_id];
            List<ItemData> itemDatas = JsonDataManager.Instance.storageData.ingredientBoxInven.itemDatas;

            int cookCount = -1;

            for (int i = 0; i < ingredientSlots.Count; i++)
            {
                Sprite sprite = ingredientSlots[i].image.sprite;
                string _name = ingredientSlots[i].nameTxt.text;

                int requCount = foodJsonData.recipe[i].count * _level * 2;
                int possCount = 0;

                foreach (var data in itemDatas)
                {
                    if (foodJsonData.recipe[i].id == data.id)
                    {
                        possCount += data.count;
                    }
                }

                ResearchSlot researchSlot = Instantiate(researchSlotPrepab, research_Panel.recipe_Content);
                researchSlot.Init(sprite, _name, requCount, possCount);
                researchSlots.Add(researchSlot);

                if (Mathf.FloorToInt((float)possCount / requCount) < cookCount || cookCount == -1)
                    cookCount = Mathf.FloorToInt((float)possCount / requCount);
            }

            isResearch = cookCount > 0 ? true : false;
        }
        else
        {
            research_Panel.research_Panel.SetActive(false);
            foodData_Panel.foodData_Panel.SetActive(false);
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
        foodData_Panel.foodData_Panel.SetActive(false);
        cook_Panel.cook_Panel.SetActive(false);
        cookCheck_Panel.cookCheck_Panel.SetActive(false);
        isCooking = !isCooking;
        Cooking_Panel.SetActive(isCooking);
        if (isCooking)
        {
            //playerInventory 끄기
            PlayerUIManager.Instance.OnPlayerInven(false);
            Init();
        }
        GameMgr.Instance.Pause(isCooking);
    }
}