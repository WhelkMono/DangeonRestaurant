using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public int id;
    public string name;
    public string[] desc;
    public int[] emotion;
}

[System.Serializable]
public class ObjectData
{
    public NPCData[] npcDatas;
}

[System.Serializable]
public class RecipeData
{
    public int id;
    public int count;
}

[System.Serializable]
public class FoodJsonData
{
    public int id;
    public string name;
    public string description;
    public RecipeData[] recipe;
    public int price;
    public int satiety;
    public int[] taste;
}

[System.Serializable]
public class IngredientJsonData
{
    public int id;
    public string name;
    public string description;
    public int price;
    public int satiety;
}

[System.Serializable]
public class ItemJsonData
{
    public List<FoodJsonData> foodDatas;
    public List<IngredientJsonData> ingredientDatas;

    public ItemJsonData()
    {
        foodDatas = new List<FoodJsonData>();
        ingredientDatas = new List<IngredientJsonData>();
    }
}

public enum ItemDataType
{
    foodData,
    ingredientData
}

[System.Serializable]
public class ItemData
{
    public ItemDataType type;
    public int id;
    public int count;
}

[System.Serializable]
public class InventoryData
{
    public int invenCount;
    public List<ItemData> itemDatas;

    public InventoryData(/*int invenCount*/)
    {
        invenCount = 10;
        itemDatas = new();

        /*//Test
        ItemData itemData = new();
        itemData.type = ItemDataType.ingredientDat;
        itemData.id = 1;
        itemData.count = 3;
        itemDatas = new();
        itemDatas.Add(itemData);*/
    }
}

[System.Serializable]
public class FoodData
{
    public int id;
    public int level;

    //tset¿ëµµ
    public FoodData(int _id)
    {
        id = _id;
        level = 1;
    }
}

[System.Serializable]
public class TimeData
{
    public int day;
    public int hour;
    public int minute;

    public TimeData()
    {
        day = 1;
        hour = 7;
        minute = 0;
    }
}

public enum SpaceType
{
    StartFloor,
    HomeFloor,
    Restaurant,
    Myroom
}

[System.Serializable]
public class LocationData
{
    public SpaceType spaceType;
    public Vector3 pos;

    public LocationData()
    {
        spaceType = SpaceType.StartFloor;
        pos = new Vector3(0, -10, 0);
    }
}

[System.Serializable]
public class PlayerData
{
    public int HP;
    public int Hunger;
    public int Coin;

    public PlayerData()
    {
        HP = 100;
        Hunger = 100;
        Coin = 0;
    }
}

[System.Serializable]

public class StorageData
{
    public bool isPlay;
    public PlayerData playerData;
    public LocationData playerLocation;
    public TimeData timeData;
    public List<FoodData> foodDatas;
    public InventoryData playerInven;
    public InventoryData foodBoxInven;
    public InventoryData ingredientBoxInven;

    public StorageData()
    {
        isPlay = false;
        playerData = new PlayerData();
        playerLocation = new LocationData();
        timeData = new TimeData();
        foodDatas = new List<FoodData>();
        playerInven = new InventoryData();
        foodBoxInven = new InventoryData();
        ingredientBoxInven = new InventoryData();

        //test
        FoodData foodData = new FoodData(0);
        foodDatas.Add(foodData);
        foodData = new FoodData(1);
        foodDatas.Add(foodData);
    }
}

public class JsonDataManager : Singleton<JsonDataManager>
{
    private string filePath;
    public StorageData storageData;
    public ObjectData objectData;
    public ItemJsonData itemData;

    void Awake()
    {
        DontDestroyOnLoad(this);

        //ResetPlayerJsonData();
        //SavePlayerJsonData();
        LoadPlayerJsonData();
        LoadItemJsonData();
        LoadObjectJsonData();
    }

    public void ResetPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string jsonData = JsonUtility.ToJson(new StorageData());
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    public void SavePlayerJsonData()
    {
        TimeManager.Instance.SaveData();
        storageData.isPlay = true;

        filePath = "Assets/7.Data/PlayerData.json";
        string jsonData = JsonUtility.ToJson(storageData);
        File.WriteAllText(filePath, jsonData, Encoding.UTF8);
    }

    public void LoadPlayerJsonData()
    {
        filePath = "Assets/7.Data/PlayerData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        storageData = JsonUtility.FromJson<StorageData>(loadedJsonData);
    }

    public void LoadObjectJsonData()
    {
        filePath = "Assets/7.Data/ObjectData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        objectData = JsonUtility.FromJson<ObjectData>(loadedJsonData);
    }

    public void LoadItemJsonData()
    {
        filePath = "Assets/7.Data/ItemData.json";

        string loadedJsonData = File.ReadAllText(filePath, Encoding.UTF8);
        itemData = JsonUtility.FromJson<ItemJsonData>(loadedJsonData);
    }
}
